using System;
using DataAccess;
using DevExpress.Web;

public partial class Products : BasePage {
    public override IRangeControl RangeControl { get { return FooterRangeControl; } }

    protected void Page_Load(object sender, EventArgs e) {
        using(ProductsProvider provider = new ProductsProvider()) {
            ProductsGridView.DataSource = provider.GetList();
            ProductsGridView.DataBind();

            if(!ProductsGridView.IsCallback) {
                Product focusedRow = ProductsGridView.GetRow(ProductsGridView.FocusedRowIndex) as Product;
                if(focusedRow != null)
                    ProductDetails.LoadContent(focusedRow.Id);
            }
        }
    }

    protected void ProductsGridView_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e) {
        if(e.DataColumn.FieldName == "UnitsInInventory" && Convert.ToInt32(e.CellValue) < 0)
            e.Cell.Font.Bold = true;
    }
}
