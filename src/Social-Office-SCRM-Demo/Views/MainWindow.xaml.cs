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
using DevExpress.Xpf.Core;
using System;
using System.Diagnostics;
using System.IO;

namespace SODemo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnContacts_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            App.NewBrowserWindowIE("http://localhost:1975/Web/Sales/Contacts/Main.aspx");

        }

        private void BtnFacebook_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            App.NewBrowserWindowIE("http://localhost:1975/Web/Marketing/Social/Facebook/Main.aspx");
        }

        private void BtnDashboards_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            App.NewBrowserWindowIE("http://localhost:1975/Web/Marketing/Social/Facebook/Main.aspx");
        }

        private void BtnMenuDesktopDashboard_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            string programPath = AppDomain.CurrentDomain.BaseDirectory + "SO.DashboardSalesDemo.Wpf.exe"; ;

            Process proc = new Process();
            proc.StartInfo.FileName = programPath;
            proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(programPath);
            proc.Start();

        }




    }
}