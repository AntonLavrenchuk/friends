using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsData.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Required, MaxLength(255)]
        public string FirstName { get; set; }

        [Required, MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        public List<Event> Events { get; set; } = new List<Event>();

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public int? RefreshTokenID { get; set; }

        public RefreshToken RefreshToken { get; set; }

    }
}
