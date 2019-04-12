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
<%@ Page Title="" Language="C#" MasterPageFile="~/Web/Sales/Contacts/ContactsMasterPage.master" AutoEventWireup="true" CodeBehind="ContactsGrid.aspx.cs"
    Inherits="SO.Web.Sales.Contacts.ContactsGrid" %>
<%@ Import Namespace="SO.Web.Sales.Contacts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHead" runat="server">

    <script type="text/javascript">
        function OnToolbarItemClick(s, e) {
            if (IsCustomExportToolbarCommand(e.item.name)) {
                e.processOnServer = true;
                e.usePostBack = true;
            }
            if (e.item.name == "ResetGrid") ResetGrid(grid);
        }
        function IsCustomExportToolbarCommand(command) {
            return command == "CustomExportToXLS" || command == "CustomExportToXLSX";
        }
        function gridDeleteRow(targetGrid, id) {
            if (confirm('¿Esta seguro de ELIMINAR el registro actual? (' + id + ')')) {
                targetGrid.DeleteRowByKey(id);
            }
        }
        function ResetGrid(targetGrid) {
            if (confirm('¿Esta seguro de RESETEAR el diseño de la grilla actual?')) {
                ASPxClientUtils.DeleteCookie("<%= ContactsGrid.MasterGridCookieName  %>");<%-- *** WARNING SET THE SAME VALUE OF SettingsCookies-CookiesID OF GRID *** --%>
                targetGrid.Refresh();
                window.location.reload();
            }
        }
        function ResetDetGrid(targetGrid) {
            if (confirm('¿Esta seguro de RESETEAR el diseño de la grilla actual?')) {
                ASPxClientUtils.DeleteCookie("<%= ContactsGrid.DetailGridCookieName  %>");<%-- *** WARNING SET THE SAME VALUE OF SettingsCookies-CookiesID OF GRID *** --%>
                targetGrid.Refresh();
                window.location.reload();
            }
        }
        function Grid_Update_Click(grid) {
            var isValid = ASPxClientEdit.ValidateEditorsInContainer(grid.GetMainElement());
            if (isValid)
                grid.UpdateEdit();
        }
        var detgridcommand = "";
        function detailGridOnBeginCallback(s, e) {
            detgridcommand = e.command;
        }
        function detailGridOnEndCallback(s, e) {
            /*  if (detgridcommand == "ADDNEWROW" || detgridcommand == "UPDATEEDIT" || detgridcommand == "DELETEROW") {
                  s.Refresh();
              }*/
        }
        function sPopup(userId, table) {
            /*
            var spage = "UsersDataFilter.aspx?time=" + new Date().getTime() + "&userId=" + userId + "&table=" + table;
            showPopupMasterModal('Edicion Filtros', spage);*/
        }

        function OnCountryChanged(cmbCountry, cmbRegion) {
            if (cmbRegion.InCallback())
                return;
            else
                cmbRegion.PerformCallback(cmbCountry.GetValue().toString());
        }
        function OnEndCallbackRegion(s, e) {
        }
        function OnRegionChanged(cmbCountry, cmbRegion, cmbCity) {
            if (cmbCity.InCallback()) return;
            else
                cmbCity.PerformCallback(cmbCountry.GetValue().toString() + '|' + cmbRegion.GetValue().toString());
        }
        function OnEndCallbackCity(s, e) {
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageMain" runat="server">

    <dx:ASPxGridView ID="ASPxGridView1" runat="server"
        DataSourceID="DataSourceMaster"
        KeyFieldName="ContactoID"
        ClientInstanceName="grid"
        OnInit="ASPxGridView1_Init"
        OnHtmlRowPrepared="ASPxGridView1_HtmlRowPrepared"
        OnRowValidating="ASPxGridView1_RowValidating"
        OnStartRowEditing="ASPxGridView1_StartRowEditing"
        OnRowUpdating="ASPxGridView1_RowUpdating"
        OnParseValue="ASPxGridView1_ParseValue"
        OnRowDeleted="ASPxGridView1_RowDeleted"
        OnRowInserted="ASPxGridView1_RowInserted"
        OnRowUpdated="ASPxGridView1_RowUpdated"
        OnDataBinding="ASPxGridView1_DataBinding"
        OnRowInserting="ASPxGridView1_RowInserting"
        OnDetailRowExpandedChanged="ASPxGridView1_DetailRowExpandedChanged"
        OnCustomCallback="ASPxGridView1_CustomCallback"
        OnCellEditorInitialize="ASPxGridView1_CellEditorInitialize"
        OnToolbarItemClick="Grid_ToolbarItemClick"
        AutoGenerateColumns="False">

        <SettingsText />

        <Settings ColumnMinWidth="40" HorizontalScrollBarMode="Hidden" VerticalScrollBarMode="Hidden"
            ShowFilterRow="True" ShowTitlePanel="True" ShowFilterBar="Hidden" />

        <SettingsCommandButton
            DeleteButton-ButtonType="Image"
            EditButton-ButtonType="Image"
            NewButton-ButtonType="Image"
            RenderMode="Button">
            <NewButton Image-IconID="actions_additem_16x16office2013" />
            <EditButton Image-IconID="edit_edit_16x16office2013" />
            <DeleteButton Image-IconID="spreadsheet_deletesheetrows_16x16" />
            <UpdateButton Image-IconID="actions_apply_16x16office2013" />
            <CancelButton Image-IconID="actions_cancel_16x16office2013" />
            <SearchPanelApplyButton>
            </SearchPanelApplyButton>
            <SearchPanelClearButton>
            </SearchPanelClearButton>
            <ApplyFilterButton></ApplyFilterButton>
            <ClearFilterButton></ClearFilterButton>
        </SettingsCommandButton>

        <SettingsDataSecurity />
        <SettingsResizing ColumnResizeMode="Control" Visualization="Postponed" />

        <SettingsBehavior ConfirmDelete="True" EnableRowHotTrack="True"
            AllowDragDrop="True" AllowFocusedRow="True" AllowGroup="True" AllowSort="True" AllowSelectByRowClick="True"
            EnableCustomizationWindow="True" AllowEllipsisInText="False" />

        <SettingsPager PageSize="10" Position="Bottom" AllButton-Visible="False" PageSizeItemSettings-Visible="false"
            PageSizeItemSettings-ShowAllItem="False" NumericButtonCount="10">
        </SettingsPager>

        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />

        <Settings ShowFilterBar="Visible" ShowFilterRow="True" ShowFilterRowMenu="True"
            ShowFooter="True" ShowGroupPanel="True" ShowTitlePanel="True" ShowColumnHeaders="True"
            ShowGroupButtons="True" ShowHeaderFilterButton="False" />

        <SettingsCookies Enabled="True" StoreControlWidth="True" />

        <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="False" ExportMode="All"></SettingsDetail>

        <SettingsContextMenu Enabled="True" EnableFooterMenu="True" EnableGroupPanelMenu="True"
            EnableRowMenu="True" EnableColumnMenu="True">
        </SettingsContextMenu>

        <SettingsSearchPanel Visible="False" CustomEditorID="tbToolbarSearch" ShowClearButton="False" ShowApplyButton="False" Delay="1500" />
        <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="DataAware" />
        <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />

        <ClientSideEvents RowDblClick="function(s, e) {
        s.StartEditRow(e.visibleIndex);
    }" />

        <Toolbars>

            <dx:GridViewToolbar ItemAlign="Left" EnableAdaptivity="False">
                <Items>
                    <dx:GridViewToolbarItem BeginGroup="true">
                        <Template>
                            <dx:ASPxButtonEdit ID="tbToolbarSearch" runat="server" NullText="Buscar..." Height="100%">
                                <Buttons>
                                    <dx:SpinButtonExtended Image-IconID="find_find_16x16gray" />
                                </Buttons>
                            </dx:ASPxButtonEdit>
                        </Template>
                    </dx:GridViewToolbarItem>

                    <dx:GridViewToolbarItem Text="Exportar" BeginGroup="true" Image-IconID="actions_download_16x16office2013">
                        <Items>
                            <dx:GridViewToolbarItem Command="ExportToPdf" />
                            <dx:GridViewToolbarItem Command="ExportToDocx" />
                            <dx:GridViewToolbarItem Command="ExportToRtf" />
                            <dx:GridViewToolbarItem Command="ExportToCsv" />
                            <dx:GridViewToolbarItem Command="ExportToXls" Text="XLS(DataAware)" />
                            <dx:GridViewToolbarItem Name="CustomExportToXLS" Text="XLS(WYSIWYG)" Image-IconID="export_exporttoxls_16x16office2013" />
                            <dx:GridViewToolbarItem Command="ExportToXlsx" Text="XLSX(DataAware)" />
                            <dx:GridViewToolbarItem Name="CustomExportToXLSX" Text="XLSX(WYSIWYG)" Image-IconID="export_exporttoxlsx_16x16office2013" />
                        </Items>
                    </dx:GridViewToolbarItem>

                </Items>
            </dx:GridViewToolbar>

            <dx:GridViewToolbar ItemAlign="Left" EnableAdaptivity="False">
                <Items>
                    <dx:GridViewToolbarItem Command="New" />
                    <dx:GridViewToolbarItem Command="Edit" />
                    <dx:GridViewToolbarItem Command="Delete" />
                    <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" DisplayMode="Image" />
                    <dx:GridViewToolbarItem Command="FullExpand" DisplayMode="Image" />
                    <dx:GridViewToolbarItem Command="FullCollapse" DisplayMode="Image" />
                    <dx:GridViewToolbarItem Name="ResetGrid" ToolTip="Resetear Diseño" DisplayMode="Image" Image-IconID="grid_autofittocontent_16x16" />

                </Items>
            </dx:GridViewToolbar>

        </Toolbars>

        <Columns>

            <dx:GridViewDataTextColumn FieldName="ContactoID" Caption="Detalle" ReadOnly="True" VisibleIndex="0" Width="90">
                <EditFormSettings Visible="False"></EditFormSettings>
                <DataItemTemplate>
                    <a href="ContactDetail.aspx?id=<%# Eval("ContactoID") %>" target="_blank"><%# Eval("ContactoID") %></a>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataBinaryImageColumn FieldName="Foto" VisibleIndex="0" Visible="false">
                <EditFormSettings Visible="True"></EditFormSettings>
                <PropertiesBinaryImage EnableServerResize="True" ImageSizeMode="ActualSizeOrFit" ImageHeight="320" ImageWidth="320">
                    <EditingSettings Enabled="true" UploadSettings-UploadValidationSettings-MaxFileSize="4194304" />
                    <ValidationSettings>
                        <ErrorImage ToolTip="Puede usar archivos JPG, GIF o PNG; de Maximo 4MB."></ErrorImage>
                    </ValidationSettings>
                </PropertiesBinaryImage>
            </dx:GridViewDataBinaryImageColumn>

            <dx:GridViewDataTextColumn FieldName="Nombre" VisibleIndex="1" Width="120">
                <PropertiesTextEdit MaxLength="30">
                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" CausesValidation="True" ErrorFrameStyle-CssClass="alert-warning">
                        <RequiredField IsRequired="True" ErrorText="Valor requerido." />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Apellido" VisibleIndex="2" Width="120"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Seudonimo" VisibleIndex="3" Width="120"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="MostrarComo" VisibleIndex="4" Width="120"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Cargo" VisibleIndex="5" Width="120"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Empresa" VisibleIndex="6" Width="120"></dx:GridViewDataTextColumn>

            <dx:GridViewDataTokenBoxColumn FieldName="Tags" VisibleIndex="7" Visible="false">
                <EditFormSettings Visible="True"></EditFormSettings>
            </dx:GridViewDataTokenBoxColumn>

            <dx:GridViewDataMemoColumn FieldName="Nota" VisibleIndex="8" Visible="false">
                <EditFormSettings Visible="True"></EditFormSettings>
                <PropertiesMemoEdit Height="200" Width="300">
                </PropertiesMemoEdit>
            </dx:GridViewDataMemoColumn>

        </Columns>

        <GroupSummary>
            <dx:ASPxSummaryItem FieldName="Nombre" SummaryType="Count" DisplayFormat="n0" />
        </GroupSummary>
        <TotalSummary>
            <dx:ASPxSummaryItem FieldName="Nombre" SummaryType="Count" DisplayFormat="n0" />
        </TotalSummary>


        <Styles>

            <AlternatingRow Enabled="True" CssClass="gridDataRowAlt"></AlternatingRow>
            <RowHotTrack CssClass="gridDataHover"></RowHotTrack>
            <SelectedRow CssClass="gridDataSelectedRow"></SelectedRow>

            <SearchPanel>
            </SearchPanel>

            <Cell Wrap="True"></Cell>
            <EditForm CssClass="gridEditForm-1Col"></EditForm>

        </Styles>

        <Templates>
            <TitlePanel>
                <h3 class="d-inline">Contactos Grilla</h3>
                <a class="btn btn-sm btn-outline-dark" data-toggle="tooltip" data-placement="bottom" title="Vista Grilla" href="ContactsGrid.aspx">
                    <i class="fa fa-table"></i></a>
                <a class="btn btn-sm btn-outline-dark" data-toggle="tooltip" data-placement="bottom" title="Vista Cartas" href="ContactsCardView.aspx">
                    <i class="fa fa-th-large"></i></a>
            </TitlePanel>

            <EmptyDataRow>
                <div class="alert alert-warning" role="alert">No existen registros.</div>
            </EmptyDataRow>

            <EditForm>
                <div class="gridEditForm">

                    <dx:ContentControl ID="ContentControl1" runat="server">
                        <dx:ASPxGridViewTemplateReplacement ID="Editors" ReplacementType="EditFormEditors"
                            runat="server"></dx:ASPxGridViewTemplateReplacement>
                    </dx:ContentControl>

                </div>
                <hr />
                <div class="gridEditForm">
                    <dx:ASPxValidationSummary ID="ASPxValidationSummary1" runat="server" RenderMode="OrderedList" ShowErrorsInEditors="True"
                        HeaderText="Errores en el Formulario a Corregir" CssClass="panel panel-danger"
                        HeaderStyle-CssClass="panel-heading panel-danger" ValidationGroup='<%# Container.ValidationGroup %>' />
                    <span>

                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server" />
                    </span>
                    &nbsp;|&nbsp; 
							<span>
                                <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server" />
                            </span>
                </div>
            </EditForm>

            <DetailRow>

                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server"
                    EnableCallbackAnimation="True" EnableTabScrolling="False" Width="100%">

                    <TabPages>
                        <dx:TabPage Text="General" Name="General" NewLine="True">

                            <ContentCollection>
                                <dx:ContentControl runat="server">

                                    <table class="table detailRow-1Col">

                                        <tr>
                                            <td colspan="2">
                                                <dx:ASPxBinaryImage runat="server" EnableViewState="false"
                                                    Value='<%# Eval("Foto") %>'
                                                    CssClass="rounded-circle"
                                                    ImageSizeMode="FitProportional" Width="120" Height="120">
                                                </dx:ASPxBinaryImage>
                                            </td>
                                        </tr>

                                        <tr>
                                            <th class="tdCaption">Nota:</th>
                                            <td>
                                                <dx:ASPxMemo runat="server" EnableViewState="false" ReadOnly="true" Text='<%# Eval("Nota") %>' Height="200" Width="300">
                                                </dx:ASPxMemo>
                                            </td>
                                        </tr>

                                        <tr>
                                            <th class="tdCaption">Tags:</th>
                                            <td>
                                                <asp:TextBox runat="server" EnableViewState="false" ReadOnly="true" Text='<%# Eval("Tags") %>' Width="300"></asp:TextBox>
                                            </td>
                                        </tr>

                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>

                        </dx:TabPage>


                        <dx:TabPage Text="Correos" Name="Correos">
                            <ContentCollection>
                                <dx:ContentControl runat="server">

                                    <dx:ASPxGridView runat="server" ID="ASPxGridViewCorreo"
                                        DataSourceID="SqlDataSourceCorreo" KeyFieldName="CorreoID" AutoGenerateColumns="False"
                                        OnStartRowEditing="ASPxGridView1_StartRowEditing"
                                        OnRowDeleted="ASPxGridView1_RowDeleted"
                                        OnRowInserted="ASPxGridView1_RowInserted"
                                        OnRowUpdated="ASPxGridView1_RowUpdated"
                                        OnBeforePerformDataSelect="ASPxGridView1Detail_BeforePerformDataSelect"
                                        OnRowInserting="ASPxGridView1Detail_RowInserting">

                                        <SettingsBehavior AllowFocusedRow="true" />
                                        <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="DataAware" />
                                        <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />

                                        <SettingsCommandButton
                                            DeleteButton-ButtonType="Image"
                                            EditButton-ButtonType="Image"
                                            NewButton-ButtonType="Image"
                                            RenderMode="Button">
                                            <NewButton Image-IconID="actions_additem_16x16office2013" />
                                            <EditButton Image-IconID="edit_edit_16x16office2013" />
                                            <DeleteButton Image-IconID="spreadsheet_deletesheetrows_16x16" />
                                            <UpdateButton Image-IconID="actions_apply_16x16office2013" />
                                            <CancelButton Image-IconID="actions_cancel_16x16office2013" />
                                            <SearchPanelApplyButton>
                                            </SearchPanelApplyButton>
                                            <SearchPanelClearButton>
                                            </SearchPanelClearButton>
                                            <ApplyFilterButton></ApplyFilterButton>
                                            <ClearFilterButton></ClearFilterButton>
                                        </SettingsCommandButton>

                                        <Toolbars>
                                            <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true">
                                                <Items>
                                                    <dx:GridViewToolbarItem Command="New" />
                                                    <dx:GridViewToolbarItem Command="Edit" />
                                                    <dx:GridViewToolbarItem Command="Delete" />
                                                    <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />
                                                    <dx:GridViewToolbarItem Text="Exportar" Image-IconID="actions_download_16x16office2013" BeginGroup="true">
                                                        <Items>
                                                            <dx:GridViewToolbarItem Command="ExportToCsv" />
                                                            <dx:GridViewToolbarItem Command="ExportToXls" Text="Exportar XLS(DataAware)" />
                                                            <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Exportar XLSX(DataAware)" />
                                                        </Items>
                                                    </dx:GridViewToolbarItem>
                                                </Items>
                                            </dx:GridViewToolbar>
                                        </Toolbars>

                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="CorreoID" ReadOnly="True" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ContactoID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>

                                            <dx:GridViewDataComboBoxColumn FieldName="Clase" VisibleIndex="2">
                                                <PropertiesComboBox DropDownStyle="DropDown">
                                                    <Items>
                                                        <dx:ListEditItem Value="Laboral" />
                                                        <dx:ListEditItem Value="Personal" />
                                                        <dx:ListEditItem Value="Otro" />
                                                    </Items>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>

                                            <dx:GridViewDataTextColumn FieldName="Correo" VisibleIndex="3">
                                                <PropertiesTextEdit MaxLength="120">
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText"
                                                        ErrorFrameStyle-CssClass="alert-warning" CausesValidation="True">
                                                        <RequiredField IsRequired="True" ErrorText="Email es requerido" />
                                                        <RegularExpression ErrorText="Email no válido." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>

                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>

                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>

                        <dx:TabPage Text="Telefonos" Name="Telefonos">
                            <ContentCollection>
                                <dx:ContentControl runat="server">

                                    <dx:ASPxGridView runat="server" ID="ASPxGridViewTelefono"
                                        DataSourceID="SqlDataSourceTelefono" KeyFieldName="TelefonoID" AutoGenerateColumns="False"
                                        OnStartRowEditing="ASPxGridView1_StartRowEditing"
                                        OnRowDeleted="ASPxGridView1_RowDeleted"
                                        OnRowInserted="ASPxGridView1_RowInserted"
                                        OnRowUpdated="ASPxGridView1_RowUpdated"
                                        OnBeforePerformDataSelect="ASPxGridView1Detail_BeforePerformDataSelect"
                                        OnRowInserting="ASPxGridView1Detail_RowInserting">

                                        <SettingsBehavior AllowFocusedRow="true" />
                                        <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="DataAware" />
                                        <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />

                                        <SettingsCommandButton
                                            DeleteButton-ButtonType="Image"
                                            EditButton-ButtonType="Image"
                                            NewButton-ButtonType="Image"
                                            RenderMode="Button">
                                            <NewButton Image-IconID="actions_additem_16x16office2013" />
                                            <EditButton Image-IconID="edit_edit_16x16office2013" />
                                            <DeleteButton Image-IconID="spreadsheet_deletesheetrows_16x16" />
                                            <UpdateButton Image-IconID="actions_apply_16x16office2013" />
                                            <CancelButton Image-IconID="actions_cancel_16x16office2013" />
                                            <SearchPanelApplyButton>
                                            </SearchPanelApplyButton>
                                            <SearchPanelClearButton>
                                            </SearchPanelClearButton>
                                            <ApplyFilterButton></ApplyFilterButton>
                                            <ClearFilterButton></ClearFilterButton>
                                        </SettingsCommandButton>

                                        <Toolbars>
                                            <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true">
                                                <Items>
                                                    <dx:GridViewToolbarItem Command="New" />
                                                    <dx:GridViewToolbarItem Command="Edit" />
                                                    <dx:GridViewToolbarItem Command="Delete" />
                                                    <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />
                                                    <dx:GridViewToolbarItem Text="Exportar" Image-IconID="actions_download_16x16office2013" BeginGroup="true">
                                                        <Items>
                                                            <dx:GridViewToolbarItem Command="ExportToCsv" />
                                                            <dx:GridViewToolbarItem Command="ExportToXls" Text="Exportar XLS(DataAware)" />
                                                            <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Exportar XLSX(DataAware)" />
                                                        </Items>
                                                    </dx:GridViewToolbarItem>
                                                </Items>
                                            </dx:GridViewToolbar>
                                        </Toolbars>

                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="TelefonoID" ReadOnly="True" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ContactoID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>

                                            <dx:GridViewDataComboBoxColumn FieldName="Clase" VisibleIndex="2">
                                                <PropertiesComboBox DropDownStyle="DropDown">
                                                    <Items>
                                                        <dx:ListEditItem Value="Movil" />
                                                        <dx:ListEditItem Value="Laboral" />
                                                        <dx:ListEditItem Value="Local" />
                                                        <dx:ListEditItem Value="Personal" />
                                                        <dx:ListEditItem Value="Hogar" />
                                                        <dx:ListEditItem Value="Familiar" />
                                                        <dx:ListEditItem Value="Fax" />
                                                        <dx:ListEditItem Value="Otro" />
                                                    </Items>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>

                                            <dx:GridViewDataTextColumn FieldName="IndicativoPais" VisibleIndex="3">
                                                <PropertiesTextEdit DisplayFormatString="n0" MaxLength="5">
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText">
                                                        <RegularExpression ValidationExpression="^\d{1,5}(?:\.\d{0,0})?$" ErrorText="Valor numerico no valido." />
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="IndicativoLocal" VisibleIndex="4">
                                                <PropertiesTextEdit DisplayFormatString="n0" MaxLength="5">
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText">
                                                        <RegularExpression ValidationExpression="^\d{1,5}(?:\.\d{0,0})?$" ErrorText="Valor numerico no valido." />
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Telefono" VisibleIndex="5">
                                                <PropertiesTextEdit DisplayFormatString="n0" MaxLength="20">
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText">
                                                        <RequiredField IsRequired="True" ErrorText="Telefono es requerido" />
                                                        <RegularExpression ValidationExpression="^\d{1,20}(?:\.\d{0,0})?$" ErrorText="Valor numerico no valido." />
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>

                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>

                        <dx:TabPage Text="Mensajeria" Name="Mensajeria">
                            <ContentCollection>
                                <dx:ContentControl runat="server">

                                    <dx:ASPxGridView runat="server" ID="ASPxGridViewMensajeria"
                                        DataSourceID="SqlDataSourceMensajeria" KeyFieldName="MensajeriaID" AutoGenerateColumns="False"
                                        OnStartRowEditing="ASPxGridView1_StartRowEditing"
                                        OnRowDeleted="ASPxGridView1_RowDeleted"
                                        OnRowInserted="ASPxGridView1_RowInserted"
                                        OnRowUpdated="ASPxGridView1_RowUpdated"
                                        OnBeforePerformDataSelect="ASPxGridView1Detail_BeforePerformDataSelect"
                                        OnRowInserting="ASPxGridView1Detail_RowInserting">

                                        <SettingsBehavior AllowFocusedRow="true" />
                                        <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="DataAware" />
                                        <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />

                                        <SettingsCommandButton
                                            DeleteButton-ButtonType="Image"
                                            EditButton-ButtonType="Image"
                                            NewButton-ButtonType="Image"
                                            RenderMode="Button">
                                            <NewButton Image-IconID="actions_additem_16x16office2013" />
                                            <EditButton Image-IconID="edit_edit_16x16office2013" />
                                            <DeleteButton Image-IconID="spreadsheet_deletesheetrows_16x16" />
                                            <UpdateButton Image-IconID="actions_apply_16x16office2013" />
                                            <CancelButton Image-IconID="actions_cancel_16x16office2013" />
                                            <SearchPanelApplyButton>
                                            </SearchPanelApplyButton>
                                            <SearchPanelClearButton>
                                            </SearchPanelClearButton>
                                            <ApplyFilterButton></ApplyFilterButton>
                                            <ClearFilterButton></ClearFilterButton>
                                        </SettingsCommandButton>

                                        <Toolbars>
                                            <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true">
                                                <Items>
                                                    <dx:GridViewToolbarItem Command="New" />
                                                    <dx:GridViewToolbarItem Command="Edit" />
                                                    <dx:GridViewToolbarItem Command="Delete" />
                                                    <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />
                                                    <dx:GridViewToolbarItem Text="Exportar" Image-IconID="actions_download_16x16office2013" BeginGroup="true">
                                                        <Items>
                                                            <dx:GridViewToolbarItem Command="ExportToCsv" />
                                                            <dx:GridViewToolbarItem Command="ExportToXls" Text="Exportar XLS(DataAware)" />
                                                            <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Exportar XLSX(DataAware)" />
                                                        </Items>
                                                    </dx:GridViewToolbarItem>
                                                </Items>
                                            </dx:GridViewToolbar>
                                        </Toolbars>
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="MensajeriaID" ReadOnly="True" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ContactoID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>

                                            <dx:GridViewDataComboBoxColumn FieldName="Clase" VisibleIndex="2">
                                                <PropertiesComboBox DropDownStyle="DropDown">
                                                    <Items>
                                                        <dx:ListEditItem Value="Facebook Messenger" />
                                                        <dx:ListEditItem Value="Skype" />
                                                        <dx:ListEditItem Value="Snapchat" />
                                                        <dx:ListEditItem Value="Telegram" />
                                                        <dx:ListEditItem Value="WeChat" />
                                                        <dx:ListEditItem Value="WhatsApp" />
                                                        <dx:ListEditItem Value="Discord" />
                                                        <dx:ListEditItem Value="eBuddy" />
                                                        <dx:ListEditItem Value="iMessage" />
                                                        <dx:ListEditItem Value="Kik Messenger" />
                                                        <dx:ListEditItem Value="Line" />
                                                        <dx:ListEditItem Value="AIM" />
                                                        <dx:ListEditItem Value="BlackBerry Messenger" />
                                                        <dx:ListEditItem Value="Hike Messenger" />
                                                        <dx:ListEditItem Value="ICQ" />
                                                        <dx:ListEditItem Value="XMPP" />
                                                        <dx:ListEditItem Value="RTC" />
                                                        <dx:ListEditItem Value="Otro" />
                                                    </Items>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>

                                            <dx:GridViewDataTextColumn FieldName="Mensajeria" VisibleIndex="3">
                                                <PropertiesTextEdit>
                                                    <ValidationSettings>
                                                        <RequiredField IsRequired="True" ErrorText="Mensajeria es requerido" />
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>

                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>

                        <dx:TabPage Text="Redes Sociales" Name="Redes">
                            <ContentCollection>
                                <dx:ContentControl runat="server">

                                    <dx:ASPxGridView runat="server" ID="ASPxGridViewRedSocial"
                                        DataSourceID="SqlDataSourceRedSocial" KeyFieldName="RedSocialID" AutoGenerateColumns="False"
                                        OnStartRowEditing="ASPxGridView1_StartRowEditing"
                                        OnRowDeleted="ASPxGridView1_RowDeleted"
                                        OnRowInserted="ASPxGridView1_RowInserted"
                                        OnRowUpdated="ASPxGridView1_RowUpdated"
                                        OnBeforePerformDataSelect="ASPxGridView1Detail_BeforePerformDataSelect"
                                        OnRowInserting="ASPxGridView1Detail_RowInserting">

                                        <SettingsBehavior AllowFocusedRow="true" />
                                        <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="DataAware" />
                                        <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />

                                        <SettingsCommandButton
                                            DeleteButton-ButtonType="Image"
                                            EditButton-ButtonType="Image"
                                            NewButton-ButtonType="Image"
                                            RenderMode="Button">
                                            <NewButton Image-IconID="actions_additem_16x16office2013" />
                                            <EditButton Image-IconID="edit_edit_16x16office2013" />
                                            <DeleteButton Image-IconID="spreadsheet_deletesheetrows_16x16" />
                                            <UpdateButton Image-IconID="actions_apply_16x16office2013" />
                                            <CancelButton Image-IconID="actions_cancel_16x16office2013" />
                                            <SearchPanelApplyButton>
                                            </SearchPanelApplyButton>
                                            <SearchPanelClearButton>
                                            </SearchPanelClearButton>
                                            <ApplyFilterButton></ApplyFilterButton>
                                            <ClearFilterButton></ClearFilterButton>
                                        </SettingsCommandButton>

                                        <Toolbars>
                                            <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true">
                                                <Items>
                                                    <dx:GridViewToolbarItem Command="New" />
                                                    <dx:GridViewToolbarItem Command="Edit" />
                                                    <dx:GridViewToolbarItem Command="Delete" />
                                                    <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />
                                                    <dx:GridViewToolbarItem Text="Exportar" Image-IconID="actions_download_16x16office2013" BeginGroup="true">
                                                        <Items>
                                                            <dx:GridViewToolbarItem Command="ExportToCsv" />
                                                            <dx:GridViewToolbarItem Command="ExportToXls" Text="Exportar XLS(DataAware)" />
                                                            <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Exportar XLSX(DataAware)" />
                                                        </Items>
                                                    </dx:GridViewToolbarItem>
                                                </Items>
                                            </dx:GridViewToolbar>
                                        </Toolbars>

                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="RedSocialID" ReadOnly="True" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ContactoID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>

                                            <dx:GridViewDataComboBoxColumn FieldName="RedSocial" VisibleIndex="2">
                                                <PropertiesComboBox DropDownStyle="DropDown">
                                                    <Items>
                                                        <dx:ListEditItem Value="Facebook" />
                                                        <dx:ListEditItem Value="LinkedIn" />
                                                        <dx:ListEditItem Value="Instagram" />
                                                        <dx:ListEditItem Value="Twitter" />
                                                        <dx:ListEditItem Value="Pinterest" />
                                                        <dx:ListEditItem Value="YouTube" />
                                                        <dx:ListEditItem Value="WhatsApp" />
                                                        <dx:ListEditItem Value="QQ" />
                                                        <dx:ListEditItem Value="Tumblr" />
                                                        <dx:ListEditItem Value="QZone" />
                                                        <dx:ListEditItem Value="Sina Weibo" />
                                                        <dx:ListEditItem Value="Baidu Tieba" />
                                                        <dx:ListEditItem Value="Viber" />
                                                        <dx:ListEditItem Value="Reddit" />
                                                        <dx:ListEditItem Value="VKontakte" />
                                                        <dx:ListEditItem Value="Otro" />
                                                    </Items>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>



                                            <dx:GridViewDataTextColumn FieldName="RedSocialUsuario" VisibleIndex="3">
                                                <PropertiesTextEdit>
                                                    <ValidationSettings>
                                                        <RequiredField IsRequired="True" ErrorText="Valor requerido" />
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="RedSocialUsuarioID" VisibleIndex="4"></dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>

                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>

                        <dx:TabPage Text="Llamadas Internet" Name="Llamadas">
                            <ContentCollection>
                                <dx:ContentControl runat="server">

                                    <dx:ASPxGridView runat="server" ID="ASPxGridViewLLamadaInternet"
                                        DataSourceID="SqlDataSourceLLamadaInternet" KeyFieldName="LLamadaInternetID" AutoGenerateColumns="False"
                                        OnStartRowEditing="ASPxGridView1_StartRowEditing"
                                        OnRowDeleted="ASPxGridView1_RowDeleted"
                                        OnRowInserted="ASPxGridView1_RowInserted"
                                        OnRowUpdated="ASPxGridView1_RowUpdated"
                                        OnBeforePerformDataSelect="ASPxGridView1Detail_BeforePerformDataSelect"
                                        OnRowInserting="ASPxGridView1Detail_RowInserting">

                                        <SettingsBehavior AllowFocusedRow="true" />
                                        <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="DataAware" />
                                        <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />

                                        <SettingsCommandButton
                                            DeleteButton-ButtonType="Image"
                                            EditButton-ButtonType="Image"
                                            NewButton-ButtonType="Image"
                                            RenderMode="Button">
                                            <NewButton Image-IconID="actions_additem_16x16office2013" />
                                            <EditButton Image-IconID="edit_edit_16x16office2013" />
                                            <DeleteButton Image-IconID="spreadsheet_deletesheetrows_16x16" />
                                            <UpdateButton Image-IconID="actions_apply_16x16office2013" />
                                            <CancelButton Image-IconID="actions_cancel_16x16office2013" />
                                            <SearchPanelApplyButton>
                                            </SearchPanelApplyButton>
                                            <SearchPanelClearButton>
                                            </SearchPanelClearButton>
                                            <ApplyFilterButton></ApplyFilterButton>
                                            <ClearFilterButton></ClearFilterButton>
                                        </SettingsCommandButton>

                                        <Toolbars>
                                            <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true">
                                                <Items>
                                                    <dx:GridViewToolbarItem Command="New" />
                                                    <dx:GridViewToolbarItem Command="Edit" />
                                                    <dx:GridViewToolbarItem Command="Delete" />
                                                    <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />
                                                    <dx:GridViewToolbarItem Text="Exportar" Image-IconID="actions_download_16x16office2013" BeginGroup="true">
                                                        <Items>
                                                            <dx:GridViewToolbarItem Command="ExportToCsv" />
                                                            <dx:GridViewToolbarItem Command="ExportToXls" Text="Exportar XLS(DataAware)" />
                                                            <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Exportar XLSX(DataAware)" />
                                                        </Items>
                                                    </dx:GridViewToolbarItem>
                                                </Items>
                                            </dx:GridViewToolbar>
                                        </Toolbars>

                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="LLamadaInternetID" ReadOnly="True" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ContactoID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>

                                            <dx:GridViewDataComboBoxColumn FieldName="Clase" VisibleIndex="2">
                                                <PropertiesComboBox DropDownStyle="DropDown">
                                                    <Items>
                                                        <dx:ListEditItem Value="Skype" />
                                                        <dx:ListEditItem Value="Google Meet" />
                                                        <dx:ListEditItem Value="Viber" />
                                                        <dx:ListEditItem Value="ooVoo" />
                                                        <dx:ListEditItem Value="WeChat" />
                                                        <dx:ListEditItem Value="Voz IP" />
                                                        <dx:ListEditItem Value="Otro" />
                                                    </Items>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>

                                            <dx:GridViewDataTextColumn FieldName="LLamadaInternet" VisibleIndex="3">
                                                <PropertiesTextEdit>
                                                    <ValidationSettings>
                                                        <RequiredField IsRequired="True" ErrorText="Valor requerido" />
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>

                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>

                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>

                        <dx:TabPage Text="Relaciones" Name="Relaciones" NewLine="true">
                            <ContentCollection>
                                <dx:ContentControl runat="server">

                                    <dx:ASPxGridView runat="server" ID="ASPxGridViewRelacion"
                                        DataSourceID="SqlDataSourceRelacion" KeyFieldName="RelacionID" AutoGenerateColumns="False"
                                        OnStartRowEditing="ASPxGridView1_StartRowEditing"
                                        OnRowDeleted="ASPxGridView1_RowDeleted"
                                        OnRowInserted="ASPxGridView1_RowInserted"
                                        OnRowUpdated="ASPxGridView1_RowUpdated"
                                        OnBeforePerformDataSelect="ASPxGridView1Detail_BeforePerformDataSelect"
                                        OnRowInserting="ASPxGridView1Detail_RowInserting">

                                        <SettingsBehavior AllowFocusedRow="true" />
                                        <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="DataAware" />
                                        <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />

                                        <SettingsCommandButton
                                            DeleteButton-ButtonType="Image"
                                            EditButton-ButtonType="Image"
                                            NewButton-ButtonType="Image"
                                            RenderMode="Button">
                                            <NewButton Image-IconID="actions_additem_16x16office2013" />
                                            <EditButton Image-IconID="edit_edit_16x16office2013" />
                                            <DeleteButton Image-IconID="spreadsheet_deletesheetrows_16x16" />
                                            <UpdateButton Image-IconID="actions_apply_16x16office2013" />
                                            <CancelButton Image-IconID="actions_cancel_16x16office2013" />
                                            <SearchPanelApplyButton>
                                            </SearchPanelApplyButton>
                                            <SearchPanelClearButton>
                                            </SearchPanelClearButton>
                                            <ApplyFilterButton></ApplyFilterButton>
                                            <ClearFilterButton></ClearFilterButton>
                                        </SettingsCommandButton>

                                        <Toolbars>
                                            <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true">
                                                <Items>
                                                    <dx:GridViewToolbarItem Command="New" />
                                                    <dx:GridViewToolbarItem Command="Edit" />
                                                    <dx:GridViewToolbarItem Command="Delete" />
                                                    <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />
                                                    <dx:GridViewToolbarItem Text="Exportar" Image-IconID="actions_download_16x16office2013" BeginGroup="true">
                                                        <Items>
                                                            <dx:GridViewToolbarItem Command="ExportToCsv" />
                                                            <dx:GridViewToolbarItem Command="ExportToXls" Text="Exportar XLS(DataAware)" />
                                                            <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Exportar XLSX(DataAware)" />
                                                        </Items>
                                                    </dx:GridViewToolbarItem>
                                                </Items>
                                            </dx:GridViewToolbar>
                                        </Toolbars>

                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="RelacionID" ReadOnly="True" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ContactoID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>

                                            <dx:GridViewDataComboBoxColumn FieldName="Clase" VisibleIndex="2">
                                                <PropertiesComboBox DropDownStyle="DropDown">
                                                    <Items>
                                                        <dx:ListEditItem Value="Jefe" />
                                                        <dx:ListEditItem Value="Superior" />
                                                        <dx:ListEditItem Value="Asistente" />
                                                        <dx:ListEditItem Value="Secretaria" />
                                                        <dx:ListEditItem Value="Subalterno" />
                                                        <dx:ListEditItem Value="Compañero Trabajo" />
                                                        <dx:ListEditItem Value="Esposo" />
                                                        <dx:ListEditItem Value="Pareja" />
                                                        <dx:ListEditItem Value="Hijos" />
                                                        <dx:ListEditItem Value="Padres" />
                                                        <dx:ListEditItem Value="Otro" />
                                                    </Items>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>

                                            <dx:GridViewDataTextColumn FieldName="Relacion" VisibleIndex="3"></dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>

                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>

                        <dx:TabPage Text="URL" Name="URL">
                            <ContentCollection>
                                <dx:ContentControl runat="server">

                                    <dx:ASPxGridView runat="server" ID="ASPxGridViewURL"
                                        DataSourceID="SqlDataSourceURL" KeyFieldName="URLID" AutoGenerateColumns="False"
                                        OnStartRowEditing="ASPxGridView1_StartRowEditing"
                                        OnRowDeleted="ASPxGridView1_RowDeleted"
                                        OnRowInserted="ASPxGridView1_RowInserted"
                                        OnRowUpdated="ASPxGridView1_RowUpdated"
                                        OnBeforePerformDataSelect="ASPxGridView1Detail_BeforePerformDataSelect"
                                        OnRowInserting="ASPxGridView1Detail_RowInserting">

                                        <SettingsBehavior AllowFocusedRow="true" />
                                        <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="DataAware" />
                                        <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />

                                        <SettingsCommandButton
                                            DeleteButton-ButtonType="Image"
                                            EditButton-ButtonType="Image"
                                            NewButton-ButtonType="Image"
                                            RenderMode="Button">
                                            <NewButton Image-IconID="actions_additem_16x16office2013" />
                                            <EditButton Image-IconID="edit_edit_16x16office2013" />
                                            <DeleteButton Image-IconID="spreadsheet_deletesheetrows_16x16" />
                                            <UpdateButton Image-IconID="actions_apply_16x16office2013" />
                                            <CancelButton Image-IconID="actions_cancel_16x16office2013" />
                                            <SearchPanelApplyButton>
                                            </SearchPanelApplyButton>
                                            <SearchPanelClearButton>
                                            </SearchPanelClearButton>
                                            <ApplyFilterButton></ApplyFilterButton>
                                            <ClearFilterButton></ClearFilterButton>
                                        </SettingsCommandButton>

                                        <Toolbars>
                                            <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true">
                                                <Items>
                                                    <dx:GridViewToolbarItem Command="New" />
                                                    <dx:GridViewToolbarItem Command="Edit" />
                                                    <dx:GridViewToolbarItem Command="Delete" />
                                                    <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />
                                                    <dx:GridViewToolbarItem Text="Exportar" Image-IconID="actions_download_16x16office2013" BeginGroup="true">
                                                        <Items>
                                                            <dx:GridViewToolbarItem Command="ExportToCsv" />
                                                            <dx:GridViewToolbarItem Command="ExportToXls" Text="Exportar XLS(DataAware)" />
                                                            <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Exportar XLSX(DataAware)" />
                                                        </Items>
                                                    </dx:GridViewToolbarItem>
                                                </Items>
                                            </dx:GridViewToolbar>
                                        </Toolbars>
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="URLID" ReadOnly="True" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ContactoID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>

                                            <dx:GridViewDataComboBoxColumn FieldName="Clase" VisibleIndex="2">
                                                <PropertiesComboBox DropDownStyle="DropDown">
                                                    <Items>
                                                        <dx:ListEditItem Value="Laboral" />
                                                        <dx:ListEditItem Value="Personal" />
                                                        <dx:ListEditItem Value="blog" />
                                                        <dx:ListEditItem Value="Otro" />
                                                    </Items>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>

                                            <dx:GridViewDataTextColumn FieldName="URL" VisibleIndex="3">
                                                <PropertiesTextEdit>
                                                    <ValidationSettings>
                                                        <RequiredField IsRequired="True" ErrorText="Valor requerido" />
                                                        <RegularExpression ValidationExpression="/(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/"
                                                            ErrorText="URL no valido." />
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>

                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>

                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>

                        <dx:TabPage Text="Fechas" Name="Fechas">
                            <ContentCollection>
                                <dx:ContentControl runat="server">

                                    <dx:ASPxGridView runat="server" ID="ASPxGridViewFecha"
                                        DataSourceID="SqlDataSourceFecha" KeyFieldName="FechaID" AutoGenerateColumns="False"
                                        OnStartRowEditing="ASPxGridView1_StartRowEditing"
                                        OnRowDeleted="ASPxGridView1_RowDeleted"
                                        OnRowInserted="ASPxGridView1_RowInserted"
                                        OnRowUpdated="ASPxGridView1_RowUpdated"
                                        OnBeforePerformDataSelect="ASPxGridView1Detail_BeforePerformDataSelect"
                                        OnRowInserting="ASPxGridView1Detail_RowInserting">

                                        <SettingsBehavior AllowFocusedRow="true" />
                                        <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="DataAware" />
                                        <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />

                                        <SettingsCommandButton
                                            DeleteButton-ButtonType="Image"
                                            EditButton-ButtonType="Image"
                                            NewButton-ButtonType="Image"
                                            RenderMode="Button">
                                            <NewButton Image-IconID="actions_additem_16x16office2013" />
                                            <EditButton Image-IconID="edit_edit_16x16office2013" />
                                            <DeleteButton Image-IconID="spreadsheet_deletesheetrows_16x16" />
                                            <UpdateButton Image-IconID="actions_apply_16x16office2013" />
                                            <CancelButton Image-IconID="actions_cancel_16x16office2013" />
                                            <SearchPanelApplyButton>
                                            </SearchPanelApplyButton>
                                            <SearchPanelClearButton>
                                            </SearchPanelClearButton>
                                            <ApplyFilterButton></ApplyFilterButton>
                                            <ClearFilterButton></ClearFilterButton>
                                        </SettingsCommandButton>

                                        <Toolbars>
                                            <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true">
                                                <Items>
                                                    <dx:GridViewToolbarItem Command="New" />
                                                    <dx:GridViewToolbarItem Command="Edit" />
                                                    <dx:GridViewToolbarItem Command="Delete" />
                                                    <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />
                                                    <dx:GridViewToolbarItem Text="Exportar" Image-IconID="actions_download_16x16office2013" BeginGroup="true">
                                                        <Items>
                                                            <dx:GridViewToolbarItem Command="ExportToCsv" />
                                                            <dx:GridViewToolbarItem Command="ExportToXls" Text="Exportar XLS(DataAware)" />
                                                            <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Exportar XLSX(DataAware)" />
                                                        </Items>
                                                    </dx:GridViewToolbarItem>
                                                </Items>
                                            </dx:GridViewToolbar>
                                        </Toolbars>

                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="FechaID" ReadOnly="True" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ContactoID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                            
                                            <dx:GridViewDataComboBoxColumn FieldName="Clase" VisibleIndex="2">
                                                <PropertiesComboBox DropDownStyle="DropDown">
                                                    <Items>
                                                        <dx:ListEditItem Value="Cumpleaños" />
                                                        <dx:ListEditItem Value="Aniversario" />
                                                        <dx:ListEditItem Value="Otro" />
                                                    </Items>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>

                                            <dx:GridViewDataDateColumn FieldName="Fecha" VisibleIndex="3"></dx:GridViewDataDateColumn>
                                        </Columns>
                                    </dx:ASPxGridView>

                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>

                        <dx:TabPage Text="Direcciones" Name="Direcciones">
                            <ContentCollection>
                                <dx:ContentControl runat="server">

                                    <dx:ASPxGridView runat="server" ID="ASPxGridViewDireccion"
                                        DataSourceID="SqlDataSourceDireccion" KeyFieldName="DireccionID" AutoGenerateColumns="False"
                                        OnStartRowEditing="ASPxGridView1_StartRowEditing"
                                        OnRowDeleted="ASPxGridView1_RowDeleted"
                                        OnRowInserted="ASPxGridView1_RowInserted"
                                        OnRowUpdated="ASPxGridView1_RowUpdated"
                                        OnBeforePerformDataSelect="ASPxGridView1Detail_BeforePerformDataSelect"
                                        OnRowInserting="ASPxGridView1Detail_RowInserting">

                                        <SettingsBehavior AllowFocusedRow="true" />
                                        <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="DataAware" />
                                        <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />

                                        <SettingsCommandButton
                                            DeleteButton-ButtonType="Image"
                                            EditButton-ButtonType="Image"
                                            NewButton-ButtonType="Image"
                                            RenderMode="Button">
                                            <NewButton Image-IconID="actions_additem_16x16office2013" />
                                            <EditButton Image-IconID="edit_edit_16x16office2013" />
                                            <DeleteButton Image-IconID="spreadsheet_deletesheetrows_16x16" />
                                            <UpdateButton Image-IconID="actions_apply_16x16office2013" />
                                            <CancelButton Image-IconID="actions_cancel_16x16office2013" />
                                            <SearchPanelApplyButton>
                                            </SearchPanelApplyButton>
                                            <SearchPanelClearButton>
                                            </SearchPanelClearButton>
                                            <ApplyFilterButton></ApplyFilterButton>
                                            <ClearFilterButton></ClearFilterButton>
                                        </SettingsCommandButton>

                                        <Toolbars>
                                            <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true">
                                                <Items>
                                                    <dx:GridViewToolbarItem Command="New" />
                                                    <dx:GridViewToolbarItem Command="Edit" />
                                                    <dx:GridViewToolbarItem Command="Delete" />
                                                    <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />
                                                    <dx:GridViewToolbarItem Text="Exportar" Image-IconID="actions_download_16x16office2013" BeginGroup="true">
                                                        <Items>
                                                            <dx:GridViewToolbarItem Command="ExportToCsv" />
                                                            <dx:GridViewToolbarItem Command="ExportToXls" Text="Exportar XLS(DataAware)" />
                                                            <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Exportar XLSX(DataAware)" />
                                                        </Items>
                                                    </dx:GridViewToolbarItem>
                                                </Items>
                                            </dx:GridViewToolbar>
                                        </Toolbars>

                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="DireccionID" ReadOnly="True" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ContactoID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Clase" VisibleIndex="2"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Direccion" VisibleIndex="3"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Indicaciones" VisibleIndex="4"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ApartadoPostal" VisibleIndex="5"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Pais" VisibleIndex="6"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Region" VisibleIndex="7"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Ciudad" VisibleIndex="8"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="CodigoPostal" VisibleIndex="9"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Categoria" VisibleIndex="10"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Nota" VisibleIndex="11"></dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>

                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>

                        <dx:TabPage Text="Personalizado" Name="Personalizado">
                            <ContentCollection>
                                <dx:ContentControl runat="server">

                                    <dx:ASPxGridView runat="server" ID="ASPxGridViewPersonalizado"
                                        DataSourceID="SqlDataSourcePersonalizado" KeyFieldName="PersonalizadoID" AutoGenerateColumns="False"
                                        OnStartRowEditing="ASPxGridView1_StartRowEditing"
                                        OnRowDeleted="ASPxGridView1_RowDeleted"
                                        OnRowInserted="ASPxGridView1_RowInserted"
                                        OnRowUpdated="ASPxGridView1_RowUpdated"
                                        OnBeforePerformDataSelect="ASPxGridView1Detail_BeforePerformDataSelect"
                                        OnRowInserting="ASPxGridView1Detail_RowInserting">

                                        <SettingsBehavior AllowFocusedRow="true" />
                                        <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="DataAware" />
                                        <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />

                                        <SettingsCommandButton
                                            DeleteButton-ButtonType="Image"
                                            EditButton-ButtonType="Image"
                                            NewButton-ButtonType="Image"
                                            RenderMode="Button">
                                            <NewButton Image-IconID="actions_additem_16x16office2013" />
                                            <EditButton Image-IconID="edit_edit_16x16office2013" />
                                            <DeleteButton Image-IconID="spreadsheet_deletesheetrows_16x16" />
                                            <UpdateButton Image-IconID="actions_apply_16x16office2013" />
                                            <CancelButton Image-IconID="actions_cancel_16x16office2013" />
                                            <SearchPanelApplyButton>
                                            </SearchPanelApplyButton>
                                            <SearchPanelClearButton>
                                            </SearchPanelClearButton>
                                            <ApplyFilterButton></ApplyFilterButton>
                                            <ClearFilterButton></ClearFilterButton>
                                        </SettingsCommandButton>

                                        <Toolbars>
                                            <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true">
                                                <Items>
                                                    <dx:GridViewToolbarItem Command="New" />
                                                    <dx:GridViewToolbarItem Command="Edit" />
                                                    <dx:GridViewToolbarItem Command="Delete" />
                                                    <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />
                                                    <dx:GridViewToolbarItem Text="Exportar" Image-IconID="actions_download_16x16office2013" BeginGroup="true">
                                                        <Items>
                                                            <dx:GridViewToolbarItem Command="ExportToCsv" />
                                                            <dx:GridViewToolbarItem Command="ExportToXls" Text="Exportar XLS(DataAware)" />
                                                            <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Exportar XLSX(DataAware)" />
                                                        </Items>
                                                    </dx:GridViewToolbarItem>
                                                </Items>
                                            </dx:GridViewToolbar>
                                        </Toolbars>

                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="PersonalizadoID" ReadOnly="True" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ContactoID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Clase" VisibleIndex="2"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Personalizado" VisibleIndex="3"></dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>

                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>


                    </TabPages>

                </dx:ASPxPageControl>

            </DetailRow>

        </Templates>

    </dx:ASPxGridView>

    <asp:SqlDataSource ID="DataSourceMaster" runat="server"
        EnableCaching="true"
        CacheExpirationPolicy="Sliding"
        CancelSelectOnNullParameter="false"
        OnSelecting="DBMainDataSources_Selecting"
        OnInit="DBMainDataSources_Init"
        ConnectionString='<%$ ConnectionStrings:Development.MsSqlServer.Main %>'
        SelectCommand="SELECT [ContactoID], [Nombre], [Apellido], [Seudonimo], [MostrarComo], [Cargo], [Empresa], [Tags], [Nota], [Foto] FROM [Contacto]"
        DeleteCommand="DELETE FROM [Contacto] WHERE [ContactoID] = @ContactoID" InsertCommand="INSERT INTO [Contacto] ([Nombre], [Apellido], [Seudonimo], [MostrarComo], [Cargo], [Empresa], [Tags], [Nota], [Foto]) VALUES (@Nombre, @Apellido, @Seudonimo, @MostrarComo, @Cargo, @Empresa, @Tags, @Nota, @Foto)"
        UpdateCommand="UPDATE [Contacto] SET [Nombre] = @Nombre, [Apellido] = @Apellido, [Seudonimo] = @Seudonimo, [MostrarComo] = @MostrarComo, [Cargo] = @Cargo, [Empresa] = @Empresa, [Tags] = @Tags, [Nota] = @Nota, [Foto] = @Foto WHERE [ContactoID] = @ContactoID">
        <DeleteParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Nombre" Type="String"></asp:Parameter>
            <asp:Parameter Name="Apellido" Type="String"></asp:Parameter>
            <asp:Parameter Name="Seudonimo" Type="String"></asp:Parameter>
            <asp:Parameter Name="MostrarComo" Type="String"></asp:Parameter>
            <asp:Parameter Name="Cargo" Type="String"></asp:Parameter>
            <asp:Parameter Name="Empresa" Type="String"></asp:Parameter>
            <asp:Parameter Name="Tags" Type="String"></asp:Parameter>
            <asp:Parameter Name="Nota" Type="String"></asp:Parameter>
            <asp:Parameter Name="Foto" DbType="Binary"></asp:Parameter>
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Nombre" Type="String"></asp:Parameter>
            <asp:Parameter Name="Apellido" Type="String"></asp:Parameter>
            <asp:Parameter Name="Seudonimo" Type="String"></asp:Parameter>
            <asp:Parameter Name="MostrarComo" Type="String"></asp:Parameter>
            <asp:Parameter Name="Cargo" Type="String"></asp:Parameter>
            <asp:Parameter Name="Empresa" Type="String"></asp:Parameter>
            <asp:Parameter Name="Tags" Type="String"></asp:Parameter>
            <asp:Parameter Name="Nota" Type="String"></asp:Parameter>
            <asp:Parameter Name="Foto" DbType="Binary"></asp:Parameter>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSourceCorreo" runat="server"
        EnableCaching="true"
        CacheExpirationPolicy="Sliding"
        CancelSelectOnNullParameter="false"
        OnSelecting="DBMainDataSources_Selecting"
        OnInit="DBMainDataSources_Init"
        ConnectionString='<%$ ConnectionStrings:Development.MsSqlServer.Main %>'
        SelectCommand="SELECT * FROM [ContactoCorreo] WHERE ([ContactoID] = @ContactoID)"
        DeleteCommand="DELETE FROM [ContactoCorreo] WHERE [CorreoID] = @CorreoID"
        InsertCommand="INSERT INTO [ContactoCorreo] ( [ContactoID], [Clase], [Correo]) VALUES ( @ContactoID, @Clase, @Correo)"
        UpdateCommand="UPDATE [ContactoCorreo] SET [ContactoID] = @ContactoID, [Clase] = @Clase, [Correo] = @Correo WHERE [CorreoID] = @CorreoID">
        <DeleteParameters>
            <asp:Parameter Name="CorreoID" Type="Int32"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Clase" Type="String"></asp:Parameter>
            <asp:Parameter Name="Correo" Type="String"></asp:Parameter>
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Clase" Type="String"></asp:Parameter>
            <asp:Parameter Name="Correo" Type="String"></asp:Parameter>
            <asp:Parameter Name="CorreoID" Type="Int32"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSourceTelefono" runat="server"
        EnableCaching="true"
        CacheExpirationPolicy="Sliding"
        CancelSelectOnNullParameter="false"
        OnSelecting="DBMainDataSources_Selecting"
        OnInit="DBMainDataSources_Init"
        ConnectionString='<%$ ConnectionStrings:Development.MsSqlServer.Main %>'
        SelectCommand="SELECT * FROM [ContactoTelefono] WHERE ([ContactoID] = @ContactoID)"
        DeleteCommand="DELETE FROM [ContactoTelefono] WHERE [TelefonoID] = @TelefonoID"
        InsertCommand="INSERT INTO [ContactoTelefono] ( [ContactoID], [Clase], [IndicativoPais], [IndicativoLocal], [Telefono]) VALUES ( @ContactoID, @Clase, @IndicativoPais, @IndicativoLocal, @Telefono)"
        UpdateCommand="UPDATE [ContactoTelefono] SET [ContactoID] = @ContactoID, [Clase] = @Clase, [IndicativoPais] = @IndicativoPais, [IndicativoLocal] = @IndicativoLocal, [Telefono] = @Telefono WHERE [TelefonoID] = @TelefonoID">
        <DeleteParameters>
            <asp:Parameter Name="TelefonoID" Type="Int32"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Clase" Type="String"></asp:Parameter>
            <asp:Parameter Name="IndicativoPais" Type="String"></asp:Parameter>
            <asp:Parameter Name="IndicativoLocal" Type="String"></asp:Parameter>
            <asp:Parameter Name="Telefono" Type="String"></asp:Parameter>
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Clase" Type="String"></asp:Parameter>
            <asp:Parameter Name="IndicativoPais" Type="String"></asp:Parameter>
            <asp:Parameter Name="IndicativoLocal" Type="String"></asp:Parameter>
            <asp:Parameter Name="Telefono" Type="String"></asp:Parameter>
            <asp:Parameter Name="TelefonoID" Type="Int32"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSourceMensajeria" runat="server"
        EnableCaching="true"
        CacheExpirationPolicy="Sliding"
        CancelSelectOnNullParameter="false"
        OnSelecting="DBMainDataSources_Selecting"
        OnInit="DBMainDataSources_Init"
        ConnectionString='<%$ ConnectionStrings:Development.MsSqlServer.Main %>'
        SelectCommand="SELECT * FROM [ContactoMensajeria] WHERE ([ContactoID] = @ContactoID)"
        DeleteCommand="DELETE FROM [ContactoMensajeria] WHERE [MensajeriaID] = @MensajeriaID"
        InsertCommand="INSERT INTO [ContactoMensajeria] ( [ContactoID], [Clase], [Mensajeria]) VALUES ( @ContactoID, @Clase, @Mensajeria)"
        UpdateCommand="UPDATE [ContactoMensajeria] SET [ContactoID] = @ContactoID, [Clase] = @Clase, [Mensajeria] = @Mensajeria WHERE [MensajeriaID] = @MensajeriaID">
        <DeleteParameters>
            <asp:Parameter Name="MensajeriaID" Type="Int32"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Clase" Type="String"></asp:Parameter>
            <asp:Parameter Name="Mensajeria" Type="String"></asp:Parameter>
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Clase" Type="String"></asp:Parameter>
            <asp:Parameter Name="Mensajeria" Type="String"></asp:Parameter>
            <asp:Parameter Name="MensajeriaID" Type="Int32"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSourceRedSocial" runat="server"
        EnableCaching="true"
        CacheExpirationPolicy="Sliding"
        CancelSelectOnNullParameter="false"
        OnSelecting="DBMainDataSources_Selecting"
        OnInit="DBMainDataSources_Init"
        ConnectionString='<%$ ConnectionStrings:Development.MsSqlServer.Main %>'
        SelectCommand="SELECT * FROM [ContactoRedSocial] WHERE ([ContactoID] = @ContactoID)"
        DeleteCommand="DELETE FROM [ContactoRedSocial] WHERE [RedSocialID] = @RedSocialID"
        InsertCommand="INSERT INTO [ContactoRedSocial] ( [ContactoID], [RedSocial], [RedSocialUsuario], [RedSocialUsuarioID]) VALUES ( @ContactoID, @RedSocial, @RedSocialUsuario, @RedSocialUsuarioID)"
        UpdateCommand="UPDATE [ContactoRedSocial] SET [ContactoID] = @ContactoID, [RedSocial] = @RedSocial, [RedSocialUsuario] = @RedSocialUsuario, [RedSocialUsuarioID] = @RedSocialUsuarioID WHERE [RedSocialID] = @RedSocialID">
        <DeleteParameters>
            <asp:Parameter Name="RedSocialID" Type="Int32"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="RedSocial" Type="String"></asp:Parameter>
            <asp:Parameter Name="RedSocialUsuario" Type="String"></asp:Parameter>
            <asp:Parameter Name="RedSocialUsuarioID" Type="String"></asp:Parameter>
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="RedSocial" Type="String"></asp:Parameter>
            <asp:Parameter Name="RedSocialUsuario" Type="String"></asp:Parameter>
            <asp:Parameter Name="RedSocialUsuarioID" Type="String"></asp:Parameter>
            <asp:Parameter Name="RedSocialID" Type="Int32"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSourceLLamadaInternet" runat="server"
        EnableCaching="true"
        CacheExpirationPolicy="Sliding"
        CancelSelectOnNullParameter="false"
        OnSelecting="DBMainDataSources_Selecting"
        OnInit="DBMainDataSources_Init"
        ConnectionString='<%$ ConnectionStrings:Development.MsSqlServer.Main %>'
        SelectCommand="SELECT * FROM [ContactoLLamadaInternet] WHERE ([ContactoID] = @ContactoID)"
        DeleteCommand="DELETE FROM [ContactoLLamadaInternet] WHERE [LLamadaInternetID] = @LLamadaInternetID"
        InsertCommand="INSERT INTO [ContactoLLamadaInternet] ( [ContactoID], [Clase], [LLamadaInternet]) VALUES ( @ContactoID, @Clase, @LLamadaInternet)"
        UpdateCommand="UPDATE [ContactoLLamadaInternet] SET [ContactoID] = @ContactoID, [Clase] = @Clase, [LLamadaInternet] = @LLamadaInternet WHERE [LLamadaInternetID] = @LLamadaInternetID">
        <DeleteParameters>
            <asp:Parameter Name="LLamadaInternetID" Type="Int32"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Clase" Type="String"></asp:Parameter>
            <asp:Parameter Name="LLamadaInternet" Type="String"></asp:Parameter>
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Clase" Type="String"></asp:Parameter>
            <asp:Parameter Name="LLamadaInternet" Type="String"></asp:Parameter>
            <asp:Parameter Name="LLamadaInternetID" Type="Int32"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSourceRelacion" runat="server"
        EnableCaching="true"
        CacheExpirationPolicy="Sliding"
        CancelSelectOnNullParameter="false"
        OnSelecting="DBMainDataSources_Selecting"
        OnInit="DBMainDataSources_Init"
        ConnectionString='<%$ ConnectionStrings:Development.MsSqlServer.Main %>'
        SelectCommand="SELECT * FROM [ContactoRelacion] WHERE ([ContactoID] = @ContactoID)"
        DeleteCommand="DELETE FROM [ContactoRelacion] WHERE [RelacionID] = @RelacionID"
        InsertCommand="INSERT INTO [ContactoRelacion] ( [ContactoID], [Clase], [Relacion]) VALUES ( @ContactoID, @Clase, @Relacion)"
        UpdateCommand="UPDATE [ContactoRelacion] SET [ContactoID] = @ContactoID, [Clase] = @Clase, [Relacion] = @Relacion WHERE [RelacionID] = @RelacionID">
        <DeleteParameters>
            <asp:Parameter Name="RelacionID" Type="Int32"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Clase" Type="String"></asp:Parameter>
            <asp:Parameter Name="Relacion" Type="String"></asp:Parameter>
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Clase" Type="String"></asp:Parameter>
            <asp:Parameter Name="Relacion" Type="String"></asp:Parameter>
            <asp:Parameter Name="RelacionID" Type="Int32"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSourceURL" runat="server"
        EnableCaching="true"
        CacheExpirationPolicy="Sliding"
        CancelSelectOnNullParameter="false"
        OnSelecting="DBMainDataSources_Selecting"
        OnInit="DBMainDataSources_Init"
        ConnectionString='<%$ ConnectionStrings:Development.MsSqlServer.Main %>'
        SelectCommand="SELECT * FROM [ContactoURL] WHERE ([ContactoID] = @ContactoID)"
        DeleteCommand="DELETE FROM [ContactoURL] WHERE [URLID] = @URLID"
        InsertCommand="INSERT INTO [ContactoURL] ( [ContactoID], [Clase], [URL]) VALUES ( @ContactoID, @Clase, @URL)"
        UpdateCommand="UPDATE [ContactoURL] SET [ContactoID] = @ContactoID, [Clase] = @Clase, [URL] = @URL WHERE [URLID] = @URLID">
        <DeleteParameters>
            <asp:Parameter Name="URLID" Type="Int32"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Clase" Type="String"></asp:Parameter>
            <asp:Parameter Name="URL" Type="String"></asp:Parameter>
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Clase" Type="String"></asp:Parameter>
            <asp:Parameter Name="URL" Type="String"></asp:Parameter>
            <asp:Parameter Name="URLID" Type="Int32"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSourceFecha" runat="server"
        EnableCaching="true"
        CacheExpirationPolicy="Sliding"
        CancelSelectOnNullParameter="false"
        OnSelecting="DBMainDataSources_Selecting"
        OnInit="DBMainDataSources_Init"
        ConnectionString='<%$ ConnectionStrings:Development.MsSqlServer.Main %>'
        SelectCommand="SELECT * FROM [ContactoFecha] WHERE ([ContactoID] = @ContactoID)"
        DeleteCommand="DELETE FROM [ContactoFecha] WHERE [FechaID] = @FechaID"
        InsertCommand="INSERT INTO [ContactoFecha] ( [ContactoID], [Clase], [Fecha]) VALUES ( @ContactoID, @Clase, @Fecha)"
        UpdateCommand="UPDATE [ContactoFecha] SET [ContactoID] = @ContactoID, [Clase] = @Clase, [Fecha] = @Fecha WHERE [FechaID] = @FechaID">
        <DeleteParameters>
            <asp:Parameter Name="FechaID" Type="Int32"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Clase" Type="String"></asp:Parameter>
            <asp:Parameter Name="Fecha" DbType="Date"></asp:Parameter>
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Clase" Type="String"></asp:Parameter>
            <asp:Parameter Name="Fecha" DbType="Date"></asp:Parameter>
            <asp:Parameter Name="FechaID" Type="Int32"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSourceDireccion" runat="server"
        EnableCaching="true"
        CacheExpirationPolicy="Sliding"
        CancelSelectOnNullParameter="false"
        OnSelecting="DBMainDataSources_Selecting"
        OnInit="DBMainDataSources_Init"
        ConnectionString='<%$ ConnectionStrings:Development.MsSqlServer.Main %>'
        SelectCommand="SELECT * FROM [ContactoDireccion] WHERE ([ContactoID] = @ContactoID)"
        DeleteCommand="DELETE FROM [ContactoDireccion] WHERE [DireccionID] = @DireccionID"
        InsertCommand="INSERT INTO [ContactoDireccion] ( [ContactoID], [Clase], [Direccion], [Indicaciones], [ApartadoPostal], [Pais], [Region], [Ciudad], [CodigoPostal], [Categoria], [Nota]) VALUES ( @ContactoID, @Clase, @Direccion, @Indicaciones, @ApartadoPostal, @Pais, @Region, @Ciudad, @CodigoPostal, @Categoria, @Nota)"
        UpdateCommand="UPDATE [ContactoDireccion] SET [ContactoID] = @ContactoID, [Clase] = @Clase, [Direccion] = @Direccion, [Indicaciones] = @Indicaciones, [ApartadoPostal] = @ApartadoPostal, [Pais] = @Pais, [Region] = @Region, [Ciudad] = @Ciudad, [CodigoPostal] = @CodigoPostal, [Categoria] = @Categoria, [Nota] = @Nota WHERE [DireccionID] = @DireccionID">
        <DeleteParameters>
            <asp:Parameter Name="DireccionID" Type="Int32"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Clase" Type="String"></asp:Parameter>
            <asp:Parameter Name="Direccion" Type="String"></asp:Parameter>
            <asp:Parameter Name="Indicaciones" Type="String"></asp:Parameter>
            <asp:Parameter Name="ApartadoPostal" Type="String"></asp:Parameter>
            <asp:Parameter Name="Pais" Type="String"></asp:Parameter>
            <asp:Parameter Name="Region" Type="String"></asp:Parameter>
            <asp:Parameter Name="Ciudad" Type="String"></asp:Parameter>
            <asp:Parameter Name="CodigoPostal" Type="String"></asp:Parameter>
            <asp:Parameter Name="Categoria" Type="String"></asp:Parameter>
            <asp:Parameter Name="Nota" Type="String"></asp:Parameter>
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Clase" Type="String"></asp:Parameter>
            <asp:Parameter Name="Direccion" Type="String"></asp:Parameter>
            <asp:Parameter Name="Indicaciones" Type="String"></asp:Parameter>
            <asp:Parameter Name="ApartadoPostal" Type="String"></asp:Parameter>
            <asp:Parameter Name="Pais" Type="String"></asp:Parameter>
            <asp:Parameter Name="Region" Type="String"></asp:Parameter>
            <asp:Parameter Name="Ciudad" Type="String"></asp:Parameter>
            <asp:Parameter Name="CodigoPostal" Type="String"></asp:Parameter>
            <asp:Parameter Name="Categoria" Type="String"></asp:Parameter>
            <asp:Parameter Name="Nota" Type="String"></asp:Parameter>
            <asp:Parameter Name="DireccionID" Type="Int32"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSourcePersonalizado" runat="server"
        EnableCaching="true"
        CacheExpirationPolicy="Sliding"
        CancelSelectOnNullParameter="false"
        OnSelecting="DBMainDataSources_Selecting"
        OnInit="DBMainDataSources_Init"
        ConnectionString='<%$ ConnectionStrings:Development.MsSqlServer.Main %>'
        SelectCommand="SELECT * FROM [ContactoPersonalizado] WHERE ([ContactoID] = @ContactoID)"
        DeleteCommand="DELETE FROM [ContactoPersonalizado] WHERE [PersonalizadoID] = @PersonalizadoID"
        InsertCommand="INSERT INTO [ContactoPersonalizado] ( [ContactoID], [Clase], [Personalizado]) VALUES ( @ContactoID, @Clase, @Personalizado)"
        UpdateCommand="UPDATE [ContactoPersonalizado] SET [ContactoID] = @ContactoID, [Clase] = @Clase, [Personalizado] = @Personalizado WHERE [PersonalizadoID] = @PersonalizadoID">
        <DeleteParameters>
            <asp:Parameter Name="PersonalizadoID" Type="Int32"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Clase" Type="String"></asp:Parameter>
            <asp:Parameter Name="Personalizado" Type="String"></asp:Parameter>
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ContactoID" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Clase" Type="String"></asp:Parameter>
            <asp:Parameter Name="Personalizado" Type="String"></asp:Parameter>
            <asp:Parameter Name="PersonalizadoID" Type="Int32"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageFooter" runat="server">
</asp:Content>
