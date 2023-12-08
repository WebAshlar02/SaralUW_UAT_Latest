<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IIBQuery.aspx.cs" Inherits="Appcode_IIBQuery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IIB Data</title>
     <%--   <script src="../plugins/jQuery/jquery-2.2.3.min.js"></script>
       <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />--%>
       <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../plugins/jQuery/jquery-2.2.3.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css" />
    <link href="../dist/css/AdminLTE.min.css" rel="stylesheet" />
    <link href="../dist/css/skins/_all-skins.min.css" rel="stylesheet" />
    <script src="../dist/js/html5shiv.min.js"></script>
    <script src="../dist/js/respond.min.js"></script>
    <link href="../dist/css/styles-app.css" rel="stylesheet" />
    <link href="../plugins/select2/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />

   <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.3/css/bootstrap.min.css" integrity="sha384-Zug+QiDoJOrZ5t4lssLdxGhVrurbmBWopoEl+M6BdEfwnCJZtKxi1KgxUyJq13dy" crossorigin="anonymous"/>
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"/>
  <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
<%--  <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>--%>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.3/js/bootstrap.min.js" integrity="sha384-a5N7Y/aK3qNeh15eJKGWxsqtnX/wWdSZSKp+81YjTmS15nvnvxKHuzaWwXHDli+4" crossorigin="anonymous"></script>

    <script type="text/javascript">
        $(function () {
            $("#<%=txtPinCode.ClientID %>,#<%=txtPhoneNo.ClientID %>,#<%=txtMobNo.ClientID %>,#<%=txtAnnualIncome.ClientID %>").keypress(function (e) {
                var specialKeys = new Array();
                specialKeys.push(8);
                var keyCode = e.which ? e.which : e.keyCode
                if (((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1)) {
                    return true
                } else {
                    return false;
                }
            });
            $(document.body).click(function () {
                Hidelblmsg();
             
            });
        });
        function Validation() {
            Hidelblmsg();
            if (document.getElementById("txtApplicationNo").value == "") {
                alert("Please enter the application No.");
                document.getElementById("txtApplicationNo").focus();
                return false;
            }

            if (document.getElementById("txtFirstName").value == "") {
                alert("Please enter the first name");
                document.getElementById("txtFirstName").focus();
                return false;
            }
            if (document.getElementById("txtSurName").value == "") {
                alert("Please enter the sur name");
                document.getElementById("txtSurName").focus();
                return false;
            }
            if (document.getElementById("txtDBO").value == "") {
                alert("Please enter the date of birth.");
                document.getElementById("txtDBO").focus();
                return false;
            }

            if (document.getElementById("txtDBO").value != "") {
                var dateString = document.getElementById("txtDBO").value;
                var regex = /(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$/;
                // var regex = /(((1)[0-9]|2[0-9]|3[0-1])\/([1-9]|1[0-2])\/((19|20)\d\d))$/;

                if (!regex.test(dateString)) {
                    alert("Please enter date in DD/MM/YYYY format.");
                    document.getElementById("txtDBO").focus();
                    return false;
                }
            }

            if (document.getElementById("dllGender").value == "0") {
                alert("Please select gender.");
                document.getElementById("dllGender").focus();
                return false;
            }
            if (document.getElementById("txtPAN").value == "") {
                alert("Please enter the PAN card no.");
                document.getElementById("txtPAN").focus();
                return false;
            }

            if (document.getElementById("txtPAN").value != "") {
                var inputvalues = document.getElementById("txtPAN").value;
                var regex = /[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
                if (!regex.test(inputvalues)) {
                    alert("Please enter the valid PAN card no.");
                    document.getElementById("txtPAN").focus();
                    return regex.test(inputvalues);
                }
            }

            if (document.getElementById("txtEmail").value != "") {
                var Email = document.getElementById("txtEmail").value;
                var mailformat = "[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$"
                if (!Email.match(mailformat)) {
                    alert("Please enter the valid email address");
                    document.getElementById("txtEmail").focus();
                    return false;
                }
            }

            if (document.getElementById("txtAnnualIncome").value == "") {
                alert("Please enter the annual income.");
                document.getElementById("txtAnnualIncome").focus();
                return false;

            }
            if (document.getElementById("txtproductCode").value == "") {
                alert("Please enter the product code.");
                document.getElementById("txtProductCode").focus();
                return false;
            }
            if (document.getElementById("txtPinCode").value == "") {
                alert("Please enter the pin code.");
                document.getElementById("txtPinCode").focus();
                return false;
            }
            Showloading();
        }

        function Hidelblmsg() {
            if (document.getElementById("lblSuccess") && document.getElementById("lblSuccess").innerHTML != "") {
                document.getElementById("lblSuccess").innerHTML = ""
                document.getElementById("lblSuccess").style.visibility = "hidden";
            }

        }
        function Showloading() {
            $('#LoadingPopup').modal('show');
        }

        function hideloading()
        {
            $('#LoadingPopup').modal('hide');
        }
       
    </script>
</head>
<body>
       <form id="FormIIB" runat="server">
        <div>          
        <div class="container text-center table-bordered" style="margin-top: 20px" >
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="margin-left: -32px;">
                <h4 style="font-weight:bold">IIB Data</h4>
            </div>
            <div>
                
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top: 18px">

                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                            <div class="col-lg-4 col-md-4 col-sm-5">
                                <asp:Label runat="server">Application No <label style="color:red">*</label></asp:Label>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                <asp:TextBox ID="txtApplicationNo" runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                    <div class="col-lg-6 col-md-6 col-sm-6">
                        <div class="col-lg-4 col-md-4 col-sm-5">
                           <asp:Label runat="server">First Name <label style="color:red">*</label></asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                           <asp:TextBox ID="txtFirstName" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    </div>
              
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top: 18px">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                        <div class="col-lg-4 col-md-4 col-sm-5">
                           <asp:Label runat="server">Sur Name <label style="color:red">*</label> </asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4">
                           <asp:TextBox ID="txtSurName" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6">
                        <div class="col-lg-4 col-md-4 col-sm-5">
                            <asp:Label runat="server">Date Of Birth <label style="color:red">*</label> </asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                             <asp:TextBox ID="txtDBO" runat="server" Placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top: 18px">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                      <div class="col-lg-4 col-md-4 col-sm-5">
                             <asp:Label runat="server">Gender <label style="color:red">*</label></asp:Label>
                          
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                           <asp:DropDownList ID="dllGender" runat="server" width="176px" height="30px" CssClass="dropdown">
                                <asp:ListItem Value="0">----Select----</asp:ListItem>
                                <asp:ListItem Value="M">Male</asp:ListItem>
                                <asp:ListItem Value="F">Female</asp:ListItem>
                            </asp:DropDownList>
                        </div>
             
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                      <div class="col-lg-4 col-md-4 col-sm-5">
                           <asp:Label runat="server">Email</asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                              <asp:TextBox ID="txtEmail" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top: 18px">

                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                        <div class="col-lg-4 col-md-4 col-sm-5">
                            <asp:Label runat="server">PAN card No. <label style="color:red">*</label></asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <asp:TextBox ID="txtPAN" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                         <div class="col-lg-4 col-md-4 col-sm-5">
                             <asp:Label runat="server">Adhar card No. </asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                             <asp:TextBox ID="txtAdhar" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                </div>
            
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top: 18px">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                         <div class="col-lg-4 col-md-4 col-sm-5">
                            <asp:Label runat="server">Annual Income <label style="color:red">*</label></asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <asp:TextBox ID="txtAnnualIncome" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                     
                   <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                       <div class="col-lg-4 col-md-4 col-sm-5">
                            <asp:Label runat="server">Product Code <label style="color:red">*</label></asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <asp:TextBox ID="txtproductCode" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top: 18px">
                  <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                         <div class="col-lg-4 col-md-4 col-sm-5">
                            <asp:Label runat="server">Mobile No.</asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <asp:TextBox ID="txtMobNo" runat="server" MaxLength="12" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                     <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                        <div class="col-lg-4 col-md-4 col-sm-5">
                            <asp:Label runat="server">Phone No. </asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <asp:TextBox ID="txtPhoneNo" runat="server" autocomplete="off" MaxLength="20"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top: 18px">

                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                        <div class="col-lg-4 col-md-4 col-sm-5">
                            <asp:Label runat="server">PinCode <label style="color:red">*</label></asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <asp:TextBox ID="txtPinCode" runat="server" autocomplete="off" MaxLength="6"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                        <div class="col-lg-4 col-md-4 col-sm-5">
                            <asp:Label runat="server">Permanent Address </asp:Label>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" Width="180px" Height="53px" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top: 22px; margin-bottom: 12px;">
                    <div class="col-lg-6 col-sm-6 col-sm-6 col-xs-6">
                        <asp:Button ID="btnIIBEnquiry" runat="server" Text="Search" Style="width: 70px" OnClientClick="return Validation();"  OnClick="btnIIBEnquiry_Click"/>
                    </div>
                </div>
            </div>


            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top: 8px;">
                <div class="col-lg-6 col-sm-6 col-sm-6 col-xs-6">
                    <asp:Label ID="lblSuccess" runat="server" style="font-size:18px;" Visible="false"></asp:Label>
                </div>
            </div>


            <div id="divLA" class="col-md-12" runat="server" style="margin-top: 12px;display: none;">
                <div id="xyze" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Insurance coverage with FGILI</h3>
                                <i class="fa fa-building"></i>
                            </div>

                        </div>
                    </div>
                    <div class="box-body">
                        <div id="LADiv" runat="server">
                            <div class="table-responsive" style="overflow-x: auto; margin-top: 6px;">
                                <asp:GridView runat="server" ID="GridViewFG" CssClass="table table-bordered" Visible="false" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="QuestDBNo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuestDBNo1" runat="server" Text='<%#Eval("QuestDBNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MatchingParameter">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMatchingPara" runat="server" Text='<%#Eval("Input_Matching_Parameter") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DoC">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDoC" runat="server" Text='<%#Eval("Quest_DoP_DoC") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SA">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsa" runat="server" Text='<%#Eval("Quest_Sum_Assured") %>' HtmlEncode="false" DataFormatString="{0:N0}"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pol Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPolStatus" runat="server" Text='<%#Eval("Quest_Policy_Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date ofExit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDateofExit" runat="server" Text='<%#Eval("Quest_Date_of_Exit") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date of Death">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDateofDeath" runat="server" Text='<%#Eval("Quest_Date_of_Death") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Update date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblupdateddate" runat="server" Text='<%#Eval("Quest_Record_last_updated") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Entity Caution Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCautionStatus" runat="server" Text='<%#Eval("Quest_Entity_Caution_Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Intermediary Caution Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInterMedCautionStatus" runat="server" Text='<%#Eval("Quest_Intermediary_Caution_Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompany" runat="server" Text='<%#Eval("Quest_Company_Number") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Negative">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNegative" runat="server" Text='<%#Eval("Is_Negative_Match") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="LA/PH/PremiumPayer">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLAProposerPayor" runat="server" Text='<%#Eval("LAProposerPayor") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>


                    </div>

                </div>
                <br />
            </div>
 
            <div id="divother" class="col-md-12" runat="server" style="margin-top: 8px;display: none;" >
                <div id="xyz1" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Insurance coverage other insurance companies</h3>
                                <i class="fa fa-building"></i>
                            </div>

                        </div>
                    </div>
                    <div class="box-body" style="overflow-x: auto">
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="GridViewOther" CssClass="table table-bordered" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="QuestDBNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuestDBNo1" runat="server" Text='<%#Eval("QuestDBNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="MatchingParameter">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMatchingPara" runat="server" Text='<%#Eval("Input_Matching_Parameter") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="DoC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDoC" runat="server" Text='<%#Eval("Quest_DoP_DoC") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="SA">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsa" runat="server" Text='<%#Eval("Quest_Sum_Assured") %>' HtmlEncode="false" DataFormatString="{0:N0}"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Pol Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPolStatus" runat="server" Text='<%#Eval("Quest_Policy_Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Date ofExit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateofExit" runat="server" Text='<%#Eval("Quest_Date_of_Exit") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Date of Death">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateofDeath" runat="server" Text='<%#Eval("Quest_Date_of_Death") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Update date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblupdateddate" runat="server" Text='<%#Eval("Quest_Record_last_updated") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Entity Caution Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCautionStatus" runat="server" Text='<%#Eval("Quest_Entity_Caution_Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Intermediary Caution Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInterMedCautionStatus" runat="server" Text='<%#Eval("Quest_Intermediary_Caution_Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompany" runat="server" Text='<%#Eval("Quest_Company_Number") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Negative">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNegative" runat="server" Text='<%#Eval("Is_Negative_Match") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="LA/PH/PremiumPayer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLAProposerPayor" runat="server" Text='<%#Eval("LAProposerPayor") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>


            <div id="LoadingPopup" class="modal fade bd-example-modal-lg" data-backdrop="static" data-keyboard="false" tabindex="-1">
                <div class="modal-dialog modal-sm">
                    <div>
                        <img src="../dist/img/loading1.gif" style="width:100px;height:100px" />
                    </div>
                </div>
</div>
        </div>
        </div>
    </form>
</body>
</html>
