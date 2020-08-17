using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ISVDemoUsage.Utils
{
    public class HttpCallAPI
    {
        public static string GetHttpCall(string requestUrl, string accessToken)
        {
            string responseString = "";

            //Crafting the HTTP call
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + accessToken);
            request.ContentType = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine(response.StatusDescription);
            Stream receiveStream = response.GetResponseStream();

            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            responseString = readStream.ReadToEnd();

            return responseString;
        }

        public static string PostHttpCall(string requestUrl, string accessToken, string requestBody)
        {
            string responseString = "";

            //Crafting the HTTP call
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + accessToken);
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(requestBody);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine(response.StatusDescription);
            Stream receiveStream = response.GetResponseStream();

            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            responseString = readStream.ReadToEnd();

            return responseString;
        }
    }
}