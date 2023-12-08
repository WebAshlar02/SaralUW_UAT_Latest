<%--//**********************************************************************
//*                      FUTURE GENERALI INDIA                        *    
//**********************************************************************/      
//*                  I N F O R M A T I O N                                       
//********************************************************************* 
// Sr. No.              : 1  
// Company              : Life            
// Module               : UW Saral          
// Program Author       : Jayendra Patankar [WebAshlar01]            
// BRD/CR/Codesk No/Win : CR-4126          
// Date Of Creation     : 27-09-2022            
// Description          : CR-4126 - SFTP integration with TPA for storing Video MER recordings
//**********************************************************************//--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MerVideoViewer.aspx.cs" Inherits="Appcode_VideoViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../plugins/jQuery/jquery-2.2.3.min.js"></script>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#Btn_GetMerVid").click(function () {
                debugger
                var str = $("#ApplicationNum").val();
                if (!str) {
                    $("#Image1").prop("visible", false);
                    alert("Please Enter Application Number");
                }
            });

        });
    </script>
    <style>
        #ApplicationNum {
            width: 250px;
            height: 25px;
        }

        .displayVideo {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container text-center">
            <h1>MER Videos</h1>
            <br />
            <br />
            <div class="row">
                <asp:TextBox runat="server" type="text" name="ApplicationNum" ID="ApplicationNum" placeholder="Enter Your Application Number"></asp:TextBox>
                <asp:Button runat="server" class="btn btn-info" Text="Search" OnClick="Btn_VidByApplicationNo" ID="Btn_GetMerVid" />
                <br />
                <br />
                <br />
                <asp:GridView runat="server" HorizontalAlign="Center" ID="VideoGrid" AutoGenerateColumns="False" Height="80px" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="680px" DataKeyNames="Name,CreationTime"
                    ShowFooter="true" OnPageIndexChanging="grdData_PageIndexChanging" AllowPaging="True"
                    ShowHeaderWhenEmpty="true" OnSelectedIndexChanged="VideoGrid_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="Name" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                <asp:Label ID="FullName" runat="server" Style="display: none" Text='<%#Eval("FullName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="File Extention" HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="Extension" runat="server" Text='<%#Eval("Extension")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Creation Date & Time" HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="CreationTime" runat="server" Text='<%#Eval("CreationTime")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Watch Video" HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:LinkButton ID="WatchVideo" CommandName="Select" runat="server" Text='<%#Eval("WatchVideo")%>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField> <asp:TemplateField HeaderText="View CheckList" HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:LinkButton ID="ViewCheckList" CommandName="Select" runat="server" Text='<%#Eval("ViewCheckList")%>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    <%--    <asp:ButtonField CommandName="Select" runat="server" Text='<%#Eval("WatchVideo")%>' />
                        <asp:ButtonField CommandName="Select" runat="server" Text='<%#Eval("CreationTime")%>' />--%>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                    <PagerSettings LastPageText="last" Mode="NumericFirstLast" PageButtonCount="4" />
                </asp:GridView>
                <br />
                <br />
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                <asp:Image CssClass="displayVideo" ID="Image1" runat="server" ImageUrl="../Images/Video-not-found.png" />
            </div>
        </div>
        <asp:Label ID="msg" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>
