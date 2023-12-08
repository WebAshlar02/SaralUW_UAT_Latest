<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" EnableEventValidation="false" %>

<%--<%@ Register Src="~/UserControl/uc_Updateprogress.ascx" TagPrefix="uc1" TagName="uc_Updateprogress" %>--%>
<%@ Register Src="~/UserControl/PopupAfiCfiUW.ascx" TagPrefix="AfiCfiWU" TagName="PopupAfiCfiUW" %>
<%@ Register Src="~/UserControl/PopupDoc.ascx" TagPrefix="Docs" TagName="PendingDocs" %>
<%@ Register Src="~/UserControl/BulkDecision.ascx" TagPrefix="Bulk" TagName="BulkDec" %>
<%@ Register Src="~/UserControl/ManualAllocation.ascx" TagPrefix="ManualAlloc" TagName="ManualAllocation" %>
<%@ Register Src="~/UserControl/UWExamQue.ascx" TagPrefix="AfiCfiWU" TagName="UWExamQue" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <%--<uc1:uc_Updateprogress runat="server" ID="uc_Updateprogress" />--%>
    <meta charset="utf-8" http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>SaralUW</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" />

    <!-- Font Awesome -->
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css"/>--%>
    <link rel="stylesheet" href="downloaded_refs/font-awesome-4.6.3/css/font-awesome.css" />

    <!-- Ionicons -->
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css"/>--%>
    <link rel="stylesheet" href="downloaded_refs/ionicons-2.0.1/css/ionicons.min.css" />
    <!-- DataTables -->
    <link rel="stylesheet" href="plugins/datatables/dataTables.bootstrap.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/AdminLTE.min.css" />
    <link rel="stylesheet" href="dist/css/styles-app.css" />
    <!-- AdminLTE Skins. We have chosen the skin-blue for this starter
        page. However, you can choose any other skin. Make sure you
        apply the skin class to the body tag so the changes take effect.
  -->
    <link rel="stylesheet" href="dist/css/skins/skin-maroon.min.css" />
    <!-- jQuery 2.2.3 -->
    <script src="plugins/jQuery/jquery-2.2.3.min.js"></script>

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
    <script type="text/javascript">
        function hideloading() {
            $("#loaderdiv").hide();
        }

        function showloading() {
            $("#loaderdiv").show();
        }

        $(window).load(function () {
            $('#loaderdiv').fadeOut();
        });

        //function LoaderPop()
        //{
        //    alert('Hi');
        //    $('#ldrModal').show();
        //}        
        function SetTarget() {
            debugger
            document.forms[0].target = "_blank";
        }

        /*added by shri on 13 june 17 to open on same page*/
        function SetTargetToSelf() {
            document.forms[0].target = "_self";
        }

        function ShowModalPopup(message) {

            $('#lblerrorofac').html(message);
            $('#mdlofac').modal();
            // alert(message);

        }

        /*added by shri on 30 oct 17 to open on policy enquiry*/
        <%--function fnPolicyEnquiery() {
            var enq = '<%=strPolicyEnquiery%>';
           window.open(enq, '_blank')
       }--%>

        /*end here*/

        $(document).ready(function () {
            //    $('#example1').DataTable({
            //        "order": [[9, "desc"]]
            //    });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {

                if (!$.fn.DataTable.isDataTable('#example1')) {
                    $('#example1').dataTable({
                        "order": [[1, "desc"]]
                    });
                }
                $('.lnkButton').click(function () {
                    $('#loaderdiv').fadeIn();
                });
            });
            //$("#lnkCommondashbord").click();
            $('.lnkButton').click(function () {
                $('#loaderdiv').fadeIn();
            });

        })

        function fnHideShowButton(type) {
            $('#ModalCommon').html('');
            if (type == 'afi') {
                $('#btnAfiSubmit').show();
                $('#btnCfiSubmit').hide();
                $('#btnUWSubmit').hide();
                $('#ModalCommon').html('AFI');
            }
            else if (type == 'cfi') {
                $('#btnAfiSubmit').hide();
                $('#btnCfiSubmit').show();
                $('#btnUWSubmit').hide();
                $('#ModalCommon').html('CFI');
            }
            else if (type = 'uw') {
                $('#btnAfiSubmit').hide();
                $('#btnCfiSubmit').hide();
                $('#btnUWSubmit').show();
                $('#ModalCommon').html('UW');
            }
            //clear user control values
            $('#txtApplnNumber').val('');
            $('#txtPolicyNumber').val('');
            $('#txtComment').val('');
            $('#tblPolicyDetails').hide();
            $('.main').hide();
            $('.children').hide();
            $('#lblError').addClass('hide');
        }
    </script>
    <script type="text/javascript">

        function fun1(myurl) {
            debugger;
            var detailsWindow;
            detailsWindow = window.open(myurl, "dfd", "width=600,height=1000, resizable, scrollbars");
            detailsWindow.focus();
            return false;
        }

    </script>
     <%--//1.1 Begin of Changes; Bhaumik Patel - [CR-5307]--%>
      <script type="text/javascript">
          function confirmation() {
              debugger
              if (confirm('are you sure you want to logout?')) {
                  return true;
              } else {
                  return false;
              }
          }

      </script>
     <%--//1.1 End of Changes; Bhaumik Patel - [CR-5307]--%>
    <style>
        .bg-title {
            background-color: #a81d22;
            color: #fff;
        }

        .loader {
            position: fixed;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url(dist/img/loading1.gif) 50% 50% no-repeat white;
            /* opacity: .75; */
            opacity: .90;
        }

        .small-box .inner a {
            color: #fff;
            font-size: 14px;
        }
    </style>

</head>
<!--
BODY TAG OPTIONS:
=================
Apply one or more of the following classes to get the
desired effect
|---------------------------------------------------------|
| SKINS         | skin-blue                               |
|               | skin-black                              |
|               | skin-purple                             |
|               | skin-yellow                             |
|               | skin-red                                |
|               | skin-green                              |
|---------------------------------------------------------|
|LAYOUT OPTIONS | fixed                                   |
|               | layout-boxed                            |
|               | layout-top-nav                          |
|               | sidebar-collapse                        |
|               | sidebar-mini                            |
|---------------------------------------------------------|
-->
<body class="hold-transition skin-maroon fixed">
    <form id="Form1" runat="server">
        <%--<div id="ldrModal" runat="server" class="LoaderModal">
            <div class="center">
                <img alt="" src="./dist/img/loader4.gif" />
            </div>
        </div>--%>
        <div class="wrapper">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="loader" id="loaderdiv"></div>
            <!-- Main Header -->
            <header class="main-header">

                <!-- Logo -->
                <a href="#" class="logo">
                    <!-- mini logo for sidebar mini 50x50 pixels -->
                    <span class="logo-mini"><b>S</b>UW</span>
                    <!-- logo for regular state and mobile devices -->
                    <span class="logo-lg"><b>Saral</b>UW</span>
                </a>

                <!-- Header Navbar -->
                <nav class="navbar navbar-static-top" role="navigation">
                    <a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button">
                        <span class="sr-only">Toggle navigation</span>
                    </a>
                    <div class="navbar-custom-menu">
                        <ul class="nav navbar-nav">


                            <li class="dropdown user user-menu" style="display:flex;">
                              
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                    
                                    <img src="dist/img/avatar5.png" class="user-image" alt="User Image" />

                                    <span id="spanusername" runat="server" class="hidden-xs"></span>
                                    <%--//1.1 Begin of Changes; Bhaumik Patel - [CR-5307]--%>
                                      <asp:LinkButton ID="lnklogout"  runat="server" OnClick="lnklogout_Click" onclientclick=" return confirmation();">Logout</asp:LinkButton>
                                     <%--//1.1 End of Changes; Bhaumik Patel - [CR-5307]--%>
                                </a>
                                <%--  <ul class="dropdown-menu">--%>

                                <%--   <li class="user-header">
                                        <img src="dist/img/avatar5.png" class="img-circle" alt="User Image" />

                                    </li>--%>

                                <%--  <li class="user-body">
                                        <div class="row">
                                            <div class="col-xs-4 text-center">
                                                <a href="#">Followers</a>
                                            </div>
                                            <div class="col-xs-4 text-center">
                                                <a href="#">Sales</a>
                                            </div>
                                            <div class="col-xs-4 text-center">
                                                <a href="#">Friends</a>
                                            </div>
                                        </div>

                                    </li>--%>

                                <%--   <li class="user-footer">
                                        <div class="pull-left">
                                            <a href="#" class="btn btn-default btn-flat">Profile</a>
                                        </div>
                                        <div class="pull-right">
                                            <a href="#" class="btn btn-default btn-flat">Sign out</a>
                                        </div>
                                    </li>--%>
                                <%-- </ul>--%>
                            </li>
                            <%-- <li>
                                <a href="#" data-toggle="control-sidebar"><i class="fa fa-gears"></i></a>
                            </li>--%>
                        </ul>
                    </div>
                </nav>
            </header>
            <!-- Left side column. contains the logo and sidebar -->
            <aside class="main-sidebar">
                <!-- sidebar: style can be found in sidebar.less -->
                <section class="sidebar">

                    <!-- Sidebar user panel (optional) -->
                    <div class="user-panel">
                        <div class="pull-left image">
                            <img src="dist/img/avatar5.png" class="img-circle" alt="User Image">
                        </div>
                        <div class="pull-left info">
                            <%-- <p>Dharmesh D Patel</p>--%>
                            <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                            <!-- Status -->
                            <a href="#"><i class="fa fa-circle text-success"></i>Branch</a>
                        </div>
                    </div>

                    <!-- search form (Optional) -->
                    <form action="#" method="get" class="sidebar-form">
                        <div class="input-group">
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtSearch" CssClass="form-control" placeholer="search..."></asp:TextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:LinkButton runat="server" ID="lnkSearchAppilcation" OnClientClick="SetTargetToSelf();" OnClick="lnkSearchAppilcation_Click"><i class="fa fa-search col-md-12"></i></asp:LinkButton>
                            </div>
                        </div>
                    </form>
                    <!-- /.search form -->

                    <!-- Sidebar Menu -->
                    <ul class="sidebar-menu">
                        <li class="header">Menu</li>
                        <!-- Optionally, you can add icons to the links -->
                        <%--        <li class="active"><a href="#"><i class="fa fa-link"></i> <span>Link</span></a></li>
                         <li><a href="#"><i class="fa fa-link"></i> <span>Another Link</span></a></li>--%>
                        <li class="treeview">
                            <a href="#"><i class="fa fa-link"></i><span>Dashboards</span>
                                <span class="pull-right-container">
                                    <i class="fa fa-angle-left pull-right"></i>
                                </span>
                            </a>
                            <ul class="treeview-menu">
                                <li>
                                    <asp:LinkButton ID="lnkOnline" runat="server" Text="ONLINE" OnClientClick="SetTargetToSelf();" OnClick="lnkOnline_Click" CssClass="lnkButton">Online</asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="lnkoffline" runat="server" Text="OFFLINE" OnClientClick="SetTargetToSelf();" OnClick="lnkoffline_Click" CssClass="lnkButton">Branch</asp:LinkButton>
                                </li>
                                 <li>
                                    <asp:LinkButton ID="lnkGrp" runat="server" Text="GROUP" OnClientClick="SetTargetToSelf();" OnClick="lnkGrp_Click" CssClass="lnkButton">Group</asp:LinkButton>
                                </li>
                            </ul>
                        </li>
                        <li class="treeview">
                            <a href="#"><i class="fa fa-link"></i><span>Activity</span>
                                <span class="pull-right-container">
                                    <i class="fa fa-angle-left pull-right"></i>
                                </span>
                            </a>
                            <ul class="treeview-menu">
                                <li style="display: none;"><a href="#">Refer to UW</a></li>
                                <li style="display: none;"><a href="#">Assign to CMO</a></li>
                                <%--<li><a href="#">Add Requirement</a></li>--%>
                                <li><a href="#" onclick="fnHideShowButton('afi');" data-toggle="modal" data-backdrop="static" data-keyboard="false" data-target="#myModal">AFI</a></li>
                                <li><a href="#" onclick="fnHideShowButton('cfi');" data-toggle="modal" data-backdrop="static" data-keyboard="false" data-target="#myModal">CFI</a></li>
                                <li><a href="#" onclick="fnHideShowButton('uw');" data-toggle="modal" data-backdrop="static" data-keyboard="false" data-target="#myModal">UW Reversal</a></li>
                                <%--<li><a href="#" data-toggle="modal" data-backdrop="static" data-keyboard="false" data-target="#divShowMessage">test</a></li>--%>
                                <%--<li><a href="Appcode/Dedup.aspx" target="_blank">Dedupe</a></li>--%>
                                <li><a href="#" data-toggle="modal" data-backdrop="static" data-keyboard="false" data-target="#divBulkPopup">Bulk Decision</a></li>
                                <li><a href="#" data-toggle="modal" data-backdrop="static" data-keyboard="false" data-target="#divManualAllocation">Manual Allocation</a></li>
                                  <li><a href="#" data-toggle="modal" data-backdrop="static" data-keyboard="false" data-target="#divQueFileUpload">Exams Questions Upload</a></li>
                                  <%--Added by Sura on 23 APRIL 2018 for UW Comments--%>
                                <li><a href="Appcode/UW_Comments.aspx" target="_blank">UW Comments</a></li>
                                <li><a href="AddNominee.aspx" target="_blank">Add Nominee</a></li>
                                <li><a href="ExceptionModule.aspx" target="_blank">Exception Module</a></li><%--added by rupali--%>
                                <li><a href="Appcode/DeactiveUsers.aspx" target="_blank">Activate/Deactivate Users</a></li>
                                <li><a href="http://10.1.41.31/pts/Index.aspx" target="_blank" >Product circulars</a></li><!--Added by suraj on 01-Apr-2019 -->
                                <%--Start of changes: Sagar Thorave[MFL00886] CR- 30573--%> 
                                <li id="PolicyRiskId" style="display:none" runat="server" ><a  href="Appcode/BmpsModule.aspx" target="_blank">Policy Risk Flag</a></li>
                                   <%--End of changes: Sagar Thorave[MFL00886] CR- 30573--%> 
                                <%--<li><a href="Appcode/MedicalStatus4Sales.aspx" target="_blank">Medical Status For Sales</a></li>--%>
                                <%--<li><a href="#" onclick="return fun1('result.aspx');" >Open Excel</a></li>--%>
                                <%--<asp:Button ID="Open" Text="Open" runat="server" OnClientClick="return fun1('result.aspx')" />--%>
                                <%--End--%>
                                 <%--Start of changes: Sushant Devkate[MFL00905] CR- 30363--%> 
                                 <li id="liAddNegPinCode" runat="server" style="display:none"><a  href="Appcode/UWAddPinCode.aspx" target="_blank">Add Negative PinCode</a></li>
                                 <%--End of changes: Sushant Devkate[MFL00905] CR- 30363--%> 
                                  <li id="MerVideos" runat="server"  style="display:none" ><a  href="Appcode/MerVideoRecording.aspx" target="_blank">MerVideos</a></li>
                                   <%--End of changes: Sagar Thorave[MFL00886] CR- 30499--%> 
                                 <%--Start of changes: Sushant Devkate[MFL00905] CR- 2809--%> 
                                 <li id="liIIBQuer" runat="server"  ><a  href="Appcode/IIBQuery.aspx" target="_blank">Get IIB Data</a></li>
                                 <%--End of changes: Sushant Devkate[MFL00905] CR- 2809--%> 
                                    <%--Start of changes: Jayendra Patankar[WebAshlar01] CR- 4126--%> 
                                 <li id="li1" runat="server"  ><a  href="Appcode/MerVideoViewer.aspx" target="_blank">Video MER</a></li>
                                 <%--End of changes: Jayendra Patankar[WebAshlar01] CR- 4126--%> 
                                  <%--Start of changes: Royson Pinto[MFL01002] CR- 4783--%> 
                                 <li><asp:LinkButton ID="LinkButton2" runat="server"  OnClientClick="hideloading()"  OnClick="lnkrights1_Click" CssClass="lnkButton"  >Users Rights</asp:LinkButton></li>
                                 <li id="li3" runat="server"  ><a  href="Appcode/DeclineThirdParty.aspx" target="_blank">Decline Third Party Payment</a></li>
                                  
                                <%--//1.1 Begin of Changes; Bhaumik Patel - [CR-5855]--%>
                                <li id="MortalityID" runat="server"  visible="false" ><a  href="Appcode/UserAccessFlatMortality.aspx" target="_blank">User Access Flat Mortality</a></li>
                                 <%--//1.1 End of Changes; Bhaumik Patel - [CR-5855 ]--%>

                                 <%--//1.1 End of Changes; Bhaumik Patel - [CR-5307]--%>
                                 <%--End of changes: Royson Pinto[MFL01002] CR- 4783--%> 
                                <%-- <%--//1.1 Begin of Changes; Bhaumik Patel - [CR-5307]--%>
                                 <%--<li><asp:LinkButton ID="lnkrights1" runat="server"  OnClientClick="hideloading()"  OnClick="lnkrights1_Click" CssClass="lnkButton"  >Users Rights</asp:LinkButton></li>--%>
                                 <%--//1.1 End of Changes; Bhaumik Patel - [CR-5307]--%>--%>
                            </ul>
                        </li>
                        <li class="treeview">
                            <a href="#"><i class="fa fa-link"></i><span>View</span>
                                <span class="pull-right-container">
                                    <i class="fa fa-angle-left pull-right"></i>
                                </span>
                            </a>
                            <ul class="treeview-menu">
                                <%--<li><a href="#" onclick="fnPolicyEnquiery();">Policy Enquiry</a></li>--%>
                                
                                <%--<li><a href="http://service.fglife.in/POLENQBOE/BPMAccess.aspx?Taskid=B9EADA4E-CCAA-47E4-9A9D-22F5A0281F33" target="_blank"">Policy Enquiry</a></li>--%>
                                <%--<li><a href="http://service.fglife.in/POLENQBOE/BPMAccess.aspx?Taskid=48AA2928-531B-4C28-8E61-52190EF243F4" target="_blank"">Policy Enquiry</a></li>--%>
                                <li><a href="http://service.fglife.in/POLENQBOE/BPMAccess.aspx?Taskid=E80CC0B7-74FF-4C3D-B4ED-40E1CD1B5F22" target="_blank"">Policy Enquiry</a></li>
                                
                                <li><a href="http://mumhoibrnchlf02/Bpmuwmodule/AppCode/UWProductivity.aspx" target="_blank" >MIS-Saral</a></li>
                                
                                 <li><a href="http://service.fglife.in/fgindia/get_login.aspx" target="_blank" >Payment Assist</a></li>
                                
                                
                                 <li><a href="http://mumhoibrnchlf02/BPMUWMODULE/Appcode/BulkManualAssignment.aspx" target="_blank" >Bulk Allocation</a></li>
                                <li><a href="http://lifeapp.futuregenerali.in/portal/login.do" target="_blank" >IBMP</a></li>
                                <%--<li><a href="http://service.fglife.in/images_uw/dmsviewer.aspx" target="_blank" >DMS</a></li>--%>
                                
                                <li><a href="http://mumhoifglife01/Images_UWViewer/DMSViewer.aspx" target="_blank" >DMS</a></li><%--Commented above by suraj as shabbir sir suggested new link --%>
                                
                                <li><a href="http://mumhoifglife01/ReqResolve/" target="_blank" >Document upload</a></li>
                                <li><a href="http://ithelpdesk/codesk/Logout.htm" target="_blank" >CoDesk</a></li>
                                <li><a href="Dedupe.aspx" target="_blank" >Dedupe</a></li>                                
                                <li><a href="http://fgvvsthn01/sites/DefaultCollection/FG_Infra1/Dashboards/MyDashboard.aspx" target="_blank" >ARF</a></li>
                                
                                <li><a href="http://mumhoifglife01/mcreports/Login.aspx?tag=logout" target="_blank" >Med - Branches</a></li>
                                
                                <li><a href="http://mumhoifglife01/FG.LF.POS.EkycAuth/#/AuthExcelupload" target="_blank" >KYC Verification</a></li>
                                <li><a href="http://mumhoimislf01.fgi.ad/Reports/Pages/Report.aspx?ItemPath=/Life/General_Reports/BPM_Reports/MMT/MMT_MedicalRequirement/MMT_Medical_Requirment" target="_blank" >Med UW Team</a></li>                                
                                <li><a href="http://mumhoimislf01/Reports/Pages/Report.aspx?ItemPath=/Life/General_Reports/PolicyDetailsDataMart/PolicyDeatils_Audit_View" target="_blank" >Policy Details</a></li>
                                <li><a href="http://mumhoimislf01.fgi.ad/Reports/Pages/Report.aspx?ItemPath=/Life/General_Reports/PolicyDetailsDataMart/PolicyDeatils_Audit_View2" target="_blank" >Portfolio</a></li>
                                <li><a href="http://mumhoimislf01.fgi.ad/Reports/Pages/Report.aspx?ItemPath=/Life/General_Reports/NB_Login/Login_Status_Summary_Archive" target="_blank" >Waterfall Archive</a></li>
                                <li><a href="http://mumhoimislf01.fgi.ad/Reports/Pages/Report.aspx?ItemPath=/Life/General_Reports/NB_Login/Login_Status_Summary" target="_blank" >Waterfall</a></li>
                                <li><a href="http://mumhoimislf01.fgi.ad/Reports/Pages/Report.aspx?ItemPath=/Life/General_Reports/NB_Login/NB_Login_Issued&SelectedSubTabId=GenericPropertiesTab" target="_blank" >Issuance Summary</a></li>
                                <li><a href="http://mumhoimislf01.fgi.ad/Reports/Pages/Report.aspx?ItemPath=/Life/General_Reports/NB_Login/Login_Status_WIP_TAT_Summary" target="_blank" >WIP</a></li>                                
                                                          
                                <li style="display: none;"><a href="#">Cases in pipeline</a></li>
                            </ul>
                        </li>
                    </ul>
                    <!-- /.sidebar-menu -->
                </section>
                <!-- /.sidebar -->
            </aside>

            <!-- Content Wrapper. Contains page content -->
            <div class="content-wrapper">
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Dashboards
                   <%-- <small>General</small>--%>
                    </h1>
                    <%--  <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                        <li class="active">General</li>
                    </ol>--%>
                </section>

                <!-- Main content -->
                <section class="content">
                    <div class="box box-solid">
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="box-group" id="accordion">
                                <!-- we are adding the .panel class so bootstrap.js collapse plugin detects it -->
                                <asp:UpdatePanel runat="server" ID="upDashboardCount" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="panel box box-primary">
                                            <div class="box-header with-border">
                                                <h4 class="box-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">Datafacts</a>
                                                </h4>
                                            </div>
                                            <div id="collapseOne" class="panel-collapse collapse in">
                                                <div class="box-body">
                                                    <!-- Small boxes (Stat box) -->
                                                    <div class="row" id="divDashbord" runat="server">
                                                        <div class="col-lg-2 col-xs-6" id="divFresh" runat="server">
                                                            <!-- small box -->
                                                            <div class="small-box bg-aqua">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblFreshcount" runat="server" Text="0"></asp:Label>
                                                                    </h3>
                                                                    <p>
                                                                        <asp:LinkButton ID="lnkFreshCase" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server" CssClass="lnkButton">FRESH CASES</asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                                <%--<div class="icon">
                                                                    <i class="ion ion-person-add"></i>
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>
                                                        <!-- ./col -->
                                                        <div class="col-lg-2 col-xs-6" style="display: none">
                                                            <!-- small box -->
                                                            <div class="small-box bg-fuchsia">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblUWRefercount" runat="server" Text="0"></asp:Label></h3>
                                                                    <p>
                                                                        <asp:LinkButton ID="lnkUwrefer" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server" CssClass="lnkButton">UWREFER CASES</asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                                <%--<div class="icon">
                                                                    <i class="ion ion-person-stalker"></i>
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>
                                                        <!-- ./col -->
                                                        <div class="col-lg-2 col-xs-6" id="divPending" runat="server">
                                                            <!-- small box -->
                                                            <div class="small-box bg-red">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblPendingcount" runat="server" Text="0"></asp:Label></h3>

                                                                    <p>
                                                                        <asp:LinkButton ID="lnkPendingcases" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server" CssClass="lnkButton">PENDING</asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                                <%-- <div class="icon">
                                                                    <i class="ion ion-person-stalker"></i>
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>
                                                        <!-- ./col -->
                                                        <div class="col-lg-2 col-xs-6" style="display: none;">
                                                            <!-- small box -->
                                                            <div class="small-box bg-purple">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblPendingVerificationcount" runat="server" Text="0"></asp:Label></h3>

                                                                    <p>
                                                                        <asp:LinkButton ID="lnkPendingVerification" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server" CssClass="lnkButton">Pending</asp:LinkButton>

                                                                    </p>
                                                                </div>
                                                                <%--<div class="icon">
                                                                    <i class="ion ion-ios-medkit"></i>
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>
                                                        <!-- ./col -->
                                                        <div class="col-lg-2 col-xs-6" style="display: none;">
                                                            <!-- small box -->
                                                            <div class="small-box bg-orange">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblDocVerifiedcount" runat="server" Text="0"></asp:Label></h3>

                                                                    <p>
                                                                        <asp:LinkButton ID="lnkDocVerified" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server" CssClass="lnkButton">Documents Verified</asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                                <%-- <div class="icon">
                                                                    <i class="ion ion-document-text"></i>
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>
                                                        <!-- ./col -->
                                                        <div class="col-lg-2 col-xs-6" style="display: none;">
                                                            <!-- small box -->
                                                            <div class="small-box bg-yellow">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblDocRealizedCount" runat="server" Text="0"></asp:Label></h3>
                                                                    <p>
                                                                        <asp:LinkButton ID="lnkDocRealized" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server" CssClass="lnkButton">Payment Realized</asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                                <%--<div class="icon">
                                                                    <i class="ion ion-android-call"></i>
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>
                                                        <!-- ./col -->
                                                        <div class="col-lg-2 col-xs-6" id="divReady" runat="server">
                                                            <!-- small box -->
                                                            <div class="small-box bg-aqua">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblReadytoissuecount" runat="server" Text="0"></asp:Label></h3>

                                                                    <p>
                                                                        <asp:LinkButton ID="lnkReadyToIssue" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server" CssClass="lnkButton">Ready To Issue</asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                                <%-- <div class="icon">
                                                                    <i class="ion ion-thumbsup"></i>
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>
                                                        <%--resolve--%>
                                                        <div class="col-lg-2 col-xs-6" id="divResolve" runat="server">
                                                            <!-- small box -->
                                                            <div class="small-box bg-fuchsia">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblResolveCount" runat="server" Text="0"></asp:Label></h3>

                                                                    <p>
                                                                        <asp:LinkButton ID="lblResolve" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server" CssClass="lnkButton">Resolve</asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                                <%--<div class="icon">
                                                                    <i class="ion ion-thumbsup"></i>
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>

                                                        <%--signoff--%>
                                                        <div class="col-lg-2 col-xs-6" id="divSignOff" runat="server">
                                                            <!-- small box -->
                                                            <div class="small-box bg-red">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblSignoffCount" runat="server" Text="0"></asp:Label></h3>

                                                                    <p>
                                                                        <asp:LinkButton ID="lblSignoff" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server" CssClass="lnkButton">Sign Off</asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                                <%--<div class="icon">
                                                                    <i class="ion ion-thumbsup"></i>
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>

                                                        <%--DOC QC--%>
                                                        <div class="col-lg-2 col-xs-6 hide">
                                                            <!-- small box -->
                                                            <div class="small-box bg-red">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblDocQcCount" runat="server" Text="0"></asp:Label></h3>
                                                                    <p>
                                                                        <asp:LinkButton ID="LinkButton1" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server" CssClass="lnkButton">DOCQC</asp:LinkButton>

                                                                    </p>
                                                                </div>
                                                                <%--<div class="icon">
                                                                    <i class="ion ion-thumbsup"></i>
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>
                                                          <%--Close File Review(Underwriting QC) Added by Suraj--%>
                                                        <div class="col-lg-2 col-xs-6" id="divClsRev" runat="server">
                                                            <!-- small box -->
                                                            <div class="small-box bg-fuchsia-active">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblclsrew" runat="server" Text="0"></asp:Label></h3>

                                                                    <p>
                                                                        <asp:LinkButton ID="lnkclsrew" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server" CssClass="lnkButton">Close File Review</asp:LinkButton>
                                                                        <asp:HiddenField ID="hdncolsefilerw" runat="server" />
                                                                    </p>
                                                                </div>
                                                                <%-- <div class="icon">
                                                                    <i class="ion ion-thumbsup"></i>
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>

														<%--Start-Added By Shyam Patil-26-02-2020--%>
														<div class="col-lg-2 col-xs-6" id="divRev" runat="server">
                                                            <!-- small box -->
                                                            <div class="small-box bg-blue">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblRevival" runat="server" Text="0"></asp:Label>
                                                                    </h3>
                                                                    <p>
                                                                        <asp:LinkButton ID="linkbtnRevivalCases" runat="server" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" CssClass="lnkButton">Revival Cases</asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                                <%--<div class="icon">
                                                                    <i class="ion ion-person-add"></i>
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>
														<%--End-Added By Shyam Patil-26-02-2020--%>
                                                         <%--//1.1 Begin of Changes; Bhaumik Patel - [CR-5307]--%>
                                                        <div id ="divrights" class="col-lg-2 col-xs-6" runat="server" visible="false">
                                                            <!-- small box -->
                                                            <div class="small-box bg-green">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblrights" runat="server" Text=""></asp:Label>
                                                                    </h3>
                                                                    <p>
                                                                        <asp:LinkButton ID="lnkrights" runat="server" OnClientClick="SetTarget();" OnClick="lnkrights_Click"   CssClass="lnkButton" ><b>USER RIGHTS</b></asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                                 <div class="icon">
                                                                    <i class="ion ion-person-add"></i>
                                                                </div>
                                                                <%--<div class="icon">

                                                                    
                                                                    <img src="dist/img/avatar5.png"  style="float: left; width: 50px;height: 50px; margin-right: 10px;margin-top: 20px;" alt="User Image" />
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>
                                                         <%--//1.1 End of Changes; Bhaumik Patel - [CR-5307]--%>
                                                        <div class="row"></div>
                                                       <hr id="hrHidden" runat="server" class="form-control bg-aqua" style="height: 2px;" />
                                                        
                                                        <%--commented by shri on 16 oct 17 as per uw requirement, sagar joshi--%>
                                                        <%--pivc pending--%>
                                                        <div class="col-lg-2 col-xs-6" id="divPivcPen" runat="server">
                                                            <!-- small box -->
                                                            <div class="small-box bg-orange">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblPivcPendingCount" runat="server" Text="0"></asp:Label></h3>
                                                                    <p>
                                                                        <asp:LinkButton ID="lblPivcPending" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server" CssClass="lnkButton">PIVC Pending</asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                                <%--<div class="icon">
                                                                    <i class="ion ion-thumbsup"></i>
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>
                                                        <%--end here--%>
                                                        <%--pivc & relalization complete--%>
                                                        <div class="col-lg-2 col-xs-6" id="divPivcRea" runat="server">
                                                            <!-- small box -->
                                                            <div class="small-box bg-aqua">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblPivcRealizationCompleteCount" runat="server" Text="0"></asp:Label></h3>

                                                                    <p>
                                                                        <asp:LinkButton ID="lblPivcRealizationComplete" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server" CssClass="lnkButton">Pivc & Realization Complete</asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                                <%--<div class="icon">
                                                                    <i class="ion ion-thumbsup"></i>
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>

                                                        <%--pivc complete & realization pending --%>
                                                        <div class="col-lg-2 col-xs-6" id="divPivc" runat="server">
                                                            <!-- small box -->
                                                            <div class="small-box bg-fuchsia">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblPivcCompleteRelizationPendingCount" runat="server" Text="0"></asp:Label></h3>

                                                                    <p>
                                                                        <asp:LinkButton ID="lblPivcCompleteRelizationPending" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server" CssClass="lnkButton">Pivc Complete Realization Pending</asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                                <%--<div class="icon">
                                                                    <i class="ion ion-thumbsup"></i>
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>

                                                        <%--CASES PENDING IN FG--%>
                                                        <div class="col-lg-2 col-xs-6" style="display:none;">
                                                            <!-- small box -->
                                                            <div class="small-box bg-aqua">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblFGCasesCount" runat="server" Text="0"></asp:Label></h3>

                                                                    <p>
                                                                        <asp:LinkButton ID="lblFGCases" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server" CssClass="lnkButton">FG Cases</asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                                <%-- <div class="icon">
                                                                    <i class="ion ion-thumbsup"></i>
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>

                                                        <%--counter offer--%>
                                                        <div class="col-lg-2 col-xs-6" id="divCounter" runat="server">
                                                            <!-- small box -->
                                                            <div class="small-box bg-aqua">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblCounterOfferCount" runat="server" Text="0"></asp:Label></h3>

                                                                    <p>
                                                                        <asp:LinkButton ID="lblCounterOffer" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server" CssClass="lnkButton">Counter Offer</asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                                <%-- <div class="icon">
                                                                    <i class="ion ion-thumbsup"></i>
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>

                                                        <%--tele conversation--%>
                                                        <div class="col-lg-2 col-xs-6" id="divTele" runat="server">
                                                            <!-- small box -->
                                                            <div class="small-box bg-aqua">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblTeleConversationCount" runat="server" Text="0"></asp:Label></h3>

                                                                    <p>
                                                                        <asp:LinkButton ID="lblTeleConversation" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server" CssClass="lnkButton">Tele Conversation</asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                                <%-- <div class="icon">
                                                                    <i class="ion ion-thumbsup"></i>
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>

                                                        <%--my wip--%>
                                                        <div class="col-lg-2 col-xs-6">
                                                            <!-- small box -->
                                                            <div class="small-box bg-aqua" id="divWIP" runat="server">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblMyWIPCount" runat="server" Text="0"></asp:Label></h3>

                                                                    <p>
                                                                        <asp:LinkButton ID="lblMyWIP" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server" CssClass="lnkButton">My WIP</asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                                <%-- <div class="icon">
                                                                    <i class="ion ion-thumbsup"></i>
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>
                                        <%--Change done by Royson CR-10 4783--%>
                                                         <%--CMO--%>
                                            <div class="col-lg-2 col-xs-6" id="divCMO" runat="server" style="margin-left: -170px !important;">
                                                            <!-- small box -->
                                                            <div class="small-box bg-red">
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblcmo" runat="server" Text="0"></asp:Label></h3>

                                                                    <p>
                                                                        <asp:LinkButton ID="lnkcmo" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server" CssClass="lnkButton">CMO</asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                                <%-- <div class="icon">
                                                                    <i class="ion ion-person-stalker"></i>
                                                                </div>--%>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>
                                                       <%--End--%>
                                                    </div>
                                                    <!-- /.row -->
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Timer runat="server" ID="tmrDashboard" Interval="60000" OnTick="tmrDashboard_Tick"></asp:Timer>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <!-- /.box-body -->
                                <asp:UpdatePanel runat="server" ID="upDashboardDetails" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="panel box box-success">
                                            <div class="box-header with-border">
                                                <h4 class="box-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">Inbox</a>
                                                </h4>
                                            </div>
                                            <div id="collapseTwo" class="panel-collapse collapse in">
                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <div class="box-body">
                                                            <div class="box">
                                                                <div class="box-header">
                                                                    <h3 class="box-title">Application Details</h3>
                                                                </div>
                                                                <!-- /.box-header -->
                                                                <div class="box-body table-responsive">
                                                                    <%--<asp:UpdatePanel runat="server">--%>
                                                                    <%--<ContentTemplate>--%>
                                                                    <asp:Label Text="" runat="server" ID="lblfocus" />
                                                                    <asp:Repeater ID="gvAppdasbord" runat="server" OnItemDataBound="gvAppdasbord_ItemDataBound" OnItemCommand="gvAppdasbord_ItemCommand" EnableViewState="true">
                                                                        
                                                                      <HeaderTemplate>
                                                                            <table id="example1" style="overflow: auto; width: 100%;" class="table-bordered table-striped">
                                                                                 <thead>
                                                                                    <tr>
                                                                                        <%--23NOV2017--%>
                                                                                        <th>Application Number</th>
                                                                                        <th>Priority</th>
                                                                                        <th>Policy Number</th>
                                                                                        <th>Customer Name</th>
                                                                                        <%--<th>Product Code</th>
                                                                                        <th>Product Name</th>--%>
                                                                                        <th>Sum Assured</th>
                                                                                        <%--<th>Base Premium</th>--%>
                                                                                        <th>APE</th>
                                                                                        <th>Channel Name</th>
                                                                                        <%--<th>UTM Source</th>--%>
                                                                                        <%--<th>Date Of Booking</th>--%>
                                                                                        <th>ENY</th>
                                                                                        <th>Risk Score</th>
                                                                                        <th>CurruentStage</th>
                                                                                        <th>User Id</th>
                                                                                        <th class="sorting_desc">Is Channel Green</th>
                                                                                        <th>Medical/Non-Medical</th>
                                                                                        <th>Assign Date</th>
                                                                                        
                                                                                        <th>Ageing</th>
                                                                                        
                                                                                        <th style="display:none;">Agent</th>
                                                                                        <th style="display:none;">MDRT</th>

                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                              <tr   id="itemrow" runat="server">
                                                                                <td>
                                                                                    <asp:LinkButton ID="lnkAppno" OnClientClick="SetTarget()" CommandArgument='<%# Bind("ApplicationNumber") %>' CommandName="Select" Text='<%# Bind("ApplicationNumber") %>' runat="server">LinkButton</asp:LinkButton>
                                                                                    <%--<asp:HiddenField ID="hdnApplicationno" runat="server" Value='<%# Eval("ApplicationNumber") %>' />
                                                                            <asp:Literal runat="server" ID="lblapp" Visible="false" Text='<%# Eval("ApplicationNumber") %>'></asp:Literal>--%>
                                                                                </td>
                                                                                  <td>
                                                                                    <asp:Label ID="lblPriority" Text='<%# Bind("Prioritys")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblpolno" Text='<%# Bind("PolicyNo")%>' runat="server"></asp:Label>
                                                                                    <asp:HiddenField ID="hdnPolicyNo" runat="server" Value='<%# Eval("PolicyNo") %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblCustname" runat="server" Text='<%# Bind("CustomerName")%>'></asp:Label>
                                                                                    <asp:HiddenField ID="hdnProductName" runat="server" Value='<%# Eval("ProductName") %>' />
                                                                                </td>
                                                                                <%--<td>
                                                                                    <asp:Label ID="Label3" Text='<%# Bind("ProductCode")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label1" Text='<%# Bind("ProductName")%>' runat="server"></asp:Label>                                                                                    
                                                                                </td>--%>
                                                                                <td>
                                                                                    <asp:Label ID="lblSumassured" Text='<%# Bind("SumAssured")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                   <%-- <asp:Label ID="lblBasePremium" Text='<%# Bind("BasePremium")%>' runat="server"></asp:Label>--%>
                                                                                    <asp:Label ID="lblAPE" Text='<%# Bind("APE")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblUtmchannel" Text='<%# Bind("ChannelName")%>' runat="server"></asp:Label>
                                                                                    <asp:HiddenField ID="hdnChannelName" runat="server" Value='<%# Eval("ChannelName") %>' />
                                                                                </td>
                                                                                <%--<td>
                                                                                    <asp:Label ID="lblDateOfBooking" runat="server" Text='<%# Bind("DateOfBooking")%>'></asp:Label>
                                                                                </td>--%>
                                                                                <td>
                                                                                    <asp:Label ID="lblENY" runat="server" Text='<%# Bind("ENY")%>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label1" Text='<%# Bind("IsRiskExist")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label5" Text='<%# Bind("CurruntStage")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblAssignedUserId" runat="server" Text='<%# Bind("AssignedUserId")%>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblIsChannelGreen" runat="server" Text='<%# Bind("IsChannelGreen")%>'></asp:Label>
                                                                                </td>
                                                                                  <td>
                                                                                    <asp:Label ID="lblMedical_NonMedical" runat="server" Text='<%# Bind("Medical_NonMedical")%>'></asp:Label>
                                                                                </td>
                                                                                 <td>
                                                                                    <asp:Label ID="lblAssignDate" runat="server" Text='<%# Bind("AssignDate")%>'></asp:Label>
                                                                                </td>
                                                                                  <td>
                                                                                    <asp:Label ID="Label4" Text='<%# Bind("Ageing")%>' runat="server"></asp:Label>
                                                                               </td>
                                                                                  
                                                                                <td>
                                                                                    <asp:Label ID="lblmdragent" style="display:none;" Text='<%# Bind("AGENT")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                   <%--//4.1 Begin of Changes ; Bhaumik Patel [CR-8222]--%>
                                                                                <td>
                                                                                    <asp:Label ID="lblMDRT" style="display:none;" Text='<%# Bind("MDRT")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                   <%--//4.1 End of Changes ; Bhaumik Patel [CR-8222]--%>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            </tbody>
                                        <tfoot>
                                            <tr>
                                                <th>Application Number</th>
                                                <th>Priority</th>
                                                <th>Policy Number</th>
                                                <th>Customer Name</th>
                                                <%--<th>Product Code</th>
                                                <th>Product Name</th>--%>
                                                <th>Sum Assured</th>
                                                <%--<th>Base Premium</th>--%>
                                                <th>APE</th>
                                                <th>Channel Name</th>
                                                <%--<th>UTM Source</th>--%>
                                                <%--<th>Date Of Booking</th>--%>
                                               <th>ENY</th>
                                                <th>Risk Score</th>
                                                <th>CurruentStage</th>
                                                <th>User Id</th>
                                                <th class="sorting_desc">Is Channel Green</th>
                                                <th>Medical/Non-Medical</th>
                                                <th>Assign Date</th>
                                                 <th>Ageing</th>
                                                                                                
                                                <th style="display:none;">Agent</th>
                                                <th style="display:none;">MDRT</th>
                                                
                                            </tr>
                                        </tfoot>
                                                                            </table>
                                                                        </FooterTemplate>

                                                                    </asp:Repeater>

                                                                    <%--Added by suraj--%>
                                                                     <asp:Repeater ID="gvAppdasbord_CMO" runat="server" OnItemDataBound="gvAppdasbord_CMO_ItemDataBound" OnItemCommand="gvAppdasbord_CMO_ItemCommand" EnableViewState="true">
                                                                        
                                                                      <HeaderTemplate>
                                                                            <table id="example1" style="overflow: auto; width: 100%;" class="table-bordered table-striped">
                                                                                 <thead>
                                                                                    <tr>
                                                                                        <%--23NOV2017--%>
                                                                                        <th>Application Number</th>
                                                                                        <th>Policy Number</th>
                                                                                        <th>Customer Name</th>
                                                                                        <%--<th>Product Code</th>
                                                                                        <th>Product Name</th>--%>
                                                                                        <th>Sum Assured</th>
                                                                                        <%--<th>Base Premium</th>--%>
                                                                                        <th>APE</th>
                                                                                        <th>Channel Name</th>
                                                                                        <%--<th>UTM Source</th>--%>
                                                                                        <%--<th>Date Of Booking</th>--%>
                                                                                        <th>ENY</th>
                                                                                        <th>Risk Score</th>
                                                                                        <th>CurruentStage</th>
                                                                                        <th>User Id</th>
                                                                                        <th class="sorting_desc">Is Channel Green</th>
                                                                                        <th>Medical/Non-Medical</th>
                                                                                        <th>Assign Date</th>
                                                                                        <th>Ageing</th>
                                                                                        <th>Priority</th>
                                                                                        <th style="display:none;">Agent</th>

                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                              <tr>
                                                                                <td>
                                                                                    <asp:LinkButton ID="lnkAppno" OnClientClick="SetTarget()" CommandArgument='<%# Bind("ApplicationNumber") %>' CommandName="Select" Text='<%# Bind("ApplicationNumber") %>' runat="server">LinkButton</asp:LinkButton>
                                                                                    <%--<asp:HiddenField ID="hdnApplicationno" runat="server" Value='<%# Eval("ApplicationNumber") %>' />
                                                                            <asp:Literal runat="server" ID="lblapp" Visible="false" Text='<%# Eval("ApplicationNumber") %>'></asp:Literal>--%>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblpolno" Text='<%# Bind("PolicyNo")%>' runat="server"></asp:Label>
                                                                                    <asp:HiddenField ID="hdnPolicyNo" runat="server" Value='<%# Eval("PolicyNo") %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblCustname" runat="server" Text='<%# Bind("CustomerName")%>'></asp:Label>
                                                                                    <asp:HiddenField ID="hdnProductName" runat="server" Value='<%# Eval("ProductName") %>' />
                                                                                </td>
                                                                                <%--<td>
                                                                                    <asp:Label ID="Label3" Text='<%# Bind("ProductCode")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label1" Text='<%# Bind("ProductName")%>' runat="server"></asp:Label>                                                                                    
                                                                                </td>--%>
                                                                                <td>
                                                                                    <asp:Label ID="lblSumassured" Text='<%# Bind("SumAssured")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                   <%-- <asp:Label ID="lblBasePremium" Text='<%# Bind("BasePremium")%>' runat="server"></asp:Label>--%>
                                                                                    <asp:Label ID="lblAPE" Text='<%# Bind("APE")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblUtmchannel" Text='<%# Bind("ChannelName")%>' runat="server"></asp:Label>
                                                                                    <asp:HiddenField ID="hdnChannelName" runat="server" Value='<%# Eval("ChannelName") %>' />
                                                                                </td>
                                                                                <%--<td>
                                                                                    <asp:Label ID="lblDateOfBooking" runat="server" Text='<%# Bind("DateOfBooking")%>'></asp:Label>
                                                                                </td>--%>
                                                                                <td>
                                                                                    <asp:Label ID="lblENY" runat="server" Text='<%# Bind("ENY")%>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label1" Text='<%# Bind("IsRiskExist")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label5" Text='<%# Bind("CurruntStage")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblAssignedUserId" runat="server" Text='<%# Bind("AssignedUserId")%>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblIsChannelGreen" runat="server" Text='<%# Bind("IsChannelGreen")%>'></asp:Label>
                                                                                </td>
                                                                                  <td>
                                                                                    <asp:Label ID="lblMedical_NonMedical" runat="server" Text='<%# Bind("Medical_NonMedical")%>'></asp:Label>
                                                                                </td>
                                                                                 <td>
                                                                                    <asp:Label ID="lblAssignDate" runat="server" Text='<%# Bind("AssignDate")%>'></asp:Label>
                                                                                </td>
                                                                                  <td>
                                                                                    <asp:Label ID="Label4" Text='<%# Bind("Ageing")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                    <td>
                                                                                    <asp:Label ID="lblPriority1" Text='<%# Bind("Prioritys")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                  <td style="display:none;">
                                                                                    <asp:Label ID="lblmdragent" style="display:none;" Text='<%# Bind("AGENT")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            </tbody>
                                        <tfoot>
                                            <tr>
                                                <th>Application Number</th>
                                                <th>Policy Number</th>
                                                <th>Customer Name</th>
                                                <%--<th>Product Code</th>
                                                <th>Product Name</th>--%>
                                                <th>Sum Assured</th>
                                                <%--<th>Base Premium</th>--%>
                                                <th>APE</th>
                                                <th>Channel Name</th>
                                                <%--<th>UTM Source</th>--%>
                                                <%--<th>Date Of Booking</th>--%>
                                               <th>ENY</th>
                                                <th>Risk Score</th>
                                                <th>CurruentStage</th>
                                                <th>User Id</th>
                                                <th class="sorting_desc">Is Channel Green</th>
                                                <th>Medical/Non-Medical</th>
                                                <th>Assign Date</th>
                                                <th>Ageing</th>
                                                 <th>Priority</th>
                                                <th style="display:none;">Agent</th>
                                                
                                            </tr>
                                        </tfoot>
                                                                            </table>
                                                                        </FooterTemplate>

                                                                    </asp:Repeater>
                                                                    <%--</ContentTemplate>
                                                            </asp:UpdatePanel>--%>

																	<asp:Repeater ID="gvAppdasbord_Revival" runat="server" OnItemDataBound="gvAppdasbord_Revival_ItemDataBound" OnItemCommand="gvAppdasbord_Revival_ItemCommand"  EnableViewState="true">
                                                                        
                                                                      <HeaderTemplate>
                                                                            <table id="example1" style="overflow: auto; width: 100%;" class="table-bordered table-striped">
                                                                                 <thead>
                                                                                    <tr>
                                                                                        <th>Application Number</th>
                                                                                        <th>Policy Number</th>
                                                                                        <th>Customer Name</th>
                                                                                        <th>Sum Assured</th>
                                                                                        <th>APE</th>
                                                                                        <th>ENY</th>
                                                                                        <th>Risk Score</th>
                                                                                        <th>CurruentStage</th>
                                                                                        <th>User Id</th>
                                                                                        <th class="sorting_desc">Is Channel Green</th>
                                                                                        <th>Medical/Non-Medical</th>
                                                                                        <th>Assign Date</th>
                                                                                        <th>Ageing</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                              <tr>
                                                                                <td>
                                                                                    <asp:LinkButton ID="lnkAppno" OnClientClick="SetTarget()" CommandArgument='<%# Bind("ApplicationNumber") %>' CommandName="Select" Text='<%# Bind("ApplicationNumber") %>' runat="server">LinkButton</asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblpolno" Text='<%# Bind("PolicyNo")%>' runat="server"></asp:Label>
                                                                                    <asp:HiddenField ID="hdnPolicyNo" runat="server" Value='<%# Eval("PolicyNo") %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblCustname" runat="server" Text='<%# Bind("CustomerName")%>'></asp:Label>
                                                                                    <asp:HiddenField ID="hdnProductName" runat="server" Value='<%# Eval("ProductName") %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblSumassured" Text='<%# Bind("SumAssured")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblAPE" Text='<%# Bind("APE")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblENY" runat="server" Text='<%# Bind("ENY")%>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label1" Text='<%# Bind("IsRiskExist")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label5" Text='<%# Bind("CurruntStage")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblAssignedUserId" runat="server" Text='<%# Bind("AssignedUserId")%>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblIsChannelGreen" runat="server" Text='<%# Bind("IsChannelGreen")%>'></asp:Label>
                                                                                </td>
                                                                                  <td>
                                                                                    <asp:Label ID="lblMedical_NonMedical" runat="server" Text='<%# Bind("Medical_NonMedical")%>'></asp:Label>
                                                                                </td>
                                                                                 <td>
                                                                                    <asp:Label ID="lblAssignDate" runat="server" Text='<%# Bind("AssignDate")%>'></asp:Label>
                                                                                </td>
                                                                                  <td>
                                                                                    <asp:Label ID="Label4" Text='<%# Bind("Ageing")%>' runat="server"></asp:Label>
                                                                                </td>
                                                                                
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            </tbody>
                                        <tfoot>
                                            <tr>
                                                <th>Application Number</th>
                                                <th>Policy Number</th>
                                                <th>Customer Name</th>
                                                <th>Sum Assured</th>
                                                <th>APE</th>
                                                <th>ENY</th>
                                                <th>Risk Score</th>
                                                <th>CurruentStage</th>
                                                <th>User Id</th>
                                                <th class="sorting_desc">Is Channel Green</th>
                                                <th>Medical/Non-Medical</th>
                                                <th>Assign Date</th>
                                                <th>Ageing</th>
                                            </tr>
                                        </tfoot>
                                                                            </table>
                                                                        </FooterTemplate>
                                                                    </asp:Repeater>
																	<%--End-Added By Shyam Patil-11-02-2020--%>

                                                                    <asp:Label ID="lbldashborderror" CssClass="label-danger" runat="server" Text="Label"></asp:Label>

                                                                </div>
                                                                <!-- /.box-body -->
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="gvAppdasbord" />
										<%--########################## Added by Shyam Starts ###########################################--%>
										<asp:PostBackTrigger ControlID="gvAppdasbord_Revival" />
										<%--########################## Added by Shyam End ###########################################--%>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>


                </section>
            </div>
            <%--added by shri to show popup for AFI CFI and UW--%>
            <!-- Modal -->
            <div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header box-header with-border text-center bg-title">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <b>
                                <h4 class="modal-title success"><span id="ModalCommon"></span></h4>
                            </b>
                        </div>
                        <div class="modal-body well">
                            <AfiCfiWU:PopupAfiCfiUW runat="server" ID="PopUpAfiCfiUW" />
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.content -->
            <div class="modal fade" id="divPendingDocPopup" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header box-header with-border text-center bg-title">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title success"><b><span id="ModalCommon1">Pending Documents</span></b></h4>
                        </div>
                        <div class="">
                            <Docs:PendingDocs runat="server" ID="PendingDocs" />
                        </div>

                    </div>
                </div>
            </div>
            <%--Bulk Decision popup--%>
            <div class="modal fade" id="divBulkPopup" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header box-header with-border text-center bg-title">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title success"><b><span id="ModalCommon2">Decision</span></b></h4>
                        </div>
                        <div class="">
                            <Bulk:BulkDec runat="server" ID="BulkDecision" />
                        </div>

                    </div>
                </div>
            </div>

            <%--added by shri for manual allocation--%>
            <div class="modal fade" id="divManualAllocation" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header box-header with-border text-center bg-title">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title success"><b><span id="Span1">Manual Allocation</span></b></h4>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel runat="server" ID="upManualAllocation">
                                <ContentTemplate>
                                    <ManualAlloc:ManualAllocation runat="server" ID="ManualAllocation" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                    </div>
                </div>
            </div>

            <%--Upload Questions and Answer Excel file Popup added by rohit shirke--%>

            <div class="modal fade" id="divQueFileUpload" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header box-header with-border text-center bg-title">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title success"><b><span id="Span2">Exams Questions</span></b></h4>
                        </div>
                        <div class="">
                            <AfiCfiWU:UWExamQue runat="server" ID="UWExamQue" />
                        </div>

                    </div>
                </div>
            </div>


            <%--end here--%>
            <div>
                <asp:HiddenField ID="hdnMenuoption" runat="server" />
                <asp:LinkButton ID="lnkCommondashbord" runat="server" Text="Getdashbord Details" CssClass="hidden" OnClick="lnkCommondashbord_Click">Getdashbord Details</asp:LinkButton>
            </div>
            <!-- /.content-wrapper -->

            <div class="modal fade modal-info" id="myModalNoDocPending" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">Info Modal</h4>
                        </div>
                        <div class="modal-body">
                            <h4>
                                <asp:Label ID="Label2" runat="server" Text="No documents are pending for the selected case"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                            <%--<asp:Label ID="Label14" class="form-control lblLable" runat="server" Text="State Bank Of India"></asp:Label>--%>
                        </div>
                    </div>
                </div>
            </div>


            <!-- Main Footer -->
            <footer class="main-footer">
                <!-- To the right -->
                <div class="pull-right hidden-xs">
                    Developed by FG-IT Life Team
                </div>
                <!-- Default to the left -->
                <strong>Copyright &copy; 2016 <a href="#">Future Generali India Life Insurance Company Limited</a>.</strong> All rights reserved.
            </footer>

            <!-- Control Sidebar -->
            <aside class="control-sidebar control-sidebar-dark">
                <!-- Create the tabs -->
                <ul class="nav nav-tabs nav-justified control-sidebar-tabs">
                    <li class="active"><a href="#control-sidebar-home-tab" data-toggle="tab"><i class="fa fa-home"></i></a></li>
                    <li><a href="#control-sidebar-settings-tab" data-toggle="tab"><i class="fa fa-gears"></i></a></li>
                </ul>
                <!-- Tab panes -->
                <div class="tab-content">
                    <!-- Home tab content -->
                    <div class="tab-pane active" id="control-sidebar-home-tab">
                        <h3 class="control-sidebar-heading">Recent Activity</h3>
                        <ul class="control-sidebar-menu">
                            <li>
                                <a href="javascript::;">
                                    <i class="menu-icon fa fa-birthday-cake bg-red"></i>

                                    <div class="menu-info">
                                        <h4 class="control-sidebar-subheading">Langdon's Birthday</h4>

                                        <p>Will be 23 on April 24th</p>
                                    </div>
                                </a>
                            </li>
                        </ul>
                        <!-- /.control-sidebar-menu -->

                        <h3 class="control-sidebar-heading">Tasks Progress</h3>
                        <ul class="control-sidebar-menu">
                            <li>
                                <a href="javascript::;">
                                    <h4 class="control-sidebar-subheading">Custom Template Design
                <span class="pull-right-container">
                    <span class="label label-danger pull-right">70%</span>
                </span>
                                    </h4>

                                    <div class="progress progress-xxs">
                                        <div class="progress-bar progress-bar-danger" style="width: 70%"></div>
                                    </div>
                                </a>
                            </li>
                        </ul>
                        <!-- /.control-sidebar-menu -->

                    </div>
                    <!-- /.tab-pane -->
                    <!-- Stats tab content -->
                    <div class="tab-pane" id="control-sidebar-stats-tab">Stats Tab Content</div>
                    <!-- /.tab-pane -->
                    <!-- Settings tab content -->
                    <div class="tab-pane" id="control-sidebar-settings-tab">
                        <form method="post">
                            <h3 class="control-sidebar-heading">General Settings</h3>

                            <div class="form-group">
                                <label class="control-sidebar-subheading">
                                    Report panel usage
                            <input type="checkbox" class="pull-right" checked="checked" />
                                </label>

                                <p>
                                    Some information about this general settings option
                                </p>
                            </div>
                            <!-- /.form-group -->
                        </form>
                    </div>
                    <asp:HiddenField ID="hdnUWoption" runat="server" />
                    <!-- /.tab-pane -->
                </div>
            </aside>
            <!-- /.control-sidebar -->
            <!-- Add the sidebar's background. This div must be placed
       immediately after the control sidebar -->
            <div class="control-sidebar-bg"></div>
        </div>
        <!-- ./wrapper -->

        <!-- REQUIRED JS SCRIPTS -->
        <div class="retPoup">
        <div class="modal fade modal-info" id="myModal11" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document" style="width:auto!important;padding-left:20%!important">
                <div class="modal-content" style="height:auto!important">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="hideloading();">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Info Modal</h4>
                    </div>
                    <div class="modal-body">
                        <h4>
                            <asp:Label ID="lblerrorinfo1" runat="server" Text=""></asp:Label>
                        </h4>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="hideloading();">Close</button>
                        <%--<asp:Label ID="Label14" class="form-control lblLable" runat="server" Text="State Bank Of India"></asp:Label>--%>
                    </div>
                </div>
            </div>
        </div>
        <%--Added by Suraj For OFAC--%>
        <div id="mdlofac" class="modal fade modal-info" aria-labelledby="myModalLabel" role="dialog" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="hideloading();">&times;</button>
                        <h4 class="modal-title">
                            <span id="spnTitle"></span>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <h4>
                            <asp:Label ID="lblerrorofac" runat="server" Text=""></asp:Label>
                        </h4>
                    </div>
                    <div class="modal-footer">
                        <%--<asp:Button runat="server" class="btn btn-primary" ID="btnYes" OnClick="btnYes_Click" Text="Yes"></asp:Button>--%>
                        <asp:Button runat="server" ID="btnclose" type="button" class="btn btn-primary" data-dismiss="modal" Text="Close"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <%--END--%>
    </div>
        <div class="modal fade modal-info" id="myModal1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Info Modal</h4>
                    </div>
                    <div class="modal-body">
                        <h4>
                            <asp:Label ID="lblErrorinfo" runat="server" Text="The case is already locked by other user"></asp:Label>
                        </h4>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                        <%--<asp:Label ID="Label14" class="form-control lblLable" runat="server" Text="State Bank Of India"></asp:Label>--%>
                    </div>
                </div>
            </div>
        </div>
        <!-- Bootstrap 3.3.6 -->
        <script src="bootstrap/js/bootstrap.min.js"></script>
        <!-- AdminLTE App -->
        <script src="dist/js/app.min.js"></script>

        <!-- DataTables -->
        <script src="plugins/datatables/jquery.dataTables.min.js"></script>
        <script src="plugins/datatables/dataTables.bootstrap.min.js"></script>
        <!-- SlimScroll -->
        <script src="plugins/slimScroll/jquery.slimscroll.min.js"></script>
        <!-- FastClick -->
        <script src="plugins/fastclick/fastclick.js"></script>

        <!-- AdminLTE for demo purposes -->
        <script src="dist/js/demo.js"></script>
        <!-- page script -->
        <script>
            $(function () {
                //$("#example1").DataTable();

                //$('#example1').DataTable({
                //    "order": [[9, "desc"]]
                //});

                /*$('#example2').DataTable({
                    "paging": true,
                    "lengthChange": false,
                    "searching": false,
                    "ordering": true,
                    "info": true,
                    "autoWidth": false
                });*/
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {

                //$("#example1").DataTable();
                //$('#example1').DataTable({
                //    "order": [[9, "desc"]]
                //});


                function SetTarget() {
                    document.forms[0].target = "_blank";
                }

                /*added by shri on 13 june 17 to open on same page*/
                function SetTargetToSelf() {
                    document.forms[0].target = "_self";
                }
                /*added by shri on 30 oct to open policy enquiery page*/
                //function fnPolicyEnquiery() {
                //    window.open('10.1.41.221/polenq/search.aspx', _blank)
                //}
            });
        </script>
        <!-- Optionally, you can add Slimscroll and FastClick plugins.
     Both of these plugins are recommended to enhance the
     user experience. Slimscroll is required when using the
     fixed layout. -->
    </form>
</body>
</html>
