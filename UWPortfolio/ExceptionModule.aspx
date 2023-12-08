<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExceptionModule.aspx.cs" Inherits="ExceptionModule" %>
<%@ MasterType VirtualPath="~/Appcode/Bpmuwmodule.master" %>
<%--<%@ Register Src="~/UserControl/uc_Updateprogress.ascx" TagPrefix="uc1" TagName="uc_Updateprogress" %>--%>
<%@ Register Src="~/UserControl/PopupManageProposar.ascx" TagPrefix="uc2" TagName="PopupManageProposar" %>
<%@ Register Src="~/UserControl/PopupDoc.ascx" TagPrefix="Docs" TagName="PendingDocs" %>
<%@ Register Src="~/UserControl/HealthProfile.ascx" TagPrefix="uc3" TagName="HealthProfile" %>
<%@ Register Src="~/UserControl/AmlOffline.ascx" TagPrefix="OfflineAml" TagName="AmlOffline" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SaralUW | AddNominee</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <link href="./bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="./plugins/jQuery/jquery-2.2.3.min.js"></script>
    <link href="./dist/css/AdminLTE.min.css" rel="stylesheet" />
    <link href="./dist/css/skins/_all-skins.min.css" rel="stylesheet" />
    <script src="./dist/js/html5shiv.min.js"></script>
    <script src="./dist/js/respond.min.js"></script>
    <link href="./dist/css/styles-app.css" rel="stylesheet" />
    <link href="./plugins/select2/select2.min.css" rel="stylesheet" />
    <script src="./plugins/jQueryUI/jquery-ui.js"></script>
    <link href="./plugins/datepicker/datepicker3.css" rel="stylesheet" />
    <script src="./plugins/datepicker/bootstrap-datepicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <br />
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="divLoadingDetails" runat="server" class="col-md-12">
            <div id="LoadingDtls" class="box box-warning box-solid">
                <div class="box-header with-border">
                    <div class="col-md-12">
                        <div class="col-md-12" style="text-align:center">
                            <h1 class="box-title"><b style="text-align:center">Exception Module</b></h1>
                            <i class="fa fa-hourglass-start"></i>
                        </div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <asp:Label ID="lblAppno" runat="server" Text="Application No :"></asp:Label>&nbsp;
                                <asp:TextBox runat="server" ID="txtAppno" CssClass="box-input"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" CssClass="btn-primary" />
                        </div>
                         
                    </div>
      
                </div>
            </div>
            <div id="LoadingDtls_container" class="box box-warning box-solid"> 
                
                <div class="box-header with-border">
                    <div class="col-md-12">
                        <div class="col-md-9">
                            <h3 class="box-title">LA Details</h3>
                            <i class="fa fa-hourglass-start"></i>
                        </div>
                    </div>
                </div>
                
                <div class="box-body">                   
                    <div id="div35" class="col-md-12" runat="server">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
                <div id="LAdetails" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Current Proposal</h3>
                                <i class="fa fa-building"></i>
                            </div>                            
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                     <asp:Label runat="server" ID="lblMsg" Text=""></asp:Label>
                    <div class="box-body">
                     
                        <div class="table-responsive"  style="overflow-x:auto">
                            <asp:GridView runat="server" ID="LACurrentGrid" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" >
                                <Columns>       
                                                <asp:BoundField DataField="ApplicationNo" HeaderText="ApplicationNo" runat="server"/>
                                                <asp:BoundField DataField="LAName" HeaderText="FullName" runat="server"/>
                                                <asp:BoundField DataField="DOB" HeaderText="DOB" runat="server"/>
                                                <asp:BoundField DataField="Gender" HeaderText="Gender" runat="server"/>                                                                                                                            
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>                     
                </div>
            </ContentTemplate>
            <Triggers>
              <%--  <asp:AsyncPostBackTrigger ControlID="lnkSarDtlsRefresh" />
                <asp:AsyncPostBackTrigger ControlID="btnSarDtlsSave" />--%>
            </Triggers>
        </asp:UpdatePanel>        
    </div>
                    <div id="div1" class="col-md-12" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="xyze" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Matching found</h3>
                                <i class="fa fa-building"></i>
                            </div>                            
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive"  style="overflow-x:auto">
                            <asp:GridView runat="server" ID="LAMatchingGrid" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" >
                                <Columns>                                            
                                                <asp:BoundField DataField="ClientID" HeaderText="ClientID" runat="server"/>
                                                <asp:BoundField DataField="LAName" HeaderText="FULLName" runat="server"/>                                               
                                                <asp:BoundField DataField="DOB" HeaderText="DOB" runat="server"/>
                                                <asp:BoundField DataField="Gender" HeaderText="Gender" runat="server"/>                                                                                 
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>                     
                </div>
            </ContentTemplate>
            <Triggers>
              <%--  <asp:AsyncPostBackTrigger ControlID="lnkSarDtlsRefresh" />
                <asp:AsyncPostBackTrigger ControlID="btnSarDtlsSave" />--%>
            </Triggers>
        </asp:UpdatePanel>          
    </div>
                    <div class="col-md-12 text-right">
                          <div class="form-group"> 
                              <div class="col-md-4"></div>
                              <div class="col-md-5">
                                   <label style="font-size:larger;background-color:blue; color:white">Bypass flag for approving IIB matched client id but allowing to create new id</label>
                              </div>
                              <div class="col-md-1">
                                <label style="font-size:larger; color:red">Bypass Flag</label>                                     
                                        </div>   
                              <div class="col-md-2">
                                   <asp:DropDownList ID="ddlLABypassFlag" class="form-control select2"   runat="server" OnSelectedIndexChanged="ddlLABypassFlag_SelectedIndexChanged">
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                            </asp:DropDownList>
                              </div>
                              </div>
                    </div>                    
                 </div>               
                </div>
            <br />

            <div id="LoadingDtls_container1" class="box box-warning box-solid"> 
                
                <div class="box-header with-border">
                    <div class="col-md-12">
                        <div class="col-md-9">
                            <h3 class="box-title">Proposer Details</h3>
                            <i class="fa fa-hourglass-start"></i>
                        </div>
                    </div>
                </div>
                
                <div class="box-body">                   
                    <div id="div6" class="col-md-12" runat="server">
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                <div id="LAdetails1" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Current Proposal</h3>
                                <i class="fa fa-building"></i>
                            </div>                            
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                     <asp:Label runat="server" ID="Label1" Text=""></asp:Label>
                    <div class="box-body">
                     
                        <div class="table-responsive"  style="overflow-x:auto">
                            <asp:GridView runat="server" ID="ProposerCGridView" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" >
                                <Columns>       
                                                <asp:BoundField DataField="ApplicationNo" HeaderText="ApplicationNo" runat="server"/>
                                                <asp:BoundField DataField="LAName" HeaderText="FullName" runat="server"/>
                                                <asp:BoundField DataField="DOB" HeaderText="DOB" runat="server"/>
                                                <asp:BoundField DataField="Gender" HeaderText="Gender" runat="server"/>                                                                                                                            
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>                     
                </div>
            </ContentTemplate>
            <Triggers> 
              <%--  <asp:AsyncPostBackTrigger ControlID="lnkSarDtlsRefresh" />
                <asp:AsyncPostBackTrigger ControlID="btnSarDtlsSave" />--%>
            </Triggers>
        </asp:UpdatePanel>        
    </div>
                    <div id="div7" class="col-md-12" runat="server">
        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>
                <div id="xyze1" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Matching found</h3>
                                <i class="fa fa-building"></i>
                            </div>                            
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive"  style="overflow-x:auto">
                            <asp:GridView runat="server" ID="ProposerMGridView" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" >
                                <Columns>                                            
                                                <asp:BoundField DataField="ClientID" HeaderText="ClientID" runat="server"/>
                                                <asp:BoundField DataField="LAName" HeaderText="FULLName" runat="server"/>                                               
                                                <asp:BoundField DataField="DOB" HeaderText="DOB" runat="server"/>
                                                <asp:BoundField DataField="Gender" HeaderText="Gender" runat="server"/>                                                                                 
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>                     
                </div>
            </ContentTemplate>
            <Triggers>
              <%--  <asp:AsyncPostBackTrigger ControlID="lnkSarDtlsRefresh" />
                <asp:AsyncPostBackTrigger ControlID="btnSarDtlsSave" />--%>
            </Triggers>
        </asp:UpdatePanel>          
    </div>
                    <div class="col-md-12 text-right">
                          <div class="form-group"> 
                              <div class="col-md-4"></div>
                              <div class="col-md-5">
                                   <label style="font-size:larger;background-color:blue; color:white">Bypass flag for approving IIB matched client id but allowing to create new id</label>
                              </div>
                              <div class="col-md-1">
                                <label style="font-size:larger; color:red">Bypass Flag</label>                                     
                                        </div>   
                              <div class="col-md-2">
                                   <asp:DropDownList ID="ddlProposer" class="form-control select2"   runat="server" OnSelectedIndexChanged="ddlLABypassFlag_SelectedIndexChanged">
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                            </asp:DropDownList>
                              </div>
                              </div>
                    </div>                    
                 </div>               
                </div>
            
            <br />
            <div id="Payer" class="box box-warning box-solid"> 
                
                <div class="box-header with-border">
                    <div class="col-md-12">
                        <div class="col-md-9">
                            <h3 class="box-title">Payer Details</h3>
                            <i class="fa fa-hourglass-start"></i>
                        </div>
                    </div>
                </div>
                
                <div class="box-body">                   
                    <div id="div4" class="col-md-12" runat="server">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div id="PayerDetails" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Current Proposal</h3>
                                <i class="fa fa-building"></i>
                            </div>                            
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                  
                    <div class="box-body">
                        <div class="table-responsive"  style="overflow-x:auto">
                            <asp:GridView runat="server" ID="PayerCGridView" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" >
                                <Columns>       
                                                <asp:BoundField DataField="ApplicationNo" HeaderText="ApplicationNo" runat="server"/>
                                                <asp:BoundField DataField="LAName" HeaderText="FullName" runat="server"/>
                                                <asp:BoundField DataField="DOB" HeaderText="DOB" runat="server"/>
                                                <asp:BoundField DataField="Gender" HeaderText="Gender" runat="server"/>                                                                                                                            
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>                     
                </div>
            </ContentTemplate>
            <Triggers>
              <%--  <asp:AsyncPostBackTrigger ControlID="lnkSarDtlsRefresh" />
                <asp:AsyncPostBackTrigger ControlID="btnSarDtlsSave" />--%>
            </Triggers>
        </asp:UpdatePanel>        
    </div>
                    <div id="div5" class="col-md-12" runat="server">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <div id="Payerdetails" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Matching found</h3>
                                <i class="fa fa-building"></i>
                            </div>                            
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive"  style="overflow-x:auto">
                            <asp:GridView runat="server" ID="PayerMGridView" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" >
                                <Columns>                                            
                                                <asp:BoundField DataField="ClientID" HeaderText="ClientID" runat="server"/>
                                                <asp:BoundField DataField="LAName" HeaderText="FULLName" runat="server"/>                                               
                                                <asp:BoundField DataField="DOB" HeaderText="DOB" runat="server"/>
                                                <asp:BoundField DataField="Gender" HeaderText="Gender" runat="server"/>                                                                                 
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>                     
                </div>
            </ContentTemplate>
            <Triggers>
              <%--  <asp:AsyncPostBackTrigger ControlID="lnkSarDtlsRefresh" />
                <asp:AsyncPostBackTrigger ControlID="btnSarDtlsSave" />--%>
            </Triggers>
        </asp:UpdatePanel>          
    </div>
                    <div class="col-md-12 text-right">
                          <div class="form-group"> 
                              <div class="col-md-4"></div>
                              <div class="col-md-5">
                                   <label style="font-size:larger;background-color:blue; color:white">Bypass flag for approving IIB matched client id but allowing to create new id</label>
                              </div>
                              <div class="col-md-1">
                                <label style="font-size:larger; color:red">Bypass Flag</label>                                     
                                        </div>   
                              <div class="col-md-2">
                                   <asp:DropDownList ID="ddlPayer" class="form-control select2"  runat="server" OnSelectedIndexChanged="ddlPayer_SelectedIndexChanged" >
                                                <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                            </asp:DropDownList>
                              </div>
                              </div>
                    </div>                    
                 </div>               
                </div>
           
            <div  class="col-md-12" style="text-align:center">
                  <asp:Button ID="btnSave"  runat="server" Text="Save Data>>" CssClass="btn btn-primary" OnClick="btnSave_Click" />  
                       
          </div>
          
            </div>

        <div></div>
        <div></div>
        <div></div>
        <br />
        <br />

    </form>
</body>
</html>
