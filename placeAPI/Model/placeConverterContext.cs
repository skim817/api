using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace placeAPI.Model
{
    public partial class placeConverterContext : DbContext
    {
        public placeConverterContext()
        {
        }

        public placeConverterContext(DbContextOptions<placeConverterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Main> Main { get; set; }
        public virtual DbSet<PlaceConverter> PlaceConverter { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:placeconverter.database.windows.net,1433;Initial Catalog=placeConverter;Persist Security Info=False;User ID=liam;Password=A1s2d3f4;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Main>(entity =>
            {
                entity.Property(e => e.PlaceName).IsUnicode(false);
            });

            modelBuilder.Entity<PlaceConverter>(entity =>
            {
                entity.Property(e => e.Photo).IsUnicode(false);

                entity.Property(e => e.PlaceId).IsUnicode(false);

                entity.Property(e => e.PlaceTitle).IsUnicode(false);

                entity.HasOne(d => d.Main)
                    .WithMany(p => p.PlaceConverter)
                    .HasForeignKey(d => d.MainId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("MainId");
            });
        }
    }
}
