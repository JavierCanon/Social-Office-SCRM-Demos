using System;
using DataAccess;

public partial class Customers : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        using(CustomersProvider provider = new CustomersProvider()) {
            CustomersGridView.DataSource = provider.GetList();
            CustomersGridView.DataBind();
            if(!CustomersGridView.IsCallback) {
                Customer focusedRow = CustomersGridView.GetRow(CustomersGridView.FocusedRowIndex) as Customer;
                if(focusedRow != null)
                    CustomerDetails.LoadContent(focusedRow.Id);
            }
        }
    }

    public override IRangeControl RangeControl { get { return FooterRangeControl; } }
}
