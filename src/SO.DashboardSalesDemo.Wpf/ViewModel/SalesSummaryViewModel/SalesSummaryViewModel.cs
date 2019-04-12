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
    public class SalesSummaryViewModel : DataViewModel {
        public static SalesSummaryViewModel Create() {
            return ViewModelSource.Create(() => new SalesSummaryViewModel());
        }
        public SalesSummaryViewModel() {
            SalesBySectorViewModel = SalesBySectorViewModel.Create();
            ThisYearAnnualSalesPerformanceViewModel = AnnualSalesPerformanceViewModel.Create(DateTime.Now);
            LastYearAnnualSalesPerformanceViewModel = AnnualSalesPerformanceViewModel.Create(DateTime.Now.AddYears(-1));
            SalesForecastViewModel = SalesForecastViewModel.Create();
            SalesForecastHeader = string.Format("SALES FORECAST ({0})", DateTimeUtils.GetCurrentYear());
        }

        public virtual SalesBySectorViewModel SalesBySectorViewModel { get; protected set; }
        public virtual AnnualSalesPerformanceViewModel ThisYearAnnualSalesPerformanceViewModel { get; protected set; }
        public virtual AnnualSalesPerformanceViewModel LastYearAnnualSalesPerformanceViewModel { get; protected set; }
        public virtual SalesForecastViewModel SalesForecastViewModel { get; protected set; }
        public virtual string SalesForecastHeader { get; protected set; }
    }
}
