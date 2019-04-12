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
    public class PerformanceAreaChartViewModel : DataViewModel {
        public static PerformanceAreaChartViewModel Create(string moduleType, PerformanceViewMode mode) {
            return ViewModelSource.Create(() => new PerformanceAreaChartViewModel(moduleType, mode));
        }
        protected string ModuleType { get; private set; }
        protected PerformanceAreaChartViewModel()
            : this(Modules.Dashboard, PerformanceViewMode.Daily) {
        }
        protected PerformanceAreaChartViewModel(string moduleType, PerformanceViewMode mode) {
            ValidateMode(moduleType, mode);
            ModuleType = moduleType;
            Mode = mode;
            if(Mode == PerformanceViewMode.Daily)
                InitializeInDailyMode();
            else InitializeInMonthlyMode();
            SelectedDate = DateTime.Now;
            UpdateVolumeLables();
        }
        protected virtual void ValidateMode(string moduleType, PerformanceViewMode mode) {
            if(moduleType != Modules.Dashboard)
                throw new NotImplementedException();
        }
        protected virtual void InitializeInDailyMode() {
            ArgumentDataMember = "StartOfPeriod";
            ValueDataMember = "TotalCost";
        }
        protected virtual void InitializeInMonthlyMode() {
            InitializeInDailyMode();
        }
        protected virtual void RequestData() {
            DateTimeRange range = Mode == PerformanceViewMode.Daily ? DateTimeUtils.GetDayRange(SelectedDate) : DateTimeUtils.GetMonthRange(SelectedDate);
            GroupingPeriod groupingPeriod = Mode == PerformanceViewMode.Daily ? GroupingPeriod.Hour : GroupingPeriod.Day;
            RequestData("ChartDataSource", x => x.GetSales(range, groupingPeriod), x => ChartDataSource = x);
        }
        protected virtual void UpdateVolumeLables() {
            Action<string, DateTimeRange, Action<SalesGroup>> requestDataCore = (id, range, callback) => {
                RequestData(id, x => x.GetTotalSalesByRange(range), callback);
            };
            if(Mode == PerformanceViewMode.Daily) {
                requestDataCore("FirstSalesVolume", DateTimeUtils.GetTodayRange(), x => FirstSalesVolume = GetTotalCost(x));
                requestDataCore("SecondSalesVolume", DateTimeUtils.GetYesterdayRange(), x => SecondSalesVolume = GetTotalCost(x));
                requestDataCore("ThirdSalesVolume", DateTimeUtils.GetLastWeekRange(), x => ThirdSalesVolume = GetTotalCost(x));
            } else {
                requestDataCore("FirstSalesVolume", DateTimeUtils.GetThidMonthRange(), x => FirstSalesVolume = GetTotalCost(x));
                requestDataCore("SecondSalesVolume", DateTimeUtils.GetLastMonthRange(), x => SecondSalesVolume = GetTotalCost(x));
                requestDataCore("ThirdSalesVolume", DateTimeUtils.GetYtdRange(), x => ThirdSalesVolume = GetTotalCost(x));
            }
        }
        string GetTotalCost(SalesGroup group) {
            return group.TotalCost.ToString("$#,#,0");
        }

        public PerformanceViewMode Mode { get; private set; }
        public virtual object ChartDataSource { get; protected set; }
        public virtual string ArgumentDataMember { get; protected set; }
        public virtual string ValueDataMember { get; protected set; }
        public virtual string FirstSalesVolume { get; protected set; }
        public virtual string SecondSalesVolume { get; protected set; }
        public virtual string ThirdSalesVolume { get; protected set; }
        public virtual DateTime SelectedDate { get; protected set; }
        public virtual string SelectedDateString { get; protected set; }

        public void TimeForward() {
            SelectedDate = Mode == PerformanceViewMode.Daily ? SelectedDate.AddDays(1) : SelectedDate.AddMonths(1);
        }
        public void TimeBackward() {
            SelectedDate = Mode == PerformanceViewMode.Daily ? SelectedDate.AddDays(-1) : SelectedDate.AddMonths(-1);
        }
        public bool CanTimeForward() {
            return Mode == PerformanceViewMode.Daily ? !DateTimeUtils.IsToday(SelectedDate) : !DateTimeUtils.IsCurrentMonth(SelectedDate);
        }
        public void SetCurrentTimePeriod() {
            SelectedDate = DateTime.Now;
        }
        public void SetLastTimePeriod() {
            SelectedDate = Mode == PerformanceViewMode.Daily ? DateTime.Now.AddDays(-1) : DateTime.Now.AddMonths(-1);
        }
        protected void OnSelectedDateChanged() {
            string fomatString = Mode == PerformanceViewMode.Daily ? "d" : "MMMM, yyyy";
            SelectedDateString = SelectedDate.ToString(fomatString);
            RequestData();
        }
    }
}
