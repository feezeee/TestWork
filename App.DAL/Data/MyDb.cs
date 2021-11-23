using App.DAL.Data.DbSet;
using App.DAL.Models;

namespace App.DAL.Data
{
    public class MyDb
    {
        private readonly string _connectionString = "server=DESKTOP-IFC1G52\\SQLEXPRESS;database=testdb;Trusted_Connection=True;";

        public MyDb()
        {          
            Workers = new WorkerDbSet(_connectionString);
        }
        public IDbSet<Worker> Workers { get; }
        


        //public async Task<IEnumerable<Worker>> WorkersList(string where = "")
        //{

        //    List<Worker> workers = new List<Worker>();
        //    if (WorkerTableExist().Result && FormTypeTableExist().Result && CompanyTableExist().Result && PositionTableExist().Result)
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            await connection.OpenAsync();
        //            string query = "SELECT* FROM workers JOIN positions ON positions.position_id = workers.position_id JOIN companies ON companies.company_id = workers.company_id JOIN form_types ON form_types.form_type_id = companies.form_type_id " + where + " ORDER BY workers.worker_id";
        //            SqlCommand command = new SqlCommand(query, connection);
        //            SqlDataReader reader = await command.ExecuteReaderAsync();

        //            if (reader.HasRows) // если есть данные
        //            {
        //                while (await reader.ReadAsync()) // построчно считываем данные
        //                {
        //                    Worker worker = new Worker
        //                    {
        //                        Id = reader.GetInt32(0),
        //                        LastName = reader.GetString(1),
        //                        FirstName = reader.GetString(2),
        //                        MiddleName = reader.GetString(3),
        //                        DateEmployment = reader.GetDateTime(4),
        //                        PositionId = reader.GetInt32(5),
        //                        CompanyId = reader.GetInt32(6),
        //                        Position = new Position
        //                        {
        //                            Id = reader.GetInt32(7),
        //                            Name = reader.GetString(8)
        //                        },
        //                        Company = new Company
        //                        {
        //                            Id = reader.GetInt32(9),
        //                            Name = reader.GetString(10),
        //                            FormTypeId = reader.GetInt32(11),
        //                            FormType = new FormType
        //                            {
        //                                Id = reader.GetInt32(12),
        //                                Name = reader.GetString(13)
        //                            }
        //                        }
        //                    };
        //                    workers.Add(worker);
        //                }
        //            }

        //            await reader.CloseAsync();
        //        }
        //    }
        //    return workers;
        //}

        ///// <summary>
        ///// Возвращает список работников по их индентификатору
        ///// </summary>
        ///// <param name="id">Необходимый индентификатор</param>
        ///// <returns>Возвращает список работников по их индентификатору Task<IEnumerable<Worker>></returns>
        //public async Task<IEnumerable<Worker>> WorkerById(int id)
        //{
        //    List<Worker> workers = new List<Worker>();
        //    if (WorkerTableExist().Result && FormTypeTableExist().Result && CompanyTableExist().Result && PositionTableExist().Result)
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            await connection.OpenAsync();
        //            string query = $"SELECT* FROM workers JOIN positions ON positions.position_id = workers.position_id JOIN companies ON companies.company_id = workers.company_id JOIN form_types ON form_types.form_type_id = companies.form_type_id WHERE workers.worker_id = {id}";
        //            SqlCommand command = new SqlCommand(query, connection);
        //            SqlDataReader reader = await command.ExecuteReaderAsync();

        //            if (reader.HasRows) // если есть данные
        //            {
        //                while (await reader.ReadAsync()) // построчно считываем данные
        //                {
        //                    Worker worker = new Worker
        //                    {
        //                        Id = reader.GetInt32(0),
        //                        LastName = reader.GetString(1),
        //                        FirstName = reader.GetString(2),
        //                        MiddleName = reader.GetString(3),
        //                        DateEmployment = reader.GetDateTime(4),
        //                        PositionId = reader.GetInt32(5),
        //                        CompanyId = reader.GetInt32(6),
        //                        Position = new Position
        //                        {
        //                            Id = reader.GetInt32(7),
        //                            Name = reader.GetString(8)
        //                        },
        //                        Company = new Company
        //                        {
        //                            Id = reader.GetInt32(9),
        //                            Name = reader.GetString(10),
        //                            FormTypeId = reader.GetInt32(11),
        //                            FormType = new FormType
        //                            {
        //                                Id = reader.GetInt32(12),
        //                                Name = reader.GetString(13)
        //                            }
        //                        }
        //                    };
        //                    workers.Add(worker);
        //                }
        //            }

        //            await reader.CloseAsync();

        //        }
        //    }
        //    return workers;
        //}

        ///// <summary>
        ///// Добавляет новый экземпляр сущности 'workers'
        ///// </summary>
        ///// <param name="worker">Сотрудник, которого необходимо занести в бд</param>
        ///// <returns></returns>
        //public async Task WorkerCreate(Worker worker)
        //{
        //    if (WorkerTableExist().Result)
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            await connection.OpenAsync();
        //            string query = $"INSERT workers(worker_last_mame,worker_first_name,worker_middle_name,worker_date_employment,position_id,company_id) VALUES ('{worker.LastName}','{worker.FirstName}','{worker.MiddleName}','{worker.DateEmployment.ToShortDateString()}',{worker.PositionId},{worker.CompanyId});";
        //            SqlCommand command = new SqlCommand(query, connection);
        //            await command.ExecuteNonQueryAsync();
        //        }
        //    }
        //}

        ///// <summary>
        ///// Обновляет сведения об указанном сотруднике в сущности 'workers'
        ///// </summary>
        ///// <param name="worker">Измененные параметры сотрудника</param>
        ///// <returns></returns>
        //public async Task WorkerUpdate(Worker worker)
        //{
        //    if (WorkerTableExist().Result)
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            await connection.OpenAsync();
        //            string query = $"UPDATE workers SET worker_last_mame = '{worker.LastName}', worker_first_name = '{worker.FirstName}', worker_middle_name = '{worker.MiddleName}', worker_date_employment = '{worker.DateEmployment}', position_id = '{worker.PositionId}', company_id = '{worker.CompanyId}' WHERE worker_id = {worker.Id};";
        //            SqlCommand command = new SqlCommand(query, connection);
        //            await command.ExecuteNonQueryAsync();
        //        }
        //    }


        //}

        ///// <summary>
        ///// Удаление сотрудника из сущности 'workers'
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public async Task WorkerDelete(int? id)
        //{
        //    if (WorkerTableExist().Result)
        //    {
        //        if (id != null)
        //        {
        //            using (SqlConnection connection = new SqlConnection(connectionString))
        //            {
        //                await connection.OpenAsync();
        //                string query = $"DELETE workers WHERE worker_id = {id.Value}";
        //                SqlCommand command = new SqlCommand(query, connection);
        //                await command.ExecuteNonQueryAsync();
        //            }
        //        }
        //    }
        //}


        public IDbSet<Position> Positions { get; }
        public IDbSet<FormType> FormTypes { get; }
        public IDbSet<Company> Companies { get; }

    }
}
