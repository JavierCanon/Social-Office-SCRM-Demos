using System;
using System.Collections.Generic;
using DataAccess;

public partial class RevenueBySector : RevenueBasePage {
    protected override VerticalChartControlBase DailySalesPerformanceVerticalChart { get { return DailySalesPerformanceChart; } }
    protected override VerticalChartControlBase UnitSalesVerticalChart { get { return UnitSalesChart; } }


    protected override IEnumerable<ChartData> GetChartSalesCountCollection(DateTime startDate, DateTime endDate) {
        return SalesProvider.GetSaleCountGroupedBySector(startDate, endDate);
    }
    protected override IEnumerable<ChartData> GetChartSalesRevenueCollection(DateTime startDate, DateTime endDate) {
        return SalesProvider.GetSalesGroupedBySector(startDate, endDate);
    }
    protected void Page_Load(object sender, EventArgs e) {
        ProductSalesRevenue.SetData(SalesProvider.GetSalesGroupedBySector(SalesStartDate, SalesEndDate));
    }

    public override IRangeControl RangeControl { get { return FooterRangeControl; } }
}
