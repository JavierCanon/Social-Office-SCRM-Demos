using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;

public interface IRangeControl {
    DateTime GetStartDate();
    DateTime GetEndDate();
}

public class UserControlBase : UserControl {
    protected DateTime GetSalesStartDate() {
        return (Page as BasePage).SalesStartDate;
    }
    protected DateTime GetSalesEndDate() {
        return (Page as BasePage).SalesEndDate;
    }
    protected SalesProvider SalesProvider { get { return (Page as BasePage).SalesProvider; } }
}

public abstract class ChartControlBase : UserControlBase {
    public string Title { get; set; }

    protected abstract WebChartControl WebChartControl { get; }

    protected override void OnInit(EventArgs e) {
        base.OnInit(e);
        PaletteHelper.LoadCommonPalette(WebChartControl);
    }
}

public abstract class SimpleChartControlBase : ChartControlBase {
    public string SubTitle { get; set; }

    public Unit Width {
        get { return WebChartControl.Width; }
        set { WebChartControl.Width = value; }
    }
    public Unit Height {
        get { return WebChartControl.Height; }
        set { WebChartControl.Height = value; }
    }
    public virtual bool ShowLegend { get; set; }
    protected ChartData[] ChartData { get; set; }

    public void SetData(IEnumerable<ChartData> data) {
        ChartData = data.ToArray();
        WebChartControl.DataSource = ChartData;
        WebChartControl.DataBind();
        AfterSetData(ChartData);
    }

    protected virtual void AfterSetData(IEnumerable<ChartData> data) { }

    public string GetPointName(int index) {
        if(ChartData != null && index >= 0 && index < ChartData.Length)
            return ChartData[index].PointName;
        return "";
    }
}

public abstract class VerticalChartControlBase : ChartControlBase {
    public double CurrentValue { get; set; }
    public double PreviousValue { get; set; }
    public double CustomPeriodValue { get; set; }
    public string CurrentSeriesName { get; set; }
    public string PreviousSeriesName { get; set; }
    public event EventHandler<EventArgs> RangeSelectionChanged;

    public abstract void SetChartData(List<ChartData> current, List<ChartData> previous);
    public abstract DateTime GetSelectedDate();
    protected void RaiseRangeSelectionChanged() {
        if(RangeSelectionChanged != null)
            RangeSelectionChanged(this, new EventArgs());
    }
}
