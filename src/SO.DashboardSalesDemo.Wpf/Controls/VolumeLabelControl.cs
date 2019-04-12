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
using DevExpress.Mvvm.UI;
using SO.SalesDemo.Model;
using SO.DashboardSalesDemo.Wpf.Converters;
using DevExpress.Xpf.Charts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SO.DashboardSalesDemo.Wpf.Controls {
    public class VolumeLabelControl : ItemsControl {
        public static readonly DependencyProperty PaletteProperty =
            DependencyProperty.Register("Palette", typeof(CustomPalette), typeof(VolumeLabelControl), 
            new PropertyMetadata(null));
        public static readonly DependencyProperty LabelForegroundProperty =
            DependencyProperty.Register("LabelForeground", typeof(Brush), typeof(VolumeLabelControl), 
            new PropertyMetadata(null));
        public CustomPalette Palette {
            get { return (CustomPalette)GetValue(PaletteProperty); }
            set { SetValue(PaletteProperty, value); }
        }
        public Brush LabelForeground {
            get { return (Brush)GetValue(LabelForegroundProperty); }
            set { SetValue(LabelForegroundProperty, value); }
        }

        public VolumeLabelControl() {
            DefaultStyleKey = typeof(VolumeLabelControl);
        }
        public int IndexOf(object item) {
            if(item is VolumeLabelItem) {
                return ItemContainerGenerator.IndexFromContainer((VolumeLabelItem)item);
            }
            return Items.IndexOf(item);
        }
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
            base.PrepareContainerForItemOverride(element, item);
            if(element is VolumeLabelItem)
                ((VolumeLabelItem)element).Initialize(this);
        }
        protected override DependencyObject GetContainerForItemOverride() {
            return new VolumeLabelItem();
        }
    }
    public class VolumeLabelItem : HeaderedContentControl {
        public const string ElementName_PART_Label = "PART_Label";
        VolumeLabelControl Owner;
        PaletteToBrushConverter PaletteToBrushConverter;
        public VolumeLabelItem() {
            DefaultStyleKey = typeof(VolumeLabelItem);
            PaletteToBrushConverter = new PaletteToBrushConverter();
        }
        public void Initialize(VolumeLabelControl owner) {
            Owner = owner;
            PaletteToBrushConverter.Palette = Owner.Palette;
        }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            if(Owner == null) return;
            TextBlock labelElement = (TextBlock)GetTemplateChild(ElementName_PART_Label);
            if(Owner.LabelForeground != null)
                labelElement.Foreground = Owner.LabelForeground;
            else if(Owner.Palette != null)
                labelElement.Foreground = (Brush)PaletteToBrushConverter.Convert(Owner.IndexOf(this), typeof(Brush), null, null);
            else
                labelElement.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
