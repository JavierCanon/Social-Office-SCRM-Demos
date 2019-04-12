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
using System.Globalization;
using System.Linq;
using System.Text;

namespace SO.SalesDemo.Model {
    public static class DateTimeUtils {
        public static DateTimeRange GetDayRange(DateTime date) {
            DateTime startOfDate = new DateTime(date.Year, date.Month, date.Day);
            DateTime endOfToday = startOfDate.AddDays(1).AddTicks(-1);
            return new DateTimeRange(startOfDate, endOfToday);
        }
        public static DateTimeRange GetTodayRange() {
            return GetDayRange(DateTime.Now);
        }
        public static DateTimeRange GetYesterdayRange() {
            return GetDayRange(DateTime.Now.AddDays(-1));
        }
        public static DateTimeRange GetLastWeekRange() {
            DayOfWeek firstDay = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            DateTime today = DateTime.Today;
            DateTime startOfWeek = today;
            while (startOfWeek.DayOfWeek != firstDay) {
                startOfWeek = startOfWeek.AddDays(-1);
            }
            DateTime endOfWeek = startOfWeek.AddDays(7).AddTicks(-1);
            return new DateTimeRange(startOfWeek, endOfWeek);
        }
        public static DateTimeRange GetMonthRange(DateTime date) {
            DateTime startOfMonth = new DateTime(date.Year, date.Month, 1);
            int daysInCurrentMonth = DateTime.DaysInMonth(date.Year, date.Month);
            DateTime endOfMonth = startOfMonth.AddDays(daysInCurrentMonth).AddTicks(-1);
            return new DateTimeRange(startOfMonth, endOfMonth);
        }
        public static DateTimeRange GetThidMonthRange() {
            return GetMonthRange(DateTime.Now);
        }
        public static DateTimeRange GetLastMonthRange() {
            return GetMonthRange(DateTime.Now.AddMonths(-1));
        }
        public static DateTimeRange GetYtdRange() {
            DateTime today = DateTime.Today;
            DateTime startOfYear = new DateTime(today.Year, 1, 1);
            DateTime endOfYear = today;// startOfYear.AddYears(1).AddTicks(-1);
            return new DateTimeRange(startOfYear, endOfYear);
        }
        public static DateTimeRange GetOneYearRange() {
            return new DateTimeRange(DateTime.Today.AddYears(-1), DateTime.Today);
        }

        public static DateTimeRange GetYearRange(DateTime date) {
            DateTime startOfYear = new DateTime(date.Year, 1, 1);
            DateTime endOfYear = startOfYear.AddYears(1).AddTicks(-1);
            return new DateTimeRange(startOfYear, endOfYear);
        }
        public static DateTimeRange GetLastYearRange() {
            return GetYearRange(DateTime.Today.AddYears(-1));
        }
        public static int GetLastYear() {
            return DateTime.Today.AddYears(-1).Year;
        }
        public static object GetCurrentYear() {
            return DateTime.Now.Year;
        }

        public static bool IsCurrentYear(DateTime date) {
            DateTime now = DateTime.Now;
            return now.Year == date.Year;
        }
        public static bool IsCurrentMonth(DateTime date) {
            DateTime now = DateTime.Now;
            return IsCurrentYear(date) && now.Month == date.Month;
        }
        public static bool IsToday(DateTime date) {
            DateTime now = DateTime.Now;
            return IsCurrentMonth(date) && now.Day == date.Day;
        }
    }

    public struct DateTimeRange {
        DateTime start;
        DateTime end;

        public DateTime Start {
            get { return start; }
        }
        public DateTime End {
            get { return end; }
        }

        public DateTimeRange(DateTime start, DateTime end) {
            this.start = start;
            this.end = end;
        }
    }

    public struct DecimalRange {
        decimal start;
        decimal end;

        public decimal Start {
            get { return start; }
        }
        public decimal End {
            get { return end; }
        }

        public DecimalRange(decimal start, decimal end) {
            this.start = start;
            this.end = end;
        }
    }

    public static class SalesRangeProvider{
        public static DecimalRange GetBadSalesRange() {
            return new DecimalRange(0, 200000000M);
        }
        public static DecimalRange GetNormalSalesRange() {
            return new DecimalRange(0, 400000000M);
        }
        public static DecimalRange GetGoodSalesRange() {
            return new DecimalRange(0, 600000000M);
        }
    }
}
