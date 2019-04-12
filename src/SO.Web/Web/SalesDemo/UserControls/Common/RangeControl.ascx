<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RangeControl.ascx.cs" Inherits="RangeControl" %>
<dx:ASPxCallbackPanel ID="CallbackPanel" ClientInstanceName="RangeControlCallbackPanel" OnCallback="CallbackPanel_Callback" runat="server" EnableCallbackAnimation="false">
    <SettingsLoadingPanel Enabled="false" />
    <PanelCollection>
        <dx:PanelContent>
            <div class="rangeControlLayout">
                <div>
                    <dx:ASPxButton ID="LeftShiftButton" runat="server" CssClass="rangeControlButton leftShift" AutoPostBack="false">
                        <DisabledStyle CssClass="disabled" />
                        <ClientSideEvents Click="function(s,e) { RangeControlHelper.ChangeRangeYear(-1); }" />
                    </dx:ASPxButton>
                </div>
                <div class="trackBarWrapper">
                    <dx:ASPxImage ID="BackgroundChartImage" runat="server"></dx:ASPxImage>
                    <dx:ASPxTrackBar ID="SalesDateRange" runat="server" ClientInstanceName="SalesDateRange" CssClass="salesDateRange" AllowRangeSelection="true"
                        ShowChangeButtons="false" ScalePosition="RightOrBottom" ValueToolTipPosition="None" AllowMouseWheel="False">
                        <ItemStyle CssClass="item" />
                        <FocusedStyle CssClass="focused" />
                        <BarHighlightStyle CssClass="barHighlight" />
                        <MainDragHandleStyle CssClass="dragHandle" HoverStyle-CssClass="dragHandleHover" />
                        <SecondaryDragHandleStyle CssClass="dragHandle" HoverStyle-CssClass="dragHandleHover" />
                        <ClientSideEvents Init="RangeControlHelper.OnTrackBarInit" />
                    </dx:ASPxTrackBar>
                </div>
                <div>
                    <dx:ASPxButton ID="RightShiftButton" runat="server" CssClass="rangeControlButton rightShift" AutoPostBack="false">
                        <DisabledStyle CssClass="disabled" />
                        <ClientSideEvents Click="function(s,e) { RangeControlHelper.ChangeRangeYear(1); }" />
                    </dx:ASPxButton>
                </div>
            </div>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
