<%-- 
 Copyright (c) 2019 Javier Cañon 
 https://www.javiercanon.com 

 Permission is hereby granted, free of charge, to any person obtaining a copy
 of this software and associated documentation files (the "Software"), to
 deal in the Software without restriction, including without limitation the
 rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 sell copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:

 The above copyright notice and this permission notice shall be included in
 all copies or substantial portions of the Software.

 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
 IN THE SOFTWARE.
--%>
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Errors/ErrorMasterPage.Master" Inherits="Errors.ErrorPageGeneric" CodeBehind="ErrorPageGeneric.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
    <title>Generic Error Page</title>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <div>
        <h2>Generic Error Page - <%: SO.Global.AppComercialName %></h2>
        <asp:Panel ID="InnerErrorPanel" runat="server" Visible="false">
            <p>
                Inner Error Message:<br />
                <asp:Label ID="innerMessage" runat="server" Font-Bold="true"
                    Font-Size="Large" /><br />
            </p>
            <pre>
        <asp:Label ID="innerTrace" runat="server" />
      </pre>
        </asp:Panel>
        <p>
            Error Message:<br />
            <asp:Label ID="exMessage" runat="server" Font-Bold="true"
                Font-Size="Large" />
        </p>
        <pre>
      <asp:Label ID="exTrace" runat="server" Visible="false" />
    </pre>
        <br />
        <%= Resources.Error.Returntothe %>
        <asp:HyperLink runat="server" Target="_self" Text="<%$ Resources:Error, DefaultPage %>" NavigateUrl="~/Default.aspx"></asp:HyperLink>
    </div>
</asp:Content>


