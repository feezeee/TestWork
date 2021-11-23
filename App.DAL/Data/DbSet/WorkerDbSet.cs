using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace App.DAL.Data.DbSet
{
    internal class WorkerDbSet : IDbSet<WorkerDAL>
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


        public void Create(WorkerDAL item)
        {
           if ( item != null)
           {

                string sql = string.Format("INSERT workers " +
                    "(worker_last_name,worker_first_name,worker_middle_name,worker_date_employment,position_id,company_id) "+
                    "VALUES (@LastName,@FirstName,@MiddleName,@DateEmployment,@PositionId,@CompanyId) ");
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(sql, this.connect))
                {
                    // Добавить параметры
                    SqlParameter LastName = new SqlParameter("@LastName", System.Data.SqlDbType.NVarChar, 32);
                    LastName.Value = item.LastName;

                    SqlParameter FirstName = new SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar, 32);
                    FirstName.Value = item.FirstName;

                    SqlParameter MiddleName = new SqlParameter("@MiddleName", System.Data.SqlDbType.NVarChar, 32);
                    MiddleName.Value = item.MiddleName;

                    SqlParameter DateEmployment = new SqlParameter("@DateEmployment", System.Data.SqlDbType.Date);
                    DateEmployment.Value = item.DateEmployment.Date.ToString("dd.MM.yyyy");

                    SqlParameter PositionId = new SqlParameter("@PositionId", System.Data.SqlDbType.Int);
                    PositionId.Value = item.PositionId;

                    SqlParameter CompanyId = new SqlParameter("@CompanyId", System.Data.SqlDbType.Int);
                    CompanyId.Value = item.CompanyId;

                    cmd.Parameters.Add(LastName);
                    cmd.Parameters.Add(FirstName);
                    cmd.Parameters.Add(MiddleName);
                    cmd.Parameters.Add(DateEmployment);
                    cmd.Parameters.Add(CompanyId);
                    cmd.Parameters.Add(PositionId);

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                CloseConnection();
           }

        }

        public void Delete(int id)
        {
            string sql = $"DELETE workers WHERE worker_id = {id}";
            OpenConnection();
            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

            CloseConnection();
        }

        public IEnumerable<WorkerDAL> GetAll()
        {
            List<WorkerDAL> workers = new List<WorkerDAL>();
            string sql = string.Format("SELECT* FROM workers " +
                "JOIN positions ON positions.position_id = workers.position_id " +
                "JOIN companies ON companies.company_id = workers.company_id " +
                "JOIN form_types ON form_types.form_type_id = companies.form_type_id " +
                "ORDER BY workers.worker_id ");
            OpenConnection();
            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            WorkerDAL worker = new WorkerDAL
                            {
                                Id = reader.GetInt32(0),
                                LastName = reader.GetString(1),
                                FirstName = reader.GetString(2),
                                MiddleName = reader.GetString(3),
                                DateEmployment = reader.GetDateTime(4),
                                PositionId = reader.GetInt32(5),
                                CompanyId = reader.GetInt32(6),
                                Position = new PositionDAL
                                {
                                    Id = reader.GetInt32(7),
                                    Name = reader.GetString(8)
                                },
                                Company = new CompanyDAL
                                {
                                    Id = reader.GetInt32(9),
                                    Name = reader.GetString(10),
                                    FormTypeId = reader.GetInt32(11),
                                    FormType = new FormTypeDAL
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

        public void Update(WorkerDAL item, int? id = null)
        {
            if (item != null)
            {
                string sql = string.Format("UPDATE workers " +
                "SET worker_last_name = @LastName, worker_first_name = @FirstName, worker_middle_name = @MiddleName, worker_date_employment = @DateEmployment, position_id = @PositionId, company_id = @CompanyId " +
                "WHERE worker_id = @Id");

                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(sql, this.connect))
                {
                    // Добавить параметры

                    // Добавить параметры
                    SqlParameter Id = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    Id.Value = id == null ? item.Id : id;

                    SqlParameter LastName = new SqlParameter("@LastName", System.Data.SqlDbType.NVarChar, 32);
                    LastName.Value = item.LastName;

                    SqlParameter FirstName = new SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar, 32);
                    FirstName.Value = item.FirstName;

                    SqlParameter MiddleName = new SqlParameter("@MiddleName", System.Data.SqlDbType.NVarChar, 32);
                    MiddleName.Value = item.MiddleName;

                    SqlParameter DateEmployment = new SqlParameter("@DateEmployment", System.Data.SqlDbType.Date);
                    DateEmployment.Value = item.DateEmployment.Date.ToString("dd.MM.yyyy");

                    SqlParameter PositionId = new SqlParameter("@PositionId", System.Data.SqlDbType.Int);
                    PositionId.Value = item.PositionId;

                    SqlParameter CompanyId = new SqlParameter("@CompanyId", System.Data.SqlDbType.Int);
                    CompanyId.Value = item.CompanyId;


                    cmd.Parameters.Add(Id);
                    cmd.Parameters.Add(LastName);
                    cmd.Parameters.Add(FirstName);
                    cmd.Parameters.Add(MiddleName);
                    cmd.Parameters.Add(DateEmployment);
                    cmd.Parameters.Add(CompanyId);
                    cmd.Parameters.Add(PositionId);



                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                CloseConnection();
            }
        }

        public IEnumerable<WorkerDAL> Find(WorkerDAL item)
        {
            
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
                        where += $"WHERE workers.worker_last_name = '{item.LastName}' ";
                    }
                    else
                    {
                        where += $"and workers.worker_last_name = '{item.LastName}' ";
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
                if (item.PositionId != 0)
                {
                    if (where.Length == 0)
                    {
                        where += $"WHERE workers.position_id = {item.PositionId} ";
                    }
                    else
                    {
                        where += $"and workers.position_id = {item.PositionId} ";
                    }
                }
                if (item.CompanyId != 0)
                {
                    if (where.Length == 0)
                    {
                        where += $"WHERE workers.company_id = {item.CompanyId} ";
                    }
                    else
                    {
                        where += $"and workers.company_id = {item.Company} ";
                    }
                }

            }

            List<WorkerDAL> workers = new List<WorkerDAL>();
            string sql = string.Format("SELECT* FROM workers " +
                "JOIN positions ON positions.position_id = workers.position_id " +
                "JOIN companies ON companies.company_id = workers.company_id " +
                "JOIN form_types ON form_types.form_type_id = companies.form_type_id " +
                where +
                "ORDER BY workers.worker_id ");

            OpenConnection();


            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            WorkerDAL worker = new WorkerDAL
                            {
                                Id = reader.GetInt32(0),
                                LastName = reader.GetString(1),
                                FirstName = reader.GetString(2),
                                MiddleName = reader.GetString(3),
                                DateEmployment = reader.GetDateTime(4),
                                PositionId = reader.GetInt32(5),
                                CompanyId = reader.GetInt32(6),
                                Position = new PositionDAL
                                {
                                    Id = reader.GetInt32(7),
                                    Name = reader.GetString(8)
                                },
                                Company = new CompanyDAL
                                {
                                    Id = reader.GetInt32(9),
                                    Name = reader.GetString(10),
                                    FormTypeId = reader.GetInt32(11),
                                    FormType = new FormTypeDAL
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
            string sql = "IF OBJECT_ID('workers') IS NOT NULL SELECT 1 ELSE SELECT 0";
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
    }
}
