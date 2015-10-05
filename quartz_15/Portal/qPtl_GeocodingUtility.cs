using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;

namespace Quartz.Portal
{
    public class qPtl_GeocodingUtility
    {
        // http://code.google.com/apis/maps/documentation/reference.html#GGeoStatusCode
        public enum GeoStatusCode
        {
            Success = 200,
            BadRequest = 400,
            ServerError = 500,
            MissingQuery = 601,
            MissingAddress = 601,
            UnknownAddress = 602,
            UnavailableAddress = 603,
            UnknownDirections = 604,
            BadKey = 610,
            TooManyQueries = 620
        }

        // http://code.google.com/apis/maps/documentation/reference.html#GGeoAddressAccuracy
        public enum GeoAddressAccuracy
        {
            UnknownLocation = 0,
            Country = 1,
            Region = 2,
            SubRegion = 3,
            Town = 4,
            PostCode = 5,
            Street = 6,
            Intersection = 7,
            Address = 8,
            Premise = 9
        }
                
        private static string connString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
        private static string googleKey = System.Configuration.ConfigurationManager.AppSettings["GoogleGeocodingKey"];
        public string[] coords;

        public string PerformGeocodeUpdate(string address1, string address2, string city, string state, string country)
        {
            string sqlGISUpdate;
            
            if (GetGeocodeInfo(address1, address2, city, state, country) == GeoStatusCode.Success)
            {
                //Save updated coordinates
                sqlGISUpdate = GenerateGISSql();

                return sqlGISUpdate;
            }
            else
            {
                //Do error work
                return "fail";
            }
        }

        public GeoStatusCode GetGeocodeInfo(string address1, string address2, string city, string state, string country)
        {
            string requestURL = "http://maps.google.com/maps/geo?";
            requestURL += "q=" + address1 + " " + address2 + ",+" + city + ",+" + state + ",+" + country + "&";
            requestURL += "output=xml&";
            requestURL += "sensor=false&";
            requestURL += "key=" + googleKey;

            HttpWebRequest geoCodeReq = (HttpWebRequest)WebRequest.Create(requestURL);
            HttpWebResponse geoCodeResp = (HttpWebResponse)geoCodeReq.GetResponse();

            try
            {
                Stream respStream = geoCodeResp.GetResponseStream();

                string xmlResp = "";
                byte[] buffer = new byte[256];

                int count = 0;
                do
                {
                    count = respStream.Read(buffer, 0, buffer.Length);

                    if (count != 0)
                    {
                        xmlResp += Encoding.ASCII.GetString(buffer, 0, count);
                    }
                } while (count > 0);

                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(xmlResp);
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xdoc.NameTable);
                nsmgr.AddNamespace("google", "http://earth.google.com/kml/2.0");

                GeoStatusCode status = (GeoStatusCode)Convert.ToInt16(xdoc.SelectSingleNode("//google:kml/google:Response/google:Status/google:code", nsmgr).InnerXml);

                if (status == GeoStatusCode.Success)
                {
                    coords = new string[2];
                    coords = xdoc.SelectSingleNode("//google:kml/google:Response/google:Placemark/google:Point/google:coordinates", nsmgr).InnerXml.Split(',');
                }

                return status;
            }
            catch (Exception)
            {
                return GeoStatusCode.ServerError;
            }
        }

        public string GenerateGISSql()
        {
            string sqlSET_GIS;
            if (coords != null)
            {
                sqlSET_GIS = "geography::Parse('POINT(" + coords[0] + " " + coords[1] + ")')";
            }
            else
            {
                sqlSET_GIS = "fail";
            }

            return sqlSET_GIS;
        }

    }
}