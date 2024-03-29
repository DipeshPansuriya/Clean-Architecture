﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace Users_Database
{
    public partial class AdminBranch
    {
        public AdminBranch()
        {
            AdminUserBranch = new HashSet<AdminUserBranch>();
        }

        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int OrgProdId { get; set; }
        public string BranchName { get; set; } = null!;
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public bool? IsHo { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual AdminCompany Comp { get; set; } = null!;
        public virtual AdminOrgProduct OrgProd { get; set; } = null!;
        public virtual ICollection<AdminUserBranch> AdminUserBranch { get; set; }
    }
}