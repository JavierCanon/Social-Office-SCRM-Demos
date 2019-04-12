using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;

public partial class HorizontalBarChart : SimpleChartControlBase {
    private AbbreviationType AbbreviationType = AbbreviationType.NoAbbreviation;

    public string CrosshairLabelPattern { 
        get { return WebChartControl.SeriesTemplate.CrosshairLabelPattern; } 
        set { WebChartControl.SeriesTemplate.CrosshairLabelPattern = value; } 
    }

    protected override WebChartControl WebChartControl { get { return ChartControl; } }

    private void CreateLegend(int length) {
        Panel firstColumn = new Panel();
        Panel secondColumn = new Panel();
        LegendHolder.Controls.Add(firstColumn);
        LegendHolder.Controls.Add(secondColumn);
        for(int i = 0; i < length; i++) {
            Panel cell = new Panel();
            Panel colorDiv = new Panel();
            colorDiv.CssClass = "colorDiv";
            colorDiv.BackColor = PaletteHelper.GetCommonPalletePointColor(i);
            LiteralControl text = new LiteralControl(GetPointName(i));
            cell.Controls.Add(colorDiv);
            cell.Controls.Add(text);
            if(i % 2 == 0)
                firstColumn.Controls.Add(cell);
            else
                secondColumn.Controls.Add(cell);
        }
    }

    protected override void AfterSetData(IEnumerable<ChartData> data) {
        if(data.Any()) {
            AbbreviationType = data.Max(x => x.Value) > 1000000 ? AbbreviationType.Millions : AbbreviationType.Thousands;
            LegendHolder.Visible = ShowLegend;
            if(ShowLegend)
                CreateLegend(data.Count());
        }
    }

    protected void ChartControl_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e) {
        if(e.Item.Axis is AxisY)
            e.Item.Text = ScaleHelper.GetCurrencyAbbreviationMask(e.Item.Text, AbbreviationType);
    }
}
