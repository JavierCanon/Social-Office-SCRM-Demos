using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DataAccess;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;

public partial class DoughnutChart : SimpleChartControlBase {

    public Unit TopCenter { get; set; }
    public int DataSourceLength { get; private set; }
    public override bool ShowLegend {
        get { return LegendHolder.Visible; }
        set { LegendHolder.Visible = value; }
    }

    protected override WebChartControl WebChartControl { get { return ChartControl; } }

    protected void Page_Load(object sender, EventArgs e) {
        if(TopCenter.IsEmpty)
            TopCenter = Unit.Pixel(131);
    }

    protected override void AfterSetData(IEnumerable<ChartData> data) {
        DataSourceLength = data.Count();
    }
}
