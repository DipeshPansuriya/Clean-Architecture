﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace Users_Database
{
    public partial class AdminProduct
    {
        public AdminProduct()
        {
            AdminOrgProduct = new HashSet<AdminOrgProduct>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public bool? IsActive { get; set; }

        public virtual ICollection<AdminOrgProduct> AdminOrgProduct { get; set; }
    }
}