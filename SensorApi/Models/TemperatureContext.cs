using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SensorApi.Models
{
    // Must inherit from IdentitytDbContext because this context is handling users.
    public class TemperatureContext : IdentityDbContext
    {
        public DbSet<TemperatureItem> TemperatureItems { get; set; }
        public DbSet<Device> Devices { get; set; }

        public TemperatureContext(DbContextOptions<TemperatureContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Must call parent OnModelCreate here, else Identity won't work.
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Device>()
                .HasKey(d => d.Id)
                .HasName("PrimaryKey_DeviceId");
            modelBuilder.Entity<Device>()
                .Property(d => d.Location)
                .IsRequired();
            modelBuilder.Entity<Device>()
                .Property(d => d.Name)
                .IsRequired();

            modelBuilder.Entity<TemperatureItem>()
                .HasKey(t => t.Id)
                .HasName("PrimaryKey_TemperatureItemId");
            modelBuilder.Entity<TemperatureItem>()
                .Property(t => t.Temperature)
                .IsRequired();
            modelBuilder.Entity<TemperatureItem>()
                .Property(t => t.Timestamp)
                .IsRequired();
            modelBuilder.Entity<TemperatureItem>()
                .HasOne(t => t.Device)
                .WithMany(d => d.TemperatureItems)
                .HasForeignKey(t => t.DeviceId)
                .HasConstraintName("ForeignKey_TemperatureItem_Device");
        }
    }
}
