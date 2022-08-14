﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace Masters_Database
{
    public partial class MasterCountry
    {
        public MasterCountry()
        {
            MasterState = new HashSet<MasterState>();
        }

        public int CountryId { get; set; }
        public string Name { get; set; } = null!;
        public string CodeTwoLtr { get; set; } = null!;
        public string CodeThreeLtr { get; set; } = null!;
        public int? NumericCode { get; set; }
        public int ContinentId { get; set; }
        public int? TimeZoneId { get; set; }
        public int? CurrencyId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsSystemGenrated { get; set; }

        public virtual MastersContinent Continent { get; set; } = null!;
        public virtual MasterCurrency? Currency { get; set; }
        public virtual MasterTimeZone? TimeZone { get; set; }
        public virtual ICollection<MasterState> MasterState { get; set; }
    }
}