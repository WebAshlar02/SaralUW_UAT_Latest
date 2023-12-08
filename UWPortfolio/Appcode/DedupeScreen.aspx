<%--//*//*********************************************************************     
//*                      FUTURE GENERALI INDIA                        *    
//**********************************************************************/      
//*                  I N F O R M A T I O N                                       
//********************************************************************* 
// Sr. No.              : 1      
// Company              : Life            
// Module               : CR-28958-Dedupe        
// Program Author       : Shyam Patil             
// BRD/CR/Codesk No/Win : / / /      
// Date Of Creation     : 08-02-2020           
// Description          : 1. Dedupe new api call and data bind
//**********************************************************************//--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DedupeScreen.aspx.cs" Inherits="Appcode_DedupeScreen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function SendClientData_Update(jsonClientData) {
            debugger;
            var Entity=jsonClientData;
            window.parent.window.fnUpdateClientNew(Entity);
            //$('#divUserControlModal').modal('toggle');
            //$('.modal-backdrop').remove();
           
        }

        function SendClientData_Create(jsonClientData) {
            debugger;
            var Entity=jsonClientData;
            window.parent.window.fnCreateClientNew(jsonClientData);
            //$('#divUserControlModal').modal('toggle');
            //$('.modal-backdrop').remove();
             
        }
        
        <%--function SendClientData(jsonClientData) {
            debugger;
            var AT = document.getElementById("<%=hdnclienttype.ClientID %>").value;

            //window.parent.window.bindLAClientData(jsonClientData);
            if (AT == "LA") {
                window.parent.window.fnCreateClientNew();
                window.parent.window.bindLAClientData(jsonClientData);
            }
            else if (AT == "Proposer") {
                window.parent.window.bindPropClientData(jsonClientData);
            }
            else if (AT == "Payer") {
                window.parent.window.bindPayerClientData(jsonClientData);
            }
            else if (AT == "NE") {
                window.parent.window.bindNomineeClientData(jsonClientData);
            }
            window.parent.window.hidePopWin(false);
        }--%>

        function ErrorMSG(msg) {
            //debugger;
            var a = msg;
            window.parent.window.DisplayError(a);
            window.parent.window.hidePopWin(false);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="hdnclienttype" runat="server" />
            <asp:HiddenField ID="hdntable" runat="server" />

        </div>
    </form>
</body>
</html>
