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
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Errors/ErrorMasterPage.Master" Inherits="Errors.ErrorCallback" CodeBehind="ErrorCallback.aspx.cs" %>

<%@ Import Namespace="SO" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentHead" runat="server">
    <title>Error Callback <%: SO.Global.AppComercialName %></title>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="content">
        <img src="<%= Global.Utils.Webpath.GetRequestApplicationUrlPath() %>Content/Icons/alert.gif" alt="" width="16" height="16" />
        <div style="padding: 10px; border: 1px solid #FF6600">
            <h2>Error en la solicitud. <%: SO.Global.AppComercialName %>.</h2>
            <p>Ha ocurrido un problema con la solicitud actual, la cual puede ser por alguna de estas razones:</p>
            <ul>
                <li>Si esta consultando un reporte o gráfico, verifique que los parámetros sean validos o seleccione otros.
                </li>
                <li>Su sesión ya no es valida, cierre el aplicativo y vuelva a ingresar.</li>
            </ul>
            <p>Por favor utilice el botón volver atras de su navegador, o haga click en el siguiente link: <a href="javascript:history.back()">volver</a>.</p>
            <hr />
            <p>Detalle Técnico:</p>
            <p><%=Request.QueryString["DXCallbackErrorMessage"]%></p>
        </div>
    </div>
</asp:Content>



