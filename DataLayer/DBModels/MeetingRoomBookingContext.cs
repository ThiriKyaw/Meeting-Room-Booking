using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataLayer.DBModels
{
    public partial class MeetingRoomBookingContext : DbContext
    {
        public MeetingRoomBookingContext()
        {
        }

        public MeetingRoomBookingContext(DbContextOptions<MeetingRoomBookingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<MeetingEvent> MeetingEvents { get; set; }
        public virtual DbSet<MeetingRoom> MeetingRooms { get; set; }
        public virtual DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.ToTable("Audit");

                entity.Property(e => e.AuditId).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Createdby).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.FunctionName)
                    .HasMaxLength(255)
                    .HasColumnName("functionName");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.ToTable("ErrorLog");

                entity.Property(e => e.ErrorLogId).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Createdby).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.FunctionName)
                    .HasMaxLength(255)
                    .HasColumnName("functionName");

                entity.Property(e => e.InnerException)
                    .HasMaxLength(255)
                    .HasColumnName("innerException");

                entity.Property(e => e.Message)
                    .HasMaxLength(255)
                    .HasColumnName("message");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Source).HasMaxLength(255);

                entity.Property(e => e.StackTrace).HasMaxLength(255);

                entity.Property(e => e.TargetSite).HasMaxLength(255);
            });

            modelBuilder.Entity<MeetingEvent>(entity =>
            {
                entity.HasKey(e => e.EventId)
                    .HasName("PK__MeetingE__7944C810D2A3494D");

                entity.ToTable("MeetingEvent");

                entity.Property(e => e.EventId).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EventCreatedby)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.EventDescription).HasMaxLength(255);

                entity.Property(e => e.EventEndDateTime).HasColumnType("datetime");

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.EventStartDateTime).HasColumnType("datetime");

                entity.Property(e => e.MeetingRoomId)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.EventCreatedbyNavigation)
                    .WithMany(p => p.MeetingEvents)
                    .HasForeignKey(d => d.EventCreatedby)
                    .HasConstraintName("FK_UserId");

                entity.HasOne(d => d.MeetingRoom)
                    .WithMany(p => p.MeetingEvents)
                    .HasForeignKey(d => d.MeetingRoomId)
                    .HasConstraintName("FK_MeetingRoomId");
            });

            modelBuilder.Entity<MeetingRoom>(entity =>
            {
                entity.ToTable("MeetingRoom");

                entity.Property(e => e.MeetingRoomId).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Createdby)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.HaveTv).HasColumnName("HaveTV");

                entity.Property(e => e.MeetingRoomMaxSize)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MeetingRoomName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Createdby)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LoginName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ModifedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
