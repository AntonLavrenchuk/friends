﻿using FriendsData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsData.Repositories
{
    public class UsersRepository : BaseRepository<User>
    {
        public UsersRepository(EventsContext context) : base(context)
        {
        }
    }
}
