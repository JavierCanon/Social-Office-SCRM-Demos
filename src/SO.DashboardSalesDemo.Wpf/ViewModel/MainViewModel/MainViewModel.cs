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
using System.Windows;

namespace SO.DashboardSalesDemo.Wpf.ViewModel {
    public class MainViewModel {
        public static MainViewModel Create() {
            return ViewModelSource.Create(() => new MainViewModel());
        }
        protected MainViewModel() {
            LoginViewModel = LoginViewModel.Create();
            NavigationViewModel = NavigationViewModel.Create();
        }

        public virtual LoginViewModel LoginViewModel { get; protected set; }
        public virtual NavigationViewModel NavigationViewModel { get; protected set; }
        protected virtual IDialogService DialogService { get { return null; } }
        public void ShowLoginView() {
            var vm = LoginViewModel.Clone(LoginViewModel);
            this.GetService<IDialogService>().ShowDialog(MessageButton.OK, ApplicationSettings.Default.MainWindowTitle, vm);
            LoginViewModel = vm;
        }
    }
}
