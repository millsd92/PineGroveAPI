using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PineGroveAPIServerless.Models
{
    public partial class PineGroveDatabaseContext : DbContext
    {
        public PineGroveDatabaseContext()
        {
        }

        public PineGroveDatabaseContext(DbContextOptions<PineGroveDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnnouncementRequest> AnnouncementRequest { get; set; }
        public virtual DbSet<Attendance> Attendance { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventRegistration> EventRegistration { get; set; }
        public virtual DbSet<PrayerRequest> PrayerRequest { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<VisitRequest> VisitRequest { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<AnnouncementRequest>(entity =>
            {
                entity.HasKey(e => e.AnnouncementId);

                entity.ToTable("announcement_request");

                entity.Property(e => e.AnnouncementId).HasColumnName("announcement_id");

                entity.Property(e => e.Announcement)
                    .IsRequired()
                    .HasColumnName("announcement")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AnnouncementDate)
                    .HasColumnName("announcement_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AnnouncementRequest)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAnnouncement");
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.ToTable("attendance");

                entity.Property(e => e.AttendanceId).HasColumnName("attendance_id");

                entity.Property(e => e.Guests).HasColumnName("guests");

                entity.Property(e => e.ServiceId).HasColumnName("service_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Attendance)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceAttendance");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Attendance)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAttendance");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("event");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .IsUnicode(false);

                entity.Property(e => e.CurrentAttendees).HasColumnName("current_attendees");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.EventDescription)
                    .IsRequired()
                    .HasColumnName("event_description")
                    .IsUnicode(false);

                entity.Property(e => e.EventTitle)
                    .HasColumnName("event_title")
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.MaxAttendees).HasColumnName("max_attendees");

                entity.Property(e => e.Picture).HasColumnName("picture");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<EventRegistration>(entity =>
            {
                entity.ToTable("event_registration");

                entity.Property(e => e.EventRegistrationId).HasColumnName("event_registration_id");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Guests).HasColumnName("guests");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventRegistration)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_EventRegistration");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EventRegistration)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRegistration");
            });

            modelBuilder.Entity<PrayerRequest>(entity =>
            {
                entity.HasKey(e => e.PrayerId);

                entity.ToTable("prayer_request");

                entity.Property(e => e.PrayerId).HasColumnName("prayer_id");

                entity.Property(e => e.PrayerDate)
                    .HasColumnName("prayer_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PrayerDescription)
                    .IsRequired()
                    .HasColumnName("prayer_description")
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PrayerRequest)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPrayer");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("service");

                entity.Property(e => e.ServiceId).HasColumnName("service_id");

                entity.Property(e => e.ServiceDate)
                    .HasColumnName("service_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ServiceDescription)
                    .HasColumnName("service_description")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.EmailAddress)
                    .HasColumnName("email_address")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLineOne)
                    .HasColumnName("address_line_one")
                    .IsUnicode(false);

                entity.Property(e => e.AddressLineTwo)
                    .HasColumnName("address_line_two")
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasColumnName("zip_code")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VisitRequest>(entity =>
            {
                entity.HasKey(e => e.VisitId);

                entity.ToTable("visit_request");

                entity.Property(e => e.VisitId).HasColumnName("visit_id");

                entity.Property(e => e.Reason)
                    .HasColumnName("reason")
                    .IsUnicode(false);

                entity.Property(e => e.RequestDate)
                    .HasColumnName("request_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.VisitDate)
                    .HasColumnName("visit_date")
                    .IsRequired(false)
                    .HasColumnType("date");

                entity.Property(e => e.Visited).HasColumnName("visited");

                entity.Property(e => e.AddressLineOne)
                    .HasColumnName("address_line_one")
                    .IsUnicode(false);

                entity.Property(e => e.AddressLineTwo)
                    .HasColumnName("address_line_two")
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasColumnName("zip_code")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VisitRequest)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserVisit");
            });
        }
    }
}
