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
<%@ Page Title="" Language="C#" MasterPageFile="~/DevTools/DevTools.Master" AutoEventWireup="true" CodeBehind="CultureInfo.aspx.cs" Inherits="SO.DevTools.CultureInfo" %>

<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .culture td {
            padding: 5px;
            border: 1px solid gray;
        }

        .culture thead th {
            font: bolder;
            text-align: center;
            border: 1px double gray;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>CULTURES INFO</h1>

    <h2>NEUTRAL CULTURES</h2>

    <table class="culture">
        <thead>
            <tr>
                <th>Name</th>
                <th>DisplayName</th>
                <th>EnglishName</th>
                <th>LCID</th>
                <th>NativeName</th>
                <th>Format Examples</th>

                <th>TwoLetterISOLanguageName</th>
                <th>ThreeLetterISOLanguageName</th>
                <th>ThreeLetterWindowsLanguageName</th>

            </tr>
        </thead>


        <%  
            
            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.NeutralCultures))
            {
                Response.Write("<tr>");
                /*
                Console.Write("{0,-7}", ci.Name);
                Console.Write(" {0,-3}", ci.TwoLetterISOLanguageName);
                Console.Write(" {0,-3}", ci.ThreeLetterISOLanguageName);
                Console.Write(" {0,-3}", ci.ThreeLetterWindowsLanguageName);
                Console.Write(" {0,-40}", ci.DisplayName);
                Console.WriteLine(" {0,-40}", ci.EnglishName);
                */
                Response.Write(string.Format("<td>[{0}]</td>", ci.Name));
                Response.Write(string.Format("<td>{0}</td>", ci.DisplayName));
                Response.Write(string.Format("<td>{0}</td>", ci.EnglishName));
                Response.Write(string.Format("<td>{0}</td>", ci.LCID));
                Response.Write(string.Format("<td>{0}</td>", ci.NativeName));

                Response.Write("<td>");

                Response.Write(string.Format("<p><b>ShortDatePattern</b>: {0}</p>", ci.DateTimeFormat.ShortDatePattern));
                Response.Write(string.Format("<p><b>LongDatePattern</b>: {0}</p>", ci.DateTimeFormat.LongDatePattern));
                Response.Write(string.Format("<p><b>CurrencySymbol</b>: {0}</p>", ci.NumberFormat.CurrencySymbol));
                Response.Write(string.Format("<p><b>NumberDecimalSeparator</b>: {0}</p>", ci.NumberFormat.NumberDecimalSeparator));
                Response.Write(string.Format("<p><b>NumberGroupSeparator</b>: {0}</p>", ci.NumberFormat.NumberGroupSeparator));
                Response.Write(string.Format("<p><b>NumberDecimalDigits</b>: {0}</p>", ci.NumberFormat.NumberDecimalDigits)); 
                                                
                Response.Write("</td>");


                Response.Write(string.Format("<td>{0}</td>", ci.TwoLetterISOLanguageName));
                Response.Write(string.Format("<td>{0}</td>", ci.ThreeLetterISOLanguageName));
                Response.Write(string.Format("<td>{0}</td>", ci.ThreeLetterWindowsLanguageName));


                Response.Write("</tr>");


            }
        
        %>
    </table>

    <hr />


        <h2>SPECIFIC - COUNTRY - CULTURES</h2>

    <table class="culture">
        <thead>
            <tr>
                <th>Name</th>
                <th>DisplayName</th>
                <th>EnglishName</th>
                <th>LCID</th>
                <th>NativeName</th>
                <th>Format Examples</th>

                <th>TwoLetterISOLanguageName</th>
                <th>ThreeLetterISOLanguageName</th>
                <th>ThreeLetterWindowsLanguageName</th>

            </tr>
        </thead>


        <%  
            
            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                Response.Write("<tr>");
                /*
                Console.Write("{0,-7}", ci.Name);
                Console.Write(" {0,-3}", ci.TwoLetterISOLanguageName);
                Console.Write(" {0,-3}", ci.ThreeLetterISOLanguageName);
                Console.Write(" {0,-3}", ci.ThreeLetterWindowsLanguageName);
                Console.Write(" {0,-40}", ci.DisplayName);
                Console.WriteLine(" {0,-40}", ci.EnglishName);
                */
                Response.Write(string.Format("<td>[{0}]</td>", ci.Name));
                Response.Write(string.Format("<td>{0}</td>", ci.DisplayName));
                Response.Write(string.Format("<td>{0}</td>", ci.EnglishName));
                Response.Write(string.Format("<td>{0}</td>", ci.LCID));
                Response.Write(string.Format("<td>{0}</td>", ci.NativeName));

                Response.Write("<td>");

                Response.Write(string.Format("<p><b>ShortDatePattern</b>: {0}</p>", ci.DateTimeFormat.ShortDatePattern));
                Response.Write(string.Format("<p><b>LongDatePattern</b>: {0}</p>", ci.DateTimeFormat.LongDatePattern));
                Response.Write(string.Format("<p><b>CurrencySymbol</b>: {0}</p>", ci.NumberFormat.CurrencySymbol));
                Response.Write(string.Format("<p><b>NumberDecimalSeparator</b>: {0}</p>", ci.NumberFormat.NumberDecimalSeparator));
                Response.Write(string.Format("<p><b>NumberGroupSeparator</b>: {0}</p>", ci.NumberFormat.NumberGroupSeparator));
                Response.Write(string.Format("<p><b>NumberDecimalDigits</b>: {0}</p>", ci.NumberFormat.NumberDecimalDigits)); 
                                                
                Response.Write("</td>");


                Response.Write(string.Format("<td>{0}</td>", ci.TwoLetterISOLanguageName));
                Response.Write(string.Format("<td>{0}</td>", ci.ThreeLetterISOLanguageName));
                Response.Write(string.Format("<td>{0}</td>", ci.ThreeLetterWindowsLanguageName));


                Response.Write("</tr>");


            }
        
        %>
    </table>


</asp:Content>
