using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LedDashboard.Modules.LeagueOfLegends
{
    public static class WebRequestUtil
    {
        public static string GetResponse(string url)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, c, ch, ssl) => true; // ignore ssl as we're querying localhost
            string json = "";
            WebRequest request = HttpWebRequest.Create(url);
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                    return json;
                }
            }
            catch (WebException e)
            {
                throw new WebException("Error retrieving " + url, e);
            }
        }
    }
}
