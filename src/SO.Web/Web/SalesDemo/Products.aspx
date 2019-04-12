<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" MasterPageFile="~/SiteBase.master" Inherits="Products" %>
<%@ Register Src="~/Web/SalesDemo/UserControls/ProductDetails.ascx" TagPrefix="uc1" TagName="ProductDetails" %>
<asp:Content runat="server" ContentPlaceHolderID="PageTitlePartPlaceHolder">Products</asp:Content>
<asp:Content ID="ContentHolder" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    <h1>PRODUCTS</h1>
    <dx:ASPxGridView ID="ProductsGridView" runat="server" CssClass="gridView" AutoGenerateColumns="False"
        KeyFieldName="Id" Width="100%" OnHtmlDataCellPrepared="ProductsGridView_HtmlDataCellPrepared" KeyboardSupport="true">
        <Styles Header-CssClass="gridViewHeader" Row-CssClass="gridViewRow" FocusedRow-CssClass="gridViewRowFocused" 
            RowHotTrack-CssClass="gridViewRow" FilterRow-CssClass="gridViewFilterRow" />
        <Columns>
            <dx:GridViewDataTextColumn FieldName="Name" Width="15%" />
            <dx:GridViewDataTextColumn FieldName="Description" Width="30%" />
            <dx:GridViewDataTextColumn FieldName="BaseCost" HeaderStyle-HorizontalAlign="Right" Width="10%">
                <PropertiesTextEdit DisplayFormatString="c0">
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ListPrice" HeaderStyle-HorizontalAlign="Right" Width="10%">
                <PropertiesTextEdit DisplayFormatString="c0">
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="UnitsInInventory" HeaderStyle-HorizontalAlign="Right" Width="15%">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="UnitsInManufacturing" HeaderStyle-HorizontalAlign="Right" Width="20%">
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsBehavior EnableRowHotTrack="True" AllowFocusedRow="True" AllowClientEventsOnLoad="false" />
        <SettingsPager PageSize="10">
            <NextPageButton Visible="False">
            </NextPageButton>
            <PrevPageButton Visible="False">
            </PrevPageButton>
            <Summary Visible="False" />
        </SettingsPager>
        <Settings ShowGroupPanel="False" GridLines="None" />
        <ClientSideEvents Init="function(s,e){ s.Focus(); }" FocusedRowChanged="function (s, e) { 
                DataDependentControlHelper.UpdateControls(s); 
            }" />
    </dx:ASPxGridView>
</asp:Content>
<asp:Content ID="BottomContent" runat="server" ContentPlaceHolderID="BottomContentPlaceHolder">
    <uc1:ProductDetails runat="server" ID="ProductDetails" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="FooterRangeControlPlaceHolder" runat="Server">
    <div class="contentBox salesDateRangeContainer">
        <uc:FooterRangeControl runat="server" ID="FooterRangeControl" />
    </div>
</asp:Content>
<asp:Content ID="HelpMenu" ContentPlaceHolderID="HelpMenuDescriptionPlaceHolder" runat="server">
    <p><a target="_blank" href="https://documentation.devexpress.com/#AspNet/CustomDocument5823">Grid View</a> - used to display products stored in the database. Sort values by clicking individual column headers.</p>
    <p><a target="_blank" href="https://documentation.devexpress.com/#AspNet/clsDevExpressXtraChartsWebWebChartControltopic">Pie Charts</a> - used to communicate the state of revenues by sector, region and sales channel.</p>
    <p><a target="_blank" href="https://documentation.devexpress.com/#AspNet/clsDevExpressWebASPxEditorsASPxTrackBartopic">Track Bar</a> – used to visually specify a date range for sales information displayed within the charts.</p>
</asp:Content>
