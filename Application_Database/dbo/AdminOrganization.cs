﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace Application_Database
{
    public partial class AdminOrganization
    {
        public AdminOrganization()
        {
            AdminOrgProduct = new HashSet<AdminOrgProduct>();
        }

        public int OrgId { get; set; }
        public string OrgName { get; set; } = default!;
        public string OrgEmail { get; set; } = default!;
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<AdminOrgProduct> AdminOrgProduct { get; set; }
    }
}