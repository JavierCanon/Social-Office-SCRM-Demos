<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HorizontalBarChart.ascx.cs" Inherits="HorizontalBarChart" %>
<div style="width: <%=Width%>;">
    <div class="chart-title"><%= Title %></div>
    <div class="chart-subtitle"><%= SubTitle %></div>
    <dxchartsui:WebChartControl ID="ChartControl" runat="server" SeriesDataMember="SeriesName" ClientIDMode="AutoID" Height="220px" CrosshairEnabled="True"
        Width="300px" BackColor="Transparent"  OnCustomDrawAxisLabel="ChartControl_CustomDrawAxisLabel" SideBySideBarDistanceVariable="100">
        <padding bottom="0" left="0" right="0" top="0" />
        <borderoptions visibility="False" />
        <diagramserializable>
        <dxcharts:XYDiagram Rotated="True">
            <axisx visibleinpanesserializable="-1" color="224, 224, 224" Visibility="False" alignment="Zero" reverse="True">
                <scalebreakoptions style="Straight" />
                <tickmarks minorvisible="False" visible="False" />
                <label enableantialiasing="True" font="Segoe UI, 13px">
                </label>
                <visualrange autosidemargins="True" />
                <wholerange autosidemargins="True" />
            </axisx>
            <axisy visibleinpanesserializable="-1" color="Transparent">
                <tickmarks minorvisible="False" visible="False" />
                <label enableantialiasing="True" font="Segoe UI, 13px">
                <resolveoverlappingoptions allowstagger="False" allowrotate="False" minindent="0" />
                </label>
                <visualrange autosidemargins="True" />
                <wholerange autosidemargins="True" />
                
            </axisy>
            <margins bottom="0" left="0" right="0" top="0" />
            <defaultpane bordervisible="False" backcolor="Transparent">
            </defaultpane>
        </dxcharts:XYDiagram>
    </diagramserializable>
        <legend visibility="False"></legend>
        <seriestemplate argumentdatamember="PointName" crosshairlabelpattern="{V:c2}" labelsvisibility="False" valuedatamembersserializable="Value">
            <viewserializable>
                <dxcharts:SideBySideBarSeriesView ColorEach="True">
                    <border visibility="False" />
                    <fillstyle fillmode="Solid">
                    </fillstyle>
                </dxcharts:SideBySideBarSeriesView>
            </viewserializable>
        </seriestemplate>
    </dxchartsui:WebChartControl>
    <dx:ASPxPanel ID="LegendHolder" runat="server" CssClass="horizontalChartLegend chartLegend"></dx:ASPxPanel>
</div>
