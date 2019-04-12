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

namespace SO.DashboardSalesDemo.Wpf.ViewModel {
    public class PerformanceBarChartViewModel : PerformanceAreaChartViewModel {
        public static new PerformanceBarChartViewModel Create(string moduleType, PerformanceViewMode mode) {
            return ViewModelSource.Create(() => new PerformanceBarChartViewModel(moduleType, mode));
        }
        protected PerformanceBarChartViewModel()
            : this(Modules.Products, PerformanceViewMode.Daily) {
        }
        protected PerformanceBarChartViewModel(string moduleType, PerformanceViewMode mode)
            : base(moduleType, mode) {
        }
        protected override void ValidateMode(string moduleType, PerformanceViewMode mode) {
            if(moduleType == Modules.Products || moduleType == Modules.Sectors || moduleType == Modules.Regions)
                return;
            throw new NotImplementedException();
        }
        protected override void InitializeInDailyMode() {
            ArgumentDataMember = "GroupName";
            ValueDataMember = "TotalCost";
        }
        protected override void InitializeInMonthlyMode() {
            ArgumentDataMember = "GroupName";
            ValueDataMember = "Units";
        }
        protected override void RequestData() {
            DateTimeRange range = Mode == PerformanceViewMode.Daily ? DateTimeUtils.GetDayRange(SelectedDate) : DateTimeUtils.GetMonthRange(SelectedDate);
            if(ModuleType == Modules.Products) {
                RequestData("ChartDataSource", x => x.GetSalesByProduct(range, GroupingPeriod.All), x => ChartDataSource = x);
                return;
            }
            if(ModuleType == Modules.Sectors) {
                RequestData("ChartDataSource", x => x.GetSalesBySector(range, GroupingPeriod.All), x => ChartDataSource = x);
                return;
            }
            if(ModuleType == Modules.Regions) {
                RequestData("ChartDataSource", x => x.GetSalesByRegion(range, GroupingPeriod.All), x => ChartDataSource = x);
                return;
            }
            throw new NotImplementedException();
        }
        protected override void UpdateVolumeLables() {
            Action<string, DateTimeRange, Action<SalesGroup>> requestDataCore = (id, range, callback) => {
                RequestData(id, x => x.GetTotalSalesByRange(range), callback);
            };

            if(Mode == PerformanceViewMode.Daily) {
                requestDataCore("FirstSalesVolume", DateTimeUtils.GetTodayRange(), x => FirstSalesVolume = x.TotalCost.ToString("$0,,.00M"));
                requestDataCore("SecondSalesVolume", DateTimeUtils.GetYesterdayRange(), x => SecondSalesVolume = x.TotalCost.ToString("$0,,.00M"));
                requestDataCore("ThirdSalesVolume", DateTimeUtils.GetLastWeekRange(), x => ThirdSalesVolume = x.TotalCost.ToString("$0,,.0M"));
            } else {
                requestDataCore("FirstSalesVolume", DateTimeUtils.GetThidMonthRange(), x => FirstSalesVolume = x.Units.ToString("0,K Units"));
                requestDataCore("SecondSalesVolume", DateTimeUtils.GetLastMonthRange(), x => SecondSalesVolume = x.Units.ToString("0,K Units"));
                requestDataCore("ThirdSalesVolume", DateTimeUtils.GetYtdRange(), x => ThirdSalesVolume = x.Units.ToString("0,K Units"));
            }
        }
    }
}
