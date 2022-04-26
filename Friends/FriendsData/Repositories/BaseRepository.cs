using FriendsCore;
using FriendsData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsData.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>, IDisposable where T: BaseEntity
    {
        protected readonly DbContext _dbContext;

        public BaseRepository(DbContext context)
        {
            _dbContext = context;
        }

        public async Task AddAsync(T obj)
        {
            if( obj is null)
            {
                return;
            }


            await _dbContext.Set<T>().AddAsync(obj);
        }

        public async Task DeleteAsync(int id)
        {
            var obj =  await _dbContext.Set<T>().FirstOrDefaultAsync(ent => ent.Id == id);

            if(obj is null)
            {
                return;
            }

            _dbContext.Set<T>().Remove(obj);
        }

        public void Dispose()
        {
            ((IDisposable)_dbContext).Dispose();
        }

        public IEnumerable<T> Get()
        {
           if(_dbContext is null ||
                _dbContext.Set<T>().Count() == 0 )
            {
                return null;
            }

            return _dbContext.Set<T>();
        }

        public async Task<T> GetAsync(int id)
        {
            var ent = await _dbContext.Set<T>().FirstOrDefaultAsync(_ => _.Id == id);

            return ent;
        }

        public async Task UpdateAsync(int id, T obj)
        {
            var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(ent => ent.Id == id);

            if(entity is null)
            {
                return;
            }

            Helper.CopyProperties(obj, entity);
        }
    }
}
