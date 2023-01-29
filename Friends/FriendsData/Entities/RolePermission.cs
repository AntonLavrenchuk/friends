using FriendsCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendsData.Entities
{   
    [Table("RolePermissions")]
    public class RolePermission : BaseEntity
    {
        public int RoleId { get; set; }

        public Role Role { get; set; }

        public PermissionType PermissionType { get; set; }
    }
}
