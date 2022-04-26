using FriendsData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsData.Repositories
{
    public class EventsRepository : BaseRepository<Event>
    {
        public EventsRepository(EventsContext context) : base(context)
        {
        }

        public async Task<Event> GetWithInclude(int id)
        {
            var evt =  await _dbContext.Set<Event>()
                .Include(_ => _.Members)
                .FirstOrDefaultAsync(_=>_.Id == id);

            return evt;
        }

        public async Task<IEnumerable<Event>> GetWithInclude()
        {
            var events = await _dbContext.Set<Event>()
                .Include(_ => _.Members)
                .ToListAsync();

            return events;
        }
    }
}
