
<%--//**********************************************************************
// Sr. No.              : 1
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Bhaumik Patel - WebAshlar02
// BRD/CR/Codesk No/Win : CR-5307 
// Date Of Creation     : 18/11/2022
// Description          :UnderWriting Assignment Details (User Access)
//**********************************************************************--%>



<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UWAssignment_AddNewGP.aspx.cs" Inherits="Appcode_UWAssignment_AddNewGP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <%--//1.1 Begin of Changes; Bhaumik Patel - [CR-5307]--%>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />

    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../plugins/jQuery/jquery-2.2.3.min.js"></script>
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css" />--%>
    <link href="../dist/css/AdminLTE.min.css" rel="stylesheet" />
    <link href="../dist/css/skins/_all-skins.min.css" rel="stylesheet" />
    <script src="../dist/js/html5shiv.min.js"></script>
    <script src="../dist/js/respond.min.js"></script>
    <link href="../dist/css/styles-app.css" rel="stylesheet" />
    <link href="../plugins/select2/select2.min.css" rel="stylesheet" />
    <%--<link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />--%>    <%--<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>--%>
    <script src="../plugins/jQueryUI/jquery-ui.js"></script>
    <link href="../dist/css/sumoselect.min.css" rel="stylesheet" />
    <script src="../dist/js/jquery.sumoselect.min.js"></script>


    <%--<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>--%><%--<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"

        rel="Stylesheet" type="text/css" />--%>
    <link href="../plugins/datepicker/datepicker3.css" rel="stylesheet" />
    <script src="../plugins/datepicker/bootstrap-datepicker.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
           
                <h2 style="display: flex; justify-content: center; }">Add New Group</h2>
                <div class="col-md-12">
                    <div class="col-md-2">
                        <div class="form-group">
                            <h4><b>Type</b></h4>
                            <asp:DropDownList ID="ddlgp" runat="server" CssClass="form-control select2 dropdown menu " Style="width: 70%;" OnSelectedIndexChanged="ddlgp_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvbucket" runat="server" ErrorMessage="* Please Select Bucket Value ...!" ForeColor="Red" ControlToValidate="ddlgp"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <h4><b>Discription</b></h4>
                            <asp:TextBox ID="txtdis" CssClass="form-control" Style="width: 70%;" runat="server" Text="">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvdis" runat="server" ErrorMessage="* Please Select Discription Value ...!" ForeColor="Red" ControlToValidate="txtdis"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div id="divfromrange" class="form-group" runat="server" visible="false">
                            <h4><b>From-Range</b></h4>
                            <asp:TextBox ID="txtfrmrange" CssClass="form-control" Style="width: 70%;" runat="server" Text="">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvfrmrange" runat="server" ErrorMessage="* Please Select From-Range Value ...!" ForeColor="Red" ControlToValidate="txtfrmrange"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div id="divtorange" class="form-group" runat="server" visible="false">
                            <h4><b>To-Range</b></h4>
                            <asp:TextBox ID="txttorange" CssClass="form-control" Style="width: 70%;" runat="server" Text="">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtorange" runat="server" ErrorMessage="* Please Select To-Range Value ...!" ForeColor="Red" ControlToValidate="txttorange"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <asp:Button ID="btnsave" Style="float: left; margin-right: 20px; margin-top: 40px" CssClass="btn btn-primary lnkButton" runat="server" Text="  SAVE  " Enabled="true" AutoPostBack="True" OnClick="btnsave_Click" />
                        </div>
                    </div>
                    <div id="divcleare" runat="server" visible="true" class="col-md-2">
                        <div class="form-group">
                            <asp:Button ID="btncleare" Style="float: left; margin-right: 20px; margin-top: 24px;" CssClass="btn btn-danger lnkButton" runat="server" Text="CLEAR" Enabled="true" AutoPostBack="True" OnClick="btncleare_Click" CausesValidation="False" UseSubmitBehavior="False" />
                        </div>
                    </div>
                </div>
          
        </div>
    </form>
</body>
</html>
<%--//1.1 End of Changes; Bhaumik Patel - [CR-5307]--%>
