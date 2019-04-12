// Copyright (c) 2019 Javier Ca�on 
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

namespace SO.DashboardSalesDemo.Wpf.ViewModel {
    public class RegionsViewModel : PageViewModel {
        public static RegionsViewModel Create() {
            return ViewModelSource.Create(() => new RegionsViewModel());
        }
        protected RegionsViewModel() { }

        public virtual PerformanceBarChartViewModel DailySalesByRegionViewModel { get; protected set; }
        public virtual PerformanceBarChartViewModel UnitSalesByRegionViewModel { get; protected set; }
        public virtual PieBarRangeViewModel PieGaugeRangeViewModel { get; protected set; }
        protected override void Initialize() {
            DailySalesByRegionViewModel = PerformanceBarChartViewModel.Create(Modules.Regions, PerformanceViewMode.Daily);
            UnitSalesByRegionViewModel = PerformanceBarChartViewModel.Create(Modules.Regions, PerformanceViewMode.Monthly);
            PieGaugeRangeViewModel = PieBarRangeViewModel.Create(Modules.Regions);
        }
    }
}
