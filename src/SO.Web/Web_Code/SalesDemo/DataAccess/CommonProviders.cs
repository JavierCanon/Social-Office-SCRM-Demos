using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Caching;
using SalesDataContext = DataContext.SalesContext;

namespace DataAccess {
    public abstract class BaseProvider<T> : IDisposable where T : class {
        public DbSet<T> DataTable { get { return DataContext.Set<T>(); } }
        protected SalesDataContext DataContext { get; private set; }
        private Cache Cache { get { return System.Web.HttpContext.Current.Cache; } }

        public BaseProvider() {
            DataContext = new SalesDataContext();
        }

        public void Dispose() {
            if(DataContext != null)
                DataContext.Dispose();
        }

        protected TResult TryGetResult<TResult>(Func<TResult> func, bool useCache = false, object keySuffix = null, bool nonExpireCache = false) {
            TResult result = default(TResult);
            if(useCache) {
                string key = string.Format("{0}.{1}.{2}", this.GetType().Name, func.Method.Name, keySuffix ?? "");
                if(Cache[key] != null)
                    result = (TResult)Cache[key];
                else {
                    result = func();
                    DateTime expirationDate = nonExpireCache ? DateTime.MaxValue : DateTimeHelper.GetIntervalEndDate(DateTime.Now, SelectionInterval.Day);
                    Cache.Insert(key, result, null, expirationDate, TimeSpan.Zero);
                }
            } else
                result = func();
            return result;
        }
    }

    public class CustomersProvider : BaseProvider<DataContext.Customers> {
        public IEnumerable<Customer> GetList() {
            return TryGetResult<IEnumerable<Customer>>(() => {
                return (from c in DataTable
                        select new Customer {
                            Id = c.Id,
                            Name = c.FullName,
                            Address = c.Address,
                            City = c.Cities.Name,
                            Fax = c.Fax,
                            PostalCode = c.Zip,
                            State = c.Cities.State,
                            Phone = c.Phone
                        }).OrderBy(c => c.Name).ToList();
            }, useCache: true, nonExpireCache: true);
        }
        public Location GetCustomerLocation(int customerId) {
            return TryGetResult<Location>(() => {
                return (from c in DataTable
                        where c.Id == customerId
                        select new Location() {
                            Latitude = c.Cities.Latitude,
                            Longitude = c.Cities.Longitude
                        }).FirstOrDefault();
            }, useCache: true, keySuffix: customerId, nonExpireCache: true);
        }
    }
    public class ProductsProvider : BaseProvider<DataContext.Products> {
        public IEnumerable<Product> GetList() {
            return TryGetResult<IEnumerable<Product>>(() => {
                return (from p in DataTable
                        select new Product {
                            Id = p.Id,
                            Name = p.Name,
                            BaseCost = p.BaseCost,
                            Description = p.Description,
                            ListPrice = p.ListPrice,
                            UnitsInInventory = p.UnitsInInventory,
                            UnitsInManufacturing = p.UnitsInManufacturing
                        }).OrderBy(p => p.Name).ToList();
            }, useCache: true, nonExpireCache: true);
        }
        public Contact GetProjectManager(int productId) {
            return TryGetResult(() => {
                return DataTable.Where(p => p.Id == productId).Select(c => new Contact() {
                    Address = c.ProjectManager.Address,
                    City = c.ProjectManager.Cities.Name,
                    Email = c.ProjectManager.Email,
                    FullName = c.ProjectManager.FullName,
                    State = c.ProjectManager.Cities.State,
                    Zip = c.ProjectManager.Zip,
                    Phone = c.ProjectManager.Phone
                }).FirstOrDefault();
            }, useCache: true, keySuffix: productId, nonExpireCache: true);
        }
        public Contact GetSupportManager(int productId) {
            return TryGetResult<Contact>(() => {
                return DataTable.Where(p => p.Id == productId).Select(c => new Contact() {
                    Address = c.SupportManager.Address,
                    City = c.SupportManager.Cities.Name,
                    Email = c.SupportManager.Email,
                    FullName = c.SupportManager.FullName,
                    State = c.SupportManager.Cities.State,
                    Zip = c.SupportManager.Zip,
                    Phone = c.SupportManager.Phone
                }).FirstOrDefault();
            }, useCache: true, keySuffix: productId, nonExpireCache: true);
        }
        public Plant GetPlant(int productId) {
            return TryGetResult<Plant>(() => {
                return DataTable.Where(x => x.Id == productId).Select(x => new Plant() {
                    Address = x.Plants.Address,
                    City = x.Plants.Cities.Name,
                    Name = x.Plants.Name,
                    State = x.Plants.Cities.State,
                    Zip = x.Plants.Zip
                }).FirstOrDefault();
            }, useCache: true, keySuffix: productId, nonExpireCache: true);
        }
    }
    #region Data Transfer Objects
    public class Customer {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
    }

    public class Product {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double BaseCost { get; set; }
        public double ListPrice { get; set; }
        public int UnitsInInventory { get; set; }
        public int UnitsInManufacturing { get; set; }
    }
    public class Contact {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
    public class Plant {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
    public class Location {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    #endregion
}
