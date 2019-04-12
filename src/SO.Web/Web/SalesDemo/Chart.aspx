<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chart.aspx.cs" Inherits="Chart" %>
<%@ OutputCache Duration="86400" VaryByParam="start;end;" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dxchartsui:WebChartControl ID="RangeChartControl" runat="server" SeriesDataMember="SeriesName" CrosshairEnabled="False" Height="50px" Width="1200px" PaletteName="Palette 1" BackColor="Transparent">
            <padding bottom="0" left="0" right="0" top="0" />
            <borderoptions visibility="False" />
            <diagramserializable>
                <dxcharts:SwiftPlotDiagram>
                    <axisx visibleinpanesserializable="-1" color="Transparent" Visibility="False" alignment="Far">
                        <tickmarks minorvisible="False" visible="False" />
                        <label visible="False">
                        </label>
                        <numericscaleoptions autogrid="False" customgridalignment="0.02" gridalignment="Custom" gridspacing="25" />
                    </axisx>
                    <axisy visibleinpanesserializable="-1" color="Transparent" Visibility="False" alignment="Far">
                        <tickmarks minorvisible="False" visible="False" />
                        <label visible="False">
                        </label>
                        <visualrange autosidemargins="False" sidemarginsvalue="0" />
                        <wholerange alwaysshowzerolevel="False" AutoSideMargins="False" SideMarginsValue="0" />
                        <gridlines visible="False">
                        </gridlines>
                        <numericscaleoptions autogrid="False" gridspacing="0.5" />
                    </axisy>
                    <margins bottom="0" left="0" right="0" top="6" />
                    <defaultpane backcolor="Transparent" bordervisible="False">
                    </defaultpane>
                </dxcharts:SwiftPlotDiagram>
            </diagramserializable>
            <fillstyle fillmode="Empty">
            </fillstyle>
            <legend visibility="False"></legend>
            <seriestemplate ArgumentScaleType="DateTime"  ArgumentDataMember="Argument" ValueDataMembersSerializable="Value">
                <viewserializable>
                    <dxcharts:SwiftPlotSeriesView Antialiasing="True">
                            <linestyle thickness="2" />
                    </dxcharts:SwiftPlotSeriesView>
                </viewserializable>
            </seriestemplate>
            <palettewrappers>
                <dxchartsui:PaletteWrapper Name="Palette 1" ScaleMode="Repeat">
                    <palette>
                        <dxcharts:PaletteEntry Color="218, 88, 89" Color2="218, 88, 89" />
                    </palette>
                </dxchartsui:PaletteWrapper>
            </palettewrappers>
        </dxchartsui:WebChartControl>
    </div>
    </form>
</body>
</html>
