using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Data.DbSet
{
    public class CompanyDbSet : IDbSet<Company>
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

        public void Create(Company item)
        {
            string sql = string.Format("INSERT companies" +
                "(company_id, company_name, form_type_id) " +
                "VALUES (@Id,'@Name',@formId) ");
            OpenConnection();
            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                // Добавить параметры
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@formId", item.FormTypeId);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            CloseConnection();
        }

        public void Delete(int id)
        {
            string sql = $"DELETE companies WHERE company_id = {id}";
            OpenConnection();
            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            CloseConnection();
        }

        public IEnumerable<Company> Find(Company item)
        {
            string where = "";
            if (item != null)
            {
                if (item.Id != 0)
                {
                    if (where.Length == 0)
                    {
                        where += $"WHERE companies.company_id = {item.Id} ";
                    }
                    else
                    {
                        where += $"and companies.company_id = {item.Id} ";
                    }
                }
                if (!String.IsNullOrEmpty(item.Name))
                {
                    if (where.Length == 0)
                    {
                        where += $"WHERE companies.company_name = '{item.Name}' ";
                    }
                    else
                    {
                        where += $"and companies.company_name = '{item.Name}' ";
                    }
                }                
            }

            List<Company> companies = new List<Company>();
            string sql = string.Format("SELECT* FROM companies " +
                 "JOIN form_types ON form_types.form_type_id = companies.form_type_id " + where +
                 "ORDER BY companies.company_id");

            OpenConnection();
            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            Company company = new Company
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                FormTypeId = reader.GetInt32(2),
                                FormType = new FormType
                                {
                                    Id = reader.GetInt32(3),
                                    Name = reader.GetString(4)
                                }
                            };
                            companies.Add(company);
                        }
                    }
                    reader.Close();
                }
                cmd.Dispose();
            }

            CloseConnection();

            return companies;
        }

        public IEnumerable<Company> GetAll()
        {
            List<Company> companies = new List<Company>();
            string sql = string.Format("SELECT* FROM companies " +
                 "JOIN form_types ON form_types.form_type_id = companies.form_type_id " + 
                 "ORDER BY companies.company_id");
            OpenConnection();
            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            Company company = new Company
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                FormTypeId = reader.GetInt32(2),
                                FormType = new FormType
                                {
                                    Id = reader.GetInt32(3),
                                    Name = reader.GetString(4)
                                }
                            };
                            companies.Add(company);
                        }
                    }
                    reader.Close();
                }
                cmd.Dispose();
            }

            CloseConnection();

            return companies;
        }

        public bool TableExist()
        {
            string sql = "IF OBJECT_ID('companies') IS NOT NULL SELECT 1 ELSE SELECT 0";
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

        public void Update(Company item)
        {
            string sql = string.Format("UPDATE companies " +
                "SET company_id = @Id, company_name = '@Name', form_type_id = @formId " +
                "WHERE company_id = @Id ");
            OpenConnection();

            using (SqlCommand cmd = new SqlCommand(sql, this.connect))
            {
                // Добавить параметры
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@form_type_id", item.FormTypeId);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            CloseConnection();
        }
    }
}
