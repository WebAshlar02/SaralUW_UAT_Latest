<%--//*************************************************************************************************
//*                      FUTURE GENERALI INDIA                        *    
//*****************************************************************************************************/      
//*                  I N F O R M A T I O N                                       
//***************************************************************************************************** 
// Sr. No.              : 1
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Sagar Thorave [Mfl00886]               
// BRD/CR/Codesk No/Win : Cr-1844
// Date Of Creation     : 23/09/2022         
// Description          : 1.Addnew textbox for UW comments
//******************************************************************************************************//--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UW_Comments.aspx.cs" Inherits="Appcode_UWProductivity" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
    <%--<link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />--%>

    <%--<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>--%>
    <script src="../plugins/jQueryUI/jquery-ui.js"></script>
    <%--<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>--%>
    <%--<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />--%>
    <link href="../plugins/datepicker/datepicker3.css" rel="stylesheet" />
    <script src="../plugins/datepicker/bootstrap-datepicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="divLoadingDetails" runat="server" class="col-md-12">


                <div id="LoadingDtls_container" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">UW Comments</h3>
                                <i class="fa fa-hourglass-start"></i>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12">
                            <div class="col-md-12>">
                                <asp:Label runat="server" ID="lblUWComments" CssClass="label-info" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row"></div>
                        <div class="col-md-12">
                            <div class="col-md-2">
                                Report Type :                                                                            
                               <br />
                                <asp:DropDownList ID="ddlrpttype" runat="server" Height="30px" Width="130px">
                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="UW Comments"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2" style="right: 100px">
                                Application No/Policy No :                                                                            
                                    <br />
                                <asp:TextBox runat="server" ID="txtAppno" OnTextChanged="txtAppno_TextChanged" AutoPostBack="true" CssClass="box-input"></asp:TextBox>
                            </div>
                            <div class="col-md-2" style="right: 150px">
                                Application No :                                                                            
                                    <br />
                                <asp:TextBox runat="server" ID="txtappnumber" Enabled="false" CssClass="box-input"></asp:TextBox>
                            </div>
                            <div class="col-md-1" style="right: 190px">
                                Policy No :                                                                            
                                    <br />
                                <asp:TextBox runat="server" ID="txtpolicyno" Enabled="false" CssClass="box-input"></asp:TextBox>
                            </div>
                            <div class="col-md-2" style="right: 100px">
                                Life Aisa Status :                                                                            
                                    <br />
                                <asp:TextBox runat="server" ID="txtLAStatus" Enabled="false" CssClass="box-input"></asp:TextBox>
                            </div>
                            <div class="col-md-1" style="right: 150px">
                                Saral Status :                                                                            
                                    <br />
                                <asp:TextBox runat="server" ID="txtSaralStatus" Enabled="false" CssClass="box-input"></asp:TextBox>
                            </div>
                            <div class="col-md-1" style="right: 100px">
                                Risk Score:                                                                            
                                    <br />
                                <asp:TextBox runat="server" ID="txtriskscore" Enabled="false" CssClass="box-input"></asp:TextBox>
                                <%--<asp:Label ID="lblRiskScore" runat="server" Text=""></asp:Label>--%>
                            </div>
                            <div class="col-md-1" style="right: 50px">
                                ENY Score:                                                                            
                                    <br />
                                <asp:TextBox runat="server" ID="txtenyscore" Enabled="false" CssClass="box-input"></asp:TextBox>
                                <%--<asp:Label ID="lblENYScore" runat="server" Text=""></asp:Label>--%>
                            </div>

                        </div>
                        <div class="row">
                            <br />
                        </div>
                        <div class="col-md-2 text-center" style="left:460px">
                            <br />
                            <asp:Button runat="server" ID="btnFetchRecord" Text="Search" OnClick="btnFetchRecord_Click" CssClass="btn-primary" />
                            <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" CssClass="btn-primary" />
                            <asp:Button runat="server" ID="btnExportToCsv" Text="Export" OnClick="btnExportToCsv_Click" CssClass="btn-primary" />
                           <br />
                                <asp:Button runat="server" ID="btnViewChecklist" Class="btn btn-md center-block" Style="width: 120px;" Text="View Checklist" OnClick="btnViewChecklist_Click" CssClass="btn-primary"  />
                            <!-- 1.1  Start of changes: Sagar Thorave[MFL00886] Cr-1844 -->    
                            <asp:Button runat="server" ID="btnUpdateComment" Class="btn btn-md center-block" Style="width: 150px;display:none" Text="Update Comment" CssClass="btn-primary" OnClick="btnUpdateComment_Click"/>
                         </div>
                           <!-- 1.1  End of changes: Sagar Thorave[MFL00886] Cr-1844 -->

                        <div class="row">
                            <br />
                            <br />
                            <br />
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2" runat="server" id="divUwComm" visible="false">
                                <b>UW Comments :
                                </b>
                                  <!-- 1.1  Start of changes: Sagar Thorave[MFL00886] Cr-1844 -->
                                   <br /><br /><br /><br /><br /><br /><br />
                            </div>
                             <br /><br />
                             
                                 <div class="input-group mb-3" style="margin-left:20px;display:none"  runat="server" id="UwCommAdd"> 
                                   <asp:TextBox ID="AddNewUWComment" runat="server" placeholder="Please enter comment here" TextMode="MultiLine"  Height="80" Width="700" style="margin-right:20px"></asp:TextBox>
                                    <asp:Button runat="server" ID="Btn_SaveComment" Text="Save" CssClass="btn-primary " Width="80px" Style="font-size:15px;" OnClick="Btn_SaveComment_Click" />
                                </div>
                           <!-- 1.1  End of changes: Sagar Thorave[MFL00886]  Cr-1844 -->
                            
                            <div class="row">
                                <br />
                            </div>
                            <div class="table-responsive">
                                <%--<asp:GridView ID="dgUWComments" runat="server" CssClass="table " AutoGenerateColumns="True" HeaderStyle-CssClass="btn-primary" ></asp:GridView>--%>
                               <%-- <asp:DataGrid ID="dgUWComments" CssClass="table " AutoGenerateColumns="True" HeaderStyle-CssClass="btn-primary" runat="server">
                                    <Columns>
                                    </Columns>
                                </asp:DataGrid>--%>
                                <asp:GridView ID="dgUWComments" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="USERNAME"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbluserName" Text='<%#Eval("User Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="CATEGORY"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbluserName" Text='<%#Eval("category") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="Remark"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Literal ID="remarks" runat="server" Text='<%#Eval("Remark") %>'></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="CommentsDate"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCommentsDate" Text='<%#Eval("CommentsDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="UserID"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbluserName" Text='<%#Eval("User ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                     <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="BPM Group"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblbpmgrp" Text='<%#Eval("BPM Group") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">

                            <br />
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2" runat="server" id="divreqlbl" visible="false">
                                <b>Requirement Summary :
                                </b>
                            </div>
                            <div class="row">
                                <br />
                            </div>
                            <div class="table-responsive">
                                <asp:GridView ID="dgRequirement" runat="server"  CssClass="table " AutoGenerateColumns="True" HeaderStyle-CssClass="btn-primary"></asp:GridView>
                              <%--  <asp:DataGrid ID="dgRequirement" CssClass="table " AutoGenerateColumns="True" HeaderStyle-CssClass="btn-primary" runat="server">
                                    <Columns>
                                    </Columns>
                                </asp:DataGrid>--%>
                            </div>
                        </div>
                        <%-- Added by ajay sahu on 25/05/2018 to get bmp audit trail--%>
                        <div class="row">
                            <br />
                        </div>
                        <div class="col-md-12">
                             <div class="col-md-2" runat="server" id="divStatusDesc" visible="false">
                                <b>BPMS Audit Trail:
                                </b>
                            </div>
                            <div class="row">
                                <br />
                            </div>
                            <div class="table-responsive" style="height:300px">
                                <asp:GridView ID="dgStatusDesc" runat="server" CssClass="table " AutoGenerateColumns="True" HeaderStyle-CssClass="btn-primary">

                                </asp:GridView>
                                <%--<asp:DataGrid ID="dgStatusDesc" CssClass="table " AutoGenerateColumns="True" HeaderStyle-CssClass="btn-primary" runat="server">
                                    <Columns>
                                    </Columns>
                                </asp:DataGrid>--%>
                            </div>
                        </div>
                        <%-- bmp audit trail end here --%>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

<script type="text/javascript">
    $(function () {
        $('.DatePicker').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy',
            onSelect: function (date) {
                alert('tail');
            }
        });
    })
</script>
