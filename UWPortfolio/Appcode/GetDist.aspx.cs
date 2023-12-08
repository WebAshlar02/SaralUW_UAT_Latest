using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using GoogleMaps.LocationServices;



public partial class Appcode_GetDist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public DataTable GetdtLatLong(string url)
    {
        try
        {
            DataTable dtGMap = null;
            WebRequest request = WebRequest.Create(url);
            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    DataSet dsResult = new DataSet();
                    dsResult.ReadXml(reader);
                    DataTable dtCoordinates = new DataTable();
                    dtCoordinates.Columns.AddRange(new DataColumn[4] { new DataColumn("Id", typeof(int)),
                    new DataColumn("Address", typeof(string)),
                    new DataColumn("Latitude",typeof(string)),
                    new DataColumn("Longitude",typeof(string)) });
                    if (dsResult.Tables.Count > 1)
                    {
                        foreach (DataRow row in dsResult.Tables["result"].Rows)
                        {
                            string geometry_id = dsResult.Tables["geometry"].Select("result_id = " + row["result_id"].ToString())[0]["geometry_id"].ToString();
                            DataRow location = dsResult.Tables["location"].Select("geometry_id = " + geometry_id)[0];
                            dtCoordinates.Rows.Add(row["result_id"], row["formatted_address"], location["lat"], location["lng"]);
                        }
                    }
                    dtGMap = dtCoordinates;
                    return dtGMap;
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + TextBox1.Text + "&destinations=" + TextBox4.Text + "&mode=car &language=us-en&sensor=false");

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();


        StreamReader resStream = new StreamReader(response.GetResponseStream());


        XmlDocument doc = new XmlDocument();

        string xmlResult = string.Empty;

        xmlResult = resStream.ReadToEnd();


        doc.LoadXml(xmlResult);

        string output = "";

        try
        {

            if (doc.DocumentElement.SelectSingleNode("/DistanceMatrixResponse/row/element/status").InnerText.ToString().ToUpper() != "OK")
            {
                //lblResult.Text = "Invalid City Name please try again";
                return;
            }



            XmlNodeList xnList = doc.SelectNodes("/DistanceMatrixResponse");
            foreach (XmlNode xn in xnList)
            {
                if (xn["status"].InnerText.ToString() == "OK")
                {

                    output = "<table align='center' width='600' cellpadding='0' cellspacing='0'>";
                    output += "<tr><td height='60' colspan='2' align='center'><b>Travel Details</b></td>";
                    output += "<tr><td height='40' width='30%' align='left'>Orgin Place</td><td align='left'>" + xn["origin_address"].InnerText.ToString() + "</td></tr>";
                    output += "<tr><td height='40' align='left'>Destination Place</td><td align='left'>" + xn["destination_address"].InnerText.ToString() + "</td></tr>";

                    output += "<tr><td height='40' align='left'>Distance</td><td align='left'>" + doc.DocumentElement.SelectSingleNode("/DistanceMatrixResponse/row/element/distance/text").InnerText + "</td></tr>";
                    output += "</table>";



                    lblResult.Text = output;
                }
            }
        }
        catch (Exception ex)
        {
            //lblResult.Text = "Error during processing";
            return;
        }



    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        DataTable dt1;
        DataTable dt2;
        double lat1;
        double lat2;
        double long1;
        double long2;

        String url = "https://maps.google.com/maps/api/geocode/xml?key=AIzaSyB6jZ7LnByZwEpfDHnQrEZOQ-0icTb-JpA&address=" + TextBox1.Text + "&sensor=false";
        dt1 = GetdtLatLong(url);
        lat1 = Convert.ToDouble(dt1.Rows[0]["Latitude"]);
        long1 = Convert.ToDouble(dt1.Rows[0]["Longitude"]);

        String url1 = "https://maps.google.com/maps/api/geocode/xml?key=AIzaSyB6jZ7LnByZwEpfDHnQrEZOQ-0icTb-JpA&address=" + TextBox4.Text + "&sensor=false";
        dt2 = GetdtLatLong(url1);
        lat2 = Convert.ToDouble(dt2.Rows[0]["Latitude"]);
        long2 = Convert.ToDouble(dt2.Rows[0]["Longitude"]);

        lblResult.Text = Convert.ToString(distance(lat1, long1, lat2, long2, 'K'));
    }
    private double distance(double lat1, double lon1, double lat2, double lon2, char unit)
    {
        double theta = lon1 - lon2;
        double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
        dist = Math.Acos(dist);
        dist = rad2deg(dist);
        dist = dist * 60 * 1.1515;
        if (unit == 'K')
        {
            dist = Math.Round((dist * 1.609344), 1);
        }
        else if (unit == 'N')
        {
            dist = dist * 0.8684;
        }

        return (dist);
    }
    private double deg2rad(double deg)
    {
        return (deg * Math.PI / 180.0);
    }
    private double rad2deg(double rad)
    {
        return (rad / Math.PI * 180.0);
    }
}


public class Distance
{
    private double dblLat1;

    public double Lat1
    {
        get { return dblLat1; }
        set { dblLat1 = value; }
    }

    private double dbllon1;

    public double lon1
    {
        get { return dbllon1; }
        set { dbllon1 = value; }
    }
    private double dbllat2;

    public double Lat2
    {
        get { return dbllat2; }
        set { dbllat2 = value; }
    }
    private double dbllon2;

    public double lon2
    {
        get { return dbllon2; }
        set { dbllon2 = value; }
    }

    public double Response
    {
        get { return CallResponse(); }        
    }
    private double deg2rad(double deg)
    {
        return (deg * Math.PI / 180.0);
    }
    private double rad2deg(double rad)
    {
        return (rad / Math.PI * 180.0);
    }

    private double CallResponse()
    {
        double theta = lon1 - lon2;
        double dist= Math.Sin(deg2rad(Lat1)) * Math.Sin(deg2rad(Lat2)) + Math.Cos(deg2rad(Lat1)) * Math.Cos(deg2rad(Lat2)) * Math.Cos(deg2rad(theta));
        dist = Math.Acos(dist);
        dist = rad2deg(dist);
        dist = dist * 60 * 1.1515;
        //if (unit == 'K')
        //{
            dist = Math.Round((dist * 1.609344), 1);
        //}
        //else if (unit == 'N')
        //{
        //    dist = dist * 0.8684;
        //}

        return (dist);
    }
}