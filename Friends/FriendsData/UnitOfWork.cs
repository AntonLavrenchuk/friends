using FriendsData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsData
{
    public class UnitOfWork : IDisposable
    {
        public EventsRepository EventsRepository { get; set; }
        public UsersRepository UsersRepository { get; set; }

        private readonly EventsContext _eventsContext;

        public UnitOfWork(EventsContext evetntsCtx)
        {
            _eventsContext = evetntsCtx;

            EventsRepository = new EventsRepository(_eventsContext);
            UsersRepository = new UsersRepository(_eventsContext);
        }

        public async Task Save()
        {
            await _eventsContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            EventsRepository.Dispose();
            UsersRepository.Dispose();
        }
    }
}
