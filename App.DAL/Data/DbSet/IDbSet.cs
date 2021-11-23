using System;
using System.Collections.Generic;

namespace App.DAL.Data.DbSet
{
    public interface IDbSet<T> where T : class
    {
        public bool TableExist();
        public IEnumerable<T> GetAll();
        public IEnumerable<T> Find(T item);
        public void Create(T item);
        public void Update(T item, int? id = null);
        public void Delete(int id);

    }
}