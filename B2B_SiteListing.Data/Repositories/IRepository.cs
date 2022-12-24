using B2B_SiteListing.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace B2B_SiteListing.Data.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<T> FindOne(Expression<Func<T,bool>> expression);
        Task<T> FindMany(Expression<Func<T,bool>> expression);
        Task<Guid> Insert(T obj);
        Task Update(T obj);
        Task Delete(T obj);
        Task Save();
    }
}
