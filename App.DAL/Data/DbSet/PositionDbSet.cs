using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Data.DbSet
{
    public class PositionDbSet : IDbSet<PositionDAL>
    {
        private readonly string connectionString;
        public PositionDbSet(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private SqlConnection connect = null;

        public void OpenConnection()
        {
            connect = new SqlConnection(connectionString);
            connect.Open();
        }
        public void CloseConnection()
        {
            connect.Close();
        }



        public void Create(PositionDAL item)
        {
            if(item != null)
            { 
                string sql = string.Format("INSERT positions " +
                    "(position_name) " +
                    "VALUES (@Name) ");

                OpenConnection();

                using (SqlCommand cmd = new SqlCommand(sql, this.connect))
                {
                    // Добавить параметры

                    SqlParameter Name = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 32);
                    Name.Value = item.Name;
                    cmd.Parameters.Add(Name);

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                CloseConnection();
            }

        }

        public void Delete(int id)
        {
            
            string sql = $"DELETE positions WHERE position_id = {id}";
            OpenConnection();
            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            CloseConnection();
        }

        public IEnumerable<PositionDAL> Find(PositionDAL item)
        {
            
            string where = "";
            if (item != null)
            {
                if (item.Id != 0)
                {
                        if (where.Length == 0)
                        {
                            where += $"WHERE positions.position_id = {item.Id} ";
                        }
                        else
                        {
                            where += $"and positions.position_id = {item.Id} ";
                        }
                    
                }
                if (!String.IsNullOrEmpty(item.Name))
                {
                        if (where.Length == 0)
                        {
                            where += $"WHERE positions.position_name = '{item.Name}' ";
                        }
                        else
                        {
                            where += $"and positions.position_name = '{item.Name}' ";
                        }
                    
                }                
            }

            List<PositionDAL> positions = new List<PositionDAL>();
            string sql = string.Format("SELECT* FROM positions " + where +
                "ORDER BY positions.position_id ");
            OpenConnection();
            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            PositionDAL position = new PositionDAL
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                            positions.Add(position);
                        }
                    }
                    reader.Close();
                }
                cmd.Dispose();
            }

            CloseConnection();

            return positions;
        }

        public IEnumerable<PositionDAL> GetAll()
        {
            
            List<PositionDAL> positions = new List<PositionDAL>();
            string sql = string.Format("SELECT* FROM positions " +
                "ORDER BY positions.position_id ");
            OpenConnection();
            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            PositionDAL position = new PositionDAL
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                            positions.Add(position);
                        }
                    }
                    reader.Close();
                }
                cmd.Dispose();
            }

            CloseConnection();

            return positions;

        }

        public bool TableExist()
        {
            string sql = "IF OBJECT_ID('positions') IS NOT NULL SELECT 1 ELSE SELECT 0";
            bool res = false;
            OpenConnection();

            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            res = Convert.ToBoolean(reader.GetInt32(0));
                        }
                    }
                    reader.Close();
                }
                cmd.Dispose();
            }

            CloseConnection();
            return res;           
        }

        public void Update(PositionDAL item, int? id = null)
        {
            if (item != null)
            {
                string sql = string.Format("UPDATE positions " +
                "SET position_name = @Name " +
                "WHERE position_id = @Id ");

                OpenConnection();

                using (SqlCommand cmd = new SqlCommand(sql, this.connect))
                {
                    // Добавить параметры
                    SqlParameter Id = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    Id.Value = id == null ? item.Id : id;
                    cmd.Parameters.Add(Id);


                    SqlParameter Name = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 32);
                    Name.Value = item.Name;
                    cmd.Parameters.Add(Name);

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                CloseConnection();
            }
        }
    }
}
