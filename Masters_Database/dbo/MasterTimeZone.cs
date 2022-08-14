﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace Masters_Database
{
    public partial class MasterTimeZone
    {
        public MasterTimeZone()
        {
            MasterCountry = new HashSet<MasterCountry>();
        }

        public int TimeZoneId { get; set; }
        public string Name { get; set; } = null!;
        public string OffSet { get; set; } = null!;
        public string OffSetName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsSystemGenrated { get; set; }

        public virtual ICollection<MasterCountry> MasterCountry { get; set; }
    }
}