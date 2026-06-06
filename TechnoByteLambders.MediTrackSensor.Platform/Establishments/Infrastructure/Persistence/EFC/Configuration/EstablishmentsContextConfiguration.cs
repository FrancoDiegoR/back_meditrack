using Microsoft.EntityFrameworkCore;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.Aggregates;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.ValueObjects;

namespace TechnoByteLambders.MediTrackSensor.Platform.Establishments.Infrastructure.Persistence.EFC.Configuration;

public static class EstablishmentsContextConfiguration
{
    public static void ApplyEstablishmentsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Establishment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.EstablishmentName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.EstablishmentType).IsRequired().HasConversion<string>();
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Website).HasMaxLength(200);

            entity.OwnsOne(e => e.Address, address =>
            {
                address.Property(a => a.Street).HasColumnName("address_street").HasMaxLength(150);
                address.Property(a => a.District).HasColumnName("address_district").HasMaxLength(100);
                address.Property(a => a.CityRegion).HasColumnName("address_city_region").HasMaxLength(100);
                address.Property(a => a.Country).HasColumnName("address_country").HasMaxLength(100);
            });

            entity.OwnsOne(e => e.Location, location =>
            {
                location.Property(l => l.Latitude).HasColumnName("location_latitude").HasPrecision(9, 6);
                location.Property(l => l.Longitude).HasColumnName("location_longitude").HasPrecision(9, 6);
            });

            entity.OwnsOne(e => e.AdminId, adminId =>
            {
                adminId.Property(a => a.Value).HasColumnName("admin_id");
            });
        });

        builder.Entity<Operator>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.AlertsAnswered).IsRequired();
            entity.Property(o => o.Schedule).IsRequired().HasMaxLength(100);
            entity.Property(o => o.EstablishmentId).IsRequired();

            entity.OwnsOne(o => o.UserId, userId =>
            {
                userId.Property(u => u.Value).HasColumnName("user_id");
            });

            entity.HasOne<Establishment>()
                .WithMany()
                .HasForeignKey(o => o.EstablishmentId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
