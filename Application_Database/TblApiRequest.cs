﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace Application_Database
{
    public partial class TblApiRequest
    {
        public int Id { get; set; }
        public string Scheme { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public int? Userid { get; set; }
        public string Request { get; set; }
        public DateTime? RequestDate { get; set; }
    }
}