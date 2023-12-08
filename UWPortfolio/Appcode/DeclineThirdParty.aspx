<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeclineThirdParty.aspx.cs" Inherits="Appcode_DeclineThirdParty" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.0.1/css/toastr.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script src="https://code.jquery.com/jquery-migrate-1.2.1.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.0.1/js/toastr.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
</head>
<body>
    <script>
        function NoData() {
            toastr.info("No Data available for the requested search");
        }
        function SuccessAlert() {
            toastr.success("Case is on hold for 1 business day");
            window.setInterval(function () {
                window.location.href = window.location.href;
            }, 3000);
        }
        //function ErrorAlert(script) {
        //    toastr.info("Error occured while uploading data");
        //}
        function Mand() {
            toastr.danger("Mandatory fields missing");
        }
    </script>
    <br />
    <form id="form1" runat="server" class="container">
        <ul class="nav nav-tabs" role="tablist">
            <li class="nav-item active">
                <a class="nav-link active" data-toggle="tab" href="#hold">Hold Cases</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" data-toggle="tab" href="#unhold">Un Hold Cases</a>
            </li>
        </ul>
        <div class="tab-content">
            <div id="hold" class="container tab-pane active">
                <br>
                <div class="row">
                    <div class="col-md-3">
                        <label for="Application_number" class="form-label">Application Number</label>
                        <input type="text" id="Application_number" name="Application_number" class="form-control" />
                    </div>
                    <div class="col-md-3" style="padding-top: 25px">
                        <asp:Button runat="server" ID="btn_SearchId" Text="Search" OnClick="btn_Search" class="btn btn-primary" />
                    </div>
                </div>
                <br />
                <asp:GridView ID="TableHold" runat="server" CssClass="table table-bordered" DataKeyNames="Application_Number" GridLines="Vertical" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckboxHoldSelect" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Application Name">
                            <ItemTemplate>
                                <asp:Label ID="Application_Number" runat="server" Text='<%# Bind("Application_Number") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Policy Status">
                            <ItemTemplate>
                                <asp:Label ID="Policy_Status" runat="server" Text='<%# Bind("Policy_Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Proposer Name">
                            <ItemTemplate>
                                <asp:Label ID="Proposal_Name" runat="server" Text='<%# Bind("Proposal_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LA Name">
                            <ItemTemplate>
                                <asp:Label ID="LA_Name" runat="server" Text='<%# Bind("LA_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--                            <asp:TemplateField HeaderText="Client Id">
                                <ItemTemplate>
                                    <asp:Label ID="Client_Id" runat="server" Text='<%# Bind("Client_Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Product Name">
                            <ItemTemplate>
                                <asp:Label ID="Product_Name" runat="server" Text='<%# Bind("Product_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Upload File">
                            <ItemTemplate>
                                <asp:FileUpload ID="UploadFile" CssClass="form-control uploadDecline" runat="server"></asp:FileUpload>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Decline Reason">
                            <ItemTemplate>
                                <asp:DropDownList CssClass="form-control" runat="server">
                                    <asp:ListItem Text="Third Party Payment" />
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                <asp:Button OnClick="Hold_Cases" Text="Submit" ID="SubmitHold" class="btn btn-primary" runat="server" />
            </div>
            <div id="unhold" class="container tab-pane fade">
                <br>
                <asp:GridView ID="TableUnHoldCases" runat="server" CssClass="table table-bordered" DataKeyNames="Application_Number" GridLines="Vertical" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:CheckBox ID="UnHoldCheckBox" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Application Name">
                            <ItemTemplate>
                                <asp:Label ID="Application_Number" runat="server" Text='<%# Bind("Application_Number") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Policy Status">
                            <ItemTemplate>
                                <asp:Label ID="Policy_Status" runat="server" Text='<%# Bind("Policy_Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Proposer Name">
                            <ItemTemplate>
                                <asp:Label ID="Proposer_Name" runat="server" Text='<%# Bind("Proposer_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LA Name">
                            <ItemTemplate>
                                <asp:Label ID="LA_Name" runat="server" Text='<%# Bind("LA_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--                            <asp:TemplateField HeaderText="Client Id">
                                <ItemTemplate>
                                    <asp:Label ID="Client_Id" runat="server" Text='<%# Bind("Client_Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Product Name">
                            <ItemTemplate>
                                <asp:Label ID="Product_Name" runat="server" Text='<%# Bind("Product_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Created By">
                            <ItemTemplate>
                                <asp:Label ID="Created_By" runat="server" Text='<%# Bind("Created_By") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Created Date">
                            <ItemTemplate>
                                <asp:Label ID="Created_Date" runat="server" Text='<%# Bind("Created_Date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                <asp:Button OnClick="Un_Hold_Case" Text="Un Hold Cases" ID="SubmitUnHold" class="btn btn-primary" runat="server" />
            </div>
        </div>
    </form>

</body>
</html>
