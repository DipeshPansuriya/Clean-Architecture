﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace Users_Database
{
    public partial class StatusNotification
    {
        public int Id { get; set; }
        public string MsgType { get; set; } = null!;
        public string? MsgFrom { get; set; }
        public string? MsgTo { get; set; }
        public string? MsgCC { get; set; }
        public string MsgSubject { get; set; } = null!;
        public string MsgBody { get; set; } = null!;
        public string MsgSatus { get; set; } = null!;
        public string? FailDetails { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}