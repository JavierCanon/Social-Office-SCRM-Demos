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
<%@ Page Title="" Language="C#" MasterPageFile="~/Web/SiteMain.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="SO.Web.Main" %>

<%@ Register Assembly="DevExpress.Dashboard.v18.2.Web.WebForms, Version=18.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.DashboardWeb" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">

    <h1 class="bg-info">SCRM <i class="fa fa-tachometer-alt"></i>
        <small>Dashboard General</small>
    </h1>

    <div style="position: relative; top: 0; left: 0; right: 0; bottom: 0;">


        <dx:ASPxDashboard ID="ASPxDashboard1" runat="server" Width="100%" Height="1200"
            WorkingMode="ViewerOnly" AllowExportDashboardItems="True"
            OnInit="ASPxDashboard1_Init"
            OnLoad="ASPxDashboard1_Load"
            OnDataLoading="ASPxDashboard1_DataLoading"
            OnConfigureDataConnection="ASPxDashboard1_ConfigureDataConnection"
            OnCustomParameters="ASPxDashboard1_CustomParameters"
            AllowExecutingCustomSql="True">
        </dx:ASPxDashboard>


    </div>

</asp:Content>
