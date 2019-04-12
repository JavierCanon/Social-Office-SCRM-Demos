// Copyright (c) 2019 Javier Cañon 
// https://www.javiercanon.com 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
using System;
using System.Data.Common;
using System.Data.OleDb;

namespace SO.Demos.SalesDBGenerator {
    public class SalesGenerator : SO.SalesDemo.Model.IDataGenerator {
        DateTime currentDate = DateTime.Today.AddDays(40);
        DateTime minRequiredDate = new DateTime(2015, 1, 1);
        int maxId = 0;
        public bool Run(string connectionString) {
            using(var connection = new OleDbConnection(connectionString)) {
                connection.Open();
                //new OleDbCommand("delete * from sales", connection).ExecuteNonQuery(); //FOR TEST PURPOSES
                var helper = new SqlHelper<OleDbConnection, OleDbCommand>();
                DateTime minDate = helper.GetDate(helper.ReadValue(connection, "select min(sale_date) from sales"));
                DateTime maxDate = helper.GetDate(helper.ReadValue(connection, "select max(sale_date) from sales"));
                this.maxId = helper.GetInt(helper.ReadValue(connection, "select max(id) from sales"));
                DateTime startDate = minRequiredDate;
                if(minDate > startDate && maxDate != DateTime.MinValue) {
                    startDate = maxDate.AddDays(1);
                }
                if(startDate > DateTime.Today.AddDays(2))
                    return true;
                double daysCount = currentDate.Subtract(startDate).TotalDays;
                RaiseStart();
                try { Generate(connection, startDate, (int)daysCount); }
                finally { RaiseComplete(); }
                connection.Close();
            }
            return true;
        }
        Random random = new Random(100);
        void Generate(OleDbConnection connection, DateTime startDate, int daysCount) {
            using(OleDbCommand command = new OleDbCommand()) {
                command.Connection = connection;
                command.CommandText = "insert into sales (id, units, cost_per_unit, discount, total_cost, sale_date, productId, RegionId, ChannelId, SectorId) values (@id, @units, @cost_per_unit, @discount, @total_cost, @sale_date, @productId, @RegionId, @ChannelId, @SectorId)";
                command.Parameters.AddRange(
                    new OleDbParameter[] {
                        new OleDbParameter("@id", OleDbType.Integer),
                        new OleDbParameter("@units", OleDbType.Integer),
                        new OleDbParameter("@cost_per_unit", OleDbType.Integer),
                        new OleDbParameter("@discount", OleDbType.Integer),
                        new OleDbParameter("@total_cost", OleDbType.Integer),
                        new OleDbParameter("@sale_date", OleDbType.Date),
                        new OleDbParameter("@productId", OleDbType.Integer),
                        new OleDbParameter("@RegionId", OleDbType.Integer),
                        new OleDbParameter("@ChannelId", OleDbType.Integer),
                        new OleDbParameter("@SectorId", OleDbType.Integer)
                    }
                );
                command.Prepare();
                for(int n = 0; n < daysCount; n++) {
                    Console.Write("{0} of {1}\r", n + 1, daysCount);
                    GenerateDay(command, startDate.AddDays(n));
                    RaiseProgress((double)n / (double)daysCount);
                }
            }
        }
        int timeInterval = 15;
        int startTime = 8;
        int endTime = 17;
        double dailySalesGrowth = 0.007d;
        void GenerateDay(DbCommand command, DateTime day) {
            double totalDays = day.Subtract(minRequiredDate).TotalDays;
            int salesPerDay = random.Next(180, (int)(200 * (1 + (totalDays * dailySalesGrowth) / 10)));
            int[] generateIntervals = GenerateTimeIntervals(salesPerDay);
            DateTime start = new DateTime(day.Year, day.Month, day.Day, startTime, 0, 0);
            for(int d = 0; d < generateIntervals.Length; d++) {
                for(int x = 0; x < generateIntervals[d]; x++)
                    GenerateSale(command, start);
                start = start.AddMinutes(timeInterval);
            }
        }
        void GenerateSale(DbCommand command, DateTime date) {
            int id = GetId();
            int region = regions[rndRegions[random.Next(0, rndRegions.Length)]].Id;
            int channel = channels[rndChannels[random.Next(0, rndChannels.Length)]].Id;
            int sector = sectors[rndSectors[random.Next(0, rndSectors.Length)]].Id;
            Product product = products[rndProducts[random.Next(0, rndProducts.Length)]];
            int productPrice = product.Price;
            int discount = rndDiscounts[random.Next(0, rndDiscounts.Length)];
            int units = rndUnits[random.Next(0, rndUnits.Length)];
            int totalPrice = (productPrice * units) - discount;

            command.Parameters["@id"].Value = id;
            command.Parameters["@units"].Value = units;
            command.Parameters["@cost_per_unit"].Value = productPrice;
            command.Parameters["@discount"].Value = discount;
            command.Parameters["@total_cost"].Value = totalPrice;
            command.Parameters["@sale_date"].Value = date;
            command.Parameters["@productId"].Value = product.Id;
            command.Parameters["@RegionId"].Value = region;
            command.Parameters["@ChannelId"].Value = channel;
            command.Parameters["@SectorId"].Value = sector;

            command.ExecuteNonQuery();
        }
        int GetId() { return ++maxId; }
        int[] GenerateTimeIntervals(int salesPerDay) {
            int count = ((endTime) - startTime) * (60 / timeInterval);
            int[] res = new int[count + 1];
            int salesPerInterval = salesPerDay / res.Length;
            for(int n = 10; ; n++) {
                int num = random.Next(salesPerInterval - (int)(salesPerInterval * 0.8), salesPerInterval + (int)(salesPerInterval * 0.3));
                if(num > salesPerDay) num = salesPerDay;
                salesPerDay -= num;
                res[n] += num;
                if(num < 1) break;
                if(n >= res.Length - 1) n = 0;
            }
            return res;
        }
        #region Progress
        public event SalesDemo.Model.ProgressEventHandler GenerationStart;
        public event SalesDemo.Model.ProgressEventHandler GenerationProgress;
        public event SalesDemo.Model.ProgressEventHandler GenerationComplete;
        void RaiseStart() {
            if(GenerationStart != null)
                GenerationStart(this, new SalesDemo.Model.ProgressEventArgs(0));
        }
        void RaiseProgress(double progress) {
            if(GenerationProgress != null)
                GenerationProgress(this, new SalesDemo.Model.ProgressEventArgs((int)(100.0 * progress)));
        }
        void RaiseComplete() {
            if(GenerationComplete != null)
                GenerationComplete(this, new SalesDemo.Model.ProgressEventArgs(100));
        }
        #endregion Progress
        #region Sample Data
        int[] rndDiscounts = new int[] { 100, 200, 300, 400, 500, 600, 700, 800 };
        int[] rndUnits = new int[] { 1, 1, 2, 2, 3, 4 };
        int[] rndRegions = new int[] { 0, 0, 0, 0, 0, 1, 2, 2, 2, 3, 3, 4 };
        int[] rndChannels = new int[] { 0, 0, 0, 0, 1, 2, 3, 3, 4, 4 };
        int[] rndSectors = new int[] { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 3, 3, 4, 4, 5, 5, 5 };
        int[] rndProducts = new int[] { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 5, 5 };

        Product[] products = new Product[6] {
            new Product() { Id = 1, Name = "Eco Max", Price = 2500 },
            new Product() { Id = 2, Name = "Eco Supreme", Price = 2000 },
            new Product() { Id = 3, Name = "EnviroCare", Price = 1750 },
            new Product() { Id = 4, Name = "EnviroCare Max", Price = 2800 },
            new Product() { Id = 5, Name = "SolarOne", Price = 1500 },
            new Product() { Id = 6, Name = "SolarMax", Price = 2250 }
        };
        Region[] regions = new Region[5] {
            new Region() { Id = 1, Name = "North America" },
            new Region() { Id = 2, Name = "South America" },
            new Region() { Id = 3, Name = "Europe" },
            new Region() { Id = 4, Name = "Asia" },
            new Region() { Id = 5, Name = "Australia" }
        };
        Channel[] channels = new Channel[5] {
            new Channel() { Id = 1, Name = "Direct" },
            new Channel() { Id = 2, Name = "VARs" },
            new Channel() { Id = 3, Name = "Consultants" },
            new Channel() { Id = 4, Name = "Resellers" },
            new Channel() { Id = 5, Name = "Retail" }
        };
        Sector[] sectors = new Sector[6] {
            new Sector() { Id = 1, Name = "Energy" },
            new Sector() { Id = 2, Name = "Manufacturing" },
            new Sector() { Id = 3, Name = "Telecom" },
            new Sector() { Id = 4, Name = "Insurance" },
            new Sector() { Id = 5, Name = "Banking" },
            new Sector() { Id = 6, Name = "Health" },
        };
        #endregion Sample Data
    }
    public class Product {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
    }
    public class Region {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Channel {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Sector {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
