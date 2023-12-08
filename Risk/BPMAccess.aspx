<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BPMAccess.aspx.cs" Inherits="BPMAccess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>POS - BPM Access</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="txtTaskID" runat="server" Text=""></asp:TextBox>
    
        <asp:Button ID="cmdGetTaskID" runat="server" onclick="cmdGetTaskID_Click" 
            Text="Get TaskID &amp; Proceed" />
    
    </div>
    </form>
</body>
</html>
