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
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using SO.DashboardSalesDemo.Wpf.Helpers;
using System;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm.ModuleInjection;

namespace SO.DashboardSalesDemo.Wpf.ViewModel {
    public class NavigationItemViewModel {
        public static NavigationItemViewModel Create(string moduleType, string caption, string description, BitmapImage glyph) {
            var res = ViewModelSource.Create(() => new NavigationItemViewModel());
            res.moduleType = moduleType;
            res.Caption = caption;
            res.Description = description;
            res.Glyph = glyph;
            return res;
        }
        protected NavigationItemViewModel() {
            if(this.IsInDesignMode())
                InitializeInDesignMode();
            ModuleManager.DefaultManager.GetEvents(this).Navigated += (s, e) => IsActive = true;
            ModuleManager.DefaultManager.GetEvents(this).NavigatedAway += (s, e) => IsActive = false;
        }
        void InitializeInDesignMode() {
            Caption = "Sales";
            Description = "Revenue" + Environment.NewLine + "Snapshots";
            Glyph = ResourceImageHelper.GetResourceImage("Sales.png");
        }

        string moduleType;
        public virtual string Caption { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual BitmapImage Glyph { get; protected set; }
        public virtual bool IsActive { get; set; }

        public void Activate() {
            if(IsActive) return;
            ModuleManager.DefaultManager.Navigate(Regions.Navigation, moduleType);
        }
        protected void OnIsActiveChanged() {
            if(IsActive)
                ModuleManager.DefaultManager.Navigate(Regions.Main, moduleType);
        }
    }
}
