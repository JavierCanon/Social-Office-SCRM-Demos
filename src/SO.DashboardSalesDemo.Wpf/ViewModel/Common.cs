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
using SO.SalesDemo.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.Mvvm.ModuleInjection;

namespace SO.DashboardSalesDemo.Wpf.ViewModel {
    public static class Regions {
        public static string Main { get { return "MainRegion"; } }
        public static string Navigation { get { return "NavigationRegion"; } }
    }
    public static class Modules {
        public static string Dashboard { get { return "Dashboard"; } }
        public static string Products { get { return "Products"; } }
        public static string Sectors { get { return "Sectors"; } }
        public static string Regions { get { return "Regions"; } }
        public static string Channels { get { return "Channels"; } }
    }
    public abstract class DataViewModel {
        protected DataViewModel() {
            if(this.IsInDesignMode())
                DataProvider = new SampleDataProvider();
            else DataProvider = DataSource.GetDataProvider();
        }
        public void RequestData<T>(string actionId, Func<IDataProvider, T> requestDataFunction, Action<T> callback) {
            if(this.IsInDesignMode()) {
                callback(requestDataFunction(DataProvider));
                return;
            }
            if(!tasks.ContainsKey(actionId)) {
                tasks.Add(actionId, new CancellationTokenSource());
                var task = new Task<T>(() => requestDataFunction(DataProvider), tasks[actionId].Token, TaskCreationOptions.LongRunning);
                task.ContinueWith(x => callback(x.Result), tasks[actionId].Token, TaskContinuationOptions.LongRunning, TaskScheduler.Current);
                task.Start();
                return;
            }
            if(tasks.ContainsKey(actionId)) {
                tasks[actionId].Cancel();
                tasks.Remove(actionId);
                RequestData(actionId, requestDataFunction, callback);
                return;
            }
        }
        IDataProvider DataProvider;
        Dictionary<string, CancellationTokenSource> tasks = new Dictionary<string,CancellationTokenSource>();
    }
    public abstract class PageViewModel {
        protected PageViewModel() {
            ModuleManager.DefaultManager.GetEvents(this).Navigated += OnNavigated;
            if(this.IsInDesignMode())
                Initialize();
        }
        protected abstract void Initialize();
        void OnNavigated(object sender, NavigationEventArgs e) {
            if(isInitialize) return;
            Initialize();
            isInitialize = true;
        }
        bool isInitialize = false;
    }
}
