<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteBase.master" CodeBehind="RevenueByChannel.aspx.cs" Inherits="RevenueByChannel" %>
<%@ Register Src="~/Web/SalesDemo/UserControls/VerticalBarChart.ascx" TagPrefix="uc" TagName="VerticalBarChart" %>
<%@ Register Src="~/Web/SalesDemo/UserControls/RevenueFooter.ascx" TagPrefix="uc" TagName="RevenueFooter" %>
<%@ Register Src="~/Web/SalesDemo/UserControls/Common/DateSelectorControl.ascx" TagPrefix="uc" TagName="DateSelectorControl" %>
<asp:Content runat="server" ContentPlaceHolderID="PageTitlePartPlaceHolder">Revenue By Channel</asp:Content>
<asp:Content ID="ContentHolder" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    <dx:ASPxCallbackPanel ID="DailyRevenueCallbackPanel" OnCallback="DailyRevenueCallbackPanel_Callback" runat="server" ClientIDMode="AutoID" EnableCallbackAnimation="true">
        <PanelCollection>
            <dx:PanelContent>
                <div class="verticalAlignTop revenueByChannelContent">
                    <div>
                        <div class="verticalAlignTop horizontalAlignLeft">
                            <h1>REVENUE BY SALES CHANNEL</h1>
                        </div>
                        <div class="verticalAlignTop horizontalAlignRight">
                            <div class="revenueByChannelLegend">
                                <% for(int i = 0; i < ChannelsRevenue.Count; i++) { %>
                                <div class="color" style="background-color: <%= PaletteHelper.GetCommonPalletePointCssColor(i)%>;"></div>
                                <div class="field"><%=ChannelsRevenue[i].PointName %></div>
                                <%  } %>
                            </div>
                        </div>
                        <div class="clear"></div>
                    </div>
                    <div>
                        <div class="verticalAlignTop horizontalAlignLeft">
                            <div class="revenue-title">
                                DAILY SUMMARY
                            </div>
                            <div class="revenueChannelList">
                                <% for(int i = 0; i < ChannelsRevenue.Count; i++) { %>
                                <div class="row">
                                    <div class="name"><%=ChannelsRevenue[i].PointName %></div>
                                    <div><%=String.Format("{0:C0}", ChannelsRevenue[i].Value)%></div>
                                </div>
                                <%  } %>
                                <div class="row total">
                                    <div class="name">Total</div>
                                    <div><%=String.Format("{0:C0}", ChannelsRevenue.Sum(x=>x.Value))%></div>
                                </div>
                            </div>
                        </div>
                        <div class="verticalAlignTop horizontalAlignRight">
                            <div class="revenue-title revenueByChannelChartTitle horizontalAlignLeft">
                                DAILY SALES PERFORMANCE
                            </div>
                            <div class="horizontalAlignRight">
                                <uc:DateSelectorControl runat="server" ID="DateSelectorControl" SelectionInterval="Day" />
                            </div>
                            <div>
                                <dxchartsui:WebChartControl ID="ChartControl" SeriesDataMember="SeriesName" runat="server" Height="220px" CrosshairEnabled="False"
                                    Width="864px"
                                    BackColor="Transparent" SideBySideBarDistanceVariable="100" OnCustomDrawAxisLabel="ChartControl_CustomDrawAxisLabel">
                                    <padding bottom="0" left="0" right="0" top="0" />
                                    <borderoptions visibility="False" color="255, 192, 192" />
                                    <diagramserializable>
        <dxcharts:XYDiagram>
            <axisx color="Transparent" visibleinpanesserializable="-1">
                <tickmarks minorvisible="False" visible="False" />
                <label enableantialiasing="True" font="Segoe UI, 13px" textpattern="{V:t}">
                    <resolveoverlappingoptions allowhide="False" allowrotate="False" allowstagger="False" />
                </label>
                <visualrange autosidemargins="True" />
                <wholerange autosidemargins="True" />
                <gridlines visible="True">
                </gridlines>
                <datetimescaleoptions autogrid="False" gridalignment="Hour" scalemode="Continuous">
                </datetimescaleoptions>
            </axisx>
            <axisy color="Transparent" visibleinpanesserializable="-1">
                <tickmarks minorvisible="False" visible="False" />
                <label enableantialiasing="True" font="Segoe UI, 13px" textpattern="{A:0,}K">
                </label>
            </axisy>
            <defaultpane backcolor="Transparent" bordercolor="224, 224, 224">
            </defaultpane>
        </dxcharts:XYDiagram>
    </diagramserializable>
                                    <legend visibility="False"></legend>
                                    <seriestemplate argumentdatamember="Argument" argumentscaletype="DateTime" valuedatamembersserializable="Value">
        <viewserializable>
            <dxcharts:LineSeriesView MarkerVisibility="False">
            </dxcharts:LineSeriesView>
        </viewserializable>
    </seriestemplate>
                                </dxchartsui:WebChartControl>
                            </div>
                        </div>
                        <div class="clear"></div>
                    </div>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</asp:Content>
<asp:Content ID="BottomContent" runat="server" ContentPlaceHolderID="BottomContentPlaceHolder">
    <uc:RevenueFooter runat="server" ID="ProductSalesRevenue" Title="CHANNEL SALES" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="FooterRangeControlPlaceHolder" runat="Server">
    <div class="contentBox salesDateRangeContainer">
        <uc:FooterRangeControl runat="server" ID="FooterRangeControl" />
    </div>
</asp:Content>
<asp:Content ID="HelpMenu" ContentPlaceHolderID="HelpMenuDescriptionPlaceHolder" runat="server">
    <p><a target="_blank" href="https://documentation.devexpress.com/#AspNet/clsDevExpressXtraChartsWebWebChartControltopic">Doughnut and Bar Charts</a> - used to compare revenues generated by each sales channel.</p>
    <p><a target="_blank" href="https://documentation.devexpress.com/#AspNet/clsDevExpressWebASPxEditorsASPxTrackBartopic">Track Bar</a> – used to visually specify a date range for sales information displayed within the charts.</p>
</asp:Content>
