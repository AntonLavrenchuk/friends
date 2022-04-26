using FriendsData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsData.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task AddAsync(T obj);

        IEnumerable<T> Get();

        Task<T> GetAsync(int id);

        Task UpdateAsync(int id, T obj);

        Task DeleteAsync(int id);
    }
}
