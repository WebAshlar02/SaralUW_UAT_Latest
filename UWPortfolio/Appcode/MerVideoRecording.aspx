<%--//******************************************************************************************************************************* 
// Sr. No.              : 1
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Sagar Thorave
 // BRD/CR/Codesk No/Win : CR - 30499    
// Date Of Creation     : 12-04-2022            
// Description          : 1.Video MER requirement-reviewed 
//*********************************************************************************************************************************/--%>
<%--1.1 Begin of Changes; Sagar Thorave - [MFL00886]--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MerVideoRecording.aspx.cs" Inherits="Appcode_MerVideoRecording" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="../plugins/jQuery/jquery-2.2.3.min.js"></script>
     <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
     <script type="text/javascript" language="javascript">
         $(document).ready(function () {
            
               
             $("#Btn_GetMerVid").click(function () {
                 var str = $("#ApplicationNum").val();
                 if (str == null) {
                     $("#msg").text("Please Enter Application Number")
                    // $("#Literal1").hide();
                 } else {
                     $("#msg").text("")
                 }
             });

         });
     </script>
     <style>
         #ApplicationNum {
            width:250px;
            height:25px;
         }
         #AvailableMerVid {
            width:200px;
            height:25px;
         }
     </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container text-center">
            <h1>MER Video Recording</h1>
            <br/>
            <br/>
            <div class="row">
                    <asp:TextBox runat="server" type="text" name="ApplicationNum" ID="ApplicationNum" placeholder="Enter application number"/> 
                    <asp:Button runat="server" class="btn btn-info" Text="Search" OnClick="Btn_VidByApplicationNo" ID="Btn_GetMerVid"/>
                <br/>
                <br/>
                <br/>
                       <asp:GridView runat="server"  HorizontalAlign="Center"  Id="VideoGrid"  AutoGenerateColumns="False" Height="80px" BackColor="White"  BorderColor="#CCCCCC" 
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="680px"  DataKeyNames="BlobFileName,BlobStoreDate"
                            ShowFooter="true"
                         ShowHeaderWhenEmpty="true" OnSelectedIndexChanged="VideoGrid_SelectedIndexChanged"> 
                        <Columns>
                            <asp:TemplateField HeaderText="Video Names"  >
                                <ItemTemplate>
                                    <asp:Label Text='<%# Eval("OriginalFileName") %>' runat="server" ID="OriginalFileName"></asp:Label>
                                 
                                    <asp:Label Text='<%# Eval("BlobFileName") %>' runat="server" ID="BlobFileName" Style="display: none"></asp:Label>
                            
                                    <asp:Label Text='<%# Eval("BlobStoreDate") %>' runat="server" ID="BlobStoreDate" Style="display: none"></asp:Label>
                                </ItemTemplate>
                             </asp:TemplateField>

                                    <asp:ButtonField  CommandName="Select" runat="server" Text="Watch Video"  />
                     
                          
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
                    </asp:GridView>

                <br />
                <br />
                  <asp:Literal ID="Literal1" runat="server"></asp:Literal>
           </div>
        </div>
        <asp:Label ID="msg" runat="server" Text="" ></asp:Label>
    </form>
</body>
</html>
<%--1.1 End of Changes; Sagar Thorave - [MFL00886]--%>

