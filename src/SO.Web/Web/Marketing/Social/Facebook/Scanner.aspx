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
    CodeBehind="Scanner.aspx.cs" Inherits="SO.Web.Marketing.Social.Facebook.Scanner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHead" runat="server">

    <link href="/Content/FBScanner.css" rel="stylesheet" />

    <script>
        var id = "me";

        function setID(val) {
            id = val;
            $("#infoUserID").html(id);
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <div class="bs-callout bs-callout-primary">
        <h1><i class="fa fa-rainbow float-left"></i>Scanner</h1>
        <p>Consulte datos de un usuario de Facebook compartidos o públicos.</p>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageMain" runat="server">

    <dx:ASPxGridView runat="server" ID="ASPxGridViewFriends"
        DataSourceID="DataSourceMaster" AutoGenerateColumns="False"
        Width="1600">

        <SettingsCookies CookiesID="ASPxGridViewFriends" Enabled="true" StoreControlWidth="True" />
        <SettingsDetail ShowDetailButtons="true" />
        <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
        <SettingsDataSecurity AllowEdit="False" AllowInsert="False" AllowDelete="False"></SettingsDataSecurity>
        <SettingsSearchPanel Visible="True" ShowApplyButton="True" ShowClearButton="True"></SettingsSearchPanel>
        <SettingsCommandButton RenderMode="Button">
        </SettingsCommandButton>

        <SettingsBehavior ConfirmDelete="True" EnableRowHotTrack="True"
            AllowDragDrop="True" AllowFocusedRow="True" AllowGroup="True" AllowSort="True" AllowSelectByRowClick="True"
            EnableCustomizationWindow="True" AllowEllipsisInText="False" />

        <SettingsPager PageSize="10" Position="Bottom" AllButton-Visible="False" PageSizeItemSettings-Visible="false"
            PageSizeItemSettings-ShowAllItem="False" NumericButtonCount="10">
        </SettingsPager>


        <Columns>
            <dx:GridViewDataTextColumn FieldName="FacebookFriendID" ReadOnly="True" Visible="false" VisibleIndex="1">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="FBUserID" Caption="SELECCIONE" VisibleIndex="2">
                <DataItemTemplate>
                    <a class="btn" href="javascript:setID(<%# Eval("FBUserID") %>)"><%# Eval("FBUserID") %></a>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="FechaCreacion" VisibleIndex="3"></dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn FieldName="Descripcion" VisibleIndex="4"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Categoria" VisibleIndex="5"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Subcategoria" VisibleIndex="6"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Usuario" VisibleIndex="7"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Nombre" VisibleIndex="8"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Apellido" VisibleIndex="9"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Email" VisibleIndex="10"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Movil" VisibleIndex="11"></dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="FechaNacimiento" VisibleIndex="12"></dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn FieldName="Pais" VisibleIndex="13"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Ciudad" VisibleIndex="14"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Sexo" VisibleIndex="15"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Empresa" VisibleIndex="16"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Profesion" VisibleIndex="17"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Amigos" VisibleIndex="18"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Grupos" VisibleIndex="19"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTokenBoxColumn FieldName="Tags" VisibleIndex="21">
                <PropertiesTokenBox Tokens="" AllowMouseWheel="True"></PropertiesTokenBox>
            </dx:GridViewDataTokenBoxColumn>
        </Columns>

        <GroupSummary>
            <dx:ASPxSummaryItem FieldName="FBUserID" SummaryType="Count" ValueDisplayFormat="n0" />
        </GroupSummary>
        <TotalSummary>
            <dx:ASPxSummaryItem FieldName="FBUserID" SummaryType="Count" ValueDisplayFormat="n0" />
        </TotalSummary>

        <Templates>
            <DetailRow>
                <dx:ASPxImage runat="server" EnableViewState="false"
                    ImageUrl='<%# Eval("FotoURL") %>' CssClass="img-thumbnail">
                </dx:ASPxImage>
                <dx:ASPxMemo runat="server" Text='<% Eval("Nota") %>'></dx:ASPxMemo>
            </DetailRow>
        </Templates>

    </dx:ASPxGridView>

    <asp:SqlDataSource ID="DataSourceMaster" runat="server"
        EnableCaching="true"
        CacheExpirationPolicy="Sliding" CacheDuration="300"
        CancelSelectOnNullParameter="false"
        OnSelecting="DBMainDataSources_Selecting"
        OnInit="DBMainDataSources_Init"
        ConnectionString='<%$ ConnectionStrings:Development.MsSqlServer.Main %>'
        SelectCommand="
SELECT [FacebookFriendID]
      ,[FBUserID]
      ,[FechaCreacion]
      ,[Descripcion]
      ,[Categoria]
      ,[Subcategoria]
      ,[Usuario]
      ,[Nombre]
      ,[Apellido]
      ,[Email]
      ,[Movil]
      ,[FechaNacimiento]
      ,[Pais]
      ,[Ciudad]
      ,[Sexo]
      ,[Empresa]
      ,[Profesion]
      ,[Amigos]
      ,[Grupos]
      ,FotoURL
      ,[Tags]
      ,[Nota]
  FROM [FacebookFriend]"></asp:SqlDataSource>

    <div id="app">
        
        <div class="alert alert-primary" role="alert">
            <b>Usuario Seleccionado:</b> <span id="infoUserID">(Logeado en FB)</span>
        </div>

        <div class="col-4 main">
            <div class="padding">
                <table class="table table-hover" id="options">
                    <thead>
                        <tr>
                            <th class="thw20">
                                <img class="fb-icon" src="/Content/img/icons/info.png" /></th>
                            <th class="alert alert-dark">Filtros Generales
                                <small class="form-text text-muted">Se combina con las otras opciones de abajo.
                                </small>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="option no-surroundings no-places no-interests">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/when.png" /></td>
                            <td class="select">
                                <select class="var" data-name="time">
                                    <option value="/" data-default="true">Todo
                                    </option>
                                    <option value="/today/date/{subject}/intersect">Hoy
                                    </option>
                                    <option value="/this-week/date/{subject}/intersect">Esta semama
                                    </option>
                                    <option value="/this-month/date/{subject}/intersect">Este mes
                                    </option>
                                    <option value="/2019/date/{subject}/intersect">2019
                                    </option>
                                    <option value="/2018/date/{subject}/intersect">2018
                                    </option>
                                    <option value="/2017/date/{subject}/intersect">2017
                                    </option>
                                    <option value="/2016/date/{subject}/intersect">2016
                                    </option>
                                    <option value="/2015/date/{subject}/intersect">2015
                                    </option>
                                    <option value="/2014/date/{subject}/intersect">2014
                                    </option>
                                    <option value="/2013/date/{subject}/intersect">2013
                                    </option>
                                    <option value="/2012/date/{subject}/intersect">2012
                                    </option>
                                    <option value="/2011/date/{subject}/intersect">2011
                                    </option>
                                    <option value="/2010/date/{subject}/intersect">2010
                                    </option>
                                    <option value="/2009/date/{subject}/intersect">2009
                                    </option>
                                    <option value="/2009/before/{subject}/intersect">Antes de 2009
                                    </option>
                                </select></td>
                        </tr>
                        <tr class="option no-profile no-surroundings">
                            <td>
                                <img class="fb-icon option-icon" src="/Content/img/icons/who.png" /></td>
                            <td class="select">
                                <select class="var" data-name="target">
                                    <option data-img="/Content/img/icons/who.png" value="/" data-default="true">Personas
                                    </option>
                                    <option data-img="/Content/img/icons/everything.png" value="/pages/all/{subject}{suffix}/intersect">Paginas
                                    </option>
                                    <option data-img="/Content/img/icons/friends.png" value="/{id}/friends/{subject}{suffix}/intersect">Amigos
                                    </option>
                                    <option data-img="/Content/img/icons/profile.png" data-toggle="friend-input" value="{friend}/{subject}{suffix}/intersect">Amigo:
                                    </option>
                                    <option data-img="/Content/img/icons/family.png" value="/{id}/relatives/{subject}{suffix}/intersect">Familia
                                    </option>
                                    <option data-img="/Content/img/icons/friendsoffriends.png" value="/{id}/friends/friends/{subject}{suffix}/intersect">Amigo de
                                        Amigos
                                    </option>
                                    <option data-img="/Content/img/icons/work.png" value="{id}/employees/{subject}{suffix}/intersect">Compañeros Trabajo
                                    </option>
                                    <option data-img="/Content/img/icons/education.png" value="{id}/schools-attended/ever-past/intersect/students/{gender}/{subject}{suffix}/intersect/">
                                        Compañeros Estudio
                                    </option>
                                    <option data-img="/Content/img/icons/map.png" value="{id}/current-cities/residents-near/present/{subject}{suffix}/intersect">
                                        Locales
                                    </option>
                                </select>
                                <input class="toggle friend-input" placeholder="https://www.facebook.com/JavierCanonR" type="text" />
                            </td>
                        </tr>
                        <tr class="option no-profile no-places">
                            <td>
                                <img class="fb-icon option-icon" src="/Content/img/icons/gender.png" /></td>
                            <td class="select">
                                <select class="var" data-name="gender">
                                    <option data-img="/Content/img/icons/gender.png" value="/">Genero
                                    </option>
                                    <option data-img="/Content/img/icons/male.png" value="/males/{subject}{suffix}/intersect">Masculino
                                    </option>
                                    <option data-img="/Content/img/icons/female.png" value="/females/{subject}{suffix}/intersect">Femenino
                                    </option>
                                </select></td>
                        </tr>

                        <tr class="option no-profile no-places">
                            <td>
                                <img class="fb-icon option-icon" src="/Content/img/icons/age.png" /></td>
                            <td class="select">
                                <select class="var" data-name="age">
                                    <option value="/" data-default="true">Edad
                                    </option>
                                    <option value="/13/users-age/{subject}{suffix}/intersect">13
                                    </option>
                                    <option value="/14/users-age/{subject}{suffix}/intersect">14
                                    </option>
                                    <option value="/15/users-age/{subject}{suffix}/intersect">15
                                    </option>
                                    <option value="/16/users-age/{subject}{suffix}/intersect">16
                                    </option>
                                    <option value="/17/users-age/{subject}{suffix}/intersect">17
                                    </option>
                                    <option value="/18/users-age/{subject}{suffix}/intersect">18
                                    </option>
                                    <option value="/19/users-age/{subject}{suffix}/intersect">19
                                    </option>
                                    <option value="/20/users-age/{subject}{suffix}/intersect">20
                                    </option>
                                    <option value="/21/users-age/{subject}{suffix}/intersect">21
                                    </option>
                                    <option value="/22/users-age/{subject}{suffix}/intersect">22
                                    </option>
                                    <option value="/23/users-age/{subject}{suffix}/intersect">23
                                    </option>
                                    <option value="/24/users-age/{subject}{suffix}/intersect">24
                                    </option>
                                    <option value="/25/users-age/{subject}{suffix}/intersect">25
                                    </option>
                                    <option value="/26/users-age/{subject}{suffix}/intersect">26
                                    </option>
                                    <option value="/27/users-age/{subject}{suffix}/intersect">27
                                    </option>
                                    <option value="/28/users-age/{subject}{suffix}/intersect">28
                                    </option>
                                    <option value="/29/users-age/{subject}{suffix}/intersect">29
                                    </option>
                                    <option value="/30/users-age/{subject}{suffix}/intersect">30
                                    </option>
                                    <option value="/31/users-age/{subject}{suffix}/intersect">31
                                    </option>
                                    <option value="/32/users-age/{subject}{suffix}/intersect">32
                                    </option>
                                    <option value="/33/users-age/{subject}{suffix}/intersect">33
                                    </option>
                                    <option value="/34/users-age/{subject}{suffix}/intersect">34
                                    </option>
                                    <option value="/35/users-age/{subject}{suffix}/intersect">35
                                    </option>
                                    <option value="/36/users-age/{subject}{suffix}/intersect">36
                                    </option>
                                    <option value="/37/users-age/{subject}{suffix}/intersect">37
                                    </option>
                                    <option value="/38/users-age/{subject}{suffix}/intersect">38
                                    </option>
                                    <option value="/39/users-age/{subject}{suffix}/intersect">39
                                    </option>
                                    <option value="/40/users-age/{subject}{suffix}/intersect">40
                                    </option>
                                    <option value="/41/users-age/{subject}{suffix}/intersect">41
                                    </option>
                                    <option value="/42/users-age/{subject}{suffix}/intersect">42
                                    </option>
                                    <option value="/43/users-age/{subject}{suffix}/intersect">43
                                    </option>
                                    <option value="/44/users-age/{subject}{suffix}/intersect">44
                                    </option>
                                    <option value="/45/users-age/{subject}{suffix}/intersect">45
                                    </option>
                                    <option value="/46/users-age/{subject}{suffix}/intersect">46
                                    </option>
                                    <option value="/47/users-age/{subject}{suffix}/intersect">47
                                    </option>
                                    <option value="/48/users-age/{subject}{suffix}/intersect">48
                                    </option>
                                    <option value="/49/users-age/{subject}{suffix}/intersect">49
                                    </option>
                                    <option value="/50/users-age/{subject}{suffix}/intersect">50
                                    </option>
                                    <option value="/51/users-age/{subject}{suffix}/intersect">51
                                    </option>
                                    <option value="/52/users-age/{subject}{suffix}/intersect">52
                                    </option>
                                    <option value="/53/users-age/{subject}{suffix}/intersect">53
                                    </option>
                                    <option value="/54/users-age/{subject}{suffix}/intersect">54
                                    </option>
                                    <option value="/55/users-age/{subject}{suffix}/intersect">55
                                    </option>
                                    <option value="/56/users-age/{subject}{suffix}/intersect">56
                                    </option>
                                    <option value="/57/users-age/{subject}{suffix}/intersect">57
                                    </option>
                                    <option value="/58/users-age/{subject}{suffix}/intersect">58
                                    </option>
                                    <option value="/59/users-age/{subject}{suffix}/intersect">59
                                    </option>
                                    <option value="/60/users-age/{subject}{suffix}/intersect">60
                                    </option>
                                    <option value="/61/users-age/{subject}{suffix}/intersect">61
                                    </option>
                                    <option value="/62/users-age/{subject}{suffix}/intersect">62
                                    </option>
                                    <option value="/63/users-age/{subject}{suffix}/intersect">63
                                    </option>
                                    <option value="/64/users-age/{subject}{suffix}/intersect">64
                                    </option>
                                    <option value="/65/users-age/{subject}{suffix}/intersect">65
                                    </option>
                                    <option value="/66/users-age/{subject}{suffix}/intersect">66
                                    </option>
                                    <option value="/67/users-age/{subject}{suffix}/intersect">67
                                    </option>
                                    <option value="/68/users-age/{subject}{suffix}/intersect">68
                                    </option>
                                    <option value="/69/users-age/{subject}{suffix}/intersect">69
                                    </option>
                                    <option value="/70/users-age/{subject}{suffix}/intersect">70
                                    </option>
                                    <option value="/71/users-age/{subject}{suffix}/intersect">71
                                    </option>
                                    <option value="/72/users-age/{subject}{suffix}/intersect">72
                                    </option>
                                    <option value="/73/users-age/{subject}{suffix}/intersect">73
                                    </option>
                                    <option value="/74/users-age/{subject}{suffix}/intersect">74
                                    </option>
                                    <option value="/75/users-age/{subject}{suffix}/intersect">75
                                    </option>
                                    <option value="/76/users-age/{subject}{suffix}/intersect">76
                                    </option>
                                    <option value="/77/users-age/{subject}{suffix}/intersect">77
                                    </option>
                                    <option value="/78/users-age/{subject}{suffix}/intersect">78
                                    </option>
                                    <option value="/79/users-age/{subject}{suffix}/intersect">79
                                    </option>
                                    <option value="/80/users-age/{subject}{suffix}/intersect">80
                                    </option>
                                    <option value="/81/users-age/{subject}{suffix}/intersect">81
                                    </option>
                                    <option value="/82/users-age/{subject}{suffix}/intersect">82
                                    </option>
                                    <option value="/83/users-age/{subject}{suffix}/intersect">83
                                    </option>
                                    <option value="/84/users-age/{subject}{suffix}/intersect">84
                                    </option>
                                    <option value="/85/users-age/{subject}{suffix}/intersect">85
                                    </option>
                                    <option value="/86/users-age/{subject}{suffix}/intersect">86
                                    </option>
                                    <option value="/87/users-age/{subject}{suffix}/intersect">87
                                    </option>
                                    <option value="/88/users-age/{subject}{suffix}/intersect">88
                                    </option>
                                    <option value="/89/users-age/{subject}{suffix}/intersect">89
                                    </option>
                                    <option value="/90/users-age/{subject}{suffix}/intersect">90
                                    </option>
                                    <option value="/91/users-age/{subject}{suffix}/intersect">91
                                    </option>
                                    <option value="/92/users-age/{subject}{suffix}/intersect">92
                                    </option>
                                    <option value="/93/users-age/{subject}{suffix}/intersect">93
                                    </option>
                                    <option value="/94/users-age/{subject}{suffix}/intersect">94
                                    </option>
                                    <option value="/95/users-age/{subject}{suffix}/intersect">95
                                    </option>
                                    <option value="/96/users-age/{subject}{suffix}/intersect">96
                                    </option>
                                    <option value="/97/users-age/{subject}{suffix}/intersect">97
                                    </option>
                                    <option value="/98/users-age/{subject}{suffix}/intersect">98
                                    </option>
                                    <option value="/99/users-age/{subject}{suffix}/intersect">99
                                    </option>
                                </select></td>
                        </tr>

                        <tr class="option no-profile no-places">
                            <td>
                                <img class="fb-icon option-icon" src="/Content/img/icons/relationships.png" /></td>
                            <td class="select">
                                <select class="var" data-name="relationship">
                                    <option value="" data-default="true">Estado Relaciones
                                    </option>
                                    <option value="single/users/{subject}{suffix}/intersect">Soltero
                                    </option>
                                    <option data-img="/Content/img/icons/engaged.png" value="engaged/users/{subject}{suffix}/intersect">Comprometido
                                    </option>
                                    <option data-img="/Content/img/icons/married.png" value="married/users/{subject}{suffix}/intersect">Casado
                                    </option>
                                </select></td>
                        </tr>
                    </tbody>
                </table>

                <table class="table table-hover" id="profile">
                    <thead>
                        <tr>
                            <th class="thw20">
                                <img class="fb-icon" src="/Content/img/icons/profile.png" /></th>
                            <th>Perfil</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="link" data-subject="photos" data-url="https://www.facebook.com/search/{id}/photos-by{time}">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/camera.png" /></td>
                            <td>Imagenes</td>
                        </tr>
                        <tr class="link" data-subject="videos" data-url="https://www.facebook.com/search/{id}/videos-by{time}">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/videos.png" /></td>
                            <td>Videos</td>
                        </tr>
                        <tr class="link" data-subject="stories" data-url="https://www.facebook.com/search/{id}/stories-by{time}">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/post.png" /></td>
                            <td>Publicaciones</td>
                        </tr>
                        <tr class="link" data-url="https://www.facebook.com/search/{id}/groups/intersect">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/groups.png" /></td>
                            <td>Grupos</td>
                        </tr>
                        <tr class="link" data-url="https://www.facebook.com/search/{id}/events-joined/">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/events.png" /></td>
                            <td>Eventos Futuros</td>
                        </tr>
                        <tr class="link" data-url="https://www.facebook.com/search/{id}/events-joined/in-past/date/events/intersect/">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/events.png" /></td>
                            <td>Eventos Pasados</td>
                        </tr>
                        <tr class="link" data-url="https://www.facebook.com/search/{id}/apps-used/game/apps/intersect">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/game.png" /></td>
                            <td>Juegos</td>
                        </tr>
                        <tr class="link" data-url="https://www.facebook.com/search/{id}/apps-used/">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/apps.png" /></td>
                            <td>Aplicativos</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <!-- /padding -->
        </div>

        <div class="col-4 main">
            <div class="padding">
                <table class="table table-hover" id="tags">
                    <thead>
                        <tr>
                            <th class="thw20">
                                <img class="fb-icon" src="/Content/img/icons/tags.png" /></th>
                            <th>Tags</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="link" data-subject="photos" data-suffix="-of" data-url="https://www.facebook.com/search/{target}/{gender}/{age}/{relationship}/{id}/photos-of{time}/intersect">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/camera.png" /></td>
                            <td>Imagenes</td>
                        </tr>
                        <tr class="link" data-subject="videos" data-suffix="-of" data-url="https://www.facebook.com/search/{target}/{gender}/{age}/{relationship}/{id}/videos-of{time}/intersect">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/videos.png" /></td>
                            <td>Videos</td>
                        </tr>
                        <tr class="link" data-subject="stories" data-suffix="-tagged" data-url="https://www.facebook.com/search/{target}/{gender}/{age}/{relationship}/{id}/stories-tagged{time}/intersect">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/post.png" /></td>
                            <td>Publicaciones</td>
                        </tr>
                    </tbody>
                </table>

                <table class="table table-hover" id="comments">
                    <thead>
                        <tr>
                            <th class="thw20">
                                <img class="fb-icon" src="/Content/img/icons/activity.png" /></th>
                            <th>Comentarios</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="link" data-subject="photos" data-suffix="/" data-url="https://www.facebook.com/search/{target}/{gender}/{age}/{relationship}/{id}/{subject}-commented{time}/intersect">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/camera.png" /></td>
                            <td>Imagenes</td>
                        </tr>
                        <tr class="link" data-subject="videos" data-suffix="/" data-url="https://www.facebook.com/search/{target}/{gender}/{age}/{relationship}/{id}/{subject}-commented{time}/intersect">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/videos.png" /></td>
                            <td>Videos</td>
                        </tr>
                        <tr class="link" data-subject="stories" data-suffix="/" data-url="https://www.facebook.com/search/{target}/{gender}/{age}/{relationship}/{id}/{subject}-commented{time}/intersect">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/post.png" /></td>
                            <td>Publicaciones</td>
                        </tr>
                    </tbody>
                </table>

                <table class="table table-hover" id="likes">
                    <thead>
                        <tr>
                            <th class="thw20">
                                <img class="fb-icon" src="/Content/img/icons/likes.png" /></th>
                            <th>Me Gusta</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="link" data-subject="photos" data-suffix="/" data-url="https://www.facebook.com/search/{age}/{target}/{relationship}/{gender}/{id}/photos-liked{time}/intersect">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/camera.png" /></td>
                            <td>Imagenes</td>
                        </tr>
                        <tr class="link" data-subject="videos" data-suffix="/" data-url="https://www.facebook.com/search/{age}/{target}/{id}/videos-liked{time}/intersect">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/videos.png" /></td>
                            <td>Videos</td>
                        </tr>
                        <tr class="link" data-subject="stories" data-suffix="/" data-url="https://www.facebook.com/search/{age}/{target}/{id}/stories-liked{time}/intersect">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/post.png" /></td>
                            <td>Publicaciones</td>
                        </tr>
                    </tbody>
                </table>

                <table class="table table-hover" id="places">
                    <thead>
                        <tr>
                            <th class="thw20">
                                <img class="fb-icon" src="/Content/img/icons/location.png" /></th>
                            <th>Lugares</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="link" data-url="https://www.facebook.com/search/{id}/places-visited/">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/everything.png" /></td>
                            <td>Todos</td>
                        </tr>
                        <tr class="link" data-url="https://www.facebook.com/search/{id}/places-visited/110290705711626/places/intersect/">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/bars.png" /></td>
                            <td>Bares</td>
                        </tr>
                        <tr class="link" data-url="https://www.facebook.com/search/{id}/places-visited/273819889375819/places/intersect/">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/restaurants.png" /></td>
                            <td>Restaurantes</td>
                        </tr>
                        <tr class="link" data-url="https://www.facebook.com/search/{id}/places-visited/200600219953504/places/intersect/">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/stores.png" /></td>
                            <td>Tiendas</td>
                        </tr>
                        <tr class="link" data-url="https://www.facebook.com/search/{id}/places-visited/935165616516865/places/intersect/">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/outdoors.png" /></td>
                            <td>Aire Libre</td>
                        </tr>
                        <tr class="link" data-url="https://www.facebook.com/search/{id}/places-visited/164243073639257/places/intersect/">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/hotels.png" /></td>
                            <td>Hoteles</td>
                        </tr>
                        <tr class="link" data-url="https://www.facebook.com/search/{id}/places-visited/192511100766680/places/intersect/">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/theaters.png" /></td>
                            <td>Teatros</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <!-- /padding -->
        </div>

        <div class="col-4 main">
            <div class="padding">
                <table class="table table-hover" id="surroundings">
                    <thead>
                        <tr>
                            <th class="thw20">
                                <img class="fb-icon" src="/Content/img/icons/surroundings.png" /></th>
                            <th>Personas</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="link" data-subject="/" data-suffix="/" data-url="https://www.facebook.com/search/{age}/{gender}/{relationship}/{id}/relatives/intersect">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/family.png" /></td>
                            <td>Familia</td>
                        </tr>
                        <tr class="link" data-subject="/" data-suffix="/" data-url="https://www.facebook.com/search/{age}/{gender}/{relationship}/{id}/friends/intersect">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/friends.png" /></td>
                            <td>Amigos</td>
                        </tr>
                        <tr class="link" data-subject="/" data-suffix="/" data-url="https://www.facebook.com/search/{age}/{gender}/{relationship}//{id}/friends/friends/intersect">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/friendsoffriends.png" /></td>
                            <td>Amigos de amigos</td>
                        </tr>
                        <tr class="link" data-subject="/" data-suffix="/" data-url="https://www.facebook.com/search/{age}/{id}/employees/{gender}/{relationship}//intersect/">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/work.png" /></td>
                            <td>Trabajo</td>
                        </tr>
                        <tr class="link" data-subject="/" data-suffix="/" data-url="https://www.facebook.com/search/{age}/{id}/schools-attended/ever-past/intersect/students/{gender}/{relationship}/intersect/">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/education.png" /></td>
                            <td>Estudio</td>
                        </tr>
                        <tr class="link" data-subject="/" data-suffix="/" data-url="https://www.facebook.com/search/{age}/{id}/current-cities/residents-near/present/{gender}/{relationship}/intersect">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/map.png" /></td>
                            <td>Locales</td>
                        </tr>
                    </tbody>
                </table>

                <table class="table table-hover" id="interests">
                    <thead>
                        <tr>
                            <th class="thw20">
                                <img class="fb-icon" src="/Content/img/icons/intrests.png" /></th>
                            <th>Intereses</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="link" data-subject="pages" data-suffix="-liked" data-url="https://www.facebook.com/search/{target}/{gender}/{age}/{relationship}/{id}/pages-liked/intersect">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/everything.png" /></td>
                            <td>Paginas</td>
                        </tr>
                        <tr class="link" data-subject="pages" data-suffix="-liked" data-url="https://www.facebook.com/search/{target}/{gender}/{age}/{relationship}/{id}/pages-liked/161431733929266/pages/intersect/">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/politics.png" /></td>
                            <td>Partidos políticos</td>
                        </tr>
                        <tr class="link" data-subject="pages" data-suffix="-liked" data-url="https://www.facebook.com/search{target}/{gender}/{age}/{relationship}/{id}/pages-liked/religion/pages/intersect/">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/camera.png" /></td>
                            <td>Religión</td>
                        </tr>
                        <tr class="link" data-subject="pages" data-suffix="-liked" data-url="https://www.facebook.com/search/{target}/{gender}/{age}/{relationship}/{id}/pages-liked/musician/pages/intersect/">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/music.png" /></td>
                            <td>Musica</td>
                        </tr>
                        <tr class="link" data-subject="pages" data-suffix="-liked" data-url="https://www.facebook.com/search/{target}/{gender}/{age}/{relationship}/{id}/pages-liked/movie/pages/intersect/">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/movies.png" /></td>
                            <td>Películas</td>
                        </tr>
                        <tr class="link" data-subject="pages" data-suffix="-liked" data-url="https://www.facebook.com/search/{target}/{gender}/{age}/{relationship}/{id}/pages-liked/book/pages/intersect/">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/book.png" /></td>
                            <td>Libros</td>
                        </tr>
                        <tr class="link" data-subject="pages" data-suffix="-liked" data-url="https://www.facebook.com/search/{target}/{gender}/{age}/{relationship}/{id}/places-liked/intersect/">
                            <td>
                                <img class="fb-icon" src="/Content/img/icons/location.png" /></td>
                            <td>Lugares</td>
                        </tr>
                    </tbody>
                    <!-- /padding -->
                    <!-- /main -->
                    <!-- script references -->
                </table>
            </div>
        </div>

    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageFooter" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PageBottom" runat="server">
    <script src="/Scripts/FBScanner.js"></script>
</asp:Content>
