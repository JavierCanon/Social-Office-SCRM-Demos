using System;
using System.Web.UI;
using DevExpress.Web;

public partial class SiteMasterBase : MasterPage {
    protected void DownloadButton_CustomJSProperties(object sender, CustomJSPropertiesEventArgs e) {
        e.Properties["cpTrialUrl"] = AssemblyInfo.DXLinkTrial;
    }
}
