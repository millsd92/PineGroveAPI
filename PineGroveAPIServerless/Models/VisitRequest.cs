using System;
using System.Collections.Generic;

namespace PineGroveAPIServerless.Models
{
    public partial class VisitRequest
    {
        public int VisitId { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? VisitDate { get; set; }
        public bool Visited { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public virtual User User { get; set; }
    }
}
