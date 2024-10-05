﻿using CarRentalSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<VehicleEntity> Vehicle { get; set; }
        public DbSet<BookingEntity> Booking { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CouponEntity> Coupon { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<UserEntity>())
            {
                // Ensure Created is UTC
                if (entry.Entity.Created.Kind != DateTimeKind.Utc)
                {
                    entry.Entity.Created = DateTime.SpecifyKind(entry.Entity.Created, DateTimeKind.Utc);
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Configuration for VehicleEntity
            builder.Entity<VehicleEntity>(v =>
            {
                // Sử dụng OwnsOne để ánh xạ VehicleEngineProperty
                v.OwnsOne(x => x.VehicleEngineProperty, engine =>
                {
                    engine.ToJson(); // Lưu trữ như JSONB
                    engine.Property(e => e.OwnerId);
                    engine.Property(e => e.NumberOfSeats);
                    engine.Property(e => e.TransmissionSystem);
                    engine.Property(e => e.Fuel);
                    engine.Property(e => e.Consumption);
                });

                v.OwnsOne(x => x.AdditionalFeeProperty, fees =>
                {
                    fees.ToJson(); // Lưu trữ như JSONB
                    fees.Property(f => f.OwnerId);
                    fees.Property(f => f.VATFee);
                    fees.Property(f => f.ReservationFee);
                    fees.Property(f => f.DepositFee);
                });

                v.OwnsOne(x => x.VehicleAmenityProperty, amenity =>
                {
                    amenity.ToJson();
                    amenity.Property(x => x.Amenities);
                });
                v.OwnsOne(x => x.VehicleImageProperty, vehicleImage =>
                {
                    vehicleImage.ToJson();
                    vehicleImage.Property(x => x.Images);
                });
                v.HasMany(vehicle => vehicle.Bookings)
                    .WithOne(booking => booking.Vehicle)
                    .HasForeignKey(booking => booking.VehicleId);
                v.OwnsOne(x => x.DataExtension, d =>
                {
                    d.ToJson();
                    d.Property(f => f.Id);
                    d.Property(f => f.Name);
                    d.Property(f => f.Data);
                });
            });

            //Configuration for BookingEntity
            builder.Entity<BookingEntity>(v =>
            {
                v.HasOne(booking => booking.User)
                    .WithMany(user => user.Bookings)
                    .HasForeignKey(booking => booking.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                v.HasOne(booking => booking.Vehicle)
                    .WithMany( vehicle => vehicle.Bookings)
                    .HasForeignKey(booking => booking.VehicleId)
                    .OnDelete(DeleteBehavior.Cascade);

                v.HasOne(booking => booking.Coupon)
                    .WithMany(coupon => coupon.Bookings)
                    .HasForeignKey(booking => booking.CouponId)
                    .OnDelete(DeleteBehavior.SetNull);
                v.OwnsOne(x => x.DataExtension, d =>
                {
                    d.ToJson();
                    d.Property(f => f.Id);
                    d.Property(f => f.Name);
                    d.Property(f => f.Data);
                });
            });

            //Configuration for CouponEntity
            builder.Entity<CouponEntity>(v => 
            {
                v.HasMany(coupon => coupon.Bookings)
                    .WithOne(booking => booking.Coupon)
                    .HasForeignKey(booking => booking.CouponId);
                v.OwnsOne(x => x.DataExtension, d =>
                {
                    d.ToJson();
                    d.Property(f => f.Id);
                    d.Property(f => f.Name);
                    d.Property(f => f.Data);
                });
            });

            //Configuration for UserEntity
            builder.Entity<UserEntity>(v =>
            {
                v.HasMany(user => user.Bookings)
                    .WithOne(booking => booking.User)
                    .HasForeignKey(booking => booking.UserId);
                v.OwnsOne(x => x.DataExtension, d =>
                {
                    d.ToJson();
                    d.Property(f => f.Id);
                    d.Property(f => f.Name);
                    d.Property(f => f.Data);
                });
            });
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("User Id=postgres.fkqbmypxiuxcgxpfdtys;Password=r.2X3vj2QfxKU5j;Server=aws-0-ap-southeast-1.pooler.supabase.com;Port=5432;Database=postgres;");

    }
}