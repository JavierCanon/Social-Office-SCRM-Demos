using System;
using System.Collections.Generic;
using DataAccess;

public partial class RevenueFooter : UserControlBase {

    public string Title { get; set; }

    protected void Page_Load(object sender, EventArgs e) {
        DoughnutChart.Title = Title;
        HorizontalBarChart.SubTitle = Title + " BY RANGE";
        HorizontalBarChart.Title = DateTimeHelper.GetDateRangeString(GetSalesStartDate(), GetSalesEndDate());
    }

    public void SetData(IEnumerable<ChartData> data) {
        DoughnutChart.SetData(data);
        HorizontalBarChart.SetData(data);
    }
}
