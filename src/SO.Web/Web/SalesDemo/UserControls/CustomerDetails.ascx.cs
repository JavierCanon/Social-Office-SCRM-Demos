using System;
using DataAccess;
using DataContext;

public partial class CustomerDetails : UserControlBase {

    public Location Location { get; private set; }

    public void LoadContent(int customerId) {
        DateTime startDate = GetSalesStartDate();
        DateTime endDate = GetSalesEndDate();

        HorizontalBarChart.Title = DateTimeHelper.GetDateRangeString(startDate, endDate);
        HorizontalBarChart.SetData(SalesProvider.GetCustomerPurchasesGroupedByProduct(customerId, startDate, endDate));
        using(CustomersProvider provider = new CustomersProvider())
            Location = provider.GetCustomerLocation(customerId);
        CustomerDetailsHolder.Visible = true;
        EmptyDataMessageHolder.Visible = false;
    }
}
