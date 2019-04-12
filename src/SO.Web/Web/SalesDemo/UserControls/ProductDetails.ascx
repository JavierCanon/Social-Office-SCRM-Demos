<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductDetails.ascx.cs" Inherits="ProductDetails" %>
<%@ Register Src="~/Web/SalesDemo/UserControls/DoughnutChart.ascx" TagPrefix="uc1" TagName="DoughnutChart" %>
<dx:ASPxCallbackPanel ID="ProductDetailsCallbackPanel" EnableCallbackAnimation="true" runat="server">
    <ClientSideEvents Init="OnDataDependentControlInit" />
    <PanelCollection>
        <dx:PanelContent>
            <div class="productDetailsHolder">
                <div class="productDetails">
                    <h5>Manufacturing Plant</h5>
                    <p class="description">
                        <dx:ASPxLabel ID="PlantName" runat="server" CssClass="descriptionLabel">
                        </dx:ASPxLabel>
                        <dx:ASPxLabel ID="PlantAddress" runat="server" CssClass="descriptionLabel">
                        </dx:ASPxLabel>
                        <dx:ASPxLabel ID="PlantCityInfo" runat="server" CssClass="descriptionLabel">
                        </dx:ASPxLabel>
                    </p>
                    <h5>Project Manager</h5>
                    <p class="description">
                        <dx:ASPxLabel ID="PMName" runat="server" CssClass="descriptionLabel">
                        </dx:ASPxLabel>
                        <dx:ASPxLabel ID="PMAddress" runat="server" CssClass="descriptionLabel">
                        </dx:ASPxLabel>
                        <dx:ASPxLabel ID="PMCityInfo" runat="server" CssClass="descriptionLabel">
                        </dx:ASPxLabel>
                        <dx:ASPxLabel ID="PMPhone" runat="server" CssClass="descriptionLabel">
                        </dx:ASPxLabel>
                        <dx:ASPxLabel ID="PMEmail" runat="server" CssClass="descriptionLabel">
                        </dx:ASPxLabel>
                    </p>
                    <h5>Support Manager</h5>
                    <p class="description" style="margin-bottom: 0px;">
                        <dx:ASPxLabel ID="SupportName" runat="server" CssClass="descriptionLabel">
                        </dx:ASPxLabel>
                        <dx:ASPxLabel ID="SupportAddress" runat="server" CssClass="descriptionLabel">
                        </dx:ASPxLabel>
                        <dx:ASPxLabel ID="SupportCityInfo" runat="server" CssClass="descriptionLabel">
                        </dx:ASPxLabel>
                        <dx:ASPxLabel ID="SupportPhone" runat="server" CssClass="descriptionLabel">
                        </dx:ASPxLabel>
                        <dx:ASPxLabel ID="SupportEmail" runat="server" CssClass="descriptionLabel">
                        </dx:ASPxLabel>
                    </p>
                </div>
                <div class="productDetailsChartsHolder">
                        <uc1:DoughnutChart runat="server" ID="RevenueBySectorChart" Title="REVENUE" SubTitle="BY SECTOR" Width="320px" Height="300px" />
                        <uc1:DoughnutChart runat="server" ID="RevenueByRegionChart" Title="REVENUE" SubTitle="BY REGION" Width="320px" Height="300px" />
                        <uc1:DoughnutChart runat="server" ID="RevenueByChannelChart" Title="REVENUE" SubTitle="BY CHANNEL" Width="320px" Height="300px" />
                </div>
                <div class="clear"></div>
            </div>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
