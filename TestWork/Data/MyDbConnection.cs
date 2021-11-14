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
            if (WorkerTableExist().Result && FormTypeTableExist().Result && CompanyTableExist().Result && PositionTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT* FROM workers JOIN positions ON positions.position_id = workers.position_id JOIN companies ON companies.company_id = workers.company_id JOIN form_types ON form_types.form_type_id = companies.form_type_id " + where + " ORDER BY workers.worker_id";
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
            if (WorkerTableExist().Result && FormTypeTableExist().Result && CompanyTableExist().Result && PositionTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = $"SELECT* FROM workers JOIN positions ON positions.position_id = workers.position_id JOIN companies ON companies.company_id = workers.company_id JOIN form_types ON form_types.form_type_id = companies.form_type_id WHERE workers.worker_id = {id}";
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
                    string query = $"INSERT workers(worker_last_mame,worker_first_name,worker_middle_name,worker_date_employment,position_id,company_id) VALUES ('{worker.LastName}','{worker.FirstName}','{worker.MiddleName}','{worker.DateEmployment.ToShortDateString()}',{worker.PositionId},{worker.CompanyId});";
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
                    string query = $"UPDATE workers SET worker_last_mame = '{worker.LastName}', worker_first_name = '{worker.FirstName}', worker_middle_name = '{worker.MiddleName}', worker_date_employment = '{worker.DateEmployment}', position_id = '{worker.PositionId}', company_id = '{worker.CompanyId}' WHERE worker_id = {worker.Id};";
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
                        string query = $"DELETE workers WHERE worker_id = {id.Value}";
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
            if (CompanyTableExist().Result && FormTypeTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM companies JOIN form_types ON form_types.form_type_id = companies.form_type_id " + where + " ORDER BY companies.company_id";
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
            if (CompanyTableExist().Result && FormTypeTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = $"SELECT * FROM companies JOIN form_types ON form_types.form_type_id = companies.form_type_id WHERE companies.company_id = {id}";
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
                    string query = $"INSERT companies(company_id, company_name, form_type_id) VALUES ({company.Id}, '{company.Name}', {company.FormTypeId});";
                    SqlCommand command = new SqlCommand(query, connection);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task CompanyUpdate(Company company, int? predId)
        {
            if (CompanyTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = $"UPDATE companies SET company_id = {company.Id}, company_name = '{company.Name}', form_type_id = {company.FormTypeId} WHERE company_id = {predId};";
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
                        string query = $"DELETE companies WHERE company_id = {id.Value}";
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
        public async Task<IEnumerable<Position>> PositionsList(string where = "")
        {
            List<Position> positions = new List<Position>();
            if (PositionTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM positions " + where + " ORDER BY positions.position_id";
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
        public async Task<IEnumerable<Position>> PositionById(int id)
        {
            List<Position> positions = new List<Position>();
            if (PositionTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = $"SELECT * FROM positions WHERE positions.position_id = {id}";
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
        public async Task PositionCreate(Position position)
        {
            if (PositionTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = $"INSERT positions(position_name) VALUES ('{position.Name}');";
                    SqlCommand command = new SqlCommand(query, connection);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task PositionUpdate(Position position)
        {
            if (PositionTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = $"UPDATE positions SET position_name = '{position.Name}' WHERE position_id = {position.Id};";
                    SqlCommand command = new SqlCommand(query, connection);
                    await command.ExecuteNonQueryAsync();
                }
            }


        }
        public async Task PositionDelete(int? id)
        {
            if (PositionTableExist().Result)
            {
                if (id != null)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();
                        string query = $"DELETE positions WHERE position_id = {id.Value}";
                        SqlCommand command = new SqlCommand(query, connection);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
        }


        private async Task<bool> FormTypeTableExist()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "IF OBJECT_ID('form_types') IS NOT NULL SELECT 1 ELSE SELECT 0";
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
        public async Task<IEnumerable<FormType>> FormTypesList(string where = "")
        {
            List<FormType> formTypes = new List<FormType>();
            if (FormTypeTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM form_types " + where + " ORDER BY form_types.form_type_id";
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
        public async Task<IEnumerable<FormType>> FormTypeById(int id)
        {
            List<FormType> formTypes = new List<FormType>();
            if (FormTypeTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = $"SELECT * FROM form_types WHERE form_types.form_type_id = {id}";
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
        public async Task FormTypeCreate(FormType formType)
        {
            if (FormTypeTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = $"INSERT form_types(form_type_name) VALUES ('{formType.Name}');";
                    SqlCommand command = new SqlCommand(query, connection);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task FormTypeUpdate(FormType formType)
        {
            if (FormTypeTableExist().Result)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = $"UPDATE form_types SET form_type_name = '{formType.Name}' WHERE form_type_id = {formType.Id};";
                    SqlCommand command = new SqlCommand(query, connection);
                    await command.ExecuteNonQueryAsync();
                }
            }


        }
        public async Task FormTypeDelete(int? id)
        {
            if (FormTypeTableExist().Result)
            {
                if (id != null)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();
                        string query = $"DELETE form_types WHERE form_type_id = {id.Value}";
                        SqlCommand command = new SqlCommand(query, connection);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
        }


    }
}
