using System;

namespace FriendsApi.Models
{
    public class EventUpdateModel
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public string Coordinates { get; set; }
    }
}
