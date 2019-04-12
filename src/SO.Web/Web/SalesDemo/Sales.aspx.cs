using System;
using System.Web.UI;
using System.Linq;
using DataAccess;
using DevExpress.Web;
using DevExpress.Web.ASPxPivotGrid;

public partial class Sales : BasePage {
    public override IRangeControl RangeControl { get { return FooterRangeControl; } }

    protected void SalesPivotGrid_Init(object sender, EventArgs e) {
        (sender as ASPxPivotGrid).CellTemplate = new CellTemplate();
    }

    protected void SalesDataSource_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e) {
        e.QueryableSource = SalesProvider.DataTable.Where(s => s.SaleDate >= SalesStartDate && s.SaleDate <= SalesEndDate);
    }
}
class CellTemplate : ITemplate {
    void ITemplate.InstantiateIn(Control container) {
        PivotGridCellTemplateContainer templateContainer = (PivotGridCellTemplateContainer)container;
        DevExpress.Web.ASPxPivotGrid.PivotGridField field = templateContainer.DataField;

        if(field != null && field.ID == "fieldTotalCostPercents") {
            ASPxProgressBar bar = new ASPxProgressBar();
            bar.CssClass = "pvProgressBar";
            bar.IndicatorStyle.CssClass = "indicator";
            bar.Position = 100 * Convert.ToDecimal(templateContainer.Value);
            bar.DisplayFormatString = "0.#";
            // The settings are defined in the Init event to override ASPxProgressBar skin's settings specified in the MetropolisBlue theme
            bar.Init += (s, e) => { (s as ASPxProgressBar).ShowPosition = true; };
            templateContainer.Controls.Add(bar);
        } else
            templateContainer.Controls.Add(new LiteralControl(templateContainer.Text));
    }
}
