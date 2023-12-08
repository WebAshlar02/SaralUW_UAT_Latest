<%@ Page Language="C#" AutoEventWireup="true" CodeFile="distancecal.aspx.cs" Inherits="Appcode_distancecal" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?sensor=false&key=AIzaSyCI2rrQ6FeYu6JvfehofKYLLKxkDxem78o"></script>
    <script type="text/javascript">
        var latitude1 = 0;
        var longitude1 = 0;
        var latitude2 = 0;
        var longitude2 = 0;
        function GetLocation() {
            debugger;
            var geocoder = new google.maps.Geocoder();
            var address = document.getElementById("txtPostalCode1").value;
            geocoder.geocode({ 'address': address }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                     latitude1 = results[0].geometry.location.lat();
                     longitude1 = results[0].geometry.location.lng();
                    document.getElementById("txtLatitude").value = latitude;
                    document.getElementById("txtLongitude").value = longitude1;
                } else {
                    alert("Request failed.")
                }
            });
        };
        function distanceTo(latitude1, longitude1, latitude2, longitude2) {
            var rlat1 = Math.PI * latitude1 / 180
            var rlat2 = Math.PI * latitude2 / 180
            var rlon1 = Math.PI * longitude1 / 180
            var rlon2 = Math.PI * longitude2 / 180
            var theta = lon1 - lon2
            var rtheta = Math.PI * theta / 180
            var dist = Math.sin(rlat1) * Math.sin(rlat2) + Math.cos(rlat1) * Math.cos(rlat2) * Math.cos(rtheta);
            dist = Math.acos(dist)
            dist = dist * 180 / Math.PI
            dist = dist * 60 * 1.1515
            dist = dist * 1.609344
            document.getElementById("txtLongitude").value = dist;
            return dist
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <input name="txtPostalCode1" type="text" value="" id="txtPostalCode1" />
                </td>
                <td>
                    <input type="submit" name="Button1" value="Get Latitude Longitude" onclick="GetLocation(); return false;"
                        id="Button1" />
                </td>
            </tr>
            <tr>
                <td>
                    <input name="txtLatitude" type="text" id="txtLatitude" />
                </td>
                <td>
                    <input name="txtLongitude" type="text" id="txtLongitude" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>