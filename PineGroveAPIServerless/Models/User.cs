using System;
using System.Collections.Generic;

namespace PineGroveAPIServerless.Models
{
    public partial class User
    {
        public User()
        {
            AnnouncementRequest = new HashSet<AnnouncementRequest>();
            Attendance = new HashSet<Attendance>();
            EventRegistration = new HashSet<EventRegistration>();
            PrayerRequest = new HashSet<PrayerRequest>();
            VisitRequest = new HashSet<VisitRequest>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public long? PhoneNumber { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public virtual ICollection<AnnouncementRequest> AnnouncementRequest { get; set; }
        public virtual ICollection<Attendance> Attendance { get; set; }
        public virtual ICollection<EventRegistration> EventRegistration { get; set; }
        public virtual ICollection<PrayerRequest> PrayerRequest { get; set; }
        public virtual ICollection<VisitRequest> VisitRequest { get; set; }
    }
}
