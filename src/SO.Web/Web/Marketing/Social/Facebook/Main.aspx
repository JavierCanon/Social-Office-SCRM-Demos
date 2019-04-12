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
<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Marketing/Social/Facebook/FacebookMasterPage.master" AutoEventWireup="true"
    CodeBehind="Main.aspx.cs" Inherits="SO.Web.Marketing.Social.Facebook.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageMain" runat="server">
    <h1>Cuenta de Facebook Activa</h1>
    <h4>Inicie sesión en una cuenta de Facebook.</h4>
    <div class="alert-info">En esta versión solo puede tener una sesión de Facebook activa al mismo tiempo.
        ¿Como cambiar de cuenta en una computadora sin cerrar la sesión? [ <a href="https://www.facebook.com/help/804757836392387" target="_blank">Leer...</a> ]
    </div>

    <div>

        <a role="button" class="btn btn-primary btn-lg" href="https://www.facebook.com" target="_blank">ABRIR FACEBOOK</a>

    </div>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="PageFooter" runat="server">
</asp:Content>
