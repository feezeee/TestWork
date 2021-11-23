using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace App.DAL.Data.DbSet
{
    public class FormTypeDbSet : IDbSet<FormTypeDAL>
    {
        private readonly string connectionString;
        public FormTypeDbSet(string connectionString)
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



        public void Create(FormTypeDAL item)
        {
            if(item != null)
            {

                string sql = string.Format("INSERT form_types " +
                    "(form_type_name) " +
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
            string sql = $"DELETE form_types WHERE form_type_id = {id}";
            OpenConnection();
            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            CloseConnection();
        }

        public IEnumerable<FormTypeDAL> Find(FormTypeDAL item)
        {
            string where = "";
            if (item != null)
            {
                if (item.Id != 0)
                {
                    if (where.Length == 0)
                    {
                        where += $"WHERE form_types.form_type_id = {item.Id} ";
                    }
                    else
                    {
                        where += $"and form_types.form_type_id = {item.Id} ";
                    }

                }
                if (!String.IsNullOrEmpty(item.Name))
                {
                    if (where.Length == 0)
                    {
                        where += $"WHERE form_types.form_type_name = '{item.Name}' ";
                    }
                    else
                    {
                        where += $"and form_types.form_type_name = '{item.Name}' ";
                    }

                }
            }

            List<FormTypeDAL> formTypes = new List<FormTypeDAL>();
            string sql = string.Format("SELECT* FROM form_types " + where +
                "ORDER BY form_types.form_type_id ");

            OpenConnection();

            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            FormTypeDAL form = new FormTypeDAL
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                            formTypes.Add(form);
                        }
                    }
                    reader.Close();
                }
                cmd.Dispose();
            }

            CloseConnection();

            return formTypes;
        }

        public IEnumerable<FormTypeDAL> GetAll()
        {
            List<FormTypeDAL> formTypes = new List<FormTypeDAL>();
            string sql = string.Format("SELECT* FROM form_types " +
                "ORDER BY form_types.form_type_id ");

            OpenConnection();

            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            FormTypeDAL formType = new FormTypeDAL
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),

                            };
                            formTypes.Add(formType);
                        }
                    }
                    reader.Close();
                }
                cmd.Dispose();
            }

            CloseConnection();

            return formTypes;
        }

        public bool TableExist()
        {
            string sql = "IF OBJECT_ID('form_types') IS NOT NULL SELECT 1 ELSE SELECT 0";
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

        public void Update(FormTypeDAL item, int? id = null)
        {
            if (item != null)
            {
                string sql = string.Format("UPDATE form_types " +
                "SET form_type_name = @Name " +
                "WHERE form_type_id = @Id ");
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
