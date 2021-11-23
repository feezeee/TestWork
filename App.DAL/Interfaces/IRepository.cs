using System.Collections.Generic;

namespace App.DAL.Interfaces
{
    /// <summary>
    /// Данный интерфейс будет реализовываться класами, которые будут представлять доступ к какому либо источнику данных, в нашем случае к БД
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Возвращает данные с репозитория
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll();

        /// <summary>
        /// Возвращается данные с репозитория по Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(int id);

        /// <summary>
        /// Поиск данных в репозитории по свойствам
        /// </summary>
        /// <param name="item">Поиск по свойствам экземпляра</param>
        /// <returns></returns>
        public IEnumerable<T> Find(T item);

        /// <summary>
        /// Добавление данных в репозиториий
        /// </summary>
        /// <param name="item">Новый экземпляр для добавления</param>
        public void Create(T item);

        /// <summary>
        /// Обновление данных в репозитории
        /// </summary>
        /// <param name="item">Измененные свойства экземплера</param>
        /// <param name="id">Индентификатор экземпляра</param>
        public void Update(T item, int? id = null);



        /// <summary>
        /// Удаление данных из репозитория по Id
        /// </summary>
        /// <param name="id">Индентификатор экземпляра</param>
        public void Delete(int id);
    }
}
