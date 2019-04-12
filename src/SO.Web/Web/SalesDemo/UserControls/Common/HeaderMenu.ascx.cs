using System;
using System.Web.UI;
using MenuItem = DevExpress.Web.MenuItem;

public partial class HeaderMenu : UserControl {
    protected void Page_Load(object sender, EventArgs e) {
        mainMenu.DataBind();
        if(mainMenu.SelectedItem != null && mainMenu.SelectedItem.Parent != mainMenu.RootItem)
            mainMenu.SelectedItem.Parent.Text = string.Format("{0}: {1}", mainMenu.SelectedItem.Parent, mainMenu.SelectedItem.Text);

        MenuItem helpMenuItem = mainMenu.Items.Add("?", "helpMenuItem");
        helpMenuItem.ItemStyle.CssClass = "helpMenuItem";
    }
}
