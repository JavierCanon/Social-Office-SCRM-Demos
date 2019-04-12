using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace DataContext {
    public partial class SalesContext : ContextBase {
        public SalesContext()
            : base("SalesViewerConnectionString") {
            Configuration.AutoDetectChangesEnabled = false;
        }

        public virtual DbSet<Channels> Channels { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Plants> Plants { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Regions> Regions { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<Sectors> Sectors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Configurations.Add(new ChannelsMap());
            modelBuilder.Configurations.Add(new CitiesMap());
            modelBuilder.Configurations.Add(new ContactsMap());
            modelBuilder.Configurations.Add(new CustomersMap());
            modelBuilder.Configurations.Add(new PlantsMap());
            modelBuilder.Configurations.Add(new ProductsMap());
            modelBuilder.Configurations.Add(new RegionsMap());
            modelBuilder.Configurations.Add(new SectorsMap());
        }
    }

    public partial class Channels {
        public Channels() {
            Sales = new HashSet<Sales>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Sales> Sales { get; set; }
    }
    public partial class Cities {
        public Cities() {
            Contacts = new HashSet<Contacts>();
            Customers = new HashSet<Customers>();
            Plants = new HashSet<Plants>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Map { get; set; }
        public string Country { get; set; }
        public string State { get; set; }

        public virtual ICollection<Contacts> Contacts { get; set; }
        public virtual ICollection<Customers> Customers { get; set; }
        public virtual ICollection<Plants> Plants { get; set; }
    }
    public partial class Contacts {
        public Contacts() {
            Products = new HashSet<Products>();
            Products1 = new HashSet<Products>();
        }

        public int Id { get; set; }

        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public int? CityId { get; set; }
        public virtual Cities Cities { get; set; }
        public virtual ICollection<Products> Products { get; set; } // check in 16.2
        public virtual ICollection<Products> Products1 { get; set; } // check in 16.2
    }
    public partial class Customers {
        public Customers() {
            Sales = new HashSet<Sales>();
        }

        public int Id { get; set; }

        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public int CityId { get; set; }
        public virtual Cities Cities { get; set; }
        public virtual ICollection<Sales> Sales { get; set; }
    }
    public partial class Plants {
        public Plants() {
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public int CityId { get; set; }
        public virtual Cities Cities { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
    public partial class Products {
        public Products() {
            Sales = new HashSet<Sales>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double BaseCost { get; set; }
        public double ListPrice { get; set; }
        public int UnitsInInventory { get; set; }
        public int PlantId { get; set; }
        public int ProjectManagerId { get; set; }
        public int SupportManagerId { get; set; }
        public int UnitsInManufacturing { get; set; }
        public virtual Contacts ProjectManager { get; set; }
        public virtual Contacts SupportManager { get; set; }
        public virtual Plants Plants { get; set; }
        public virtual ICollection<Sales> Sales { get; set; }
    }
    public partial class Regions {
        public Regions() {
            Sales = new HashSet<Sales>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Sales> Sales { get; set; }
    }
    public partial class Sales {
        public int Id { get; set; }
        public int Units { get; set; }
        public double CostPerUnit { get; set; }
        public double Discount { get; set; }
        public double TotalCost { get; set; }
        public DateTime SaleDate { get; set; }
        public int ProductId { get; set; }
        public int RegionId { get; set; }
        public int ChannelId { get; set; }
        public int SectorId { get; set; }
        public int CustomerId { get; set; }
        public virtual Channels Channels { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual Products Products { get; set; }
        public virtual Regions Regions { get; set; }
        public virtual Sectors Sectors { get; set; }
    }
    public partial class Sectors {
        public Sectors() {
            Sales = new HashSet<Sales>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Sales> Sales { get; set; }
    }

    public partial class ChannelsMap : EntityTypeConfiguration<Channels> {
        public ChannelsMap() {
            this.HasKey(t => t.Id);
            this.Property(t => t.Id).ClearDatabaseGeneratedOption();
            this.Property(t => t.Name).IsRequired().HasMaxLength(4000);

            this.HasMany(e => e.Sales)
                .WithRequired(e => e.Channels)
                .HasForeignKey(e => e.ChannelId)
                .WillCascadeOnDelete(false);
        }
    }
    public partial class CitiesMap : EntityTypeConfiguration<Cities> {
        public CitiesMap() {
            this.HasKey(t => t.Id);
            this.Property(t => t.Id).ClearDatabaseGeneratedOption();
            this.Property(t => t.Name).IsRequired().HasMaxLength(4000);
            this.Property(t => t.Map).IsRequired().HasMaxLength(4000);
            this.Property(t => t.Country).IsRequired().HasMaxLength(4000);
            this.Property(t => t.State).IsRequired().HasMaxLength(4000);

            this.HasMany(e => e.Contacts)
                .WithOptional(e => e.Cities)
                .HasForeignKey(e => e.CityId);

            this.HasMany(e => e.Customers)
                .WithRequired(e => e.Cities)
                .HasForeignKey(e => e.CityId)
                .WillCascadeOnDelete(false);

            this.HasMany(e => e.Plants)
                .WithRequired(e => e.Cities)
                .HasForeignKey(e => e.CityId)
                .WillCascadeOnDelete(false);
        }
    }
    public partial class ContactsMap : EntityTypeConfiguration<Contacts> {
        public ContactsMap() {
            this.HasKey(t => t.Id);
            this.Property(t => t.Id).ClearDatabaseGeneratedOption();
            this.Property(t => t.FullName).IsRequired().HasMaxLength(4000);
            this.Property(t => t.Phone).HasMaxLength(4000);
            this.Property(t => t.Email).HasMaxLength(4000);
            this.Property(t => t.Address).HasMaxLength(4000);
            this.Property(t => t.Zip).HasMaxLength(4000);

            this.HasMany(e => e.Products)
                .WithRequired(e => e.ProjectManager)
                .HasForeignKey(e => e.ProjectManagerId)
                .WillCascadeOnDelete(false);

            this.HasMany(e => e.Products1)
                .WithRequired(e => e.SupportManager)
                .HasForeignKey(e => e.SupportManagerId)
                .WillCascadeOnDelete(false);
        }
    }
    public partial class CustomersMap : EntityTypeConfiguration<Customers> {
        public CustomersMap() {
            this.HasKey(t => t.Id);
            this.Property(t => t.Id).ClearDatabaseGeneratedOption();
            this.Property(t => t.FullName).IsRequired().HasMaxLength(4000);
            this.Property(t => t.Phone).HasMaxLength(4000);
            this.Property(t => t.Fax).HasMaxLength(4000);
            this.Property(t => t.Address).HasMaxLength(4000);
            this.Property(t => t.Zip).HasMaxLength(4000);

            this.HasMany(e => e.Sales)
                .WithRequired(e => e.Customers)
                .HasForeignKey(e => e.CustomerId)
                .WillCascadeOnDelete(false);
        }
    }
    public partial class PlantsMap : EntityTypeConfiguration<Plants> {
        public PlantsMap() {
            this.HasKey(t => t.Id);
            this.Property(t => t.Id).ClearDatabaseGeneratedOption();
            this.Property(t => t.Name).IsRequired().HasMaxLength(4000);
            this.Property(t => t.Address).IsRequired().HasMaxLength(4000);
            this.Property(t => t.Zip).IsRequired().HasMaxLength(4000);

            this.HasMany(e => e.Products)
                .WithRequired(e => e.Plants)
                .HasForeignKey(e => e.PlantId)
                .WillCascadeOnDelete(false);
        }
    }
    public partial class ProductsMap : EntityTypeConfiguration<Products> {
        public ProductsMap() {
            this.HasKey(t => t.Id);
            this.Property(t => t.Id).ClearDatabaseGeneratedOption();
            this.Property(t => t.Name).IsRequired().HasMaxLength(4000);
            this.Property(t => t.Description).IsRequired().HasMaxLength(4000);

            this.HasMany(e => e.Sales)
                .WithRequired(e => e.Products)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);
        }
    }
    public partial class RegionsMap : EntityTypeConfiguration<Regions> {
        public RegionsMap() {
            this.HasKey(t => t.Id);
            this.Property(t => t.Id).ClearDatabaseGeneratedOption();
            this.Property(t => t.Name).IsRequired().HasMaxLength(4000);

            this.HasMany(e => e.Sales)
                .WithRequired(e => e.Regions)
                .HasForeignKey(e => e.RegionId)
                .WillCascadeOnDelete(false);
        }
    }
    public partial class SalesMap : EntityTypeConfiguration<Sales> {
        public SalesMap() {
            this.HasKey(t => t.Id);
            this.Property(t => t.Id).ClearDatabaseGeneratedOption();
        }
    }
    public partial class SectorsMap : EntityTypeConfiguration<Sectors> {
        public SectorsMap() {
            this.HasKey(t => t.Id);

            this.Property(t => t.Id).ClearDatabaseGeneratedOption();
            this.Property(t => t.Name).IsRequired().HasMaxLength(4000);

            this.HasMany(e => e.Sales)
                .WithRequired(e => e.Sectors)
                .HasForeignKey(e => e.SectorId)
                .WillCascadeOnDelete(false);
        }
    }

    public static class SalesContextExtensions {
        const string DatabaseGeneratedOptionMethodName = "HasDatabaseGeneratedOption";
        const string DatabaseGeneratedOptionParameterName = "DatabaseGeneratedOption";

        public static StringPropertyConfiguration ClearDatabaseGeneratedOption(this StringPropertyConfiguration instance) {
            return ClearDatabaseGeneratedOptionCore(instance);
        }
        public static PrimitivePropertyConfiguration ClearDatabaseGeneratedOption(this PrimitivePropertyConfiguration instance) {
            return ClearDatabaseGeneratedOptionCore(instance);
        }
        static T ClearDatabaseGeneratedOptionCore<T>(T instance) where T : class {
            var method = instance.GetType().GetMethod(DatabaseGeneratedOptionMethodName);
            if(method != null) {
                var dbGeneratedOptionType = method.DeclaringType.Assembly.GetTypes().FirstOrDefault(at => at.Name == DatabaseGeneratedOptionParameterName);
                if(dbGeneratedOptionType != null)
                    return method.Invoke(instance, new object[] { Activator.CreateInstance(dbGeneratedOptionType) }) as T;
            }
            return null;
        }
    }
}
