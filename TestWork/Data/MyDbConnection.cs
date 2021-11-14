using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TestWork.Models;

namespace TestWork.Data
{
    public class MyDbConnection
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;

        public MyDbConnection(IConfiguration config)
        {
            _config = config;
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        private async Task<bool> WorkerTableExist()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "IF OBJECT_ID('workers') IS NOT NULL SELECT 1 ELSE SELECT 0";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                bool res = false;
                
                if (reader.HasRows) // если есть данные
                {
                    while (await reader.ReadAsync()) // построчно считываем данные
                    {
                        res = Convert.ToBoolean(reader.GetInt32(0));
                    }
                }

                await reader.CloseAsync();
                return res;
            }
        }
        public async Task<IEnumerable<Worker>> WorkersList(string where = "")
        {

            List<Worker> workers = new List<Worker>();
            if (WorkerTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT* FROM workers JOIN positions ON positions.Id = workers.PositionId JOIN companies ON Companies.Id = workers.CompanyId JOIN FormTypes ON FormTypes.Id = Companies.FormTypeId " + where;
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows) // если есть данные
                    {
                        while (await reader.ReadAsync()) // построчно считываем данные
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

                    await reader.CloseAsync();
                }
            }
            return workers;
        }
        public async Task<IEnumerable<Worker>> WorkerById(int id)
        {
            List<Worker> workers = new List<Worker>();
            if (WorkerTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = $"SELECT* FROM workers JOIN positions ON positions.Id = workers.PositionId JOIN companies ON Companies.Id = workers.CompanyId JOIN FormTypes ON FormTypes.Id = Companies.FormTypeId WHERE workers.Id = {id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows) // если есть данные
                    {
                        while (await reader.ReadAsync()) // построчно считываем данные
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

                    await reader.CloseAsync();
                    
                }
            }
            return workers;
        }
        public async Task WorkerCreate(Worker worker)
        {
            if (WorkerTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = $"INSERT Workers(LastName,FirstName,MiddleName,DateEmployment,PositionId,CompanyId) VALUES ('{worker.LastName}','{worker.FirstName}','{worker.MiddleName}','{worker.DateEmployment.ToShortDateString()}',{worker.PositionId},{worker.CompanyId});";
                    SqlCommand command = new SqlCommand(query, connection);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task WorkerUpdate(Worker worker)
        {
            if (WorkerTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = $"UPDATE workers SET LastName = '{worker.LastName}', FirstName = '{worker.FirstName}', MiddleName = '{worker.MiddleName}', DateEmployment = '{worker.DateEmployment}', PositionId = '{worker.PositionId}', CompanyId = '{worker.CompanyId}' WHERE Id = {worker.Id};";
                    SqlCommand command = new SqlCommand(query, connection);
                    await command.ExecuteNonQueryAsync();
                }
            }


        }
        public async Task WorkerDelete(int? id)
        {
            if (WorkerTableExist().Result)
            {
                if (id != null)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();
                        string query = $"DELETE workers WHERE Id = {id.Value}";
                        SqlCommand command = new SqlCommand(query, connection);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
        }


        private async Task<bool> CompanyTableExist()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "IF OBJECT_ID('companies') IS NOT NULL SELECT 1 ELSE SELECT 0";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                bool res = false;

                if (reader.HasRows) // если есть данные
                {
                    while (await reader.ReadAsync()) // построчно считываем данные
                    {
                        res = Convert.ToBoolean(reader.GetInt32(0));
                    }
                }

                await reader.CloseAsync();
                return res;
            }
        }
        public async Task<IEnumerable<Company>> CompaniesList(string where = "")
        {
            List<Company> companies = new List<Company>();
            if (CompanyTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM companies JOIN formTypes ON formTypes.Id = companies.FormTypeId " + where;
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows) // если есть данные
                    {
                        while (await reader.ReadAsync()) // построчно считываем данные
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

                    await reader.CloseAsync();
                }
            }
            return companies;
        }
        public async Task<IEnumerable<Company>> CompanyById(int id)
        {
            List<Company> companies = new List<Company>();
            if (CompanyTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = $"SELECT * FROM companies JOIN formTypes ON formTypes.Id = companies.FormTypeId WHERE companies.Id = {id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows) // если есть данные
                    {
                        while (await reader.ReadAsync()) // построчно считываем данные
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

                    await reader.CloseAsync();
                }
            }
            return companies;            
        }
        public async Task CompanyCreate(Company company)
        {
            if (CompanyTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = $"INSERT Companies(Id, Name, FormTypeId) VALUES ({company.Id}, '{company.Name}', {company.FormTypeId});";
                    SqlCommand command = new SqlCommand(query, connection);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task CompanyUpdate(Company company)
        {
            if (CompanyTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = $"UPDATE companies SET Name = '{company.Name}', FormTypeId = {company.FormTypeId} WHERE Id = {company.Id};";
                    SqlCommand command = new SqlCommand(query, connection);
                    await command.ExecuteNonQueryAsync();
                }
            }


        }
        public async Task CompanyDelete(int? id)
        {
            if (CompanyTableExist().Result)
            {
                if (id != null)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();
                        string query = $"DELETE companies WHERE Id = {id.Value}";
                        SqlCommand command = new SqlCommand(query, connection);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
        }




        private async Task<bool> PositionTableExist()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "IF OBJECT_ID('positions') IS NOT NULL SELECT 1 ELSE SELECT 0";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                bool res = false;

                if (reader.HasRows) // если есть данные
                {
                    while (await reader.ReadAsync()) // построчно считываем данные
                    {
                        res = Convert.ToBoolean(reader.GetInt32(0));
                    }
                }

                await reader.CloseAsync();
                return res;
            }
        }
        public async Task<IEnumerable<Position>> PositionsList()
        {
            List<Position> positions = new List<Position>();
            if (PositionTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM positions;";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows) // если есть данные
                    {
                        while (await reader.ReadAsync()) // построчно считываем данные
                        {
                            Position position = new Position
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                            positions.Add(position);
                        }
                    }

                    await reader.CloseAsync();
                }
            }
            return positions;
            
        }


        private async Task<bool> FormTypeTableExist()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "IF OBJECT_ID('formTypes') IS NOT NULL SELECT 1 ELSE SELECT 0";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                bool res = false;

                if (reader.HasRows) // если есть данные
                {
                    while (await reader.ReadAsync()) // построчно считываем данные
                    {
                        res = Convert.ToBoolean(reader.GetInt32(0));
                    }
                }

                await reader.CloseAsync();
                return res;
            }
        }
        public async Task<IEnumerable<FormType>> FormTypesList()
        {
            List<FormType> formTypes = new List<FormType>();
            if (FormTypeTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM formTypes;";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows) // если есть данные
                    {
                        while (await reader.ReadAsync()) // построчно считываем данные
                        {
                            FormType formType = new FormType
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),

                            };
                            formTypes.Add(formType);
                        }
                    }

                    await reader.CloseAsync();

                }
            }
            return formTypes;            
        }


    }
}
