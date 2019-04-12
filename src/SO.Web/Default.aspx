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
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SO.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />

    <title>Social Office SCRM</title>

    <link href="/Content/bootstrap-4.2.1/bootstrap.min.css" rel="stylesheet" />
    <link href="/Content/StyleSheet.css" rel="stylesheet" />

    <script>
        window.fbAsyncInit = function () {
            FB.init({
                appId: '688902541511693',
                xfbml: true,
                version: 'v3.2'
            });
            FB.AppEvents.logPageView();
        };

        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) { return; }
            js = d.createElement(s); js.id = id;
            js.src = "https://connect.facebook.net/es_LA/sdk.js";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));
    </script>

</head>
<body>
    <div class="container">
        <form id="form1" runat="server">

            <div class="row">
                <div class="col-12">

                    <div class="mx-auto" style="width: 400px;">
                        <h1 class="mx-auto text-center">SOCIAL OFFICE SCRM</h1>
                        <h3 class="mx-auto text-center">By 
                            <a href="https://www.javiercanon.com" target="_blank">Javier Cañon
                            </a>
                        </h3>

                        <a class="btn btn-primary btn-lg mx-auto d-block" href="/Web/Main.aspx">INGRESAR</a>
                        <%-- 
                        <br />
                        <a class="btn btn-warning btn-lg mx-auto d-block" onclick="window.external.JSInvokeTest('invoke WPF Code from HTML')">TEST WPF INVOKE</a>
                            --%>
                    </div>

                    <div class="mx-auto" style="width: 640px; margin-top: 20px;">

                        <div id="fb-root"></div>
                        <div class="fb-group" data-href="https://www.facebook.com/groups/social.office.scrm/" data-width="640" data-show-social-context="true"
                            data-show-metadata="true">
                        </div>

                    </div>

                </div>
            </div>

        </form>
    </div>
    <script src="/Scripts/jquery-3.3.1.min.js"></script>
    <script src="/Scripts/bootstrap-4.2.1/bootstrap.bundle.min.js"></script>

</body>
</html>
