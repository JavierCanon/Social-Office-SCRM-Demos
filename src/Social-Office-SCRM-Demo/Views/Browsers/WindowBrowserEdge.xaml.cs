﻿// Copyright (c) 2019 Javier Cañon 
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
using System.Windows;

namespace SODemo.Views.Browsers
{
    /// <summary>
    /// Interaction logic for WindowBrowserEdge.xaml
    /// </summary>
    public partial class WindowBrowserEdge : DevExpress.Xpf.Core.ThemedWindow
    {
        public WindowBrowserEdge()
        {
            InitializeComponent();
            this.Show();
        }


        private void WebView_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            // how to share same process
            var webView2 = new WebView(WebView1.Process);
            webView2.BeginInit();
            webView2.EndInit();

            Grid.SetRow(webView2, 0);
            Grid.SetColumn(webView2, 1);

            Grid1.Children.Add(webView2);
            */

            this.WebView1.Navigate("https://www.google.com");
        }

    }
}
