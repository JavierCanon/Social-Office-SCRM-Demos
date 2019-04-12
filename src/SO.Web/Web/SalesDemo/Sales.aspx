<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" MasterPageFile="~/SiteBase.master" Inherits="Sales" %>
<%@ Register assembly="DevExpress.Web.v18.2" namespace="DevExpress.Data.Linq" tagprefix="dx" %>
<asp:Content runat="server" ContentPlaceHolderID="PageTitlePartPlaceHolder">Sales</asp:Content>
<asp:Content ID="ContentHolder" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    <h1>SALES BY DATE RANGE</h1>
    <div>
        <dx:EntityServerModeDataSource ID="SalesDataSource" runat="server" OnSelecting="SalesDataSource_Selecting" />
        <dx:ASPxPivotGrid ID="SalesPivotGrid" runat="server" OnInit="SalesPivotGrid_Init"
            EnableRowsCache="false" Width="100%" DataSourceID="SalesDataSource">
            <ClientSideEvents Init="OnDataDependentControlInit" />
            <Fields>
                <dx:PivotGridField ID="fieldGroupName" AllowedAreas="ColumnArea" Area="ColumnArea" AreaIndex="0" Caption="Product" FieldName="Products.Name">
                </dx:PivotGridField>
                <%--Year - Month Group fields--%>
                <dx:PivotGridField ID="fieldStartOfPeriod" Area="RowArea" AllowedAreas="RowArea" AreaIndex="0" Caption="Year"
                    GroupIndex="0" InnerGroupIndex="0"
                    CellFormat-FormatType="DateTime" FieldName="SaleDate" GroupInterval="DateYear">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldStartOfPeriodMonth" Area="RowArea" AllowedAreas="RowArea" AreaIndex="1" Caption="Month"
                    GroupIndex="0" InnerGroupIndex="1"
                    CellFormat-FormatType="DateTime" FieldName="SaleDate" GroupInterval="DateMonth">
                </dx:PivotGridField>
                <%--Sales Group fields--%>
                <dx:PivotGridField ID="fieldTotalCost" Area="DataArea" AreaIndex="0" Caption="Sales" FieldName="TotalCost"
                    CellFormat-FormatType="Custom" AllowedAreas="DataArea" CellFormat-FormatString="$#,##,K" GroupIndex="1" InnerGroupIndex="0">
                </dx:PivotGridField>
                <dx:PivotGridField ID="fieldTotalCostPercents" Area="DataArea" AreaIndex="1"
                    SummaryDisplayType="PercentOfColumn" AllowedAreas="DataArea" FieldName="TotalCost" Caption="%" GroupIndex="1" InnerGroupIndex="1">
                    <CellStyle CssClass="pivotGridPercentsFieldCell" />
                </dx:PivotGridField>
            </Fields>
            <Groups>
                <dx:PivotGridWebGroup Caption="Year - Month" />
                <dx:PivotGridWebGroup Caption="Sales" />
            </Groups>
            <OptionsView ShowColumnGrandTotals="True" ShowFilterHeaders="False" ShowGrandTotalsForSingleValues="False" ShowRowTotals="True" />
            <OptionsPager PagerAlign="Center" Position="Bottom" RowsPerPage="16">
            </OptionsPager>
        </dx:ASPxPivotGrid>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="FooterRangeControlPlaceHolder" runat="Server">
    <div class="contentBox salesDateRangeContainer">
        <uc:FooterRangeControl runat="server" ID="FooterRangeControl" />
    </div>
</asp:Content>
<asp:Content ID="HelpMenu" ContentPlaceHolderID="HelpMenuDescriptionPlaceHolder" runat="server">
    <p><a target="_blank" href="https://documentation.devexpress.com/#AspNet/clsDevExpressWebASPxPivotGridASPxPivotGridtopic">Pivot Grid</a> - used for multi-dimensional analysis against sales information.</p>
    <p><a target="_blank" href="https://documentation.devexpress.com/#AspNet/clsDevExpressWebASPxEditorsASPxTrackBartopic">Track Bar</a> – used to visually specify a date range for sales information displayed within the Pivot Grid.</p>
</asp:Content>
