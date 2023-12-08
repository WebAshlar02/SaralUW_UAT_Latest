<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/uc_Updateprogress.ascx" TagPrefix="uc1" TagName="uc_Updateprogress" %>
<%@ Register Src="~/UserControl/PopupAfiCfiUW.ascx" TagPrefix="AfiCfiWU" TagName="PopupAfiCfiUW" %>
<%@ Register Src="~/UserControl/PopupDoc.ascx" TagPrefix="Docs" TagName="PendingDocs" %>
<%@ Register Src="~/UserControl/BulkDecision.ascx" TagPrefix="Bulk" TagName="BulkDec" %>
<%--added by shri on 08 june 17--%>

<%--end here--%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <uc1:uc_Updateprogress runat="server" ID="uc_Updateprogress" />
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


        $(window).load(function () {
            $('#ldrModal').fadeOut();
        });

        function SetTarget() {
            document.forms[0].target = "_blank";
        }

        /*added by shri on 13 june 17 to open on same page*/
        function SetTargetToSelf() {
            document.forms[0].target = "_self";
        }
        /*end here*/

        $(document).ready(function () {
            $("#lnkCommondashbord").click();
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
    <style>
        .bg-title {
            background-color: #a81d22;
            color: #fff;
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
    <form runat="server">
        <div id="ldrModal" runat="server" class="LoaderModal">
            <div class="center">
                <img alt="" src="./dist/img/loader4.gif" />
            </div>
        </div>
        <div class="wrapper">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                    <!-- Sidebar toggle button-->
                    <a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button">
                        <span class="sr-only">Toggle navigation</span>
                    </a>
                    <!-- Navbar Right Menu -->
                    <div class="navbar-custom-menu">
                        <ul class="nav navbar-nav">
                            <!-- Messages: style can be found in dropdown.less-->
                            <li class="dropdown messages-menu">
                                <!-- Menu toggle button -->
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                    <i class="fa fa-envelope-o"></i>
                                    <span class="label label-success">4</span>
                                </a>
                                <ul class="dropdown-menu">
                                    <li class="header">You have 4 messages</li>
                                    <li>
                                        <!-- inner menu: contains the messages -->
                                        <ul class="menu">
                                            <li>
                                                <!-- start message -->
                                                <a href="#">
                                                    <div class="pull-left">
                                                        <!-- User Image -->
                                                        <img src="dist/img/user2-160x160.jpg" class="img-circle" alt="User Image">
                                                    </div>
                                                    <!-- Message title and timestamp -->
                                                    <h4>Support Team
                        <small><i class="fa fa-clock-o"></i>5 mins</small>
                                                    </h4>
                                                    <!-- The message -->
                                                    <p>Why not buy a new awesome theme?</p>
                                                </a>
                                            </li>
                                            <!-- end message -->
                                        </ul>
                                        <!-- /.menu -->
                                    </li>
                                    <li class="footer"><a href="#">See All Messages</a></li>
                                </ul>
                            </li>
                            <!-- /.messages-menu -->

                            <!-- Notifications Menu -->
                            <li class="dropdown notifications-menu">
                                <!-- Menu toggle button -->
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                    <i class="fa fa-bell-o"></i>
                                    <span class="label label-warning">10</span>
                                </a>
                                <ul class="dropdown-menu">
                                    <li class="header">You have 10 notifications</li>
                                    <li>
                                        <!-- Inner Menu: contains the notifications -->
                                        <ul class="menu">
                                            <li>
                                                <!-- start notification -->
                                                <a href="#">
                                                    <i class="fa fa-users text-aqua"></i>5 new members joined today
                                                </a>
                                            </li>
                                            <!-- end notification -->
                                        </ul>
                                    </li>
                                    <li class="footer"><a href="#">View all</a></li>
                                </ul>
                            </li>
                            <!-- Tasks Menu -->
                            <li class="dropdown tasks-menu">
                                <!-- Menu Toggle Button -->
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                    <i class="fa fa-flag-o"></i>
                                    <span class="label label-danger">9</span>
                                </a>
                                <ul class="dropdown-menu">
                                    <li class="header">You have 9 tasks</li>
                                    <li>
                                        <!-- Inner menu: contains the tasks -->
                                        <ul class="menu">
                                            <li>
                                                <!-- Task item -->
                                                <a href="#">
                                                    <!-- Task title and progress text -->
                                                    <h3>Design some buttons<small class="pull-right">20%</small>
                                                    </h3>
                                                    <!-- The progress bar -->
                                                    <div class="progress xs">
                                                        <!-- Change the css width attribute to simulate progress -->
                                                        <div class="progress-bar progress-bar-aqua" style="width: 20%" role="progressbar" aria-valuenow="20" aria-valuemin="0" aria-valuemax="100">
                                                            <span class="sr-only">20% Complete</span>
                                                        </div>
                                                    </div>
                                                </a>
                                            </li>
                                            <!-- end task item -->
                                        </ul>
                                    </li>
                                    <li class="footer">
                                        <a href="#">View all tasks</a>
                                    </li>
                                </ul>
                            </li>
                            <!-- User Account Menu -->
                            <li class="dropdown user user-menu">
                                <!-- Menu Toggle Button -->
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                    <!-- The user image in the navbar-->
                                    <img src="dist/img/avatar5.png" class="user-image" alt="User Image">
                                    <!-- hidden-xs hides the username on small devices so only the image appears. -->
                                    <span id="spanusername" runat="server" class="hidden-xs"></span>
                                </a>
                                <ul class="dropdown-menu">
                                    <!-- The user image in the menu -->
                                    <li class="user-header">
                                        <img src="dist/img/avatar5.png" class="img-circle" alt="User Image">

                                        <%--<p>
                                            Dharmesh D Patel - UW<small>Member since Nov. 2012</small>
                                        </p>--%>
                                    </li>
                                    <!-- Menu Body -->
                                    <li class="user-body">
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
                                        <!-- /.row -->
                                    </li>
                                    <!-- Menu Footer-->
                                    <li class="user-footer">
                                        <div class="pull-left">
                                            <a href="#" class="btn btn-default btn-flat">Profile</a>
                                        </div>
                                        <div class="pull-right">
                                            <a href="#" class="btn btn-default btn-flat">Sign out</a>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                            <!-- Control Sidebar Toggle Button -->
                            <li>
                                <a href="#" data-toggle="control-sidebar"><i class="fa fa-gears"></i></a>
                            </li>
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
                            <a href="#"><i class="fa fa-circle text-success"></i>Online</a>
                        </div>
                    </div>

                    <!-- search form (Optional) -->
                    <form action="#" method="get" class="sidebar-form">
                        <div class="input-group">
                            <input type="text" name="q" class="form-control" placeholder="Search..." />
                            <span class="input-group-btn">
                                <button type="submit" name="search" id="search-btn" class="btn btn-flat">
                                    <i class="fa fa-search"></i>
                                </button>
                            </span>
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
                            <a href="#"><i class="fa fa-link"></i><span>Dashboard</span>
                                <span class="pull-right-container">
                                    <i class="fa fa-angle-left pull-right"></i>
                                </span>
                            </a>
                            <ul class="treeview-menu">
                                <li>
                                    <asp:LinkButton ID="lnkOnline" runat="server" Text="ONLINE" OnClientClick="SetTargetToSelf();" OnClick="lnkOnline_Click">Online</asp:LinkButton>
                                </li>
                                <%-- <li>
                                    <asp:LinkButton ID="lnkgreenchannel" runat="server" Text="green channel" OnClick="lnkgreenchannel_Click">green channel</asp:LinkButton>
                                </li>--%>
                                <li>
                                    <asp:LinkButton ID="lnkoffline" runat="server" Text="OFFLINE" OnClientClick="SetTargetToSelf();" OnClick="lnkoffline_Click">offline</asp:LinkButton>
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
                                <li><a href="#">Refer to UW</a></li>
                                <li><a href="#">Assign to CMO</a></li>
                                <li><a href="#">Add Requirement</a></li>
                                <li><a href="#" onclick="fnHideShowButton('afi');" data-toggle="modal" data-backdrop="static" data-keyboard="false" data-target="#myModal">AFI</a></li>
                                <li><a href="#" onclick="fnHideShowButton('cfi');" data-toggle="modal" data-backdrop="static" data-keyboard="false" data-target="#myModal">CFI</a></li>
                                <li><a href="#" onclick="fnHideShowButton('uw');" data-toggle="modal" data-backdrop="static" data-keyboard="false" data-target="#myModal">UW</a></li>
                                <li><a href="Appcode/Dedup.aspx" target="_blank">Dedupe</a></li>
                                <li><a href="#" data-toggle="modal" data-backdrop="static" data-keyboard="false" data-target="#divBulkPopup">Bulk Decision</a></li>
                            </ul>
                        </li>
                        <li class="treeview">
                            <a href="#"><i class="fa fa-link"></i><span>View</span>
                                <span class="pull-right-container">
                                    <i class="fa fa-angle-left pull-right"></i>
                                </span>
                            </a>
                            <ul class="treeview-menu">
                                <li><a href="#">Policy Enquiry</a></li>
                                <li><a href="#">Cases in pipeline</a></li>
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
                    <h1>Dashboard
                    <small>General</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                        <li class="active">General</li>
                    </ol>
                </section>

                <!-- Main content -->
                <section class="content">
                    <div class="box box-solid">
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="box-group" id="accordion">
                                <!-- we are adding the .panel class so bootstrap.js collapse plugin detects it -->
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
                                                <div class="col-lg-3 col-xs-6">
                                                    <!-- small box -->
                                                    <div class="small-box bg-aqua">
                                                        <div class="inner">
                                                            <h3>
                                                                <asp:Label ID="lblFreshcount" runat="server" Text="Label"></asp:Label>
                                                            </h3>
                                                            <p>
                                                                <asp:LinkButton ID="lnkFreshCase" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server">FRESH CASES</asp:LinkButton>
                                                            </p>
                                                        </div>
                                                        <div class="icon">
                                                            <i class="ion ion-person-add"></i>
                                                        </div>
                                                        <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                    </div>
                                                </div>
                                                <!-- ./col -->
                                                <div class="col-lg-3 col-xs-6">
                                                    <!-- small box -->
                                                    <div class="small-box bg-fuchsia">
                                                        <div class="inner">
                                                            <h3>
                                                                <asp:Label ID="lblUWRefercount" runat="server" Text="Label"></asp:Label></h3>
                                                            <p>
                                                                <asp:LinkButton ID="lnkUwrefer" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server">UWREFER CASES</asp:LinkButton>
                                                            </p>
                                                        </div>
                                                        <div class="icon">
                                                            <i class="ion ion-person-stalker"></i>
                                                        </div>
                                                        <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                    </div>
                                                </div>
                                                <!-- ./col -->
                                                <div class="col-lg-3 col-xs-6">
                                                    <!-- small box -->
                                                    <div class="small-box bg-fuchsia">
                                                        <div class="inner">
                                                            <h3>
                                                                <asp:Label ID="lblPendingcount" runat="server" Text="Label"></asp:Label></h3>

                                                            <p>
                                                                <asp:LinkButton ID="lnkPendingcases" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server">PENDING CASES</asp:LinkButton>
                                                            </p>
                                                        </div>
                                                        <div class="icon">
                                                            <i class="ion ion-person-stalker"></i>
                                                        </div>
                                                        <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                    </div>
                                                </div>
                                                <!-- ./col -->
                                                <div class="col-lg-3 col-xs-6">
                                                    <!-- small box -->
                                                    <div class="small-box bg-purple">
                                                        <div class="inner">
                                                            <h3>
                                                                <asp:Label ID="lblPendingVerificationcount" runat="server" Text="Label"></asp:Label></h3>

                                                            <p>
                                                                <asp:LinkButton ID="lnkPendingVerification" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server">Verification Pending</asp:LinkButton>

                                                            </p>
                                                        </div>
                                                        <div class="icon">
                                                            <i class="ion ion-ios-medkit"></i>
                                                        </div>
                                                        <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                    </div>
                                                </div>
                                                <!-- ./col -->
                                                <div class="col-lg-3 col-xs-6">
                                                    <!-- small box -->
                                                    <div class="small-box bg-orange">
                                                        <div class="inner">
                                                            <h3>
                                                                <asp:Label ID="lblDocVerifiedcount" runat="server" Text="Label"></asp:Label></h3>

                                                            <p>
                                                                <asp:LinkButton ID="lnkDocVerified" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server">Documents Verified</asp:LinkButton>
                                                            </p>
                                                        </div>
                                                        <div class="icon">
                                                            <i class="ion ion-document-text"></i>
                                                        </div>
                                                        <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                    </div>
                                                </div>
                                                <!-- ./col -->
                                                <div class="col-lg-3 col-xs-6">
                                                    <!-- small box -->
                                                    <div class="small-box bg-yellow">
                                                        <div class="inner">
                                                            <h3>
                                                                <asp:Label ID="lblDocRealizedCount" runat="server" Text="Label"></asp:Label></h3>
                                                            <p>
                                                                <asp:LinkButton ID="lnkDocRealized" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server">Payment Realized</asp:LinkButton>
                                                            </p>
                                                        </div>
                                                        <div class="icon">
                                                            <i class="ion ion-android-call"></i>
                                                        </div>
                                                        <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                    </div>
                                                </div>
                                                <!-- ./col -->
                                                <div class="col-lg-3 col-xs-6">
                                                    <!-- small box -->
                                                    <div class="small-box bg-green">
                                                        <div class="inner">
                                                            <h3>
                                                                <asp:Label ID="lblReadytoissuecount" runat="server" Text="Label"></asp:Label></h3>

                                                            <p>
                                                                <asp:LinkButton ID="lnkReadyToIssue" OnClientClick="SetTargetToSelf();" OnClick="lnkCommondashbord_Click" runat="server">Ready To Issue</asp:LinkButton>
                                                            </p>
                                                        </div>
                                                        <div class="icon">
                                                            <i class="ion ion-thumbsup"></i>
                                                        </div>
                                                        <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- /.row -->
                                        </div>
                                    </div>
                                </div>
                                <div class="panel box box-success">
                                    <div class="box-header with-border">
                                        <h4 class="box-title">
                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">Inbox</a>
                                        </h4>
                                    </div>
                                    <div id="collapseTwo" class="panel-collapse collapse">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="box-body">
                                                    <div class="box">
                                                        <div class="box-header">
                                                            <h3 class="box-title">Application Details</h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <%--<asp:UpdatePanel runat="server">--%>
                                                            <%--<ContentTemplate>--%>
                                                            <asp:Repeater ID="gvAppdasbord" runat="server" OnItemCommand="gvAppdasbord_ItemCommand" EnableViewState="true">
                                                                <HeaderTemplate>
                                                                    <table id="example1" class="table table-bordered table-striped">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>Application Number</th>
                                                                                <th>Policy Number</th>
                                                                                <th>Customer Name</th>
                                                                                <th>Product Name</th>
                                                                                <th>Is Comby Plan</th>
                                                                                <th>Total Docs</th>
                                                                                <th>Docs Uploded</th>
                                                                                <th>Docs Pending</th>
                                                                                <th>Docs Remainig</th>
                                                                                <th>Sum Assured</th>
                                                                                <th>Base Premium</th>
                                                                                <th>Channel Name</th>
                                                                                <th>UTM Source</th>
                                                                                <th>Date Of Booking</th>

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
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblProductName" Text='<%# Bind("ProductName")%>' runat="server"></asp:Label>
                                                                            <asp:HiddenField ID="hdnProductName" runat="server" Value='<%# Eval("ProductName") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblIsCombyPlan" Text='<%# Bind("IsCombyPlan")%>' runat="server"></asp:Label>
                                                                            <asp:HiddenField ID="hdnIsCombyPlan" runat="server" Value='<%# Eval("IsCombyPlan") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblTotalDocs" Text='<%# Bind("TotalDocs")%>' runat="server"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbldocsuploaded" Text='<%# Bind("DocsUploded")%>' runat="server"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <%-- <asp:Label ID="lblPendingDocs" Text='<%# Bind("PendingDocs")%>' runat="server">
                                                                                <a href="#" data-toggle="modal" data-backdrop="static" data-keyboard="false" data-target="#divPendingDocPopup"></a>
                                                                            </asp:Label>--%>
                                                                            <asp:LinkButton ID="lnkPendingDocs" runat="server" OnClientClick="SetTargetToSelf()" CommandArgument='<%# Bind("PendingDocs") %>' CommandName="PendingDocs" Text='<%# Bind("PendingDocs") %>'></asp:LinkButton>
                                                                            <%--<asp:HiddenField ID="hdbAppNo" runat="server" Value='<%# Eval("ApplicationNumber") %>' />--%>
                                                                            <%--data-toggle="modal" data-backdrop="static" data-keyboard="false" data-target="#divPendingDocPopup"--%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblDocsRemainig" Text='<%# Bind("DocsRemainig")%>' runat="server"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblSumassured" Text='<%# Bind("SumAssured")%>' runat="server"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblBasePremium" Text='<%# Bind("BasePremium")%>' runat="server"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblUtmchannel" Text='<%# Bind("ChannelName")%>' runat="server"></asp:Label>
                                                                            <asp:HiddenField ID="hdnChannelName" runat="server" Value='<%# Eval("ChannelName") %>' />

                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblUtmsource" runat="server" Text='<%# Bind("UTMSource")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblDateOfBooking" runat="server" Text='<%# Bind("DateOfBooking")%>'></asp:Label>
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
                                                <th>Product Name</th>
                                                 <th>Is Comby Plan</th>
                                                <th>Total Docs</th>
                                                <th>Docs Uploded</th>
                                                <th>Docs Pending</th>
                                                <th>Docs Remainig</th>
                                                <th>Sum Assured</th>
                                                <th>Base Premium</th>
                                                <th>Channel Name</th>
                                                <th>UTM Source</th>
                                                <th>Date Of Booking</th>

                                            </tr>
                                        </tfoot>
                                                                    </table>
                                                                </FooterTemplate>

                                                            </asp:Repeater>
                                                            <%--</ContentTemplate>
                                                            </asp:UpdatePanel>--%>
                                                            <asp:Label ID="lbldashborderror" CssClass="label-danger" runat="server" Text="Label"></asp:Label>

                                                        </div>
                                                        <!-- /.box-body -->
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                        <!-- /.box-body -->
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
                $("#example1").DataTable();

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

                $("#example1").DataTable();
            });
        </script>
        <!-- Optionally, you can add Slimscroll and FastClick plugins.
     Both of these plugins are recommended to enhance the
     user experience. Slimscroll is required when using the
     fixed layout. -->
    </form>
</body>
</html>
