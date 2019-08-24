using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ERM.DataAccess.Models
{
    public class ERMSinkDbDbContext : DbContext
    {
        public ERMSinkDbDbContext()
        {
        }

        public ERMSinkDbDbContext(DbContextOptions<ERMSinkDbDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ErmsinkTable> ErmsinkTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<ErmsinkTable>(entity =>
            {
                entity.ToTable("ERMSinkTable");

                entity.Property(e => e.DataType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.MeterCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

        }
    }
}
