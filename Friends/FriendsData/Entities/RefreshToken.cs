using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsData.Entities
{
    [Table("RefreshTokens")]
    public class RefreshToken : BaseEntity
    {
        public Guid Value { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
