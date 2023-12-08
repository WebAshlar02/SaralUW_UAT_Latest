<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DMSVeiwerNXT.aspx.cs" Inherits="DMSVeiwerNext" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>DMS File</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body>
    <form runat="server">
    <div class="container-fluid">
        
        <div class="row" id="divrw1" visible="true" runat="server" >
             <div class="col-md-12">
                 <asp:Label ID="LblAppNo" runat="server" Text="Application Number"></asp:Label>
                 <asp:TextBox ID="TxtAppNo" runat="server"></asp:TextBox>
                 <asp:Button ID="Getimg" runat="server" OnClick="Getimg_Click" Text="Search Image" />
                 </div>

            </div>
           
       

        <div class="row" id="row2" visible="false" runat="server">
        <div class="col-sm-3" >
             
            
            <iframe name="myIframe" id="IframeAPP" height="600px" widht="100%" runat="server" ></iframe>
        
            </div>
        
     
        <div class="col-sm-3">

            <iframe name="myIframe" id="IframeAdr" height="300px" widht="100%" runat="server" ></iframe>
             <iframe name="myIframe" id="IframeAge" height="300px" widht="5000px" runat="server" ></iframe>
        </div>
         
       
            <div class="col-sm-3">
                <iframe name="myIframe" id="IframeID" height="300px" widht="100%" runat="server" ></iframe>
                 <iframe name="myIframe" id="IframeIncome" height="300px" widht="100%" runat="server" ></iframe>
            </div>
            <div class="col-sm-3">
                <iframe name="myIframe" id="IframeSIS" height="300px" widht="100%" runat="server" ></iframe>
                 <iframe name="myIframe" id="IframePIC" height="300px" widht="100%" runat="server" ></iframe>
            </div>
        </div>
  </div>
        
        </form>
</body>
</html>
