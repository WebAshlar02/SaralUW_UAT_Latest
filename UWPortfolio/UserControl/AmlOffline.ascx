<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AmlOffline.ascx.cs" Inherits="UserControl_AmlOffline" %>
<div id="divDocumentList" runat="server" class="panel box box-success">
    <script type="text/javascript">
        function openModal() {
            $('#divPendingDocPopup').modal('show');
        }
        /*added by shri on 10 july 17 to check if file exist or not*/
        function fnHasFile() {
            var count = 0;
            var val = '';
            $('.DocFileUpload').each(function () {
                if ($(this).val().split("").reverse().join("").indexOf("\\") > 0) {
                    val = $(this).parent().parent().find('.ddlUWStatus').val();
                    if (val == "0") {
                        $(this).parent().parent().find('.ddlUWStatus').addClass('errorborder');
                        count = -1;
                        //return false;                        
                    }
                    else {
                        $(this).parent().parent().find('.ddlUWStatus').removeClass('errorborder');
                        count++;
                    }

                }
            });
            if (count > 0) {
                //document.forms[0].encoding = 'multipart/form-data';
                showloading();
                return true;
            }
            else if (count == -1) {
                hideloading();
                alert('Select UW Decision');
            }
            else {
                hideloading();
                alert('Select file to upload');
            }
            return false;
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            function fnHasFile() {
                var count = 0;
                var val = '';
                $('.DocFileUpload').each(function () {
                    if ($(this).val().split("").reverse().join("").indexOf("\\") > 0) {
                        val = $(this).parent().parent().find('.ddlUWStatus').val();
                        if (val == "0") {
                            $(this).parent().parent().find('.ddlUWStatus').addClass('errorborder');
                            count = -1;
                            //return false;                        
                        }
                        else {
                            $(this).parent().parent().find('.ddlUWStatus').removeClass('errorborder');
                            count++;
                        }

                    }
                });
                if (count > 0) {
                    //document.forms[0].encoding = 'multipart/form-data';
                    return true;
                }
                else if (count == -1) {
                    alert('Select UW Decision');
                }
                else {
                    alert('Select file to upload');
                }
                return false;
            }
        });
        /*end here*/
    </script>

    <div class="row">
        <div class="col-md-12 form-group">
            <div class="">
                <div class="">
                    <asp:UpdatePanel ID="UpdatePanel111" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblApplicationNo" runat="server" ForeColor="Green" Font-Bold="true" Font-Size="Medium" Text="" Style="margin-right: 25px; margin-left: 20px"></asp:Label>
                            <asp:Label ID="lblClientName" runat="server" ForeColor="Green" Font-Bold="true" Font-Size="Medium" Text="" Style="margin-right: 25px"></asp:Label>
                            <asp:Label ID="lblMobileNo" runat="server" ForeColor="Green" Font-Bold="true" Font-Size="Medium" Text="" Style="margin-right: 25px"></asp:Label>
                            <div class="box-body">
                                <table class="table table-bordered" cellpadding="20" cellspacing="1">
                                    <%-- <tr>--%>
                                    <%--<td id="showdocfulfillment">--%>
                                    <%--<table class="table table-bordered table-striped">--%>
                                    <tr>
                                        <td>
                                            <asp:Repeater ID="rptDocumentList" OnItemDataBound="rptDocumentList_ItemDataBound" runat="Server">
                                                <HeaderTemplate>
                                                    <table class="table table-bordered " style="background-color: #deebfd" cellpadding="3" cellspacing="1">
                                                        <div class="col-md-12">
                                                            <tr>
                                                                <th>Document Of</th>
                                                                <th>Document Proof</th>
                                                                <%--<th>Document File</th>  --%>
                                                                <th class="HideControl">Assured Type</th>
                                                                <th class="HideControl">Document Type</th>
                                                            </tr>
                                                        </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="col-md-12">
                                                        <tr>
                                                            <td>
                                                                <%--<asp:HyperLink ID="HlkDocument" runat="server" NavigateUrl='<%#DataBinder.Eval(Container.DataItem, "FilePath")%>'
                                                                    Target="_blank" Text='<%#DataBinder.Eval(Container.DataItem, "DocumentTypeName")%>'
                                                                    Visible="false"></asp:HyperLink>--%>
                                                                <asp:Label ID="lblDocument" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DOCUMENT_OF")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlDocProof" runat="server" ClientIDMode="AutoID" CssClass="form-control">
                                                                    <%--<asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblDocTypeId" runat="server" Style="display: none;" Text='<%#DataBinder.Eval(Container.DataItem, "DOCUMENT_TYPE")%>'></asp:Label>
                                                            </td>
                                                            <%--<td>
                                                                <asp:FileUpload ID="FileUploadDoc" class="DocFileUpload" runat="server" />
                                                            </td>             --%>
                                                            <td>
                                                                <asp:Label ID="lblAssuredType" class="HideControl" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ASSURED_TYPE")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblDocTypeName" class="HideControl" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DOCUMENT_TYPE_NAME")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td align="right" class="style3" style="white-space: nowrap">
                                            <asp:Label runat="server" ID="lblMsg" Visible="false" Font-Bold="true" Font-Size="Medium" Text="" Style="float: left"></asp:Label>
                                            &nbsp;<asp:Button ID="btnDoc" runat="server" CssClass="button_search btn btn-primary lnkButton " OnClick="btnDoc_Click"
                                                Text="Save" /><%--OnClientClick="return fnHasFile();"--%>
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                        </td>

                                    </tr>
                                    <tr>
                                    </tr>
                                    <%--</table>--%>
                                    <%-- </td>--%>
                                    <%-- </tr>--%>
                                </table>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <%--<asp:AsyncPostBackTrigger ControlID="btnDoc" />--%>
                            <asp:PostBackTrigger ControlID="btnDoc" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</div>
