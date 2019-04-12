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
    public class AnnualSalesPerformanceViewModel : DataViewModel {
        public static AnnualSalesPerformanceViewModel Create(DateTime date) {
            return ViewModelSource.Create(() => new AnnualSalesPerformanceViewModel(date));
        }
        protected AnnualSalesPerformanceViewModel()
            : this(DateTime.Now) {
        }
        protected AnnualSalesPerformanceViewModel(DateTime date) {
            DecimalRange badSalesRange = SalesRangeProvider.GetBadSalesRange();
            DecimalRange normalSalesRange = SalesRangeProvider.GetNormalSalesRange();
            DecimalRange goodSalesRange = SalesRangeProvider.GetGoodSalesRange();
            AnnualSalesFirstRangeEnd = badSalesRange.End;
            AnnualSalesSecondRangeEnd = normalSalesRange.End;
            AnnualSalesThirdRangeEnd = goodSalesRange.End;
            if(DateTimeUtils.IsCurrentYear(date)) {
                VolumeHeader = "YEAR TO DATE";
                RequestData("Volume", x => x.GetTotalSalesByRange(DateTimeUtils.GetYtdRange()).TotalCost, x => Volume = x);
            } else {
                VolumeHeader = "YEAR " + date.Year;
                RequestData("Volume", x => x.GetTotalSalesByRange(DateTimeUtils.GetYearRange(date)).TotalCost, x => Volume = x);
            }
        }
        
        public virtual decimal AnnualSalesFirstRangeEnd { get; protected set; }
        public virtual decimal AnnualSalesSecondRangeEnd { get; protected set; }
        public virtual decimal AnnualSalesThirdRangeEnd { get; protected set; }
        public virtual string VolumeHeader { get; protected set; }
        public virtual decimal Volume { get; protected set; }
    }
}
