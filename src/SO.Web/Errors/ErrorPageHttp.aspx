<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Errors/ErrorMasterPage.Master" Inherits="Errors.ErrorPageHttp" CodeBehind="ErrorPageHttp.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
    <title>Http Error Page</title>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <div>
        <h2>Http Error Page - <%: SO.Global.AppComercialName %></h2>
        <asp:Panel ID="InnerErrorPanel" runat="server" Visible="false">
            <asp:Label ID="innerMessage" runat="server" Font-Bold="true"
                Font-Size="Large" /><br />
            <pre>
        <asp:Label ID="innerTrace" runat="server" />
      </pre>
        </asp:Panel>
        Mensaje de Error:<br />
        <asp:Label ID="exMessage" runat="server" Font-Bold="true" Font-Size="Large" />
        <pre>
        <asp:Label ID="exTrace" runat="server" Visible="false" />
    </pre>
        <br />
        <%= Resources.Error.Returntothe %>
        <asp:HyperLink runat="server" Target="_self" Text="<%$ Resources:Error, DefaultPage %>" NavigateUrl="~/Default.aspx"></asp:HyperLink>
    </div>
</asp:Content>

