using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Data.DbSet
{
    internal class WorkerDbSet : IDbSet<Worker>
    {
        private readonly string connectionString;
        public WorkerDbSet(string connectionString)
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


        public void Create(Worker item)
        {
            OpenConnection();
            string sql = string.Format("INSERT workers " +
                "(worker_last_mame,worker_first_name,worker_middle_name,worker_date_employment,position_id,company_id) "+
                "VALUES ('@LastName','@FirstName','@MiddleName','@DateEmployment',@PositionId,@CompanyId) ");

            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                // Добавить параметры
                cmd.Parameters.AddWithValue("@LastName", item.LastName);
                cmd.Parameters.AddWithValue("@FirstName", item.FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", item.MiddleName);
                cmd.Parameters.AddWithValue("@DateEmployment", item.DateEmployment.ToShortDateString());
                cmd.Parameters.AddWithValue("@PositionId", item.PositionId);
                cmd.Parameters.AddWithValue("@CompanyId", item.CompanyId);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            CloseConnection();
            
        }

        public void Delete(int id)
        {
            OpenConnection();
            string sql = $"DELETE workers WHERE worker_id = {id}";
            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

            CloseConnection();
        }

        public IEnumerable<Worker> GetAll()
        {
            OpenConnection();
            List<Worker> workers = new List<Worker>();
            string sql = string.Format("SELECT* FROM workers " +
                "JOIN positions ON positions.position_id = workers.position_id " +
                "JOIN companies ON companies.company_id = workers.company_id " +
                "JOIN form_types ON form_types.form_type_id = companies.form_type_id " +
                "ORDER BY workers.worker_id ");

            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            Worker worker = new Worker
                            {
                                Id = reader.GetInt32(0),
                                LastName = reader.GetString(1),
                                FirstName = reader.GetString(2),
                                MiddleName = reader.GetString(3),
                                DateEmployment = reader.GetDateTime(4),
                                PositionId = reader.GetInt32(5),
                                CompanyId = reader.GetInt32(6),
                                Position = new Position
                                {
                                    Id = reader.GetInt32(7),
                                    Name = reader.GetString(8)
                                },
                                Company = new Company
                                {
                                    Id = reader.GetInt32(9),
                                    Name = reader.GetString(10),
                                    FormTypeId = reader.GetInt32(11),
                                    FormType = new FormType
                                    {
                                        Id = reader.GetInt32(12),
                                        Name = reader.GetString(13)
                                    }
                                }
                            };
                            workers.Add(worker);
                        }
                    }
                    reader.Close();
                }
                cmd.Dispose();
            }

            CloseConnection();

            return workers;
        }               

        public void Update(Worker item)
        {
            OpenConnection();
            string sql = string.Format("UPDATE workers" +
                "SET worker_last_mame = '@LastName', worker_first_name = '@FirstName', worker_middle_name = '@MiddleName', worker_date_employment = '@DateEmployment', position_id = @PositionId, company_id = @CompanyId" +
                "WHERE worker_id = @Id");

            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                // Добавить параметры
                cmd.Parameters.AddWithValue("@LastName", item.LastName);
                cmd.Parameters.AddWithValue("@FirstName", item.FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", item.MiddleName);
                cmd.Parameters.AddWithValue("@DateEmployment", item.DateEmployment.ToShortDateString());
                cmd.Parameters.AddWithValue("@PositionId", item.PositionId);
                cmd.Parameters.AddWithValue("@CompanyId", item.CompanyId);
                cmd.Parameters.AddWithValue("@Id", item.Id);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            CloseConnection();

        }

        public IEnumerable<Worker> Find(Worker item)
        {
            
            OpenConnection();
            string where = "";
            if(item != null)
            {
                if (item.Id != 0)
                {
                    if (where.Length == 0)
                    {
                        where += $"WHERE workers.worker_id = {item.Id} ";
                    }
                    else
                    {
                        where += $"and workers.worker_id = {item.Id} ";
                    }
                }
                if (item.LastName != null)
                {
                    if (where.Length == 0)
                    {
                        where += $"WHERE workers.worker_last_mame = '{item.LastName}' ";
                    }
                    else
                    {
                        where += $"and workers.worker_last_mame = '{item.LastName}' ";
                    }
                }
                if (item.FirstName != null)
                {
                    if (where.Length == 0)
                    {
                        where += $"WHERE workers.worker_first_name = '{item.FirstName}' ";
                    }
                    else
                    {
                        where += $"and workers.worker_first_name = '{item.FirstName}' ";
                    }
                }
                if (item.MiddleName != null)
                {
                    if (where.Length == 0)
                    {
                        where += $"WHERE workers.worker_middle_name = '{item.MiddleName}' ";
                    }
                    else
                    {
                        where += $"and workers.worker_middle_name = '{item.MiddleName}' ";
                    }
                }
            }

            List<Worker> workers = new List<Worker>();
            string sql = string.Format("SELECT* FROM workers " +
                "JOIN positions ON positions.position_id = workers.position_id " +
                "JOIN companies ON companies.company_id = workers.company_id " +
                "JOIN form_types ON form_types.form_type_id = companies.form_type_id " +
                where +
                "ORDER BY workers.worker_id ");

            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            Worker worker = new Worker
                            {
                                Id = reader.GetInt32(0),
                                LastName = reader.GetString(1),
                                FirstName = reader.GetString(2),
                                MiddleName = reader.GetString(3),
                                DateEmployment = reader.GetDateTime(4),
                                PositionId = reader.GetInt32(5),
                                CompanyId = reader.GetInt32(6),
                                Position = new Position
                                {
                                    Id = reader.GetInt32(7),
                                    Name = reader.GetString(8)
                                },
                                Company = new Company
                                {
                                    Id = reader.GetInt32(9),
                                    Name = reader.GetString(10),
                                    FormTypeId = reader.GetInt32(11),
                                    FormType = new FormType
                                    {
                                        Id = reader.GetInt32(12),
                                        Name = reader.GetString(13)
                                    }
                                }
                            };
                            workers.Add(worker);
                        }
                    }
                    reader.Close();
                }
                cmd.Dispose();
            }

            CloseConnection();

            return workers;
        }

        public bool TableExist()
        {
            OpenConnection();
            string sql = "IF OBJECT_ID('workers') IS NOT NULL SELECT 1 ELSE SELECT 0";
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
    }
}
