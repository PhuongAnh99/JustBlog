using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Infrastructures
{
    public interface IBaseRepository<T> where T : class
    {
        public T Find(int id);
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public void Delete(int id);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);
        IList<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
    }
}
