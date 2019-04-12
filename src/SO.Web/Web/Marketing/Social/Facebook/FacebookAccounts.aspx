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
    CodeBehind="FacebookAccounts.aspx.cs" Inherits="SO.Web.Marketing.Social.Facebook.FacebookAccounts" %>

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
                ASPxClientUtils.DeleteCookie("<%= MasterGridCookieName  %>");<%-- *** WARNING SET THE SAME VALUE OF SettingsCookies-CookiesID OF GRID *** --%>
                targetGrid.Refresh();
                window.location.reload();
            }
        }
        function ResetDetGrid(targetGrid) {
            if (confirm('¿Esta seguro de RESETEAR el diseño de la grilla actual?')) {
                ASPxClientUtils.DeleteCookie("<%= DetailGridCookieName  %>");<%-- *** WARNING SET THE SAME VALUE OF SettingsCookies-CookiesID OF GRID *** --%>
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
        KeyFieldName="FacebookCuentaID"
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

            <dx:GridViewDataTextColumn FieldName="FacebookCuentaID" ReadOnly="True" VisibleIndex="0">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataBinaryImageColumn FieldName="FotoPerfil" VisibleIndex="0" Visible="false">
                <EditFormSettings Visible="True"></EditFormSettings>
                <PropertiesBinaryImage EnableServerResize="True" ImageSizeMode="ActualSizeOrFit" ImageHeight="320" ImageWidth="320">
                    <EditingSettings Enabled="true" UploadSettings-UploadValidationSettings-MaxFileSize="4194304" />
                    <ValidationSettings>
                        <ErrorImage ToolTip="Puede usar archivos JPG, GIF o PNG; de Maximo 4MB."></ErrorImage>
                    </ValidationSettings>
                </PropertiesBinaryImage>
            </dx:GridViewDataBinaryImageColumn>

            <dx:GridViewDataTextColumn FieldName="FBUserID" VisibleIndex="1" Width="150">
                <PropertiesTextEdit MaxLength="20">
                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" CausesValidation="True" ErrorFrameStyle-CssClass="alert-warning">
                        <RequiredField IsRequired="True" ErrorText="Valor requerido." />
                    </ValidationSettings>
                </PropertiesTextEdit>

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataDateColumn FieldName="FechaCreacion" VisibleIndex="2">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn FieldName="Categoria" VisibleIndex="4"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Subcategoria" VisibleIndex="5"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Usuario" VisibleIndex="6"></dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="Nombre" VisibleIndex="7">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Apellido" VisibleIndex="8"></dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="Login" VisibleIndex="9">
                <PropertiesTextEdit MaxLength="30">
                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" CausesValidation="True" ErrorFrameStyle-CssClass="alert-warning">
                        <RequiredField IsRequired="True" ErrorText="Valor requerido." />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="Password" Visible="false" VisibleIndex="10">
                <EditFormSettings Visible="True"></EditFormSettings>
                <PropertiesTextEdit MaxLength="30">
                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" CausesValidation="True" ErrorFrameStyle-CssClass="alert-warning">
                        <RequiredField IsRequired="True" ErrorText="Valor requerido." />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="Email" VisibleIndex="11">
                <PropertiesTextEdit MaxLength="120">
                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText"
                        ErrorFrameStyle-CssClass="alert-warning" CausesValidation="True">
                        <RegularExpression ErrorText="Email no válido." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="Movil" VisibleIndex="12">
                <PropertiesTextEdit DisplayFormatString="n0" MaxLength="20">
                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText">
                        <RegularExpression ValidationExpression="^\d{1,20}(?:\.\d{0,0})?$" ErrorText="Valor numerico no valido." />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataDateColumn FieldName="FechaNacimiento" VisibleIndex="13"></dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn FieldName="Pais" VisibleIndex="14"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Ciudad" VisibleIndex="15"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Empresa" VisibleIndex="17"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Profesion" VisibleIndex="18"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Descripcion" VisibleIndex="3"></dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="Amigos" VisibleIndex="19">
                <PropertiesTextEdit DisplayFormatString="n0" MaxLength="20">
                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText">
                        <RegularExpression ValidationExpression="^\d{1,10}(?:\.\d{0,0})?$" ErrorText="Valor numerico no valido." />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="Grupos" VisibleIndex="20">
                <PropertiesTextEdit DisplayFormatString="n0" MaxLength="20">
                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText">
                        <RegularExpression ValidationExpression="^\d{1,10}(?:\.\d{0,0})?$" ErrorText="Valor numerico no valido." />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTokenBoxColumn FieldName="Tags" VisibleIndex="21"></dx:GridViewDataTokenBoxColumn>

            <dx:GridViewDataMemoColumn FieldName="Nota" VisibleIndex="22" Visible="false">
                <EditFormSettings Visible="True"></EditFormSettings>
                <PropertiesMemoEdit Height="200" Width="300">
                </PropertiesMemoEdit>
            </dx:GridViewDataMemoColumn>

        </Columns>

        <GroupSummary>
            <dx:ASPxSummaryItem FieldName="FBUserID" SummaryType="Count" DisplayFormat="n0" />
        </GroupSummary>

        <TotalSummary>
            <dx:ASPxSummaryItem FieldName="FBUserID" SummaryType="Count" DisplayFormat="n0" />
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
                <h3 class="d-inline">Cuentas Facebook Grilla</h3>
                <a class="btn btn-sm btn-outline-dark" data-toggle="tooltip" data-placement="bottom" title="Vista Grilla" href="ContactsGrid.aspx">
                    <i class="fa fa-table"></i></a><%-- 
                <a class="btn btn-sm btn-outline-dark" data-toggle="tooltip" data-placement="bottom" title="Vista Cartas" href="ContactsCardView.aspx">
                    <i class="fa fa-th-large"></i></a>--%>
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

                <table class="table detailRow-1Col">

                    <tr>
                        <td colspan="2">
                            <dx:ASPxBinaryImage runat="server" EnableViewState="false"
                                Value='<%# Eval("FotoPerfil") %>'
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
        SelectCommand="SELECT [FacebookCuentaID], [FBUserID], [FechaCreacion], [Descripcion], [Categoria], [Subcategoria], [Usuario], [Nombre], [Apellido], [Login], [Password], [Email], [Movil], [FechaNacimiento], [Pais], [Ciudad], [Sexo], [Empresa], [Profesion], [Amigos], [Grupos], [Tags], [Nota], [FotoPerfil] FROM [FacebookCuenta]"
        DeleteCommand="DELETE FROM [FacebookCuenta] WHERE [FacebookCuentaID] = @FacebookCuentaID"
        InsertCommand="INSERT INTO [FacebookCuenta] ([FBUserID], [FechaCreacion], [Descripcion], [Categoria], [Subcategoria], [Usuario], [Nombre], [Apellido], [Login], [Password], [Email], [Movil], [FechaNacimiento], [Pais], [Ciudad], [Sexo], [Empresa], [Profesion], [Amigos], [Grupos], [Tags], [Nota], [FotoPerfil]) VALUES
        (@FBUserID, 
         GETDATE() --@FechaCreacion
        , @Descripcion, @Categoria, @Subcategoria, @Usuario, @Nombre, @Apellido, @Login, @Password, @Email, @Movil, @FechaNacimiento, @Pais, @Ciudad, @Sexo, @Empresa, @Profesion, @Amigos, @Grupos, @Tags, @Nota, @FotoPerfil)"
        UpdateCommand="UPDATE [FacebookCuenta] SET [FBUserID] = @FBUserID, [Descripcion] = @Descripcion, [Categoria] = @Categoria, [Subcategoria] = @Subcategoria, [Usuario] = @Usuario, [Nombre] = @Nombre, [Apellido] = @Apellido, [Login] = @Login, [Password] = @Password, [Email] = @Email, [Movil] = @Movil, [FechaNacimiento] = @FechaNacimiento, [Pais] = @Pais, [Ciudad] = @Ciudad, [Sexo] = @Sexo, [Empresa] = @Empresa, [Profesion] = @Profesion, [Amigos] = @Amigos, [Grupos] = @Grupos, [Tags] = @Tags, [Nota] = @Nota, [FotoPerfil] = @FotoPerfil WHERE [FacebookCuentaID] = @FacebookCuentaID">
        <DeleteParameters>
            <asp:Parameter Name="FacebookCuentaID" Type="Int32"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="FBUserID" Type="String"></asp:Parameter>
            <asp:Parameter Name="FechaCreacion" Type="DateTime"></asp:Parameter>
            <asp:Parameter Name="Descripcion" Type="String"></asp:Parameter>
            <asp:Parameter Name="Categoria" Type="String"></asp:Parameter>
            <asp:Parameter Name="Subcategoria" Type="String"></asp:Parameter>
            <asp:Parameter Name="Usuario" Type="String"></asp:Parameter>
            <asp:Parameter Name="Nombre" Type="String"></asp:Parameter>
            <asp:Parameter Name="Apellido" Type="String"></asp:Parameter>
            <asp:Parameter Name="Login" Type="String"></asp:Parameter>
            <asp:Parameter Name="Password" Type="String"></asp:Parameter>
            <asp:Parameter Name="Email" Type="String"></asp:Parameter>
            <asp:Parameter Name="Movil" Type="String"></asp:Parameter>
            <asp:Parameter DbType="Date" Name="FechaNacimiento"></asp:Parameter>
            <asp:Parameter Name="Pais" Type="String"></asp:Parameter>
            <asp:Parameter Name="Ciudad" Type="String"></asp:Parameter>
            <asp:Parameter Name="Sexo" Type="String"></asp:Parameter>
            <asp:Parameter Name="Empresa" Type="String"></asp:Parameter>
            <asp:Parameter Name="Profesion" Type="String"></asp:Parameter>
            <asp:Parameter Name="Amigos" Type="Int32" DefaultValue="0"></asp:Parameter>
            <asp:Parameter Name="Grupos" Type="Int32" DefaultValue="0"></asp:Parameter>
            <asp:Parameter Name="Tags" Type="String"></asp:Parameter>
            <asp:Parameter Name="Nota" Type="String"></asp:Parameter>
            <asp:Parameter Name="FotoPerfil" DbType="Binary"></asp:Parameter>
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="FBUserID" Type="String"></asp:Parameter>
            <asp:Parameter Name="FechaCreacion" Type="DateTime"></asp:Parameter>
            <asp:Parameter Name="Descripcion" Type="String"></asp:Parameter>
            <asp:Parameter Name="Categoria" Type="String"></asp:Parameter>
            <asp:Parameter Name="Subcategoria" Type="String"></asp:Parameter>
            <asp:Parameter Name="Usuario" Type="String"></asp:Parameter>
            <asp:Parameter Name="Nombre" Type="String"></asp:Parameter>
            <asp:Parameter Name="Apellido" Type="String"></asp:Parameter>
            <asp:Parameter Name="Login" Type="String"></asp:Parameter>
            <asp:Parameter Name="Password" Type="String"></asp:Parameter>
            <asp:Parameter Name="Email" Type="String"></asp:Parameter>
            <asp:Parameter Name="Movil" Type="String"></asp:Parameter>
            <asp:Parameter DbType="Date" Name="FechaNacimiento"></asp:Parameter>
            <asp:Parameter Name="Pais" Type="String"></asp:Parameter>
            <asp:Parameter Name="Ciudad" Type="String"></asp:Parameter>
            <asp:Parameter Name="Sexo" Type="String"></asp:Parameter>
            <asp:Parameter Name="Empresa" Type="String"></asp:Parameter>
            <asp:Parameter Name="Profesion" Type="String"></asp:Parameter>
            <asp:Parameter Name="Amigos" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Grupos" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="Tags" Type="String"></asp:Parameter>
            <asp:Parameter Name="Nota" Type="String"></asp:Parameter>
            <asp:Parameter Name="FotoPerfil" DbType="Binary"></asp:Parameter>
            <asp:Parameter Name="FacebookCuentaID" Type="Int32"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageFooter" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageBottom" runat="server">
</asp:Content>
