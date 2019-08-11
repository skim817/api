using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PlanYourDate.Model
{
    public partial class PlacesForDateContext : DbContext
    {
        public PlacesForDateContext()
        {
        }

        public PlacesForDateContext(DbContextOptions<PlacesForDateContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Places> Places { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:placesforsearch.database.windows.net,1433;Initial Catalog=PlacesForDate;Persist Security Info=False;User ID=saemin;Password=A1s2d3f4;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Places>(entity =>
            {
                entity.HasKey(e => e.PlaceId)
                    .HasName("PK__Places__D5222B6E819F0671");

                entity.Property(e => e.Comment).IsUnicode(false);

                entity.Property(e => e.PhoneNumber).IsUnicode(false);

                entity.Property(e => e.PhotoRef).IsUnicode(false);

                entity.Property(e => e.PlaceAddress).IsUnicode(false);

                entity.Property(e => e.PlaceName).IsUnicode(false);
            });

            modelBuilder.Entity<Reviews>(entity =>
            {
                entity.HasKey(e => e.ReviewId)
                    .HasName("PK__Reviews__74BC79CE0A9E6C60");

                entity.Property(e => e.AuthorName).IsUnicode(false);

                entity.Property(e => e.Comment).IsUnicode(false);

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PlaceId");
            });
        }
    }
}
