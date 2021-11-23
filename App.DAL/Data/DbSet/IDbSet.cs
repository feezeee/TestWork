using System.Collections.Generic;

namespace App.DAL.Data.DbSet
{
    /// <summary>
    /// Интерфейс, заменяющий Entity. Внутри представлены функции для работы с бд
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDbSet<T> where T : class
    {
        /// <summary>
        /// Проверка на существование таблицы
        /// </summary>
        /// <returns></returns>
        public bool TableExist();

        /// <summary>
        /// Получение всех экземпляров сущности
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll();

        /// <summary>
        /// Поиск среди экземпляров сущности
        /// </summary>
        /// <param name="item">Свойства, по которым осуществляется поиск</param>
        /// <returns></returns>
        public IEnumerable<T> Find(T item);

        /// <summary>
        /// Добавление нового экземплера в сущность
        /// </summary>
        /// <param name="item">Ее свойства</param>
        public void Create(T item);


        /// <summary>
        /// Изменение существующего экземплера
        /// </summary>
        /// <param name="item">Новые свойства</param>
        /// <param name="id">Уникальный индентификатор экземпляра</param>
        public void Update(T item, int? id = null);

        /// <summary>
        /// Удаление экземпляра из сущности по указанному Id
        /// </summary>
        /// <param name="id">Индентификатор удаления</param>
        public void Delete(int id);

    }
}