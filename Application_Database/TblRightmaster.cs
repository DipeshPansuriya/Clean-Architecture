﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace Application_Database
{
    public partial class TblRightmaster
    {
        public int RightId { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool View { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool IsDeleted { get; set; }
        public bool RecordLocked { get; set; }
        public int RecordLockedBy { get; set; }
        public DateTime RecordLockedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int DeletedBy { get; set; }
        public DateTime DeletedOn { get; set; }

        public virtual TblRolemaster Role { get; set; }
    }
}