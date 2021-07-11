using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Domain.UserConfig
{
    public class rights_cls : audit_cls
    {
        public int RightId { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool View { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }

        public virtual role_cls RoleDetails { get; set; }
    }
}