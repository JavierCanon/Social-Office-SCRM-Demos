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
using DevExpress.Mvvm.UI.Interactivity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SO.DashboardSalesDemo.Wpf.Helpers {
    public class ImageHyperlinkBehavior : Behavior<Image> {
        public static readonly DependencyProperty NavigatetUriProperty =
            DependencyProperty.Register("NavigatetUri", typeof(string), typeof(ImageHyperlinkBehavior), new PropertyMetadata(null));
        public string NavigatetUri {
            get { return (string)GetValue(NavigatetUriProperty); }
            set { SetValue(NavigatetUriProperty, value); }
        }
        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.MouseLeftButtonUp += OnImageMouseLeftButtonUp;
        }
        protected override void OnDetaching() {
            AssociatedObject.MouseLeftButtonUp -= OnImageMouseLeftButtonUp;
            base.OnDetaching();
        }
        void OnImageMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            System.Diagnostics.Process.Start(NavigatetUri.ToString());
        }
    }
}
