using System;

namespace FriendsApi.Models
{
    public class EventCreateModel
    {
        public string Name { get; set; }

        public int OrganizatorId { get; set; }

        public DateTime StartDate { get; set; }

        public string Coordinates { get; set; }
    }
}
