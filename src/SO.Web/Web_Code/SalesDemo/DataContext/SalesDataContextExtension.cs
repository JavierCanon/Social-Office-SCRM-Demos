using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;
using System.Web;
using DevExpress.Internal;

namespace DataContext {
    public partial class SalesContext {
        private const int BatchSize = 2500;

        private static int TotalDataRowsCount = 0;
        private static int InsertedDataRowsCount = 0;

        protected DataGenerator fDataGenerator = null;
        protected DataGenerator DataGenerator {
            get {
                if(fDataGenerator == null)
                    fDataGenerator = new DataGenerator();
                return fDataGenerator;
            }
        }

        public static bool IsDatabasePopulated() {
            using(var dataContext = new SalesContext())
                return dataContext.IsDatabaseTablesPopulated();
        }
        public static void PopulateDatabaseIfNecessary() {
            if(!IsDatabasePopulated())
                using(var dataContext = new SalesContext())
                    dataContext.PopulateDatabase();
        }
        public static int DatabasePopulatingProgressPercentValue { get; private set; }

        bool IsDatabaseTablesPopulated() {
            bool hasAnyData = Regions.Any() && Cities.Any() && Channels.Any() && Sectors.Any() && Contacts.Any() &&
                    Customers.Any() && Plants.Any() && Products.Any() && Sales.Any();
            bool hasActualData = Sales.Any(s => s.SaleDate.Year == DateTime.Today.Year);
            return hasAnyData && hasActualData;
        }
        void PopulateDatabase() {
            TotalDataRowsCount += DataGenerator.Regions.Count;
            TotalDataRowsCount += DataGenerator.Cities.Count;
            TotalDataRowsCount += DataGenerator.Channels.Count;
            TotalDataRowsCount += DataGenerator.Sectors.Count;
            TotalDataRowsCount += DataGenerator.Contacts.Count;
            TotalDataRowsCount += DataGenerator.Customers.Count;
            TotalDataRowsCount += DataGenerator.Plants.Count;
            TotalDataRowsCount += DataGenerator.Products.Count;
            TotalDataRowsCount += DataGenerator.Sales.Count;

            PurgeTable<Sales>();
            PurgeTable<Products>();
            PurgeTable<Plants>();
            PurgeTable<Customers>();
            PurgeTable<Contacts>();
            PurgeTable<Sectors>();
            PurgeTable<Channels>();
            PurgeTable<Cities>();
            PurgeTable<Regions>();

            PopulateTable(DataGenerator.Regions);
            PopulateTable(DataGenerator.Cities);
            PopulateTable(DataGenerator.Channels);
            PopulateTable(DataGenerator.Sectors);
            PopulateTable(DataGenerator.Contacts);
            PopulateTable(DataGenerator.Customers);
            PopulateTable(DataGenerator.Plants);
            PopulateTable(DataGenerator.Products);
            PopulateTable(DataGenerator.Sales);

            InsertedDataRowsCount = 0;
            TotalDataRowsCount = 0;
        }
        void PurgeTable<T>() where T : class {
            Database.ExecuteSqlCommand("DELETE FROM [" + typeof(T).Name + "]");
        }

        static void PopulateTable<T>(IEnumerable<T> data) where T : class {
            using(var context = new SalesContext()) {
                var table = context.Set<T>();
                int tableRowCount = table.Count();
                if(tableRowCount < data.Count())
                    BulkInsert(context, data.Skip(tableRowCount), table);
            }
        }
        static void BulkInsert<T>(SalesContext context, IEnumerable<T> data, DbSet<T> table) where T : class {
            var maxPage = (int)Math.Ceiling(data.Count() / (double)BatchSize);
            for(var page = 0; page < maxPage; page++) {
                var dataToInsert = data.Skip(page * BatchSize).Take(BatchSize);
                InsertedDataRowsCount += dataToInsert.Count();
                table.AddRange(dataToInsert);
                context.SaveChanges();
                // Progress
                DatabasePopulatingProgressPercentValue = (int)(InsertedDataRowsCount * 100 / TotalDataRowsCount);
            }
        }
    }
}
