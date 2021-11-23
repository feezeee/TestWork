using System.Collections.Generic;

namespace App.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public T GetById(int id);
        public IEnumerable<T> Find(T item);
        public void Create(T item);
        public void Update(T item, int? id = null);
        public void Delete(int id);
    }
}
