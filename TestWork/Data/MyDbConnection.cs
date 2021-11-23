using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TestWork.Models;

namespace TestWork.Data
{
    /// <summary>
    /// Класс, взаимодействующий с бд
    /// </summary>
    public class MyDbConnection
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;

        public MyDbConnection(IConfiguration config)
        {
            _config = config;
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Проверка на существование таблицы 'workers' в базе данных
        /// </summary>
        /// <returns>true - если сущетсвует, false - если не существует</returns>
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

        /// <summary>
        /// Вовращает список работников из сущности 'workers'
        /// </summary>
        /// <param name="where">Дополнительная фильтрация списка. По умолчанию фильтрация отсутствует</param>
        /// <returns>Вовращает список работников Task<IEnumerable<Worker>></returns>
        public async Task<IEnumerable<WorkerViewModel>> WorkersList(string where = "")
        {

            List<WorkerViewModel> workers = new List<WorkerViewModel>();
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
                            WorkerViewModel worker = new WorkerViewModel
                            {
                                Id = reader.GetInt32(0),
                                LastName = reader.GetString(1),
                                FirstName = reader.GetString(2),
                                MiddleName = reader.GetString(3),
                                DateEmployment = reader.GetDateTime(4),
                                PositionId = reader.GetInt32(5),
                                CompanyId = reader.GetInt32(6),
                                Position = new PositionViewModel
                                {
                                    Id = reader.GetInt32(7),
                                    Name = reader.GetString(8)
                                },
                                Company = new CompanyViewModel
                                {
                                    Id = reader.GetInt32(9),
                                    Name = reader.GetString(10),
                                    FormTypeId = reader.GetInt32(11),
                                    FormType = new FormTypeViewModel
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

        /// <summary>
        /// Возвращает список работников по их индентификатору
        /// </summary>
        /// <param name="id">Необходимый индентификатор</param>
        /// <returns>Возвращает список работников по их индентификатору Task<IEnumerable<Worker>></returns>
        public async Task<IEnumerable<WorkerViewModel>> WorkerById(int id)
        {
            List<WorkerViewModel> workers = new List<WorkerViewModel>();
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
                            WorkerViewModel worker = new WorkerViewModel
                            {
                                Id = reader.GetInt32(0),
                                LastName = reader.GetString(1),
                                FirstName = reader.GetString(2),
                                MiddleName = reader.GetString(3),
                                DateEmployment = reader.GetDateTime(4),
                                PositionId = reader.GetInt32(5),
                                CompanyId = reader.GetInt32(6),
                                Position = new PositionViewModel
                                {
                                    Id = reader.GetInt32(7),
                                    Name = reader.GetString(8)
                                },
                                Company = new CompanyViewModel
                                {
                                    Id = reader.GetInt32(9),
                                    Name = reader.GetString(10),
                                    FormTypeId = reader.GetInt32(11),
                                    FormType = new FormTypeViewModel
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

        /// <summary>
        /// Добавляет новый экземпляр сущности 'workers'
        /// </summary>
        /// <param name="worker">Сотрудник, которого необходимо занести в бд</param>
        /// <returns></returns>
        public async Task WorkerCreate(WorkerViewModel worker)
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

        /// <summary>
        /// Обновляет сведения об указанном сотруднике в сущности 'workers'
        /// </summary>
        /// <param name="worker">Измененные параметры сотрудника</param>
        /// <returns></returns>
        public async Task WorkerUpdate(WorkerViewModel worker)
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

        /// <summary>
        /// Удаление сотрудника из сущности 'workers'
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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



        /// <summary>
        /// Проверка на существование таблицы 'companies' в базе данных
        /// </summary>
        /// <returns>true - если сущетсвует, false - если не существует</returns>
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

        /// <summary>
        /// Вовращает список компаний из сущности 'companies'
        /// </summary>
        /// <param name="where">Дополнительная фильтрация списка. По умолчанию фильтрация отсутствует</param>
        /// <returns>Вовращает список компаний из сущности 'companies' Task<IEnumerable<Company>></returns>
        public async Task<IEnumerable<CompanyViewModel>> CompaniesList(string where = "")
        {
            List<CompanyViewModel> companies = new List<CompanyViewModel>();
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
                            CompanyViewModel company = new CompanyViewModel
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                FormTypeId = reader.GetInt32(2),
                                FormType = new FormTypeViewModel
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

        /// <summary>
        /// Возвращает список компаний по их индентификатору
        /// </summary>
        /// <param name="id">Необходимый индентификатор</param>
        /// <returns>Возвращает список компаний по их индентификатору Task<IEnumerable<Company>></returns>
        public async Task<IEnumerable<CompanyViewModel>> CompanyById(int id)
        {
            List<CompanyViewModel> companies = new List<CompanyViewModel>();
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
                            CompanyViewModel company = new CompanyViewModel
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                FormTypeId = reader.GetInt32(2),
                                FormType = new FormTypeViewModel
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

        /// <summary>
        /// Добавляет новый экземпляр сущности 'companies'
        /// </summary>
        /// <param name="company">Компания, которую необходимо занести в бд</param>
        /// <returns></returns>
        public async Task CompanyCreate(CompanyViewModel company)
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

        /// <summary>
        /// Обновление данных о компании в сущности 'companies'
        /// </summary>
        /// <param name="company">Изменения компании</param>
        /// <param name="predId">Предыдущий индентификатор компании</param>
        /// <returns></returns>
        public async Task CompanyUpdate(CompanyViewModel company, int? predId)
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

        /// <summary>
        /// Удаление компании из сущности 'companies'
        /// </summary>
        /// <param name="id">Индентификатор компании</param>
        /// <returns></returns>
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



        /// <summary>
        /// Проверка на существование таблицы 'positions' в базе данных
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Вовращает список должностей из сущности 'positions'
        /// </summary>
        /// <param name="where">Дополнительная фильтрация списка. По умолчанию фильтрация отсутствует</param>
        /// <returns></returns>
        public async Task<IEnumerable<PositionViewModel>> PositionsList(string where = "")
        {
            List<PositionViewModel> positions = new List<PositionViewModel>();
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
                            PositionViewModel position = new PositionViewModel
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

        /// <summary>
        /// Возвращает список должностей по индентификатору
        /// </summary>
        /// <param name="id">Индентификатор должности</param>
        /// <returns></returns>
        public async Task<IEnumerable<PositionViewModel>> PositionById(int id)
        {
            List<PositionViewModel> positions = new List<PositionViewModel>();
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
                            PositionViewModel position = new PositionViewModel
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

        /// <summary>
        /// Добавление новой должности в сущность 'positions'
        /// </summary>
        /// <param name="position">Новая должность</param>
        /// <returns></returns>
        public async Task PositionCreate(PositionViewModel position)
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

        /// <summary>
        /// Обновление сведений о должности
        /// </summary>
        /// <param name="position">Измененная должность</param>
        /// <returns></returns>
        public async Task PositionUpdate(PositionViewModel position)
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

        /// <summary>
        /// Удаление должности из сущность 'positions' по индентификатору
        /// </summary>
        /// <param name="id">Индентификатор должности</param>
        /// <returns></returns>
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



        /// <summary>
        /// Проверка на существование таблицы 'form_types' в базе данных
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Получение списка ОПФ из сущности 'form_types'
        /// </summary>
        /// <param name="where">Дополнительная фильтрация списка. По умолчанию фильтрация отсутствует</param>
        /// <returns></returns>
        public async Task<IEnumerable<FormTypeViewModel>> FormTypesList(string where = "")
        {
            List<FormTypeViewModel> formTypes = new List<FormTypeViewModel>();
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
                            FormTypeViewModel formType = new FormTypeViewModel
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

        /// <summary>
        /// Получение списка ОПФ из сущности 'form_types' по индентификатору
        /// </summary>
        /// <param name="id">Индентификатор ОПФ</param>
        /// <returns></returns>
        public async Task<IEnumerable<FormTypeViewModel>> FormTypeById(int id)
        {
            List<FormTypeViewModel> formTypes = new List<FormTypeViewModel>();
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
                            FormTypeViewModel formType = new FormTypeViewModel
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

        /// <summary>
        /// Добавление новой ОПФ в сущность 'form_types'
        /// </summary>
        /// <param name="formType">Новая ОПФ</param>
        /// <returns></returns>
        public async Task FormTypeCreate(FormTypeViewModel formType)
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

        /// <summary>
        /// Изменение информации в существующей ОПФ
        /// </summary>
        /// <param name="formType">Измененная ОПФ</param>
        /// <returns></returns>
        public async Task FormTypeUpdate(FormTypeViewModel formType)
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

        /// <summary>
        /// Удаление ОПФ из сущности 'form_types' по индентификатору
        /// </summary>
        /// <param name="id">Индентификатор ОПФ</param>
        /// <returns></returns>
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
