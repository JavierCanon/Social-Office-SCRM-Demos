using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.XtraCharts.Web;
using DevExpress.Web.ASPxPivotGrid;

namespace Gestaventa.Web_Code.UI
{
    public class PageBaseChart : PageBase
    {
        /*
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Control demoToolbar = LoadControl("~/Web/CRM/Controls/WUCToolbarCharts.ascx");
            if (demoToolbar != null)
            {
                Control phContent = Form.FindControl("chartToolBar");
                if (phContent != null)
                    phContent.Controls.AddAt(0, demoToolbar);
            }
        }
        */

        public virtual WebChartControl FindWebChartControl()
        {
            Control phContent = this.Master.FindControl("ContentMain").FindControl("chartContent");

            if (phContent == null)
                return null;
            return phContent.FindControl("WebChartControl1") as WebChartControl;
        }

        public virtual ASPxPivotGrid FindPivotControl()
        {
            Control phContent = this.Master.FindControl("ContentMain").FindControl("ASPxPivotGrid1");

            if (phContent == null)
                return null;
            return phContent as ASPxPivotGrid;
        }



    }
}