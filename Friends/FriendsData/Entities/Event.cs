using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsData.Entities
{
    [Table("Events")]
    public class Event : BaseEntity
    {
        [Required, MaxLength(255)]
        public string Name { get; set; }

        public User Organizator { get; set; }

        [Required]
        public int OrganizatorId { get; set; }

        [Required]
        public List<User> Members { get; set; } = new List<User>();

        [Required]
        public DateTime StartDate { get; set; }

        [Required, RegularExpression(@"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$")]
        public string Coordinates { get; set; }


    }
}
