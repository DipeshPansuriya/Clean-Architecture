﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace Users_Database
{
    public partial class AdminRights
    {
        public int RightId { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool? ViewAccess { get; set; }
        public bool? AddAccess { get; set; }
        public bool? EditAccess { get; set; }
        public bool? DeleteAccess { get; set; }

        public virtual AdminMenu Menu { get; set; } = null!;
        public virtual AdminRole Role { get; set; } = null!;
    }
}