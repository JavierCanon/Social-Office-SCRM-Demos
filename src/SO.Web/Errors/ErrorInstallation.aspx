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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Errors/ErrorMasterPage.Master" Inherits="Errors.ErrorInstallation" CodeBehind="ErrorInstallation.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">

    <title>Error Instalacion <%: SO.Global.AppComercialName %></title>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="content">
        <div class="error">
            <h2><%: SO.Global.AppComercialName %>: El aplicativo no se encuentra correctamente instalado y configurado, contáctese con soporte. </h2>
            <h3>Servidor:
                <% = Request.Url.Authority.ToLower() %>
            </h3>
        </div>
    </div>
</asp:Content>

