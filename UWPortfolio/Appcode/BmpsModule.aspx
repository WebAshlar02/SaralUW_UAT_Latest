<!-- Start of changes: Sagar Thorave [MFL00886] CR-30573-->
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BmpsModule.aspx.cs" Inherits="BmpsModule" %>
 <script src="../plugins/jQuery/jquery-2.2.3.min.js"></script>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bmps Policy Flag</title>
    <script type="text/javascript" language="javascript">
         function validation() {
            if(document.getElementById("FileUploadDocument").value==""){
                alert("Please upload excel file.");   
            return false;
            }else{
                return true;
            }
               
             }
    $(document).ready(function(){
      $("#FileUploadDocument").click(function(){
            $("#labelMsg").hide();
        });

    });
    
    </script>
      <style type="text/css">
        .style1 {
            text-align: center;
            background-color: #6699FF;
        }

        .style2 {
            margin-top:5000px;
            text-align: right;
            background-color: #D9ECFF;
            height: 35px;
        }

        .style3 {
            text-align: left;
            background-color: #D9ECFF;
            height: 35px;
        }

        .style4 {
            text-align: center;
            background-color: #D9ECFF;

        }
          #labelmsg {
          }

        .auto-style1 {
            height: 13px;
        }

        .auto-style2 {
            text-align: center;
            background-color: #6699FF;
            height: 14px;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;

        }

        .table {
        }
    </style>
</head>

    <body>
    <form id="form2" runat="server" autocomplete="off">
        
        <div>
            <table style="margin: 0 auto; height: 726px; width: 988px; border-style: solid; border-color: Black">
                <tr>
                    <td colspan="2" class="auto-style2">
                        <h1>
                            <b>Policy Risk Flag</b>
                        </h1>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <b>
                            <label>Upload Excel File<span style="color: red;">*</span></label></b>
                    </td>
                    <td class="style3">

                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                      <asp:FileUpload ID="FileUploadDocument" runat="server" Font-Names="Upload file" /> 
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:Button OnClick="btnUpload_Click" ID="btnUpload" runat="server" Text="Upload" 
                            Width="108px" Height="26px"   OnClientClick="return validation()" />
                    </td>&nbsp;&nbsp;&nbsp;&nbsp;
                    <td class="style3">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btndownload" runat="server" OnClick="btndownload_Click"  Text="Export To Excel" ValidationGroup="Date"
                            Width="117px" />
                   </td>
                </tr>
                <tr>
                    <td colspan="2" style="background-color: #D9ECFF; font-size:18px" class="auto-style1">
                        <asp:Label class="style5" runat="server" ID="labelMsg"  CssClass="label-default" Text=""></asp:Label>
                    </td>
                </tr>
               <tr>
                   <td colspan="2" style="background-color: #D9ECFF"> 
                       
                    </td>
                </tr>



            </table>
        </div>
    </form>
</body>

</html>

    <!-- End of changes: Sagar Thorave [MFL00886] CR-30573-->
