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
using System.Collections.Generic;

namespace SO.SalesDemo.Model {
    public class SampleDataProvider : IDataProvider{
        Random rnd = new Random();
        List<string> channels = new List<string>(){
            "Consultants",
            "Direct",
            "Resellers",
            "Retail",
            "VARs"
        };
        List<string> products = new List<string>(){
            "Eco Max",
            "Eco Supreme",
            "EnviroCare",
            "EnviroCare Max",
            "SolarMax",
            "SolarOne"
        };
        List<string> sectors = new List<string>(){
            "Banking",
            "Energy",
            "Health",
            "Insurance",
            "Manufacturing",
            "Telecom"
        };
        List<string> regions = new List<string>() {
            "Asia",
            "Australia",
            "Europe",
            "North America",
            "South America"
        };

        public SalesGroup GetTotalSalesByRange(DateTime start, DateTime end) {
            TimeSpan diff = end - start;
            int totalHours = (int)diff.TotalHours;
            int amount = start.Day + rnd.Next((int)(0.5 * totalHours), (int)(1.5 * totalHours)) * 10000;
            return new SalesGroup("Total Sales by Range", amount, amount/15, start, end);
        }
        public decimal GetYtdSalesForecast() {
            return 432123456.78M;
        }
        public IEnumerable<SalesGroup> GetSales(DateTime start, DateTime end, GroupingPeriod period) {
            TimeSpan step;
            DateTime actualStart;
            int randomStart;
            int randomEnd;
            switch (period) {
                case GroupingPeriod.Hour:
                    step = new TimeSpan(1, 0, 0);
                    actualStart = new DateTime(start.Year, start.Month, start.Day, start.Hour, 0, 0);
                    randomStart = 50000;
                    randomEnd = 250000;
                    break;
                case GroupingPeriod.Day:
                    step = new TimeSpan(24, 0, 0);
                    actualStart = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0);
                    randomStart = 900000;
                    randomEnd = 150000000;
                    break;
                case GroupingPeriod.All:
                    return null;
                default:
                    throw new Exception();
            }
            List<SalesGroup> list = new List<SalesGroup>();
            for (DateTime date = actualStart; date <= end; date += step) {
                if (period == GroupingPeriod.Hour && (date.Hour < 8 || date.Hour > 18))
                    continue;
                decimal amount = rnd.Next(randomStart, randomEnd);
                int units = (int)amount / 15;
                SalesGroup sg = new SalesGroup(period.ToString(), amount, units, date, date + step);
                list.Add(sg);
            }
            return list;
        }
        public IEnumerable<SalesGroup> GetSalesByChannel(DateTime start, DateTime end, GroupingPeriod period) {
            List<SalesGroup> list = new List<SalesGroup>();
            if (period == GroupingPeriod.All) {             
                foreach (string channel in channels) {
                    decimal amount = rnd.Next(4750, 87756);
                    int units = (int)amount / 15;
                    SalesGroup sg = new SalesGroup(channel, amount, units, start, end);
                    list.Add(sg);
                }
                return list;
            }
            else {
                TimeSpan step;
                DateTime actualStart;
                int randomStart;
                int randomEnd;
                switch (period) {
                    case GroupingPeriod.Hour:
                        step = new TimeSpan(1, 0, 0);
                        actualStart = new DateTime(start.Year, start.Month, start.Day, start.Hour, 0, 0);
                        randomStart = 50000;
                        randomEnd = 250000;
                        break;
                    case GroupingPeriod.Day:
                        step = new TimeSpan(24, 0, 0);
                        actualStart = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0);
                        randomStart = 900000;
                        randomEnd = 150000000;
                        break;
                    case GroupingPeriod.All:
                        return null;
                    default:
                        throw new Exception();
                }
                foreach (string channel in channels) {
                    for (DateTime date = actualStart; date <= end; date += step) {
                        if (period == GroupingPeriod.Hour && (date.Hour < 8 || date.Hour > 18))
                            continue;
                        decimal amount = rnd.Next(randomStart, randomEnd);
                        int units = (int)amount / 15;
                        SalesGroup sg = new SalesGroup(channel, amount, units, date, date + step);
                        list.Add(sg);
                    }
                }
                return list;
            }
        }
        public IEnumerable<SalesGroup> GetSalesByProduct(DateTime start, DateTime end, GroupingPeriod period) {
            List<SalesGroup> list = new List<SalesGroup>();
            foreach (string channel in channels) {
                decimal amount = rnd.Next(4750, 87756);
                int units = (int)amount / 15;
                SalesGroup sg = new SalesGroup(channel, amount, units, start, end);
                list.Add(sg);
            }
            return list;
        }
        public IEnumerable<SalesGroup> GetSalesByRegion(DateTime start, DateTime end, GroupingPeriod period) {
            List<SalesGroup> list = new List<SalesGroup>();
            foreach (string region in regions) {
                decimal amount = rnd.Next(4750, 87756);
                int units = (int)amount / 15;
                SalesGroup sg = new SalesGroup(region, amount, units, start, end);
                list.Add(sg);
            }
            return list;
        }
        public IEnumerable<SalesGroup> GetSalesBySector(DateTime start, DateTime end, GroupingPeriod period) {
            List<SalesGroup> list = new List<SalesGroup>();
            foreach (string sector in sectors) {
                decimal amount = rnd.Next(4750, 87756);
                int units = (int)amount / 15;
                SalesGroup sg = new SalesGroup(sector, amount, units, start, end);
                list.Add(sg);
            }
            return list;
        }
    }
}
