using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

//Scaffolded by Entity Framework.

namespace HotelApp.Models
{
    public partial class HotelDBContext : DbContext
    {
        public HotelDBContext()
        {
        }

        public HotelDBContext(DbContextOptions<HotelDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomersByReservationTime> CustomersByReservationTimes { get; set; }
        public virtual DbSet<FutureReservationsByRoom> FutureReservationsByRooms { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=localhost;Database=HotelDB;Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(x => x.Phone, "UQ__Customer__5C7E359E24BB7639")
                    .IsUnique();

                entity.HasIndex(x => x.Email, "UQ__Customer__A9D105349F233AF3")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomersByReservationTime>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CustomersByReservationTime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FutureReservationsByRoom>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("FutureReservationsByRoom");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.RoomId).HasColumnName("RoomID");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.ReservationId).HasColumnName("ReservationID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.FromDate)
                    .HasColumnType("date")
                    .HasAnnotation("Relational:ColumnType", "date");

                entity.Property(e => e.NumberOfGuests).HasDefaultValueSql("((1))");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.ToDate)
                    .HasColumnType("date")
                    .HasAnnotation("Relational:ColumnType", "date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reservati__Custo__5165187F");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(x => x.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reservati__RoomI__52593CB8");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasIndex(x => x.RoomNumber, "UQ__Rooms__AE10E07A0D68B912")
                    .IsUnique();

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.FloorNumber).HasDefaultValueSql("((1))");

                entity.Property(e => e.RoomType)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
