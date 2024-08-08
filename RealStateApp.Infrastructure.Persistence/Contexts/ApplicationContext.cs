using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RealStateApp.Core.Domain.Common;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Services;

namespace RealStateApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<TypeProperty> TypeProperties { get; set; }
        public DbSet<TypeSale> TypeSales { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
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

            return await base.SaveChangesAsync(cancellationToken);
        }

        private async Task AddIdsAsync()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    var idProperty = entry.Entity.GetType().GetProperty("Id");
                    if (idProperty != null && idProperty.PropertyType == typeof(string))
                    {
                        var currentId = idProperty.GetValue(entry.Entity) as string;
                        if (string.IsNullOrEmpty(currentId))
                        {
                            var generatedId = await CodeGenerator.GenerateUniqueCodeAsync(this);
                            idProperty.SetValue(entry.Entity, generatedId);
                        }
                    }
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Tables

            modelBuilder.Entity<Amenity>()
                .ToTable("Amenities");

            modelBuilder.Entity<Favorite>()
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
            modelBuilder.Entity<Property>().Property(p => p.Price).HasColumnType("decimal(5, 2)");
            #endregion

            #region Primary keys

            modelBuilder.Entity<Amenity>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Favorite>()
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
            .HasOne<TypeProperty>(p => p.TypeProperty)
            .WithMany(tp => tp.Properties)
            .HasForeignKey(p => p.TypePropertyId);

            modelBuilder.Entity<Property>()
                .HasOne<Agent>(p => p.Agent)
                .WithMany(a => a.Properties)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Property>()
                        .HasOne<TypeSale>(p => p.TypeSale)
                        .WithMany(ts => ts.Properties)
                        .HasForeignKey(p => p.PropertyTypeSaleId);

            modelBuilder.Entity<Favorite>()
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
