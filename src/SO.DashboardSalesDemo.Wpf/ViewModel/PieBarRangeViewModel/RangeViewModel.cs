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
    public class RangeViewModel : DataViewModel {
        public static RangeViewModel Create() {
            return ViewModelSource.Create(() => new RangeViewModel());
        }
        protected RangeViewModel() {
            AreaSeriesArgumentDataMember = "StartOfPeriod";
            AreaSeriesValueDataMember = "TotalCost";

            DateTime today = DateTime.Today;
            RangeEnd = today;
            RangeStart = today.AddDays(-DaysInMonth);
            SelectedRangeStart = RangeStart.Value.AddDays(OneThirdOfMonth);
            SelectedRangeEnd = RangeEnd.Value.AddDays(-OneThirdOfMonth);

            if(this.IsInDesignMode())
                OnInitializeInDesignMode();
        }

        const int OneThirdOfMonth = 10;
        const int DaysInMonth = 30;
        public virtual object AreaSeriesDataSource { get; protected set; }
        public virtual DateTime? RangeStart { get; set; }
        public virtual DateTime? RangeEnd { get; set; }
        public virtual DateTime? VisibleRangeStart { get; set; }
        public virtual DateTime? VisibleRangeEnd { get; set; }
        public virtual DateTime? SelectedRangeStart { get; set; }
        public virtual DateTime? SelectedRangeEnd { get; set; }
        public virtual string AreaSeriesArgumentDataMember { get; protected set; }
        public virtual string AreaSeriesValueDataMember { get; protected set; }
        public event EventHandler RangeChanged;
        public event EventHandler SelectedRangeChanged;


        public DateTimeRange? GetSelectedRange() {
            if(SelectedRangeStart == null || SelectedRangeEnd == null || SelectedRangeStart >= SelectedRangeEnd)
                return null;
            return new DateTimeRange(SelectedRangeStart.Value, SelectedRangeEnd.Value);
        }
        public void LoadData(object data) {
            AreaSeriesDataSource = data;
        }
        public void SetPrePeriod() {
            if(RangeStart == null || RangeEnd == null)
                return;
            DateTime oldRangeEnd = RangeEnd.Value;
            DateTime dateInPreMotnth = oldRangeEnd.AddMonths(-1);
            DateTime start = new DateTime(dateInPreMotnth.Year, dateInPreMotnth.Month, 1);
            int daysInPreMonts = DateTime.DaysInMonth(dateInPreMotnth.Year, dateInPreMotnth.Month);
            DateTime end = new DateTime(dateInPreMotnth.Year, dateInPreMotnth.Month, daysInPreMonts);
            RangeStart = start;
            RangeEnd = end;
        }
        public void SetNextPeriod() {
            if(RangeStart == null || RangeEnd == null)
                return;
            RangeEnd = RangeEnd.Value.AddMonths(1);
            RangeStart = RangeStart.Value.AddMonths(1);
            SetSelectedRangeInMiddle();
        }
        public bool CanSetNextPeriod() {
            return DateTime.Today > RangeEnd;
        }
        protected void OnRangeStartChanged() {
            SetSelectedRangeInMiddle();
            VisibleRangeStart = RangeStart;
            RaiseRangeChanged();
        }
        protected void OnRangeEndChanged() {
            SetSelectedRangeInMiddle();
            VisibleRangeEnd = RangeEnd;
            RaiseRangeChanged();
        }
        protected void OnSelectedRangeStartChanged() {
            RaiseSelectedRangeChanged();
        }
        protected void OnSelectedRangeEndChanged() {
            RaiseSelectedRangeChanged();
        }
        void OnInitializeInDesignMode() {
            RequestData("Data", x => x.GetSales(RangeStart.Value, RangeEnd.Value.AddDays(1), GroupingPeriod.Day), x => LoadData(x));
        }
        void SetSelectedRangeInMiddle() {
            if(RangeStart == null || RangeEnd == null || RangeStart > RangeEnd)
                return;
            SelectedRangeEnd = RangeEnd.Value.AddDays(-OneThirdOfMonth);
            SelectedRangeStart = RangeStart.Value.AddDays(OneThirdOfMonth);
        }
        void RaiseRangeChanged() {
            if(RangeChanged != null)
                RangeChanged(this, EventArgs.Empty);
        }
        void RaiseSelectedRangeChanged() {
            if(SelectedRangeChanged != null)
                SelectedRangeChanged(this, EventArgs.Empty);
        }
    }
}
