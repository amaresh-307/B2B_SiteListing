using B2B_SiteListing.Data.DbContext;
using B2B_SiteListing.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace B2B_SiteListing.Data.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private AppDbContext _context = null;
        private DbSet<T> table = null;
        
        public Repository(AppDbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public async Task Delete(T obj)
        {
            obj.IsDeleted = true;
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<T> FindOne(Expression<Func<T, bool>> expression)
        {
            var res = await table.Where(expression).FirstOrDefaultAsync();
            return res;
        }
        public async Task<T> FindMany(Expression<Func<T, bool>> expression)
        {
            var res = await table.Where(expression).FirstOrDefaultAsync();
            return res;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await table.ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await table.FindAsync(id);
        }

        public async Task<Guid> Insert(T obj)
        {
            var id = Guid.NewGuid();
            obj.Id = id;
            await table.AddAsync(obj);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(T obj)
        {
             table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
