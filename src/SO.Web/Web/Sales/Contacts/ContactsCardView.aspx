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
<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Sales/Contacts/ContactsMasterPage.master" AutoEventWireup="true" CodeBehind="ContactsCardView.aspx.cs"
    Inherits="SO.Web.Sales.Contacts.ContactsCardView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageMain" runat="server">

    <dx:ASPxCardView ID="ASPxCardView1" runat="server"
        DataSourceID="DataSourceMaster" AutoGenerateColumns="False" KeyFieldName="ContactoID"
        OnInit="ASPxCardView1_Init">

        <Settings ShowHeaderPanel="False" ShowSummaryPanel="True" ShowTitlePanel="True" ShowFilterBar="Visible"></Settings>

        <SettingsBehavior AllowEllipsisInText="true" AllowFocusedCard="True" AllowSelectByCardClick="True" AllowSelectSingleCardOnly="True"
            ConfirmDelete="True" EnableCardHotTrack="True" EnableCustomizationWindow="True" />

        <SettingsPager AlwaysShowPager="True" EnableAdaptivity="true"></SettingsPager>

        <SettingsSearchPanel Visible="True" ShowApplyButton="True" ShowClearButton="True" Delay="1500"></SettingsSearchPanel>

        <Settings ShowSummaryPanel="true" />

        <SettingsCommandButton RenderMode="Button"></SettingsCommandButton>
        <SettingsCookies Enabled="True" />

        <ClientSideEvents CardDblClick="function(s, e) {
        window.open('ContactDetail.aspx?id=' + e.visibleIndex);
    }" />

        <Columns>
            <dx:CardViewBinaryImageColumn FieldName="Foto" VisibleIndex="0">
                <PropertiesBinaryImage NullDisplayText="Sin foto..." ImageHeight="240" ImageWidth="240" EnableServerResize="True">
                </PropertiesBinaryImage>
            </dx:CardViewBinaryImageColumn>
            <dx:CardViewTextColumn FieldName="ContactoID" Caption="ID" ReadOnly="True" VisibleIndex="1">
                <DataItemTemplate>
                    <a href="ContactDetail.aspx?id=<%# Eval("ContactoID") %>" target="_blank"><%# Eval("ContactoID") %></a>
                </DataItemTemplate>
            </dx:CardViewTextColumn>
            <dx:CardViewTextColumn FieldName="Nombre" VisibleIndex="2"></dx:CardViewTextColumn>
            <dx:CardViewTextColumn FieldName="Apellido" VisibleIndex="3"></dx:CardViewTextColumn>
            <dx:CardViewTextColumn FieldName="Seudonimo" VisibleIndex="4"></dx:CardViewTextColumn>
            <dx:CardViewTextColumn FieldName="MostrarComo" VisibleIndex="5"></dx:CardViewTextColumn>
            <dx:CardViewTextColumn FieldName="Cargo" VisibleIndex="6"></dx:CardViewTextColumn>
            <dx:CardViewTextColumn FieldName="Empresa" VisibleIndex="7"></dx:CardViewTextColumn>
            <dx:CardViewTextColumn FieldName="Tags" VisibleIndex="8"></dx:CardViewTextColumn>
            <dx:CardViewTextColumn FieldName="Nota" VisibleIndex="9"></dx:CardViewTextColumn>
        </Columns>

        <CardLayoutProperties ColCount="1">
            <Items>
                <dx:CardViewColumnLayoutItem ColumnName="Foto" ShowCaption="False" ColSpan="1" RowSpan="3"
                    HorizontalAlign="Center" VerticalAlign="Top" CssClass="rounded-circle">
                </dx:CardViewColumnLayoutItem>
                <dx:CardViewColumnLayoutItem ColumnName="ContactoID" ColSpan="1"></dx:CardViewColumnLayoutItem>
                <dx:CardViewColumnLayoutItem ColumnName="MostrarComo" ColSpan="1"></dx:CardViewColumnLayoutItem>
                <dx:CardViewColumnLayoutItem ColumnName="Cargo" ColSpan="1"></dx:CardViewColumnLayoutItem>
                <dx:CardViewColumnLayoutItem ColumnName="Empresa" ColSpan="1"></dx:CardViewColumnLayoutItem>
                <dx:CardViewTabbedLayoutGroup ColSpan="1">
                    <Items>
                        <dx:CardViewColumnLayoutItem ColumnName="Tags" ColSpan="1"></dx:CardViewColumnLayoutItem>
                        <dx:CardViewColumnLayoutItem ColumnName="Nota" ColSpan="1"></dx:CardViewColumnLayoutItem>
                        <dx:CardViewLayoutGroup ColCount="1" ColSpan="1" Caption="Datos">
                            <Items>
                                <dx:CardViewColumnLayoutItem ColumnName="Nombre" ColSpan="1"></dx:CardViewColumnLayoutItem>
                                <dx:CardViewColumnLayoutItem ColumnName="Apellido" ColSpan="1"></dx:CardViewColumnLayoutItem>
                                <dx:CardViewColumnLayoutItem ColumnName="Seudonimo" ColSpan="1"></dx:CardViewColumnLayoutItem>
                            </Items>
                        </dx:CardViewLayoutGroup>
                    </Items>
                </dx:CardViewTabbedLayoutGroup>
            </Items>
        </CardLayoutProperties>

        <TotalSummary>
            <dx:ASPxCardViewSummaryItem FieldName="ContactoID" SummaryType="Count" ValueDisplayFormat="<b>{0:n}</b>" />
        </TotalSummary>

        <Styles>
            <SearchPanel CssClass="gridSearchPanel">
            </SearchPanel>
        </Styles>

        <Templates>
            <TitlePanel>
                <h3 class="d-inline">Contactos Cartas</h3>
                <a class="btn btn-sm btn-outline-dark" data-toggle="tooltip" data-placement="bottom" title="Vista Grilla" href="ContactsGrid.aspx">
                    <i class="fa fa-table"></i></a>
                <a class="btn btn-sm btn-outline-dark" data-toggle="tooltip" data-placement="bottom" title="Vista Cartas" href="ContactsCardView.aspx">
                    <i class="fa fa-th-large"></i></a>
            </TitlePanel>

        </Templates>

    </dx:ASPxCardView>

    <asp:SqlDataSource ID="DataSourceMaster" runat="server"
        EnableCaching="true"
        CacheExpirationPolicy="Sliding"
        CancelSelectOnNullParameter="false"
        OnSelecting="DBMainDataSources_Selecting"
        OnInit="DBMainDataSources_Init"
        ConnectionString='<%$ ConnectionStrings:Development.MsSqlServer.Main %>'
        SelectCommand="SELECT [ContactoID], [Nombre], [Apellido], [Seudonimo], [MostrarComo], [Cargo], [Empresa], [Tags], [Nota], [Foto] FROM [Contacto]">
    </asp:SqlDataSource>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageFooter" runat="server">
</asp:Content>
