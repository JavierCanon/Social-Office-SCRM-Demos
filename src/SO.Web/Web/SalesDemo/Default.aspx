<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteBase.master" CodeBehind="Default.aspx.cs" Inherits="_Default" %>
<asp:Content runat="server" ContentPlaceHolderID="PageTitlePartPlaceHolder">Database Generation</asp:Content>
<asp:Content ID="ContentHolder" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    <div class="createDbMessage">
        <dx:ASPxLabel runat="server" ID="Label" ClientInstanceName="CreateDbMessage" Theme="Moderno"
            Text="In order to execute this demo, you must first generate sample data. The database generation process will take a few minutes. To proceed, click the Create Database button below." />
    </div>
    <dx:ASPxButton runat="server" ID="CreateDbButton" CssClass="button" ClientInstanceName="CreateDbButton" Text="Create Database" AutoPostBack="false">
        <HoverStyle CssClass="hover" />
        <ClientSideEvents Click="OnCreateDbButtonClick" />
    </dx:ASPxButton>
    <dx:ASPxProgressBar ID="Progress" ShowPosition="true" Height="32px" runat="server" ClientInstanceName="CreateDbProgress" CssClass="dbProgress" ClientVisible="false" Minimum="0" Maximum="100">
        <IndicatorStyle CssClass="indicator" />
    </dx:ASPxProgressBar>
    <dx:ASPxCallback runat="server" ID="Callback" ClientInstanceName="CreateDbCallback" OnCallback="Callback_Callback">
        <ClientSideEvents CallbackComplete="OnCreateDbCallbackCallbackComplete" CallbackError="OnCreateDbCallbackCallbackError" />
    </dx:ASPxCallback>
</asp:Content>
