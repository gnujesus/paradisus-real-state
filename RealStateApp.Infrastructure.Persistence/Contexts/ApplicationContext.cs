using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RealStateApp.Core.Domain.Common;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<TypeProperty> TypeProperties { get; set; }
        public DbSet<TypeSale> TypeSales { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Tables

            modelBuilder.Entity<Amenity>()
                .ToTable("Amenities");

            modelBuilder.Entity<Favorites>()
                .ToTable("Favorites");

            modelBuilder.Entity<Property>()
                .ToTable("Properties");

            modelBuilder.Entity<TypeProperty>()
                .ToTable("TypeProperties");

            modelBuilder.Entity<TypeSale>()
                .ToTable("TypeSales");

            modelBuilder.Entity<PropertyImage>()
                .ToTable("PropertyImages");
            #endregion

            #region Properties configuration
            modelBuilder.Entity<Property>().Property(p => p.Value_Sale).HasColumnType("decimal(5, 2)");
            #endregion

            #region Primary keys

            modelBuilder.Entity<Amenity>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Favorites>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Property>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<TypeProperty>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<TypeSale>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<PropertyImage>()
                .HasKey(x => x.Id);

            #endregion

            #region Relationships

            modelBuilder.Entity<Property>()
                .HasMany<Amenity>(property => property.Amenities)
                .WithMany(amenity => amenity.Properties);

            modelBuilder.Entity<Property>()
            .HasOne<TypeProperty>(p => p.Type_Property)
            .WithMany(tp => tp.Properties)
            .HasForeignKey(p => p.TypeProperty_Id);

            modelBuilder.Entity<Property>()
                        .HasOne<TypeSale>(p => p.Type_sale)
                        .WithMany(ts => ts.Properties)
                        .HasForeignKey(p => p.TypeSale_Id);

            modelBuilder.Entity<Favorites>()
            .HasOne(f => f.Property)
            .WithMany()
            .HasForeignKey(f => f.Property_Id);

            modelBuilder.Entity<PropertyImage>()
            .HasOne(pi => pi.Property)
            .WithMany(p => p.Images)
            .HasForeignKey(pi => pi.PropertyId);

            #endregion
        }
    }
}
