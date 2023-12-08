<%@ Page Title="" Language="C#" MasterPageFile="~/Appcode/Bpmuwmodule.master" AutoEventWireup="true" CodeFile="GetDist.aspx.cs" Inherits="Appcode_GetDist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?sensor=false&key=AIzaSyCI2rrQ6FeYu6JvfehofKYLLKxkDxem78o"></script>
    <script type="text/javascript">
        var latitude1 = 0;
        var longitude1 = 0;
       
        function GetLocation() {
            debugger;
            var geocoder = new google.maps.Geocoder();
            var address1 = document.getElementById("TextBox1").value;
          
            geocoder.geocode({ 'address': address1 }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    latitude1 = results[0].geometry.location.lat();
                    longitude1 = results[0].geometry.location.lng();
                    document.getElementById("txtLatitude").value = latitude1;
                    document.getElementById("lblResult").textContent = longitude1;
                } else {
                    alert("Request failed.")
                }
            });
           
        };
           
    </script>
    <script type="text/javascript">
        function OrginAutoComplete() {
            try {
                var input = document.getElementById('TextBox1');
                var autocomplete = new google.maps.places.Autocomplete(input);
                autocomplete.setTypes('changetype-geocode');
            }
            catch (err) {

            }
        }


        function DestAutoComplete() {
            try {
                var input = document.getElementById('TextBox4');
                var autocomplete = new google.maps.places.Autocomplete(input);
                autocomplete.setTypes('changetype-geocode');
            }
            catch (err) {

            }
        }


        google.maps.event.addDomListener(window, 'load', OrginAutoComplete);
        google.maps.event.addDomListener(window, 'load', DestAutoComplete);
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Pick Up address"></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="DroppOff Address"></asp:Label>
    &nbsp;
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Details" Visible="false" OnClientClick="GetLocation()"
        OnClick="Button1_Click" Style="margin-left: 55px" Width="97px" />
     <asp:Button ID="Button2" runat="server" Text="Distance" OnClick="Button2_Click" Style="margin-left: 55px" Width="97px" />
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;
                <br />
     <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
</asp:Content>

