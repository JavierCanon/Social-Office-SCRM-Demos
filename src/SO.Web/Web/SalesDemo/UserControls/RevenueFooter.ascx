<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RevenueFooter.ascx.cs" Inherits="RevenueFooter" %>
<%@ Register Src="~/Web/SalesDemo/UserControls/HorizontalBarChart.ascx" TagPrefix="uc" TagName="HorizontalBarChart" %>
<%@ Register Src="~/Web/SalesDemo/UserControls/DoughnutChart.ascx" TagPrefix="uc" TagName="DoughnutChart" %>
<dx:ASPxCallbackPanel ID="RevenueFooterCallbackPanel" runat="server" EnableCallbackAnimation="true">
    <SettingsLoadingPanel Enabled="false" />
    <ClientSideEvents Init="OnDataDependentControlInit" />
    <PanelCollection>
        <dx:PanelContent>
            <div class="revenueBottom">
                <div class="revenuePieChart">
                    <uc:DoughnutChart runat="server" ID="DoughnutChart" Width="320px" Height="320px" TopCenter="141px" SubTitle="BY RANGE" ShowLegend="false" />
                </div>
                <div class="revenueHorChart">
                    <uc:HorizontalBarChart runat="server" ID="HorizontalBarChart" CrosshairLabelPattern="{A}: {V:c2}" ShowLegend="false" Height="220px" Width="840px" />
                </div>
            </div>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
