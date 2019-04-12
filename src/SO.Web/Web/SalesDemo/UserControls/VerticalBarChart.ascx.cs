using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using DataAccess;
using DevExpress.Web;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;

public partial class VerticalBarChart : VerticalChartControlBase {

    public bool IsCurrency { get; set; }

    public int ChartOffsetX { get; set; }

    public string CurrentTitle { get; set; }
    public string PreviousTitle { get; set; }
    public string CustomPeriodTitle { get; set; }

    public string TitleFormatString { get; set; }
    public string RangeSelectorFormatString { get; set; }
    public string CrosshairFormatString {
        get { return WebChartControl.SeriesTemplate.CrosshairLabelPattern; }
        set { WebChartControl.SeriesTemplate.CrosshairLabelPattern = value; }
    }
    public SelectionInterval SelectionInterval {
        get { return DateSelectorControl.SelectionInterval; }
        set { DateSelectorControl.SelectionInterval = value; }
    }
    public Unit Width {
        get { return VerticalBarCallbackPanel.Width; }
        set { VerticalBarCallbackPanel.Width = value; }
    }
    public Unit Height {
        get { return VerticalBarCallbackPanel.Height; }
        set { VerticalBarCallbackPanel.Height = value; }
    }
    protected override WebChartControl WebChartControl { get { return VerticalChartControl; } }

    protected void Page_Load(object sender, EventArgs e) {
        DateSelectorControl.CallbackPanelId = VerticalBarCallbackPanel.ClientID;
        if(ChartOffsetX < 0)
            VerticalBarCallbackPanel.Paddings.PaddingLeft = Unit.Pixel(-ChartOffsetX);
    }

    public override DateTime GetSelectedDate() {
        return DateSelectorControl.CurrentDate;
    }

    public override void SetChartData(List<ChartData> current, List<ChartData> previous) {
        foreach(var item in current)
            item.SeriesName = CurrentSeriesName;
        foreach(var item in previous)
            item.SeriesName = PreviousSeriesName;
        WebChartControl.DataSource = previous.Union(current).ToList();
        WebChartControl.DataBind();
    }

    protected void VerticalBarCallbackPanel_Callback(object sender, CallbackEventArgsBase e) {
        int delta = 0;
        if(Int32.TryParse(e.Parameter, out delta) && delta != 0) {
            DateSelectorControl.ChangeDate(delta);
            RaiseRangeSelectionChanged();
        }
    }
    protected void VerticalChartControl_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e) {
        if(e.Item.Axis is AxisY) {
            if(IsCurrency)
                e.Item.Text = ScaleHelper.GetCurrencyAbbreviationMask(e.Item.Text, AbbreviationType.Thousands);
            else
                e.Item.Text = ScaleHelper.GetAbbreviationMask(e.Item.Text, AbbreviationType.Thousands);
        }
    }
    protected void VerticalChartControl_BoundDataChanged(object sender, EventArgs e) {
        Series currentSeries = WebChartControl.GetSeriesByName(CurrentSeriesName);
        if (currentSeries != null)
            ((SideBySideBarSeriesView)currentSeries.View).ColorEach = true;
    }
}

