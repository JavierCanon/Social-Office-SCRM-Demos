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
    public class PieBarRangeViewModel : DataViewModel {
        public static PieBarRangeViewModel Create(string moduleType) {
            return ViewModelSource.Create(() => new PieBarRangeViewModel(moduleType));
        }
        protected string ModuleType { get; private set; }
        protected PieBarRangeViewModel()
            : this(Modules.Regions) {
        }
        protected PieBarRangeViewModel(string moduleType) {
            ModuleType = moduleType;

            BarViewModel = BarViewModel.Create();
            PieViewModel = PieViewModel.Create();
            RangeViewModel = RangeViewModel.Create();
            RangeViewModel.RangeChanged += OnRangeViewModelRangeChanged;
            RangeViewModel.SelectedRangeChanged += OnRangeViewModelSelectedRangeChanged;

            RequestDataForRange();
            RequestDataForPieAndBar();
        }
        public virtual RangeViewModel RangeViewModel { get; protected set; }
        public virtual BarViewModel BarViewModel { get; protected set; }
        public virtual PieViewModel PieViewModel { get; protected set; }

        void OnRangeViewModelRangeChanged(object sender, EventArgs e) {
            RequestDataForRange();
        }
        void OnRangeViewModelSelectedRangeChanged(object sender, EventArgs e) {
            RequestDataForPieAndBar();
        }
        void RequestDataForRange() {
            if(RangeViewModel.RangeStart == null || RangeViewModel.RangeEnd == null || RangeViewModel.RangeStart >= RangeViewModel.RangeEnd)
                return;
            RequestData("RangeViewModel", x => x.GetSales(RangeViewModel.RangeStart.Value, RangeViewModel.RangeEnd.Value.AddDays(1), GroupingPeriod.Day), x => RangeViewModel.LoadData(x));
        }
        void RequestDataForPieAndBar() {
            var range = RangeViewModel.GetSelectedRange();
            if(range == null) return;
            RequestData("Data", x => {
                if(ModuleType == Modules.Regions)
                    return x.GetSalesByRegion(range.Value, GroupingPeriod.All);
                else if(ModuleType == Modules.Sectors)
                    return x.GetSalesBySector(range.Value, GroupingPeriod.All);
                else if(ModuleType == Modules.Products)
                    return x.GetSalesByProduct(range.Value, GroupingPeriod.All);
                else if(ModuleType == Modules.Channels)
                    return x.GetSalesByChannel(range.Value, GroupingPeriod.All);
                throw new NotImplementedException();
            }, x => {
                BarViewModel.LoadData(x);
                PieViewModel.LoadData(x);
            });
        }
    }
}
