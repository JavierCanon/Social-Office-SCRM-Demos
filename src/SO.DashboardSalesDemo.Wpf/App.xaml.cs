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
using System;
using System.Windows;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Internal;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using SO.SalesDemo.Model;
using SO.DashboardSalesDemo.Wpf.Helpers;
using SO.DashboardSalesDemo.Wpf.View;
using SO.DashboardSalesDemo.Wpf.View.Common;
using SO.DashboardSalesDemo.Wpf.ViewModel;
using DevExpress.Xpf.Core;

namespace SO.DashboardSalesDemo.Wpf {
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            ExceptionHelper.Initialize();
            ViewModelLocator.Default = new ViewModelLocator(typeof(App).Assembly);
            ViewLocator.Default = new ViewLocator(typeof(App).Assembly);
            ApplicationThemeHelper.ApplicationThemeName = Theme.Office2013Name;
            using(DataSource.Progress.Subscribe(new DataGenerationProgress()))
                DataSource.EnsureDataProvider();
            base.OnStartup(e);
            InitModules();
        }
        static void InitModules() {
            ModuleManager.DefaultManager.Register(
                Regions.Navigation, 
                new DevExpress.Mvvm.ModuleInjection.Module(Modules.Dashboard, 
                    () => NavigationItemViewModel.Create(Modules.Dashboard, "Sales", "Revenue" + Environment.NewLine + "Snapshots", ResourceImageHelper.GetResourceImage("Sales.png")),
                    typeof(NavigationItemView)));
            ModuleManager.DefaultManager.Register(
                Regions.Navigation, 
                new DevExpress.Mvvm.ModuleInjection.Module(Modules.Products,
                    () => NavigationItemViewModel.Create(Modules.Products, "Products", "Revenue" + Environment.NewLine + "by Products", ResourceImageHelper.GetResourceImage("Products.png")),
                    typeof(NavigationItemView)));
            ModuleManager.DefaultManager.Register(
                Regions.Navigation,
                new DevExpress.Mvvm.ModuleInjection.Module(Modules.Sectors,
                    () => NavigationItemViewModel.Create(Modules.Sectors, "Sectors", "Revenue" + Environment.NewLine + "by Sectors", ResourceImageHelper.GetResourceImage("Sectors.png")),
                    typeof(NavigationItemView)));
            ModuleManager.DefaultManager.Register(
                Regions.Navigation,
                new DevExpress.Mvvm.ModuleInjection.Module(Modules.Regions,
                    () => NavigationItemViewModel.Create(Modules.Regions, "Regions", "Revenue" + Environment.NewLine + "by Regions", ResourceImageHelper.GetResourceImage("Regions.png")),
                    typeof(NavigationItemView)));
            ModuleManager.DefaultManager.Register(
                Regions.Navigation,
                new DevExpress.Mvvm.ModuleInjection.Module(Modules.Channels,
                    () => NavigationItemViewModel.Create(Modules.Channels, "Channels", "Revenue" + Environment.NewLine + "by Sales Channels", ResourceImageHelper.GetResourceImage("Channels.png")),
                    typeof(NavigationItemView)));

            ModuleManager.DefaultManager.Register(Regions.Main, new DevExpress.Mvvm.ModuleInjection.Module(Modules.Dashboard, () => SalesDashboardViewModel.Create(), typeof(SalesDashboard)));
            ModuleManager.DefaultManager.Register(Regions.Main, new DevExpress.Mvvm.ModuleInjection.Module(Modules.Products, () => ProductsViewModel.Create(), typeof(ProductsView)));
            ModuleManager.DefaultManager.Register(Regions.Main, new DevExpress.Mvvm.ModuleInjection.Module(Modules.Sectors, () => SectorsViewModel.Create(), typeof(SectorsView)));
            ModuleManager.DefaultManager.Register(Regions.Main, new DevExpress.Mvvm.ModuleInjection.Module(Modules.Regions, () => RegionsViewModel.Create(), typeof(RegionsView)));
            ModuleManager.DefaultManager.Register(Regions.Main, new DevExpress.Mvvm.ModuleInjection.Module(Modules.Channels, () => ChannelsViewModel.Create(), typeof(ChannelsView)));

            ModuleManager.DefaultManager.Inject(Regions.Navigation, Modules.Dashboard);
            ModuleManager.DefaultManager.Inject(Regions.Navigation, Modules.Products);
            ModuleManager.DefaultManager.Inject(Regions.Navigation, Modules.Sectors);
            ModuleManager.DefaultManager.Inject(Regions.Navigation, Modules.Regions);
            ModuleManager.DefaultManager.Inject(Regions.Navigation, Modules.Channels);

            ModuleManager.DefaultManager.Inject(Regions.Main, Modules.Dashboard);
            ModuleManager.DefaultManager.Inject(Regions.Main, Modules.Products);
            ModuleManager.DefaultManager.Inject(Regions.Main, Modules.Sectors);
            ModuleManager.DefaultManager.Inject(Regions.Main, Modules.Regions);
            ModuleManager.DefaultManager.Inject(Regions.Main, Modules.Channels);

            ModuleManager.DefaultManager.Navigate(Regions.Navigation, Modules.Dashboard);
        }
    }
    class DataGenerationProgress : IObserver<int> {
        void IObserver<int>.OnCompleted() {
            DXSplashScreen.Close();
        }
        void IObserver<int>.OnNext(int value) {
            if(!DXSplashScreen.IsActive) {
                DXSplashScreen.Show(typeof(ProgressSplashScreenView));
                DXSplashScreen.SetState(ProgressSplashScreenViewModel.Create("Generating Sales...", AssemblyInfo.AssemblyCopyright));
            }
            DXSplashScreen.Progress((double)value);
        }
        void IObserver<int>.OnError(Exception error) { throw error; }
    }
}
#if CLICKONCE
namespace DevExpress.Internal.DemoLauncher {
    public static class ClickOnceEntryPoint {
        public static Tuple<Action, System.Threading.Tasks.Task<Window>> Run() {
            var done = new System.Threading.Tasks.TaskCompletionSource<Window>();
            Action run = () => {
                var app = new SO.DashboardSalesDemo.Wpf.App();
                app.InitializeComponent();
                typeof(Application).GetField("_startupUri", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(app, null);
                app.Startup += (s, e) => {
                    var window = new SO.DashboardSalesDemo.Wpf.MainWindow();
                    window.Show();
                    app.MainWindow = window;
                    done.SetResult(window);
                };
                app.Run();
            };
            return Tuple.Create(run, done.Task);
        }
    }
}
#endif
