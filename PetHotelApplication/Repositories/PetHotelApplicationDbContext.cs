using System;
using System.Collections.Generic;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repositories;

public partial class PetHotelApplicationDbContext : DbContext
{
    public PetHotelApplicationDbContext()
    {
    }

    public PetHotelApplicationDbContext(DbContextOptions<PetHotelApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accommodation> Accommodations { get; set; }

    public virtual DbSet<BookingInformation> BookingInformations { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<PaymentRecord> PaymentRecords { get; set; }

    public virtual DbSet<Pet> Pets { get; set; }

    public virtual DbSet<PetCareService> PetCareServices { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ServiceBooking> ServiceBookings { get; set; }

    public virtual DbSet<ServiceImage> ServiceImages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        return config["ConnectionStrings:PetHotelApplication"];
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accommodation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Accommod__3214EC27AE4280CC");

            entity.ToTable("Accommodation");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<BookingInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookingI__3214EC275998A604");

            entity.ToTable("BookingInformation");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.AccommodationId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("AccommodationID");
            entity.Property(e => e.BoardingType).HasMaxLength(50);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.PetId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PetID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UserId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserID");

            entity.HasOne(d => d.Accommodation).WithMany(p => p.BookingInformations)
                .HasForeignKey(d => d.AccommodationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingIn__Accom__4222D4EF");

            entity.HasOne(d => d.Pet).WithMany(p => p.BookingInformations)
                .HasForeignKey(d => d.PetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingIn__PetID__412EB0B6");

            entity.HasOne(d => d.User).WithMany(p => p.BookingInformations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingIn__UserI__403A8C7D");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Feedback__3214EC27F028BFD3");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Comment).IsUnicode(false);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedbacks__UserI__5165187F");
        });

        modelBuilder.Entity<PaymentRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentR__3214EC27F5911A35");

            entity.ToTable("PaymentRecord");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.BookingId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("BookingID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Method)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserID");

            entity.HasOne(d => d.Booking).WithMany(p => p.PaymentRecords)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaymentRe__Booki__4E88ABD4");

            entity.HasOne(d => d.User).WithMany(p => p.PaymentRecords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaymentRe__UserI__4D94879B");
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pet__3214EC278AB5F2DA");

            entity.ToTable("Pet");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Avatar).IsUnicode(false);
            entity.Property(e => e.Breed).HasMaxLength(50);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.PetName).HasMaxLength(100);
            entity.Property(e => e.Species).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UserId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Pets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pet__UserID__3B75D760");
        });

        modelBuilder.Entity<PetCareService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PetCareS__3214EC2749E18AFB");

            entity.ToTable("PetCareService");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC2733E35F4A");

            entity.ToTable("Role");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ServiceBooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ServiceB__3214EC27720A5158");

            entity.ToTable("ServiceBooking");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.BookingId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ServiceId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ServiceID");

            entity.HasOne(d => d.Booking).WithMany(p => p.ServiceBookings)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceBo__Booki__4AB81AF0");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceBookings)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceBo__Servi__49C3F6B7");
        });

        modelBuilder.Entity<ServiceImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ServiceI__3214EC2700A104A1");

            entity.ToTable("ServiceImage");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Image).IsUnicode(false);
            entity.Property(e => e.ServiceId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ServiceID");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceImages)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceIm__Servi__46E78A0C");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC27E89F24AF");

            entity.ToTable("User");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Avatar).IsUnicode(false);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.RoleId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RoleID");
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__RoleID__38996AB5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
