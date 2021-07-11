using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Domain.UserConfig
{
    public class role_cls : audit_cls
    {
        public int RoleId { get; set; }
        public string RoleNmae { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<rights_cls> RightDetails { get; set; }
    }
}