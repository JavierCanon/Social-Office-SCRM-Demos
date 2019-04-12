using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext {
    public class DataGenerator {
        // Data generation code is taken from DevExtreme SalesViewer demo
        public List<Sales> Sales { get { return GetData<Sales>(() => CreateSales()); } }
        public List<Products> Products { get { return GetData<Products>(() => CreateProducts()); } }
        public List<Plants> Plants { get { return GetData<Plants>(() => CreatePlants()); } }
        public List<Customers> Customers { get { return GetData<Customers>(() => CreateCustomers()); } }
        public List<Contacts> Contacts { get { return GetData<Contacts>(() => CreateContacts()); } }
        public List<Sectors> Sectors { get { return GetData<Sectors>(() => CreateSectors()); } }
        public List<Channels> Channels { get { return GetData<Channels>(() => CreateChannels()); } }
        public List<Regions> Regions { get { return GetData<Regions>(() => CreateRegions()); } }
        public List<Cities> Cities { get { return GetData<Cities>(() => CreateCities()); } }

        private Dictionary<string, object> DataDictionary = new Dictionary<string, object>();

        private List<T> GetData<T>(Func<List<T>> populateFunction) {
            string key = typeof(T).Name;
            if(!DataDictionary.ContainsKey(key))
                DataDictionary.Add(key, populateFunction());
            return (List<T>)DataDictionary[key];
        }

        private List<Sales> CreateSales() {
            int[] regionCityCount = new int[] { 15, 9, 10, 10, 7, 10 };
            int companiesCount = 22;

            var today = DateTime.Today;
            var startDate = new DateTime(today.AddYears(-2).Year, 1, 1, 8, 0, 0);
            var endDate = new DateTime(today.Year, 12, 31, 17, 0, 0);
            var curDate = startDate;
            List<Sales> result = new List<Sales>();
            var rnd = new Random(Environment.TickCount);

            var sectorsWeights = new int[] { 5, 3, 3, 2, 1, 1 };
            var productsWeights = new int[] { 12, 20, 17, 23, 15, 13 };
            var regionsWeights = new int[] { 12, 2, 7, 4, 5, 1 };
            var channelsWeights = new int[] { 5, 2, 2, 3, 4 };
            var citiesWeights = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 6, 9, 8, 7, 6 };
            var companiesWeights = new int[] { 5, 3, 2, 4, 5, 3, 2, 4, 5, 3, 2, 4, 5, 3, 2, 4, 5, 3, 2, 4, 5, 3 };

            var minSalesCurveParams1 = InitSaleCurveFunction(2, 4, 5, 0.7);
            var maxSalesCurveParams1 = InitSaleCurveFunction(6, 8, 11, 0.7);

            var minSalesCurveParams2 = InitSaleCurveFunction(3, 6, 7, 0.7);
            var maxSalesCurveParams2 = InitSaleCurveFunction(6, 9, 12, 0.7);

            var minSalesCurveParams3 = InitSaleCurveFunction(2, 5, 6, 0.8);
            var maxSalesCurveParams3 = InitSaleCurveFunction(5, 9, 13, 0.8);

            while(curDate <= endDate) {
                if(curDate.Hour < 8 || curDate.Hour > 17 || (curDate.Hour == 17 && curDate.Minute != 0)) {
                    curDate = curDate.AddMinutes(15);
                    continue;
                }
                var minSalesCurveParams = (curDate.Day < 10)
                                              ? minSalesCurveParams1
                                              : ((curDate.Day < 20) ? minSalesCurveParams2 : minSalesCurveParams3);
                var maxSalesCurveParams = (curDate.Day < 10)
                                              ? maxSalesCurveParams1
                                              : ((curDate.Day < 20) ? maxSalesCurveParams2 : maxSalesCurveParams3);
                var minSalesCount = CalculateSalesCount(minSalesCurveParams, curDate);
                var maxSalesCount = CalculateSalesCount(maxSalesCurveParams, curDate);

                var salesCount = rnd.Next(minSalesCount, maxSalesCount);
                for(var i = 0; i < salesCount; i++) {
                    var prodId = GetRankRandom(rnd, getWeightsFromMonth(curDate.Month % 3, 6));
                    int regionId = GetRankRandom(rnd, regionsWeights);
                    int cityId = GetRankRandom(rnd, citiesWeights.Take(regionCityCount[regionId]).ToArray());
                    int companyIdentifier = GetRankRandom(rnd, companiesWeights.Take(companiesCount).ToArray()) + 1;
                    int channelId = GetRankRandom(rnd, getWeightsFromMonth(curDate.Month, 5));
                    int sectorId = GetRankRandom(rnd, getWeightsFromMonth(curDate.Month + 2, 6));
                    Sales sale = new Sales {
                        SaleDate = curDate,
                        RegionId = regionId + 1,
                        ChannelId = channelId + 1,
                        SectorId = sectorId + 1,
                        Units = rnd.Next(1, 5),
                        Discount = rnd.Next(5, 75) * 10,
                        ProductId = prodId + 1,
                        CustomerId = companyIdentifier
                    };
                    sale.TotalCost = sale.Units * Products[prodId].ListPrice - sale.Discount;

                    result.Add(sale);
                }
                curDate = curDate.AddMinutes(15);
            }
            return result;
        }
        private int CalculateSalesCount(IList<double> parameters, DateTime x) {
            var xx = (double)(x.Hour * 60 + x.Minute - 480) / 540.0;
            return (int)Math.Round(parameters[0] * Math.Pow(xx, 6) + parameters[1] * Math.Pow(xx, 4) + parameters[2] * Math.Pow(xx, 2) + parameters[3]);
        }

        private double[] InitSaleCurveFunction(int a, int b, int c, double p) {
            double t;
            var r1 = new int[] { 1, 1, 1, b - a };

            double s = t = p * p;
            var r2 = new double[] { t, t * s, t * s * s, c - a };
            t = p;
            var r3 = new double[] { 2 * t, 4 * (t * s), 6 * (t * s * s), 0 };

            t = r2[0] / r1[0];
            r2 = new double[] { 0, r2[1] - t * r1[1], r2[2] - t * r1[2], r2[3] - t * r1[3] };
            t = r3[0] / r1[0];
            r3 = new double[] { 0, r3[1] - t * r1[1], r3[2] - t * r1[2], r3[3] - t * r1[3] };
            t = r3[1] / r2[1];
            r3 = new double[] { 0, 0, r3[2] - t * r2[2], r3[3] - t * r2[2] };

            var res = new double[4] { r3[3] / r3[2], 0, 0, 0 };
            res[1] = (r2[3] - r2[2] * res[0]) / r2[1];
            res[2] = (r1[3] - r1[2] * res[0] - r1[1] * res[1]) / r1[0];
            res[3] = a;

            return res;
        }

        private int GetRankRandom(Random rnd, int[] weights) {
            var coords = new int[weights.Length];
            var summ = 0;
            for(var i = 0; i < weights.Length; i++) {
                summ += weights[i];
                coords[i] = summ;
            }

            var next = rnd.Next(summ);

            for(var i = 0; i < coords.Length; i++) {
                if(next < coords[i])
                    return i;
            }
            throw new InvalidOperationException();
        }

        private int[] getWeightsFromMonth(int monthOfYear, int weightsNumber) {
            int[] weights = new int[weightsNumber];
            for(int i = 0; i < weightsNumber; i++) {
                weights[i] = (int)Math.Pow(((Math.Sin(monthOfYear / 2 - i / 3.14)) * 7), 2) + 1;
            }
            return weights;
        }


        private List<Products> CreateProducts() {
            var result = new List<Products>();
            result.Add(new Products() {
                Id = 1, Name = "Eco Max", Description = "Low pollution industrial air cleaner",
                BaseCost = 2400, ListPrice = 2500, UnitsInInventory = 289, PlantId = 1, ProjectManagerId = 1, SupportManagerId = 2,
                UnitsInManufacturing = 11
            });
            result.Add(new Products() {
                Id = 2, Name = "Eco Supreme", Description = "High-output air cleaner powered by solar",
                BaseCost = 1850, ListPrice = 2000, UnitsInInventory = -14, PlantId = 2, ProjectManagerId = 3, SupportManagerId = 4,
                UnitsInManufacturing = 54
            });
            result.Add(new Products() {
                Id = 3, Name = "EnviroCare", Description = "Consumer air filtration and vent system",
                BaseCost = 1600, ListPrice = 1750, UnitsInInventory = 50, PlantId = 3, ProjectManagerId = 5, SupportManagerId = 6,
                UnitsInManufacturing = 40
            });
            result.Add(new Products() {
                Id = 4, Name = "EnviroCare Max", Description = "Industrial environmental control system",
                BaseCost = 2700, ListPrice = 2800, UnitsInInventory = 150, PlantId = 4, ProjectManagerId = 7, SupportManagerId = 8,
                UnitsInManufacturing = 13
            });
            result.Add(new Products() {
                Id = 5, Name = "SolarOne", Description = "Environmentally friendly power generation",
                BaseCost = 1350, ListPrice = 1500, UnitsInInventory = 100, PlantId = 5, ProjectManagerId = 9, SupportManagerId = 10,
                UnitsInManufacturing = 14
            });
            result.Add(new Products() {
                Id = 6, Name = "SolarMax", Description = "Consumer targeted solar power cell",
                BaseCost = 1150, ListPrice = 2250, UnitsInInventory = 67, PlantId = 6, ProjectManagerId = 11, SupportManagerId = 13,
                UnitsInManufacturing = 6
            });
            return result;

        }

        private List<Plants> CreatePlants() {
            List<Plants> result = new List<Plants>();
            result.Add(new Plants() { Id = 1, CityId = 66, Name = "Harrisburg", Zip = "00098", Address = "188 Market Street" });
            result.Add(new Plants() { Id = 2, CityId = 67, Name = "Las Vegas", Zip = "89120", Address = "1445 Las Vegas Blvd" });
            result.Add(new Plants() { Id = 3, CityId = 2, Name = "Los Angeles", Zip = "90020", Address = "8247 Sunset Blvd" });
            result.Add(new Plants() { Id = 4, CityId = 68, Name = "San Francisco", Zip = "90311", Address = "4242 Hill Street" });
            result.Add(new Plants() { Id = 5, CityId = 1, Name = "New York", Zip = "00092", Address = "628 Broadway" });
            result.Add(new Plants() { Id = 6, CityId = 65, Name = "Atlanta", Zip = "20092", Address = "289 Olympic Road" });
            return result;
        }

        private List<Customers> CreateCustomers() {
            List<Customers> result = new List<Customers>();
            result.Add(new Customers { Id = 1, FullName = "Johnson & Assoc", Address = "2379 Broadway", CityId = 1, Zip = "10024", Fax = "(718) 917-6839", Phone = "(718) 917-6839" });
            result.Add(new Customers { Id = 2, FullName = "McCord Builders", Address = "10756 Ashton Ave", CityId = 2, Zip = "90024", Fax = "(410) 418-9108", Phone = "(410) 418-9108" });
            result.Add(new Customers { Id = 3, FullName = "Building Management Inc", Address = "107 S State St", CityId = 3, Zip = "60603", Fax = "(773) 260-8071", Phone = "(773) 260-8071" });
            result.Add(new Customers { Id = 4, FullName = "System Integrators", Address = "5215 W 34th St", CityId = 4, Zip = "77092", Fax = "(707) 220-9114", Phone = "(707) 220-9114" });
            result.Add(new Customers { Id = 5, FullName = "Discovery Systems", Address = "2620 Dean Rd", CityId = 5, Zip = "32216", Fax = "(847) 371-1555", Phone = "(847) 371-1555" });
            result.Add(new Customers { Id = 6, FullName = "Arthur & Sons", Address = "2331 Lewis Ave", CityId = 6, Zip = "59101", Fax = "(561) 843-3972", Phone = "(561) 843-3972" });
            result.Add(new Customers { Id = 7, FullName = "Smith & Co", Address = "1823 SW Spring St", CityId = 7, Zip = "97201", Fax = "(973) 312-3977", Phone = "(973) 312-3977" });
            result.Add(new Customers { Id = 8, FullName = "Beacon Systems", Address = "1435 Wazee St #102", CityId = 8, Zip = "80202", Fax = "(567) 644-4728", Phone = "(567) 644-4728" });
            result.Add(new Customers { Id = 9, FullName = "Gemini Stores", Address = "1401 S May Ave", CityId = 9, Zip = "73108", Fax = "(817) 308-0853", Phone = "(817) 308-0853" });
            result.Add(new Customers { Id = 10, FullName = "Columbia Solar", Address = "1933 Shearwood Forest Drive", CityId = 10, Zip = "95126", Fax = "(860) 205-1690", Phone = "(860) 205-1691" });
            result.Add(new Customers { Id = 11, FullName = "Renewable Supplies", Address = "484 Broome St", CityId = 1, Zip = "10002", Fax = "(804) 615-5863", Phone = "(804) 615-5863" });
            result.Add(new Customers { Id = 12, FullName = "Supply Warehous", Address = "2239 W 29th Pl", CityId = 2, Zip = "90018", Fax = "(315) 924-8962", Phone = "(315) 924-8962" });
            result.Add(new Customers { Id = 13, FullName = "Get Solar Inc", Address = "500 W Madison St", CityId = 3, Zip = "60606", Fax = "(954) 718-3876", Phone = "(954) 718-3876" });
            result.Add(new Customers { Id = 14, FullName = "Solar Warehouse", Address = "1510 Polk St", CityId = 4, Zip = "77002", Fax = "(775) 202-0360", Phone = "(775) 202-0360" });
            result.Add(new Customers { Id = 15, FullName = "Green Energy Inc", Address = "6045 Greenland Rd", CityId = 5, Zip = "32256", Fax = "(541) 286-0792", Phone = "(541) 286-0792" });
            result.Add(new Customers { Id = 16, FullName = "Energy Systems", Address = "PO Box 31156", CityId = 6, Zip = "59116", Fax = "(724) 255-0997", Phone = "(724) 255-0997" });
            result.Add(new Customers { Id = 17, FullName = "Apollo Inc", Address = "1221 SW Yamhill St", CityId = 7, Zip = "97204", Fax = "(804) 264-8719", Phone = "(804) 264-8713" });
            result.Add(new Customers { Id = 18, FullName = "Mercury Solar", Address = "851 S Santa Fe Ave", CityId = 8, Zip = "80292", Fax = "(731) 451-8686", Phone = "(731) 451-8686" });
            result.Add(new Customers { Id = 19, FullName = "Global Services", Address = "8300 Greystone Ave", CityId = 9, Zip = "73116", Fax = "(916) 992-6492", Phone = "(916) 992-6492" });
            result.Add(new Customers { Id = 20, FullName = "Market Eco", Address = "1602 The Alameda # 1013", CityId = 10, Zip = "95126", Fax = "(619) 946-9413", Phone = "(619) 946-9413" });
            result.Add(new Customers { Id = 21, FullName = "Environment Solar", Address = "185 Columbus Ave", CityId = 1, Zip = "10023", Fax = "(601) 534-6561", Phone = "(601) 534-6561" });
            result.Add(new Customers { Id = 22, FullName = "Health Plus Inc", Address = "8001 Sunset Blvd", CityId = 2, Zip = "90046", Fax = "(603) 214-8741", Phone = "(603) 214-8741" });
            return result;
        }

        private List<Contacts> CreateContacts() {
            List<Contacts> result = new List<Contacts>();
            result.Add(new Contacts() { Id = 1, FullName = "John Smith", CityId = 66, Address = "188 Market Street", Zip = "00098", Phone = "(212) 555-2321", Email = "jsmith@harrisburg.com" });
            result.Add(new Contacts() { Id = 2, FullName = "Jeff Hall", Email = "jeffh@harrisburg.com" });
            result.Add(new Contacts() { Id = 3, FullName = "Kent Hardy", CityId = 67, Address = "1445 Las Vegas Blvd", Zip = "89120", Phone = "(702) 555-2321", Email = "khardy@lasvegas.com" });
            result.Add(new Contacts() { Id = 4, FullName = "Brent Moffat", Email = "brentf@lasvegas.com" });
            result.Add(new Contacts() { Id = 5, FullName = "Jenny James", CityId = 2, Address = "8247 Sunset Blvd", Zip = "90020", Phone = "(415) 555-2321", Email = "jennyj@losangeles.com" });
            result.Add(new Contacts() { Id = 6, FullName = "Carter Smith", Email = "carterc@losangeles.com" });
            result.Add(new Contacts() { Id = 7, FullName = "Dirk Porter", Address = "4242 Hill Street", CityId = 68, Zip = "90311", Phone = "(415) 555-2321", Email = "dirkp@sanfran.com" });
            result.Add(new Contacts() { Id = 8, FullName = "Angela Vega", Email = "angelav@sanfran.com" });
            result.Add(new Contacts() { Id = 9, FullName = "Paul Heart", CityId = 1, Address = "628 Broadway", Zip = "00092", Phone = "(212) 555-2321", Email = "paulh@nyc.com" });
            result.Add(new Contacts() { Id = 10, FullName = "Karen McCarthy", Email = "karenm@nyc.com" });
            result.Add(new Contacts() { Id = 11, FullName = "Mike Arthur", CityId = 65, Address = "289 Olympic Road", Zip = "20092", Phone = "(404) 555-2321", Email = "mikea@atl.com" });
            result.Add(new Contacts() { Id = 13, FullName = "Robert Jones", Email = "robertj@atl.com" });
            return result;
        }

        private List<Sectors> CreateSectors() {
            List<Sectors> result = new List<Sectors>();
            result.Add(new Sectors() { Id = 1, Name = "Energy" });
            result.Add(new Sectors() { Id = 2, Name = "Manufacturing" });
            result.Add(new Sectors() { Id = 3, Name = "Telecom" });
            result.Add(new Sectors() { Id = 4, Name = "Insurance" });
            result.Add(new Sectors() { Id = 5, Name = "Banking" });
            result.Add(new Sectors() { Id = 6, Name = "Health" });
            return result;
        }

        private List<Channels> CreateChannels() {
            List<Channels> result = new List<Channels>();
            result.Add(new Channels() { Id = 1, Name = "Direct" });
            result.Add(new Channels() { Id = 2, Name = "VARs" });
            result.Add(new Channels() { Id = 3, Name = "Consultants" });
            result.Add(new Channels() { Id = 4, Name = "Resellers" });
            result.Add(new Channels() { Id = 5, Name = "Retail" });
            return result;
        }

        private List<Regions> CreateRegions() {
            List<Regions> result = new List<Regions>();
            result.Add(new Regions() { Id = 1, Name = "North America" });
            result.Add(new Regions() { Id = 2, Name = "South America" });
            result.Add(new Regions() { Id = 3, Name = "Europe" });
            result.Add(new Regions() { Id = 4, Name = "Asia" });
            result.Add(new Regions() { Id = 5, Name = "Australia" });
            result.Add(new Regions() { Id = 6, Name = "Africa" });
            return result;
        }

        private List<Cities> CreateCities() {
            List<Cities> result = new List<Cities>();
            result.Add(new Cities() { Id = 1, Name = "New York", Latitude = 40.66, Longitude = -74.03, Country = "United States", State = "NY", Map = "usa" });
            result.Add(new Cities() { Id = 2, Name = "Los Angeles", Latitude = 34.02, Longitude = -118.41, Country = "United States", State = "CA", Map = "usa" });
            result.Add(new Cities() { Id = 3, Name = "Chicago", Latitude = 41.83, Longitude = -87.73, Country = "United States", State = "IL", Map = "usa" });
            result.Add(new Cities() { Id = 4, Name = "Houston", Latitude = 29.78, Longitude = -95.38, Country = "United States", State = "TX", Map = "usa" });
            result.Add(new Cities() { Id = 5, Name = "Jacksonville", Latitude = 30.337, Longitude = -81.66, Country = "United States", State = "FL", Map = "usa" });
            result.Add(new Cities() { Id = 6, Name = "Billings", Latitude = 45.78, Longitude = -108.56, Country = "United States", State = "MT", Map = "usa" });
            result.Add(new Cities() { Id = 7, Name = "Portland", Latitude = 45.54, Longitude = -122.65, Country = "United States", State = "OR", Map = "usa" });
            result.Add(new Cities() { Id = 8, Name = "Denver", Latitude = 39.76, Longitude = -104.85, Country = "United States", State = "CO", Map = "usa" });
            result.Add(new Cities() { Id = 9, Name = "Oklahoma Cities", Latitude = 35.48, Longitude = -97.47, Country = "United States", State = "OK", Map = "usa" });
            result.Add(new Cities() { Id = 10, Name = "San Jose", Latitude = 37.29, Longitude = -121.81, Country = "United States", State = "CA", Map = "usa" });
            result.Add(new Cities() { Id = 11, Name = "Ottawa", Latitude = 45.6, Longitude = -75.7, Country = "Canada", State = "Ontario", Map = "canada" });
            result.Add(new Cities() { Id = 12, Name = "Winnipeg", Latitude = 49.88, Longitude = -97.14, Country = "Canada", State = "Manitoba", Map = "canada" });
            result.Add(new Cities() { Id = 13, Name = "Edmonton", Latitude = 53.54, Longitude = -113.49, Country = "Canada", State = "Alberta", Map = "canada" });
            result.Add(new Cities() { Id = 14, Name = "Vancouver", Latitude = 49.25, Longitude = -123, Country = "Canada", State = "British Columbia", Map = "canada" });
            result.Add(new Cities() { Id = 15, Name = "Halifax", Latitude = 44.69, Longitude = -63.56, Country = "Canada", State = "Nova Scotia", Map = "canada" });
            result.Add(new Cities() { Id = 16, Name = "Rio de Janeiro", Latitude = -22.91, Longitude = -43.44, Country = "Brazil", State = "", Map = "" });
            result.Add(new Cities() { Id = 17, Name = "Buenos Aires", Latitude = -34.61, Longitude = -58.43, Country = "Argentina", State = "", Map = "" });
            result.Add(new Cities() { Id = 18, Name = "Manaus", Latitude = -3.1, Longitude = -60.02, Country = "Brazil", State = "", Map = "" });
            result.Add(new Cities() { Id = 19, Name = "Asuncion", Latitude = -25.27, Longitude = -57.62, Country = "Paraguay", State = "", Map = "" });
            result.Add(new Cities() { Id = 20, Name = "La Paz", Latitude = -16.5, Longitude = -68.14, Country = "Bolivia", State = "", Map = "" });
            result.Add(new Cities() { Id = 21, Name = "Quito", Latitude = -0.21, Longitude = -78.51, Country = "Ecuador", State = "", Map = "" });
            result.Add(new Cities() { Id = 22, Name = "Lima", Latitude = -12.05, Longitude = -77.07, Country = "Peru", State = "", Map = "" });
            result.Add(new Cities() { Id = 23, Name = "Santiago", Latitude = -33.45, Longitude = -70.61, Country = "Chile", State = "", Map = "" });
            result.Add(new Cities() { Id = 24, Name = "Bogota", Latitude = 4.59, Longitude = -74.08, Country = "Colombia", State = "", Map = "" });
            result.Add(new Cities() { Id = 25, Name = "London", Latitude = 51.5, Longitude = -0.12, Country = "United Kingdom", State = "", Map = "europe" });
            result.Add(new Cities() { Id = 26, Name = "Berlin", Latitude = 52.52, Longitude = 13.41, Country = "Germany", State = "", Map = "europe" });
            result.Add(new Cities() { Id = 27, Name = "MadrId", Latitude = 40.41, Longitude = -3.7, Country = "Spain", State = "", Map = "europe" });
            result.Add(new Cities() { Id = 28, Name = "Rome", Latitude = 41.91, Longitude = 12.53, Country = "Italy", State = "", Map = "europe" });
            result.Add(new Cities() { Id = 29, Name = "Lyon", Latitude = 45.75, Longitude = 4.83, Country = "France", State = "", Map = "europe" });
            result.Add(new Cities() { Id = 30, Name = "Vienna", Latitude = 48.2, Longitude = 16.37, Country = "Austria", State = "", Map = "europe" });
            result.Add(new Cities() { Id = 31, Name = "Sofia", Latitude = 42.69, Longitude = 23.32, Country = "Bulgaria", State = "", Map = "europe" });
            result.Add(new Cities() { Id = 32, Name = "Athens", Latitude = 37.97, Longitude = 23.71, Country = "Greece", State = "", Map = "europe" });
            result.Add(new Cities() { Id = 33, Name = "Moscow", Latitude = 55.75, Longitude = 37.61, Country = "Russia", State = "", Map = "europe" });
            result.Add(new Cities() { Id = 34, Name = "Oslo", Latitude = 59.91, Longitude = 10.73, Country = "Norway", State = "", Map = "europe" });
            result.Add(new Cities() { Id = 35, Name = "Tokio", Latitude = 35.673343, Longitude = 139.710388, Country = "Japan", State = "", Map = "eurasia" });
            result.Add(new Cities() { Id = 36, Name = "Beijing", Latitude = 39.9, Longitude = 116.39, Country = "China", State = "", Map = "eurasia" });
            result.Add(new Cities() { Id = 37, Name = "Jerusalem", Latitude = 31.78, Longitude = 35.2, Country = "Israel", State = "", Map = "eurasia" });
            result.Add(new Cities() { Id = 38, Name = "Singapore", Latitude = 1.28, Longitude = 103.85, Country = "Malaysia", State = "", Map = "eurasia" });
            result.Add(new Cities() { Id = 39, Name = "Seoul", Latitude = 37.51, Longitude = 126.98, Country = "South Korea", State = "", Map = "eurasia" });
            result.Add(new Cities() { Id = 40, Name = "Abu Dhabi", Latitude = 24.47, Longitude = 54.37, Country = "United Arab Emirates", State = "", Map = "eurasia" });
            result.Add(new Cities() { Id = 41, Name = "Sanaa", Latitude = 15.35, Longitude = 44.2, Country = "Yemen", State = "", Map = "eurasia" });
            result.Add(new Cities() { Id = 42, Name = "Kuwait Cities(){", Latitude = 29.37, Longitude = 47.98, Country = "Kuwait", State = "", Map = "eurasia" });
            result.Add(new Cities() { Id = 43, Name = "Dhaka", Latitude = 23.71, Longitude = 90.39, Country = "Bangladesh", State = "", Map = "eurasia" });
            result.Add(new Cities() { Id = 44, Name = "Ulan Bator", Latitude = 47.91, Longitude = 106.92, Country = "Mongolia", State = "", Map = "eurasia" });
            result.Add(new Cities() { Id = 45, Name = "Sydney", Latitude = -33.79, Longitude = 150.92, Country = "Australia", State = "", Map = "" });
            result.Add(new Cities() { Id = 46, Name = "Brisbane", Latitude = -27.47, Longitude = 153.02, Country = "Australia", State = "", Map = "" });
            result.Add(new Cities() { Id = 47, Name = "Perth", Latitude = -31.96, Longitude = 115.93, Country = "Australia", State = "", Map = "" });
            result.Add(new Cities() { Id = 48, Name = "Melbourne", Latitude = -37.86, Longitude = 145.08, Country = "Australia", State = "", Map = "" });
            result.Add(new Cities() { Id = 49, Name = "AdelaIde", Latitude = -34.92, Longitude = 138.58, Country = "Australia", State = "", Map = "" });
            result.Add(new Cities() { Id = 51, Name = "Hobart", Latitude = -42.88, Longitude = 147.32, Country = "Australia", State = "", Map = "" });
            result.Add(new Cities() { Id = 52, Name = "Darwin", Latitude = -12.59, Longitude = 131, Country = "Australia", State = "", Map = "" });
            result.Add(new Cities() { Id = 55, Name = "Pretoria", Latitude = -25.98, Longitude = 28.21, Country = "South Africa", State = "", Map = "africa" });
            result.Add(new Cities() { Id = 56, Name = "Cairo", Latitude = 30.05, Longitude = 31.26, Country = "Egypt", State = "", Map = "africa" });
            result.Add(new Cities() { Id = 57, Name = "Mogadishu", Latitude = 2.05, Longitude = 45.3, Country = "Somalia", State = "", Map = "africa" });
            result.Add(new Cities() { Id = 58, Name = "AbIdjan", Latitude = 5.34, Longitude = -3.97, Country = "Ivory Coast", State = "", Map = "africa" });
            result.Add(new Cities() { Id = 59, Name = "Casablanca", Latitude = 33.57, Longitude = -7.58, Country = "Morocco", State = "", Map = "africa" });
            result.Add(new Cities() { Id = 60, Name = "Cape Town", Latitude = -33.91, Longitude = 18.65, Country = "South Africa", State = "", Map = "africa" });
            result.Add(new Cities() { Id = 61, Name = "N’Djamena", Latitude = 12.12, Longitude = 15.05, Country = "Chad", State = "", Map = "africa" });
            result.Add(new Cities() { Id = 62, Name = "Nairobi", Latitude = -1.3, Longitude = 36.84, Country = "Kenya", State = "", Map = "africa" });
            result.Add(new Cities() { Id = 63, Name = "Kano", Latitude = 11.99, Longitude = 8.52, Country = "Nigeria", State = "", Map = "africa" });
            result.Add(new Cities() { Id = 64, Name = "Dakar", Latitude = 14.73, Longitude = -17.38, Country = "Senegal", State = "", Map = "africa" });
            result.Add(new Cities() { Id = 65, Name = "Atlanta", Latitude = 34.45, Longitude = -84.52, Country = "United States", State = "GA", Map = "usa" });
            result.Add(new Cities() { Id = 66, Name = "Harrisburg", Latitude = 40.16, Longitude = -76.52, Country = "United States", State = "PA", Map = "usa" });
            result.Add(new Cities() { Id = 67, Name = "Las Vegas", Latitude = 36.10, Longitude = -115.08, Country = "United States", State = "NV", Map = "usa" });
            result.Add(new Cities() { Id = 68, Name = "San Francisco", Latitude = 37.47, Longitude = -122.25, Country = "United States", State = "CA", Map = "usa" });
            return result;
        }
    }
}
