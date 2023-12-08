<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UWExamQue.ascx.cs" Inherits="UserControl_UWExamQue" %>

<script>
    function openBulkModal() {
        $('#divQueFileUpload').modal('show');
    }
</script>

<div id="divUWExamsQue" runat="server" class="panel box box-success">

    <div class="row">

        <div class="col-md-12 form-group">
            <div id="Div1" class="box-body" runat="server">
                <table class="table table-bordered" cellpadding="20" cellspacing="1">
                    <tr>
                        <td>
                            <div class="col-xs-12 col-md-6 col-lg-6">
                                <div class="form-group">
                                    <label>Select File</label>
                                    <asp:FileUpload ID="QueFileUploadExcel" class="DocFileUpload" runat="server" />
                                </div>
                            </div>
                        </td>
                    </tr>
                    <%--<tr>
                        <td>
                            <div class="col-xs-12">
                                <div class="form-group">
                                    <asp:GridView ID="gvDecisionLog" CssClass="table table-bordered table-striped" runat="server"></asp:GridView>
                                </div>
                            </div>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <div class="col-xs-12 col-md-6 col-lg-6">
                                <div class="form-group">
                                    <br />

                                    <asp:Button ID="btnQueUpload" runat="server" CssClass="button_search btn btn-primary" OnClientClick="return fnHasFile();" OnClick="btnQueUpload_Click"  Text="Upload Exams Que" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="col-xs-12 col-md-6 col-lg-6">
                                <div class="form-group">
                                    <br />
                                    <asp:Label runat="server" ID="lblMsg" Visible="false" Font-Bold="true" Font-Size="Medium" Text="" Style="float: left"></asp:Label>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

    </div>

</div>
