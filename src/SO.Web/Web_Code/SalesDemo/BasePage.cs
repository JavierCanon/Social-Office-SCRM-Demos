using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

public class BasePage : System.Web.UI.Page {
    protected const string CreateDatabasePageUrl = "~/"; // i.e. default.aspx
    protected const string StartPage = "~/RevenueBySector.aspx";

    static bool? fIsDatabasePopulated = null;
    public static bool IsDatabasePopulated {
        get {
            if(!fIsDatabasePopulated.HasValue)
                fIsDatabasePopulated = DataContext.SalesContext.IsDatabasePopulated();
            return fIsDatabasePopulated == true;
        }
        set { fIsDatabasePopulated = value; }
    }
    public static bool IsDatabasePopulating { get; set; }

    protected virtual bool IsPopulateDatabasePage { get { return false; } }

    private SalesProvider fSalesProvider;
    protected internal SalesProvider SalesProvider {
        get {
            if(fSalesProvider == null)
                fSalesProvider = new SalesProvider();
            return fSalesProvider;
        }
    }

    protected override void OnPreInit(EventArgs e) {
        base.OnPreInit(e);
        // Populate Database if necessary
        if(!IsCallback) {
            if(!IsDatabasePopulated) {
                if(!IsPopulateDatabasePage)
                    Response.Redirect(CreateDatabasePageUrl);
            } else if(IsPopulateDatabasePage)
                Response.Redirect(StartPage);
        }
    }

    protected override void OnUnload(EventArgs e) {
        base.OnUnload(e);
        if(SalesProvider != null)
            SalesProvider.Dispose();
    }

    public virtual IRangeControl RangeControl { get { return null; } }

    public DateTime SalesStartDate {
        get { return RangeControl != null ? RangeControl.GetStartDate() : DateTime.MinValue; }
    }
    public DateTime SalesEndDate {
        get { return RangeControl != null ? RangeControl.GetEndDate() : DateTime.MaxValue; }
    }
}
