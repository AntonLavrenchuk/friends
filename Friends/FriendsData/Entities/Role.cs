using FriendsCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsData.Entities
{
   [Table("Roles")]
   public class Role : BaseEntity
   {
        public RoleType RoleType { get; set; }

        public List<RolePermission> RolePermissions { get; set; }
    }
}
