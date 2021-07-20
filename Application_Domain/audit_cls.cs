using System;

namespace Application_Domain
{
    public class audit_cls
    {
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
    }
}