using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public T GetById(int id);
        public IEnumerable<T> Find(Func<T, Boolean> predicate);
        public void Create(T item);
        public void Update(T item);
        public void Delete(int id);
    }
}
