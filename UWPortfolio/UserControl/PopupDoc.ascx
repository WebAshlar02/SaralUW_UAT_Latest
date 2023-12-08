<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PopupDoc.ascx.cs" Inherits="UserControl_PopupDoc"  %>

<div id="divDocumentList" runat="server" class="panel box box-success">
    <script type="text/javascript">
        function openModal() {
            $('#divPendingDocPopup').modal('show');
        }
        /*cakk view dics*/
        function ViewDocs1() {
            /*added by shri on 10 aug 17 to add dms page of megha into our system*/            
            var appno = $('#<%=lblApplicationNo.ClientID%>').html().split(' ')[3];
            window.open('Appcode/DmsVeiwer.aspx?ApplnNo=' + appno);            
            /*end here*/
            return false;
            /*end here*/
        };
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
                    <asp:UpdatePanel ID="UpdatePanel112" runat="server">
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
                                            <asp:Repeater ID="rptDocumentList" runat="Server">
                                                <HeaderTemplate>
                                                    <table class="table table-bordered " style="background-color: #deebfd" cellpadding="3" cellspacing="1">
                                                        <div class="col-md-12">
                                                            <tr>
                                                                <%-- <th class="columnBorder1" style="background-color: #deebfd; text-align: left; padding: 7px">Document Type
                                                                </th>
                                                                <th class="columnBorder1" style="background-color: #deebfd; text-align: left; padding: 7px">Document Proof
                                                                </th>
                                                                <th class="columnBorder1" style="background-color: #deebfd; text-align: left; padding: 7px">
                                                                    <asp:Label ID="lblHdrVerified" runat="server" Text="Verified"></asp:Label>
                                                                </th>
                                                                <th class="columnBorder1" style="background-color: #deebfd; text-align: left; padding: 7px">Upload
                                                                </th>
                                                                <th class="columnBorder1" style="background-color: #deebfd; text-align: left; padding: 7px;">Reject
                                                                </th>
                                                                <th class="columnBorder1" style="background-color: #deebfd; text-align: left; padding: 7px;">UW Status Mark
                                                                </th>
                                                                <th class="columnBorder1" style="background-color: #deebfd; text-align: left; padding: 7px; display: none"></th>--%>
                                                                <col width="30%">
                                                                <col width="30%">
                                                                <%-- <col width="10%">--%>
                                                                <%--<col width="5%">--%>
                                                                <col width="10%">
                                                                <col width="40%">
                                                                <th>Document Type</th>
                                                                <th>Document Proof</th>
                                                                <%-- <th>Verified</th>--%>
                                                                <%--<th>Upload</th>--%>
                                                                <%-- <th>Reject</th>--%>
                                                                <th>UW Status Mark</th>

                                                            </tr>
                                                        </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="col-md-12">
                                                        <tr>
                                                            <td>
                                                                <asp:HyperLink ID="HlkDocument" runat="server" NavigateUrl='<%#DataBinder.Eval(Container.DataItem, "FilePath")%>'
                                                                    Target="_blank" Text='<%#DataBinder.Eval(Container.DataItem, "DocumentTypeName")%>'
                                                                    Visible="false"></asp:HyperLink>
                                                                <asp:Label ID="lblDocument" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Title")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlDocProof" runat="server" CssClass="form-control">
                                                                    <%--<asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <%--<td align="center">
                                                                <asp:CheckBox ID="chkVerified" runat="server" Text="" />
                                                            </td>--%>
                                                           <%-- <td>
                                                                <asp:FileUpload ID="FileUploadDoc" class="DocFileUpload" runat="server" />
                                                            </td>--%>

                                                            <%-- <td align="center">
                                                                <asp:CheckBox ID="chkDeleteDoc" runat="server" Text="" />
                                                            </td>--%>
                                                            <td>
                                                                <asp:DropDownList ID="ddlUWStatus" runat="server" CssClass="form-control ddlUWStatus">
                                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Verified" Value="Verified"></asp:ListItem>
                                                                    <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                                                                    <asp:ListItem Text="Waived" Value="Waived"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="display: none">
                                                                <asp:Label ID="lblDocTypeId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DocumentTypeId")%>'></asp:Label>
                                                                <asp:Label ID="lblFileName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FileName")%>'></asp:Label>
                                                                <asp:Label ID="lblID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ID")%>'></asp:Label>
                                                                <asp:Label ID="lblFilePath" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FilePath")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </div>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <div class="col-md-12">
                                                        <tr>
                                                            <td>
                                                                <asp:HyperLink ID="HlkDocument" runat="server" NavigateUrl='<%#DataBinder.Eval(Container.DataItem, "FilePath")%>'
                                                                    Target="_blank" Text='<%#DataBinder.Eval(Container.DataItem, "DocumentTypeName")%>'
                                                                    Visible="false"></asp:HyperLink>
                                                                <asp:Label ID="lblDocument" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Title")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlDocProof" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <%--<td align="center">
                                                                <asp:CheckBox ID="chkVerified" runat="server" Text="" />
                                                            </td>--%>
                                                           <%-- <td>
                                                                <asp:FileUpload ID="FileUploadDoc" class="DocFileUpload" runat="server" />
                                                            </td>--%>

                                                            <%--<td align="center">
                                                                <asp:CheckBox ID="chkDeleteDoc" runat="server" Text="" />
                                                            </td>--%>
                                                            <td>
                                                                <asp:DropDownList ID="ddlUWStatus" runat="server" CssClass="form-control ddlUWStatus">
                                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Verified" Value="Verified"></asp:ListItem>
                                                                    <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                                                                    <asp:ListItem Text="Waived" Value="Waived"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="display: none">
                                                                <asp:Label ID="lblDocTypeId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DocumentTypeId")%>'></asp:Label>
                                                                <asp:Label ID="lblFileName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FileName")%>'></asp:Label>
                                                                <asp:Label ID="lblID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ID")%>'></asp:Label>
                                                                <asp:Label ID="lblFilePath" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FilePath")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </div>
                                                </AlternatingItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td align="right" class="style3" style="white-space: nowrap">
                                            <asp:Label runat="server" ID="lblMsg" CssClass="col-md-6 HideControl" Font-Bold="true" Font-Size="Medium" Text="" Style="float: left"></asp:Label>
                                            &nbsp;<asp:Button ID="btnDoc" runat="server" CssClass="button_search btn btn-primary" OnClick="btnDoc_Click"
                                                Text="Save" />
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                        </td>

                                    </tr>
                                    <%--added by shri on 31 oct to add dedupe details--%>
                                    <tr>
                                        <td>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <div class="col-md-6 pull-left">
                                                        <asp:Button CssClass="btn-link" ID="btnDedupeClient" runat="server" Text="Dedupe" OnClick="btnDedupeClient_Click" />
                                                    </div>                                                    
                                                    <div class="col-md-6 pull-right">
                                                        <asp:Button CssClass="btn-link" ID="btnViewDocs" runat="server" Text="View Docs" OnClientClick="return ViewDocs1();" />                                                        
                                                    </div>                                                      
                                                </div>
                                            </div>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="col-md-12 HideControl" runat="server" id="divDgOnlineAMlClientDedupe">
                                                <div class="table-responsive" style="overflow: auto; width: 100%; height: 300px">
                                                    <asp:DataGrid ID="dgOnlineAMlUwDedupe" runat="server" HeaderStyle-CssClass="text-bold" AutoGenerateColumns="false" CssClass="table table-bordered table-striped">
                                                        <Columns>
                                                            <asp:BoundColumn HeaderText="GCN" DataField="gcn" />
                                                            <asp:BoundColumn HeaderText="Given Name" DataField="givenname" />
                                                            <asp:BoundColumn HeaderText="Surname" DataField="surname" />
                                                            <asp:BoundColumn HeaderText="Gender" DataField="gender" />
                                                            <asp:BoundColumn HeaderText="Birth Date" DataField="BirthRegDate" />
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <%--end here--%>                                    
                                </table>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <%--<asp:AsyncPostBackTrigger ControlID="btnDoc" />--%>
                            <asp:PostBackTrigger ControlID="btnDoc" />
                            <asp:PostBackTrigger ControlID="btnDedupeClient" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</div>


