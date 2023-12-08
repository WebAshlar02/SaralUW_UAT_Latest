<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeactiveUsers.aspx.cs" Inherits="Appcode_DeactiveUsers" %>

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
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <script src="jquery.sumoselect.min.js"></script>
    <link href="../dist/css/sumoselect.css" rel="stylesheet" />
    <script src="../dist/js/jquery.sumoselect.js"></script>
   
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="divLoadingDetails" runat="server" class="col-md-12">


                <div id="LoadingDtls_container" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">UW Users Deactivate</h3>
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
                                <asp:Label runat="server" ID="lblUWDeact" CssClass="label-danger" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row"></div>
                        <div class="col-md-12">
                           
                            <div class="col-md-2">
                                Action :
                                 <asp:DropDownList runat="server" ID="ddlAction" CssClass="form-control">
                                     <asp:ListItem Value="-1">--Select--</asp:ListItem>
                                     <asp:ListItem Value="1">Active</asp:ListItem>
                                     <asp:ListItem Value="0">Deactive</asp:ListItem>
                                 </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <br />
                                <asp:Button runat="server" ID="btnubmit" Text="Submit" OnClick="btnubmit_Click" CssClass="btn-primary" />
                            </div>

                        </div>
                        <div class="col-md-2">
                            <br/>
                              Select Users :
                        <br/>
                            </div>
                        <div class="col-md-12">
                            <asp:GridView ID="gduser" runat="server" AutoGenerateColumns="False" Height="600px" Width="800px"
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                ForeColor="Black" GridLines="Vertical">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                     <asp:TemplateField HeaderText="Select Data">
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="USer ID">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtUserId" runat="server" Text='<%# Bind("USERID") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserId" runat="server" Text='<%# Bind("USERID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Name">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtUSrName" runat="server" Text='<%# Bind("USERNAME") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("USERNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtStatus" runat="server" Text='<%# Bind("Status") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <RowStyle BackColor="#F7F7DE" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

