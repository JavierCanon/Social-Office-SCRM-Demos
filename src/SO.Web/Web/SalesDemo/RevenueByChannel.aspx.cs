using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using DevExpress.Utils;
using DevExpress.XtraCharts;

public partial class RevenueByChannel : BasePage {
    public override IRangeControl RangeControl { get { return FooterRangeControl; } }
    public List<ChartData> ChannelsRevenue { get; set; }
    private DateTime EarliestDateTime { get; set; }

    protected void Page_Init(object sender, EventArgs e) {
        PaletteHelper.LoadCommonPalette(ChartControl);
    }
    protected void Page_Load(object sender, EventArgs e) {
        DateSelectorControl.CallbackPanelId = DailyRevenueCallbackPanel.ClientID;
        ProductSalesRevenue.SetData(SalesProvider.GetSalesGroupedByChannel(SalesStartDate, SalesEndDate));
        if(!IsCallback)
            PopulateDailySalesData();
    }
    private void PopulateDailySalesData() {
        List<RangeChartData> data = SalesProvider.GetDailySalesGroupedByChannel(DateSelectorControl.CurrentDate).ToList();
        ChannelsRevenue = data.GroupBy(d => d.SeriesName).Select(item =>
            new ChartData() {
                Value = item.Sum(x => x.Value), PointName = item.Key
            }).ToList();
        EarliestDateTime = data.Min(x => x.Argument);
        ChartControl.DataSource = data;
        ChartControl.DataBind();
    }

    protected void DailyRevenueCallbackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e) {
        int delta = 0;
        if(Int32.TryParse(e.Parameter, out delta) && delta != 0) {
            DateSelectorControl.ChangeDate(delta);
            PopulateDailySalesData();
        }
    }
    protected void ChartControl_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e) {
        if(e.Item.Axis is AxisX && (DateTime)e.Item.AxisValue == EarliestDateTime)
            e.Item.Text = "";
    }
}
