<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dedupe.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head  id="Head1"  runat="server">
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
        <link rel="stylesheet" href="dist/css/skins/skin-maroon.min.css" />
    
    <link rel="stylesheet" href="dist/css/AdminLTE.min.css" />
    <link rel="stylesheet" href="dist/css/styles-app.css" />
    <!-- AdminLTE Skins. We have chosen the skin-blue for this starter
        page. However, you can choose any other skin. Make sure you
        apply the skin class to the body tag so the changes take effect.
  -->
<!-- jQuery 2.2.3 -->
    <script src="plugins/jQuery/jquery-2.2.3.min.js"></script>

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>-->
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <!-- Bootstrap 3.3.6 -->
        <script src="bootstrap/js/bootstrap.min.js"></script>
        <!-- AdminLTE App -->
        <script src="dist/js/app.min.js"></script>
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

         /*div { 
            font:11px Verdana; 
            width:500px;
        }*/
        .grid { 
            width:100%; 
            font:inherit; 
            background-color:#FFF; 
            border:solid 1px #525252;
        }
        .grid td { 
            font:inherit; 
            padding:2px; 
            border:solid 1px #C1C1C1; 
            color:#333; 
            text-align:center; 
            text-transform: uppercase;
        }
        .grid th {  
            padding:3px; 
            color:#FFF; 
            background:#424242 url(grd.png) repeat-x top; 
            border-left:solid 1px #525252; 
            font:inherit; 
            text-align:center; 
            text-transform:uppercase;
        }
        /*input { 
            width:auto; 
            background-color:#4F94CD; 
            color:#FFF; 
            padding:3px;
            border:solid 1px #4F94CD; 
            border-radius:3px; -moz-border-radius:3px; -webkit-border-radius:3px; 
            cursor:pointer;
        }*/

        .errorAgree {
        border: 0.2rem solid #a81d22;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnSave').click(function () {
                debugger;
                var isChecked = $('#rdoSearchSelect').is(':checked');
                if (isChecked == false) {
                    var rdo =  $(document.getElementById("rdoSearchSelect"));
                    
                  //  $('#rdoSearchSelect')
                    $('#rdoSearchSelect').css("background-color", "0.2rem solid #a81d22");
                }
            });
        });
    </script>
    <script>
        // SELECT SINGLE RADIO BUTTON ONLY.
        function check(objID) {
            var rbSelAppNo = $(document.getElementById(objID));
            $(rbSelAppNo).attr('checked', true);      // CHECK RADIO BUTTON WHICH IS SELECTED.

            // UNCHECK ALL OTHER RADIO BUTTONS IN THE GRID.
            var rbUnSelect =
                rbSelAppNo.closest('table')
                    .find("input[type='radio']")
                    .not(rbSelAppNo);

            rbUnSelect.attr('checked', false);
        }
        $('#btView').click(function () {

            var bSelected = true;
            var sAppNo;

            // CHECK EACH ROW FOR THE SELECTED RADIO BUTTON.
            $('#grdPendingDedupe')
                .find('input:radio')
                .each(function () {

                    var name = $(this).attr('name');

                    if ($('input:radio[name=' + name + ']:checked').length == 0) {
                        bSelected = false
                    }
                    else {
                        // GET THE EMPLOYEE NAME (3rd COLUMN) FROM THE ROW WHICH IS CHECKED.
                        sAppNo = $('input:radio[name=' + name + ']:checked').closest('tr')
                            .children('td:nth-child(3)')
                            .map(function () {

                                return $.trim($(this).text());
                            }).get();
                    }
                });

            // FINALLY SHOW THE MESSAGE.
            //if (bSelected == false) {
            //    alert('Invalid Selection'); return false
            //}
            //else
            //    alert('Employee Name: ' + sEmpName);
        });
</script>

 
    </head>
    <body class="hold-transition skin-maroon fixed">
         <form id="Form1" runat="server"> 
        <div class="wrapper">
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
                    <%-- <span title="Online Manual Dedupe" style="color:black;text-decoration-style:solid;font-weight:700;margin-left: 320px;" class="navbar-text">Online Manual Dedupe</span>--%>
                  <%--  <a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button">
                        <span class="sr-only">Toggle navigation</span>
                    </a>
                    <div class="navbar-custom-menu">
                        <ul class="nav navbar-nav">


                            <li class="dropdown user user-menu">

                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">

                                    <img src="dist/img/avatar5.png" class="user-image" alt="User Image" />

                                    <span id="spanusername" runat="server" class="hidden-xs"></span>
                                </a>
                                </li>
                        </ul>
                    </div>--%>
                </nav>
            </header>
                        <!-- Content Wrapper. Contains page content -->
           <%-- <div class="content-wrapper">--%>
            <div >
                <!-- Content Header (Page header) -->
                <section class="content-header">
                   <%-- <small>General</small>--%>

                    <%--  <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                        <li class="active">General</li>
                    </ol>--%>
                </section>

                <!-- Main content -->
                <section class="content">
                     <h1 style="color:white;margin-left:450px">Online Manual Dedupe</h1>
                    <asp:ScriptManager ID="scrpt" runat="server"></asp:ScriptManager>
                    <div class="box box-solid">
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="box-group" id="accordion"></div>
                            <div class="row" id="divDashbord" runat="server">
                                <div class="col-xs-12">
                                    <table id="tblSearch" style="overflow: auto; width: 100%;" class="table-bordered table-striped">
                                        <thead>
                                            <tr>
                                                <td>
                                                    <label id="lblappno" style="margin-right: 10px">Application No</label>
                                                    <input id="txtappno" style="margin-right: 10px" type="text" runat="server" maxlength="11" />
                                                    <%--<button id="btnSearch" style="margin-right: 10px" class="btn btn-primary" type="submit">Search</button>--%>
                                                    <asp:Button id="btnSearch" Text="Search" OnClick="btnSearch_Click" style="margin-right: 10px" CssClass="btn btn-primary" runat="server"/>
                                                </td>
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                                <div class="col-xs-12">
                                    <table id="tblPendingGrid" style="overflow: auto; width: 100%; margin-top: 10px" class="table-bordered table-striped">
                                        <thead title="List of Dedupe Pending Application">
                                            <tr>
                                                <asp:GridView ID="grdPendingDedupe" DataKeyNames="PinCode,REFNO" CssClass="grid" GridLines="None" AutoGenerateColumns="false" OnPageIndexChanging="grdPendingDedupe_PageIndexChanging" OnRowDataBound="grdPendingDedupe_RowDataBound" AllowPaging="true" PageSize="10" runat="server">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select">
                                                            <ItemTemplate>
                                                                <%--onclick="javascript:check(this.id)"--%>
                                                                <asp:RadioButton ID="rdoSelect" onclick="javascript:check(this.id)" AutoPostBack="true" OnCheckedChanged="rdoSelect_CheckedChanged" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ApplicationNumber" HeaderText="App No" />
                                                        <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                                                        <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                                                        <asp:BoundField DataField="Gender" HeaderText="Gender" />
                                                        <asp:BoundField DataField="DOB" HeaderText="DOB" />
                                                        <asp:BoundField DataField="ClientRole" HeaderText="Client Role" />          
<%--                                                        <asp:BoundField DataField="PinCode" HeaderText="PinCode" />
                                                        <asp:BoundField DataField="REFNO" HeaderText="REF NO" />--%>
                                                    </Columns>
<%--                                                        <EmptyDataTemplate> <div style="color:red" align="center"><strong>No records found.</strong></div></EmptyDataTemplate>--%>
                                                </asp:GridView>
                                            </tr>
                                        </thead>
                                    </table>
                                <label id="lblDedupeMsg" style="color:red;margin-left:560px" runat="server" Visible ="false"><strong>No records found.</strong></label>
                                </div>
                                <div class="col-xs-12">
                                    <table id="tblSelectedGrid" style="overflow: auto; width: 100%;margin-top:10px" class="table-bordered table-striped">
                                        <thead title="Dedupe Search Result">
                                            <tr>
                                                <asp:GridView ID="grdSelectedDedupe" OnPageIndexChanging="grdSelectedDedupe_PageIndexChanging" CssClass="grid" GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" runat="server">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:RadioButton ID="rdoSearchSelect" ClientIDMode="Static" OnCheckedChanged="rdoSearchSelect_CheckedChanged" onclick="javascript:check(this.id)" AutoPostBack="true" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="gcn" HeaderText="Client ID" />
                                                        <asp:BoundField DataField="givenname" HeaderText="First Name" />
                                                       <%-- <asp:BoundField DataField="MiddleName" HeaderText="Middle Name" />--%>
                                                        <asp:BoundField DataField="surname" HeaderText="Last Name" />
                                                        <asp:BoundField DataField="gender" HeaderText="Gender" />
                                                        <asp:BoundField DataField="BirthRegDate" HeaderText="DOB" />
                                                        <%--<asp:BoundField DataField="PANNumber" HeaderText="PAN Number" />--%>
                                                    </Columns>
                                                  <%-- <EmptyDataTemplate><div style="color:red" align="center"><strong>No records found.</strong></div></EmptyDataTemplate>--%>
                                                </asp:GridView>
                                            </tr>
                                        </thead>
                                    </table>
                                    <label id="lblSelDedupeMsg" style="color:red;margin-left:560px" runat="server" Visible ="false"><strong>No records found.</strong></label>
<%--                                    <button id="btnSave" style="margin-left:500px;margin-top:10px" type="button" class="btn btn-danger" runat="server">Save</button>--%>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button Text="Save" Visible="false"  OnClick="btnSave_Click"  id="btnSave" style="margin-left:400px;margin-top:10px;width:200px" type="button" CssClass="btn btn-danger" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Button Text="New Client" OnClick="btnNewClient_Click"  id="btnNewClient"  type="button" CssClass="btn btn-primary" runat="server" /><br />
                                            </td>
                                        </tr>
                                    </table>
                                    <div >
                                        <label id="lblMsg" visible="false" style="color:red;font-weight:500;margin-top:10px;margin-left:527px" runat="server"></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
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
         </div>
             </form>
    </body>
</html>
