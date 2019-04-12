using System;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI.WebControls;
using DataAccess;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Native;
using System.Linq;
using System.Collections.Generic;

public partial class Chart : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        DateTime startDate = DateTime.Parse(Request.QueryString["start"]);
        DateTime endDate = DateTime.Parse(Request.QueryString["end"]);

        RangeChartControl.Width = Unit.Parse(Request.QueryString["w"].ToString());
        RangeChartControl.Height = Unit.Parse(Request.QueryString["h"].ToString());

        RangeChartControl.DataSource = SalesProvider.GetRangeChartData(startDate, endDate).ToList();
        RangeChartControl.DataBind();
        Response.ContentType = "image/png";
        Response.Clear();

        using(MemoryStream stream = new MemoryStream()) {
            ((IChartContainer)RangeChartControl).Chart.ExportToImage(stream, ImageFormat.Png);
            stream.Position = 0;
            stream.CopyTo(Response.OutputStream);
        }
    }
}
