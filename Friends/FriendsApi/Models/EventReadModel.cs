using System;
using System.Collections.Generic;

namespace FriendsApi.Models
{
    public class EventReadModel
    {
        public int Id { get; set; }

        public string Name { get; set; }    

        public int OrganizatorId { get; set; }

        public List<int> MembersId { get; set; }

        public DateTime StartDate { get; set; }

        public string Coordinates { get; set; }
    }
}
