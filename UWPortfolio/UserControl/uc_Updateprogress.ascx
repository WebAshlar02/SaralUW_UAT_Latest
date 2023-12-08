<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_Updateprogress.ascx.cs" Inherits="UserControl_uc_Updateprogress" %>
<style type="text/css">
    .auto-style1 {
        width: 40px;
    }
</style>

<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
    <ProgressTemplate>
        <div id="ldrModal1" runat="server" class="LoaderModal">
            <div class="center">
                <img alt="" src="./dist/img/loader4.gif" />
            </div>
        </div>
        <%--<div style="background-color: Gray; z-index: 100; background-color: Gray; filter: alpha(opacity=60); opacity: 0.60; width: 100%; top: 0px; left: 0px; position: fixed; height: 100%;">
        </div>
        <div style="z-index: 200; margin: auto; font-family: Trebuchet MS; filter: alpha(opacity=100); opacity: 1; font-size: small; vertical-align: middle; top: 45%; position: fixed; right: 44%; color: #275721; text-align: center; background-color: White; height: 42px; width: 150px;">
            <table style="background-color: white; font-family: Sans-Serif; text-align: center; border: solid 1px #275721; color: #275721; width: inherit; padding: 15px;">
                <tr style="vertical-align: middle;">
                    <td style="text-align: inherit;" class="auto-style1">
                        <asp:Image ID="imgLoader" runat="server" ImageUrl="../dist/img/loading.gif" Width="40px" Height="40px" /></td>
                    <td style="text-align: inherit;"><span style="font-family: Sans-Serif; font-size: small; color: flavor;">&nbsp;In Securing...&nbsp;</span></td>
                </tr>
            </table>
        </div>--%>
    </ProgressTemplate>
</asp:UpdateProgress>
