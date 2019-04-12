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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SO.DashboardSalesDemo.Wpf.View {
    public partial class PerformanceBarChartView : UserControl {
        public static readonly DependencyProperty DateBorderMarginProperty =
            DependencyProperty.Register("DateBorderMargin", typeof(Thickness), typeof(PerformanceBarChartView), new PropertyMetadata(new Thickness()));
        public static readonly DependencyProperty SalesVolumesMarginProperty =
            DependencyProperty.Register("SalesVolumesMargin", typeof(Thickness), typeof(PerformanceBarChartView), new PropertyMetadata(new Thickness()));
        public static readonly DependencyProperty AreaAndSalesVolumesBrushProperty =
            DependencyProperty.Register("AreaAndSalesVolumesBrush", typeof(SolidColorBrush), typeof(PerformanceBarChartView), new PropertyMetadata(Brushes.Red));
        public static readonly DependencyProperty ButtonsGridMarginProperty =
            DependencyProperty.Register("ButtonsGridMargin", typeof(Thickness), typeof(PerformanceBarChartView), new PropertyMetadata(new Thickness(0)));
        public static readonly DependencyProperty AxisXMinorCountProperty =
          DependencyProperty.Register("AxisXMinorCount", typeof(int), typeof(PerformanceBarChartView), new PropertyMetadata(1));
        public static readonly DependencyProperty AxisXLabelFormatStringProperty =
           DependencyProperty.Register("AxisXLabelFormatString", typeof(string), typeof(PerformanceBarChartView), new PropertyMetadata("d"));

        public Thickness DateBorderMargin {
            get { return (Thickness)GetValue(DateBorderMarginProperty); }
            set { SetValue(DateBorderMarginProperty, value); }
        }
        public Thickness SalesVolumesMargin {
            get { return (Thickness)GetValue(SalesVolumesMarginProperty); }
            set { SetValue(DateBorderMarginProperty, value); }
        }
        public SolidColorBrush AreaAndSalesVolumesBrush {
            get { return (SolidColorBrush)GetValue(AreaAndSalesVolumesBrushProperty); }
            set { SetValue(AreaAndSalesVolumesBrushProperty, value); }
        }
        public Thickness ButtonsGridMargin {
            get { return (Thickness)GetValue(ButtonsGridMarginProperty); }
            set { SetValue(ButtonsGridMarginProperty, value); }
        }
        public int AxisXMinorCount {
            get { return (int)GetValue(AxisXMinorCountProperty); }
            set { SetValue(AxisXMinorCountProperty, value); }
        }
        public string AxisXLabelFormatString {
            get { return (string)GetValue(AxisXLabelFormatStringProperty); }
            set { SetValue(AxisXLabelFormatStringProperty, value); }
        }

        public PerformanceBarChartView() {
            InitializeComponent();
        }
    }
}
