using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Data.DbSet
{
    public class PositionDbSet : IDbSet<Position>
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



        public void Create(Position item)
        {
            OpenConnection();
            string sql = string.Format("INSERT positions " +
                "(position_name) " +
                "VALUES ('@PositionName') ");

            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                // Добавить параметры
                cmd.Parameters.AddWithValue("@PositionName", item.Name);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            CloseConnection();
        }

        public void Delete(int id)
        {
            OpenConnection();
            string sql = $"DELETE positions WHERE position_id = {id}";
            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            CloseConnection();
        }

        public IEnumerable<Position> Find(Position item)
        {
            OpenConnection();
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

            List<Position> positions = new List<Position>();
            string sql = string.Format("SELECT* FROM positions " + where +
                "ORDER BY positions.position_id ");

            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            Position position = new Position
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

        public IEnumerable<Position> GetAll()
        {
            OpenConnection();
            List<Position> positions = new List<Position>();
            string sql = string.Format("SELECT* FROM positions " +
                "ORDER BY positions.position_id ");

            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            Position position = new Position
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
            OpenConnection();
            string sql = "IF OBJECT_ID('positions') IS NOT NULL SELECT 1 ELSE SELECT 0";
            bool res = false;
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

        public void Update(Position item)
        {
            OpenConnection();
            string sql = string.Format("UPDATE positions " +
                "SET position_name = '@Name' " +
                "WHERE position_id = @Id ");

            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                // Добавить параметры
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            CloseConnection();

        }
    }
}
