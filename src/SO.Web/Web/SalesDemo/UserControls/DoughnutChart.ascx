<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DoughnutChart.ascx.cs" Inherits="DoughnutChart" %>
<div class="doughnutChart" style="width: <%= Width%>">
    <dxchartsui:WebChartControl ID="ChartControl" runat="server" SeriesDataMember="SeriesName" CrosshairEnabled="True" Height="280px" ClientIDMode="AutoID" Width="280px">
        <padding bottom="0" left="0" right="0" top="0" />
        <emptycharttext font="Segoe UI, 12pt" />
        <borderoptions visibility="False" />
        <diagramserializable>
        <dxcharts:SimpleDiagram EqualPieSize="True">
        </dxcharts:SimpleDiagram>
    </diagramserializable>
        <fillstyle fillmode="Empty">
    </fillstyle>
        <legend visibility="False"></legend>
        <seriestemplate crosshairlabelvisibility="True" argumentdatamember="PointName" valuedatamembersserializable="Value">
        <viewserializable>
            <dxcharts:DoughnutSeriesView RuntimeExploding="False">
                    <border visibility="False" />
            </dxcharts:DoughnutSeriesView>
        </viewserializable>
        <labelserializable>
            <dxcharts:DoughnutSeriesLabel EnableAntialiasing="True" BackColor="Transparent" Font="Segoe UI, 10pt" 
                    TextAlignment="Far" TextColor="Black" Position="Outside" TextPattern="{VP:0%}" LineVisibility="False" LineLength="7">
                <pointoptionsserializable>
                    <dxcharts:PiePointOptions>
                        <valuenumericoptions format="Percent" />
                    </dxcharts:PiePointOptions>
                </pointoptionsserializable>
                <border visibility="False" />
            </dxcharts:DoughnutSeriesLabel>
        </labelserializable>
    </seriestemplate>
    </dxchartsui:WebChartControl>
    <div class="doughnutChartTitle" style="top: <%= TopCenter%>; width: <%= Width%>;">
        <div>
            <%= Title%><br />
            <%= SubTitle %>
        </div>
    </div>
    <dx:ASPxPanel ID="LegendHolder" runat="server" ClientIDMode="AutoID" CssClass="doughnutChartLegend chartLegend">
        <PanelCollection>
            <dx:PanelContent>
                <ul class="ul_legend">
                    <% for(int i = 0; i < DataSourceLength; i++) { %>
                    <li>
                        <div class="colorDiv" style="background-color: <%=PaletteHelper.GetCommonPalletePointCssColor(i)%>"></div>
                        <%=GetPointName(i)%></li>
                    <% } %>
                </ul>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
</div>
