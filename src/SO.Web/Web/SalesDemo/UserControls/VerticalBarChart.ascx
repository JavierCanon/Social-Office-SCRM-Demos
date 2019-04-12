<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VerticalBarChart.ascx.cs" Inherits="VerticalBarChart" %>
<%@ Register Src="~/Web/SalesDemo/UserControls/Common/DateSelectorControl.ascx" TagPrefix="uc" TagName="DateSelectorControl" %>
<dx:ASPxCallbackPanel ID="VerticalBarCallbackPanel" OnCallback="VerticalBarCallbackPanel_Callback"
    runat="server" EnableCallbackAnimation="true" ClientIDMode="AutoID">
    <SettingsLoadingPanel Enabled="false" />
    <PanelCollection>
        <dx:PanelContent>
            <span class="verticalBarChartTitle"><%= Title %></span>
            <div class="verticalBarChartHeader">
                <div>
                    <div class="verticalBarChartHeaderTitle">
                        <%= CurrentTitle %>
                    </div>
                    <div>
                        <%= string.Format(TitleFormatString, CurrentValue) %>
                    </div>
                </div>
                <div>
                    <div class="verticalBarChartHeaderTitle">
                        <%= PreviousTitle %>
                    </div>
                    <div>
                        <%= string.Format(TitleFormatString, PreviousValue) %>
                    </div>
                </div>
                <div>
                    <div class="verticalBarChartHeaderTitle">
                        <%= CustomPeriodTitle %>
                    </div>
                    <div>
                        <%= string.Format(TitleFormatString, CustomPeriodValue) %>
                    </div>
                </div>
                <div class="dateSelectorHolder">
                    <uc:DateSelectorControl runat="server" ID="DateSelectorControl" />
                </div>
            </div>
            <div class="verticalChart" style="margin-left: <%=ChartOffsetX%>px;">
                <dxchartsui:WebChartControl ID="VerticalChartControl" ClientIDMode="AutoID" SeriesDataMember="SeriesName" OnBoundDataChanged="VerticalChartControl_BoundDataChanged" OnCustomDrawAxisLabel="VerticalChartControl_CustomDrawAxisLabel" runat="server" Height="200px" CrosshairEnabled="True" Width="600px" BackColor="Transparent">
                    <padding bottom="0" left="0" right="0" top="0" />
                    <borderoptions visibility="False" />
                    <fillstyle fillmode="Empty">
    </fillstyle>
                    <diagramserializable>
        <dxcharts:XYDiagram>
            <axisx visibleinpanesserializable="-1" color="224, 224, 224">
                <scalebreakoptions color="Transparent" /><tickmarks minorvisible="False" visible="False" />
                <label enableantialiasing="True" font="Segoe UI, 13px">
                </label>
            </axisx>
            <axisy visibleinpanesserializable="-1" color="Transparent">
                <tickmarks minorvisible="False" visible="False" />
                <label enableantialiasing="True" font="Segoe UI, 13px">
                </label>
                <visualrange autosidemargins="True" />
                <wholerange autosidemargins="True" />
                
            <gridlines color="224,224,224"></gridlines></axisy>
            <margins bottom="0" left="0" right="0" top="0" />
            <defaultpane bordervisible="False" backcolor="Transparent">
            </defaultpane>
        </dxcharts:XYDiagram>
    </diagramserializable>
                    <legend visibility="False"></legend>
                    <seriestemplate crosshairlabelvisibility="True" argumentdatamember="PointName" valuedatamembersserializable="Value">
        <viewserializable>
                          <dxcharts:SideBySideBarSeriesView Color="128, 219, 219, 219">
                              <border visibility="False" />
                              <fillstyle fillmode="Solid">
                              </fillstyle>
                          </dxcharts:SideBySideBarSeriesView>
                      </viewserializable>
    </seriestemplate>
                </dxchartsui:WebChartControl>
            </div>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
