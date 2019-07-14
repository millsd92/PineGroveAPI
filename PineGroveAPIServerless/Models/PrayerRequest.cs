using System;
using System.Collections.Generic;

namespace PineGroveAPIServerless.Models
{
    public partial class PrayerRequest
    {
        public int PrayerId { get; set; }
        public int UserId { get; set; }
        public string PrayerDescription { get; set; }
        public DateTime PrayerDate { get; set; }

        public virtual User User { get; set; }
    }
}
