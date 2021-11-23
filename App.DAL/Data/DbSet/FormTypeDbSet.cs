using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace App.DAL.Data.DbSet
{
    public class FormTypeDbSet : IDbSet<FormType>
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



        public void Create(FormType item)
        {
            OpenConnection();
            string sql = string.Format("INSERT form_types " +
                "(form_type_name) " +
                "VALUES ('@Name') ");

            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                // Добавить параметры
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            CloseConnection();
        }
        public void Delete(int id)
        {
            OpenConnection();
            string sql = $"DELETE form_types WHERE form_type_id = {id}";
            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            CloseConnection();
        }

        public IEnumerable<FormType> Find(FormType item)
        {
            OpenConnection();
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

            List<FormType> formTypes = new List<FormType>();
            string sql = string.Format("SELECT* FROM form_types " + where +
                "ORDER BY form_types.form_type_id ");

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
                            formTypes.Add(position);
                        }
                    }
                    reader.Close();
                }
                cmd.Dispose();
            }

            CloseConnection();

            return formTypes;
        }

        public IEnumerable<FormType> GetAll()
        {
            OpenConnection();
            List<FormType> formTypes = new List<FormType>();
            string sql = string.Format("SELECT* FROM form_types " +
                "ORDER BY form_types.form_type_id ");

            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            FormType formType = new FormType
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
            OpenConnection();
            string sql = "IF OBJECT_ID('form_types') IS NOT NULL SELECT 1 ELSE SELECT 0";
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

        public void Update(FormType item)
        {
            OpenConnection();
            string sql = string.Format("UPDATE form_types " +
                "SET form_type_name = '@Name' " +
                "WHERE form_type_id = @Id ");

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
