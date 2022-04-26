using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FriendsData.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        
    }
}
