using System;
using DataAccess;
using DevExpress.Web;

public partial class ProductDetails : UserControlBase {
    private string CityInfoFormatString = "{0}, {1} {2}";

    public void LoadContent(int productId) {
        using(ProductsProvider productsProvider = new ProductsProvider()) {
            Contact projectManager = productsProvider.GetProjectManager(productId);
            Contact supportManager = productsProvider.GetSupportManager(productId);
            Plant plant = productsProvider.GetPlant(productId);

            FillPlantInfo(plant);
            FillSupportManagerInfo(supportManager);
            FillProjectManagerInfo(projectManager);
        }
        DateTime startDate = GetSalesStartDate();
        DateTime endDate = GetSalesEndDate();
        RevenueByChannelChart.SetData(SalesProvider.GetSalesGroupedByChannel(productId, startDate, endDate));
        RevenueByRegionChart.SetData(SalesProvider.GetSalesGroupedByRegion(productId, startDate, endDate));
        RevenueBySectorChart.SetData(SalesProvider.GetSalesGroupedBySector(productId, startDate, endDate));
    }
    private void FillPlantInfo(Plant model) {
        PlantName.Text = model.Name;
        PlantAddress.Text = model.Address;
        PlantCityInfo.Text = string.Format(CityInfoFormatString, model.City, model.State, model.Zip);
    }
    private void FillSupportManagerInfo(Contact model) {
        SupportName.Text = model.FullName;
        SupportAddress.Text = model.Address;
        SupportEmail.Text = model.Email;
        SupportPhone.Text = model.Phone;
        if(HasCityInfo(model))
            SupportCityInfo.Text = string.Format(CityInfoFormatString, model.City, model.State, model.Zip);
    }
    private void FillProjectManagerInfo(Contact model) {
        PMName.Text = model.FullName;
        PMAddress.Text = model.Address;
        PMEmail.Text = model.Email;
        PMPhone.Text = model.Phone;
        if(HasCityInfo(model))
            PMCityInfo.Text = string.Format(CityInfoFormatString, model.City, model.State, model.Zip);
    }
    private bool HasCityInfo(Contact model) {
        return !string.IsNullOrEmpty(model.City) && !string.IsNullOrEmpty(model.State) && !string.IsNullOrEmpty(model.Zip);
    }
}
