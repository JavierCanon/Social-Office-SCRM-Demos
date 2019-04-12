<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateSelectorControl.ascx.cs" Inherits="DateSelectorControl" %>
<div class="dateSelectorControl">
    <div class="prevButton" id="PrevButton" runat="server"></div>
    <div><%= Text%></div>
    <div class="nextButton" id="NextButton" runat="server"></div>
</div>
<dx:ASPxHiddenField ID="DateSelectorHiddenField" runat="server" ClientIDMode="AutoID"></dx:ASPxHiddenField>
