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
    public class BarViewModel : DataViewModel {
        public static BarViewModel Create() {
            return ViewModelSource.Create(() => new BarViewModel());
        }
        protected BarViewModel() {
            PieArgumentDataMember = "GroupName";
            PieValueDataMember = "TotalCost";
            if(this.IsInDesignMode())
                OnInitializeInDesignMode();
        }

        public virtual object PieDataSource { get; protected set; }
        public virtual string PieArgumentDataMember { get; protected set; }
        public virtual string PieValueDataMember { get; protected set; }
        public void LoadData(object data) {
            PieDataSource = data;
        }
        void OnInitializeInDesignMode() {
            DateTime today = DateTime.Today;
            DateTime monthAgo = today.AddMonths(-1);
            int ThirdOfMonth = 10;
            var rangeStart = monthAgo;
            var rangeEnd = today;
            var selectedRangeStart = monthAgo.AddDays(ThirdOfMonth);
            var selectedRangeEnd = monthAgo.AddDays(2 * ThirdOfMonth);
            RequestData("PieDataSource", x => x.GetSalesBySector(selectedRangeStart, selectedRangeEnd, GroupingPeriod.All), x => PieDataSource = x);
        }
    }
}
