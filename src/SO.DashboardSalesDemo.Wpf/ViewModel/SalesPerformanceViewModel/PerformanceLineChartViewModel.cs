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
using DevExpress.Mvvm.POCO;
using SO.SalesDemo.Model;
using System;
using System.Collections.Generic;

namespace SO.DashboardSalesDemo.Wpf.ViewModel {
    public class PerformanceLineChartViewModel : DataViewModel {
        public static PerformanceLineChartViewModel Create(string moduleType) {
            return ViewModelSource.Create(() => new PerformanceLineChartViewModel(moduleType));
        }
        protected string ModuleType { get; private set; }
        protected PerformanceLineChartViewModel()
            : this(Modules.Channels) {
        }
        protected PerformanceLineChartViewModel(string moduleType) {
            ModuleType = moduleType;
            SelectedDate = DateTime.Now;
        }
        protected virtual void RequestData() {
            if(ModuleType == Modules.Channels) {
                DateTimeRange todayRange = DateTimeUtils.GetDayRange(SelectedDate);
                RequestData("ChartDataSource", x => x.GetSalesByChannel(todayRange, GroupingPeriod.Hour), x => ChartDataSource = x);
                RequestData("TotalSalesBySector", x => x.GetSalesByChannel(todayRange, GroupingPeriod.All), x => {
                    TotalSalesBySector = x;
                    decimal total = 0M;
                    foreach(SalesGroup grp in TotalSalesBySector)
                        total += grp.TotalCost;
                    TotalSalesVolumeForDay = total;
                });
                return;
            }
            throw new NotImplementedException();
        }

        public PerformanceViewMode Mode { get { return PerformanceViewMode.Daily; } }
        public virtual object ChartDataSource { get; protected set; }
        public virtual DateTime SelectedDate { get; protected set; }
        public virtual string SelectedDateString { get; protected set; }
        public virtual IEnumerable<SalesGroup> TotalSalesBySector { get; protected set; }
        public virtual decimal TotalSalesVolumeForDay { get; protected set; }
        
        public void TimeForward() {
            SelectedDate = SelectedDate.AddDays(1);
        }
        public void TimeBackward() {
            SelectedDate = SelectedDate.AddDays(-1);
        }
        public bool CanTimeForward() {
            return !DateTimeUtils.IsToday(SelectedDate);
        }
        public void SetCurrentTimePeriod() {
            throw new NotImplementedException();
        }
        public void SetLastTimePeriod() {
            throw new NotImplementedException();
        }
        protected void OnSelectedDateChanged() {
            SelectedDateString = SelectedDate.ToString("D");
            RequestData();
        }
    }
}
