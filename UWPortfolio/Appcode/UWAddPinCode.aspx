<%--//*//*********************************************************************     
//*                      FUTURE GENERALI INDIA                        *    
//**********************************************************************/      
//*                  I N F O R M A T I O N                                       
//**********************************************************************
// Sr. No.              : 1
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Sushant Devkate - MFL00905
// BRD/CR/Codesk No/Win :  CR-30363 
// Date Of Creation     : 21/02/2022
// Description          : Add and delete the Negative Pincode
//**********************************************************************--%>

  <%--1.1 Start: Sushant Devkate MFL00905--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UWAddPinCode.aspx.cs" Inherits="Appcode_UWAddPinCode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       <script src="../plugins/jQuery/jquery-2.2.3.min.js"></script>
       <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            $("#<%=txtPinCode.ClientID %>").keypress(function (e) {
                var specialKeys = new Array();
                specialKeys.push(8);
                var keyCode = e.which ? e.which : e.keyCode
                if (((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1)) {
                    return true
                } else
                {
                    return false;
                }
            });

            $(document.body).click(function () {
                Hidelblmsg();
                
            });

        });  
        function Validation() {
            Hidelblmsg();
            if (document.getElementById("txtPinCode").value == "") {
                alert("Please Enter the PinCode value");
                document.getElementById("txtPinCode").focus();
                return false;
            }

            if (document.getElementById("txtDistrict").value == "") {
                alert("Please Enter the District value");
                document.getElementById("txtDistrict").focus();
                return false;
            }
        }
        function isDelete() {
            Hidelblmsg();
            return confirm("Do you want to delete this pincode ?");
        }
       function SearchValidation()
       {
           Hidelblmsg();
            if (document.getElementById("txtPincodeSearch").value == "") {
                alert("Please Enter the pincode for search");
                document.getElementById("txtPincodeSearch").focus();
                return false;
            }
        }

        function Hidelblmsg() {
            if (document.getElementById("lblSuccess") && document.getElementById("lblSuccess").innerHTML!="") {
                document.getElementById("lblSuccess").innerHTML= ""
                document.getElementById("lblSuccess").style.visibility = "hidden";
            }

        }
    </script>
    <style>
        /* .scrolling {  
                position: absolute;  
            }  
              
            .gvWidthHight {  
                overflow: scroll;  
                height: 645px;  
  
            } */ 
        th{
             text-align: center !important;
             padding: 5px !important;
        }
        .GridHeader {
           
            text-align: center !important;
        }
        td {
            padding: 5px !important;
        }
    </style>

</head>
<body>

    <form id="form1" runat="server">
        <div>
        <div class="container text-center table-bordered" style="margin-top: 20px" >
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="margin-left: -32px;">
                <h4 style="font-weight:bold">Add Negative Pincode</h4>
            </div>
            <div style="margin-left: -45px;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <asp:Label runat="server">Circle Name </asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <asp:TextBox ID="txtCircleName" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" style="margin-left: -221px;">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <asp:Label runat="server">Region Name </asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <asp:TextBox ID="txtRegionName" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top: 18px">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <asp:Label runat="server">Division Name </asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <asp:TextBox ID="txtDivisionName" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" style="margin-left: -221px;">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <asp:Label runat="server">Office Name </asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <asp:TextBox ID="txtOfficeName" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top: 18px">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <asp:Label runat="server">PinCode <label style="color:red">*</label></asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <asp:TextBox ID="txtPinCode" runat="server" autocomplete="off" MaxLength="6"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" style="margin-left: -221px;">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <asp:Label runat="server">Office Type </asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <asp:TextBox ID="txtOfficeType" runat="server" autocomplete="off"></asp:TextBox>

                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top: 18px">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <asp:Label runat="server">District <label style="color:red">*</label></asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <asp:TextBox ID="txtDistrict" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" style="margin-left: -221px;">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <asp:Label runat="server">Delivery </asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <asp:TextBox ID="txtDelivery" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>

                </div>

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top: 18px">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <asp:Label runat="server">State Name</asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <asp:TextBox ID="txtStateName" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" style="margin-left: -221px;">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <asp:Label runat="server">Remarks </asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <asp:TextBox ID="txtRemarks" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top: 22px; margin-bottom: 12px;">
                    <div class="col-lg-6 col-sm-6 col-sm-6 col-xs-6">
                        <asp:Button ID="btnAdd" runat="server" Text="Add" Style="width: 70px" OnClick="btnAdd_Click" OnClientClick="return Validation();" />
                    </div>
                </div>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top: 5px; margin-bottom: -11px;">
                <div class="col-lg-6 col-sm-6 col-sm-6 col-xs-6">
                    <asp:Label ID="lblSuccess" runat="server" style="font-size:18px;" Visible="false"></asp:Label>
                </div>
            </div>
           <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="margin-left: -62px;margin-top: 22px;">
                <h4 style="font-weight:bold">Negative Pincode</h4>
            </div>
           </div>
            <br />
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-left: -337px;">
                Search By Pincode : <asp:TextBox ID="txtPincodeSearch" runat="server"/> <asp:Button Text="Search" ID="btnSearch" type="button" runat="server" OnClick="btnSearch_Click" OnClientClick="return SearchValidation();" /> 
                <asp:Button Text="Reset" type="button" ID="btnreset" runat="server" OnClick="btnreset_Click" OnClientClick="return Hidelblmsg();"  />
            </div>
             <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top: 22px;margin-bottom: 12px;">
                 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="Id" EmptyDataText="No Record Found" Width="100%"
                     OnRowDeleting="GridView1_RowDeleting" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="10">
                     <Columns>

                         <asp:BoundField DataField="CircleName" HeaderText="Circle Name" ItemStyle-Width="12%" />
                         <asp:BoundField DataField="RegionName" HeaderText="Region Name" ItemStyle-Width="12%" />
                         <asp:BoundField DataField="DivisionName" HeaderText="Division Name" ItemStyle-Width="12%" />
                         <asp:BoundField DataField="OfficeName" HeaderText="Office Name" ItemStyle-Width="12%" />
                         <asp:BoundField DataField="PinCode" HeaderText="PinCode" ItemStyle-Width="6%" />
                         <asp:BoundField DataField="OfficeType" HeaderText="Office Type" ItemStyle-Width="5%" />
                         <asp:BoundField DataField="District" HeaderText="District" ItemStyle-Width="12%" />
                         <asp:BoundField DataField="StateName" HeaderText="State Name" ItemStyle-Width="12%" />
                         <asp:BoundField DataField="Delivery" HeaderText="Delivery" ItemStyle-Width="10%" />
                         <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-Width="10%" />
                         <asp:TemplateField ItemStyle-Width="10%">
                             <HeaderTemplate>Action</HeaderTemplate>
                             <ItemTemplate>
                                 <asp:LinkButton ID="DeleteBtn" runat="server" CommandName="Delete"
                                     OnClientClick="return isDelete();">Delete
                                 </asp:LinkButton>
                             </ItemTemplate>
                         </asp:TemplateField>
                     </Columns>

                 </asp:GridView>  
            </div>  
           
            
        </div>
           </div>
        
    </form>
</body>
</html>
  <%--1.1 End: Sushant Devkate MFL00905--%>