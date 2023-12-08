<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="UserControl_test" %>

<%@ Register Src="~/UserControl/AmlOffline.ascx" TagPrefix="uc1" TagName="AmlOffline" %>
<%@ Register Src="~/UserControl/HealthProfile.ascx" TagPrefix="uc1" TagName="HealthProfile" %>
<%--<%@ Register Src="~/Appcode/ManualAllocation.ascx" TagPrefix="uc1" TagName="ManualAllocation" %>--%>
<link rel="stylesheet" href="../bootstrap/css/bootstrap.min.css" />

    <!-- Font Awesome -->
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css"/>--%>
    <link rel="stylesheet" href="../downloaded_refs/font-awesome-4.6.3/css/font-awesome.css" />

    <!-- Ionicons -->
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css"/>--%>    
<link href="../downloaded_refs/ionicons-2.0.1/css/ionicons.css" rel="stylesheet" />
    <!-- DataTables -->
    <link rel="stylesheet" href="../plugins/datatables/dataTables.bootstrap.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="../dist/css/AdminLTE.min.css" />
    <link rel="stylesheet" href="../dist/css/styles-app.css" />
    <!-- AdminLTE Skins. We have chosen the skin-blue for this starter
        page. However, you can choose any other skin. Make sure you
        apply the skin class to the body tag so the changes take effect.
  -->
    <link rel="stylesheet" href="dist/css/skins/skin-maroon.min.css" />
    <!-- jQuery 2.2.3 -->
    <script src="plugins/jQuery/jquery-2.2.3.min.js"></script>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        function fnShowAdjDiv(a) {
            debugger;
            if ($(a).find('input[type="radio"]:checked').val() == 'No') {
                //$('#divHideWeightCause').addClass('HideControl');
                $(a).parent().parent().closest('div').find('.HideShowDiv').addClass('HideControl');
            }
            else {
                //$('#divHideWeightCause').removeClass('HideControl');
                $(a).parent().parent().closest('div').find('.HideShowDiv').removeClass('HideControl');
            }
        }

        function fnShowAdjDivDdl(a) {
            debugger;
            if ($(a).val() == 'No') {
                $(a).parent().parent().next('.HideShowDiv').addClass('HideControl');
            }
            else {
                $(a).parent().parent().next('.HideShowDiv').removeClass('HideControl');
            }
        }
        function fnShowAdjDivOnYesNo(a) {
            debugger;
            if ($(a).find('input[type="radio"]:checked').val() == 'No') {
                $(a).parent().parent().closest('div').find('.HideShowDivOnYes').addClass('HideControl');
                $(a).parent().parent().closest('div').find('.HideShowDivOnNo').removeClass('HideControl');
            }
            else {
                $(a).parent().parent().closest('div').find('.HideShowDivOnYes').removeClass('HideControl');
                $(a).parent().parent().closest('div').find('.HideShowDiv').addClass('HideControl');
                $(a).parent().parent().closest('div').find('.HideShowDivOnNo').addClass('HideControl');
            }
        }
        function fnShowAsset(a) {
            var count = 0;
            $(a).find('input[type="checkbox"]').each(function () {
                if ($(this).val() == "Life Insurance" && $(this).is(':checked') == true) {
                    count++;
                    return;
                }
                if ($(this).val() == "Health Insurance" && $(this).is(':checked') == true) {
                    count++;
                    return;
                }

            })
            if (count > 0) {
                $(a).parent().parent().closest('div').find('.HideShowDiv').removeClass('HideControl');
            }
            else {
                $(a).parent().parent().closest('div').find('.HideShowDiv').addClass('HideControl');
            }
        }

        function fnCollapsedBox(req) {
            debugger;
            //Find the box parent        
            var box = $(req).parents(".box").first();
            //Find the body and the footer
            var bf = box.find(".box-warning , .box-solid");
            if (!box.hasClass("collapsed-box")) {
                box.addClass("collapsed-box");
                bf.slideUp();
            } else {
                box.removeClass("collapsed-box");
                bf.slideDown();
            }
        }

        function fnChaneNsap() {
            debugger;
            //alert($('#cbIsNsap').is('checked'));
            if (confirm('Do you want to change NSAP loading')) {
                return true;
            }
            else {
                $('#cbIsNsap').attr('checked', !$('#cbIsNsap').is(':checked'));
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row" id="divDashbord" runat="server">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <div>
                    <div class="col-lg-3 col-xs-6">
                        <!-- small box -->
                        <div class="small-box bg-aqua">
                            <div class="inner">
                                <h3>
                                    <asp:Label ID="lblFreshcount" runat="server" Text="0"></asp:Label>
                                </h3>
                                <p>
                                    <%--<asp:LinkButton runat="server" ID='<%#Eval("Id") %>' Text='<%#Eval("Name")%>' ></asp:LinkButton>--%>
                                    <%--<asp:LinkButton ID="lnkFreshCase" OnClientClick="SetTargetToSelf();" href runat="server" CssClass="lnkButton">FRESH CASES</asp:LinkButton>--%>
                                </p>
                            </div>
                            <div class="icon">
                                <i class="ion ion-person-add"></i>
                            </div>
                            <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <%--<asp:Label ID="lblCarNumber" runat="server" Text='<%# string.Format("Car Number:{0}", Eval("carnumber")) %>' />--%>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        </div>
    </form>
</body>
</html>
