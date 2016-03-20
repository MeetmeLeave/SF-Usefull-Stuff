using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace RESTSalesforceConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient authClient = new HttpClient();
            string oauthToken, serviceUrl;

            //set OAuth key and secret variables
            string sfdcConsumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
            string sfdcConsumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];

            //set to Force.com user account that has API access enabled
            string sfdcUserName = ConfigurationManager.AppSettings["ForceUserName"];
            string sfdcPassword = ConfigurationManager.AppSettings["ForcePassword"];
            string sfdcToken = ConfigurationManager.AppSettings["ForceToken"];

            //create login password value
            string loginPassword = sfdcPassword + sfdcToken;

            HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>
             {
                 {"grant_type","password"},
                 {"client_id",sfdcConsumerKey},
                 {"client_secret",sfdcConsumerSecret},
                 {"username",sfdcUserName},
                 {"password",loginPassword}
             });

            HttpResponseMessage message = authClient.PostAsync("https://test.salesforce.com/services/oauth2/token", content).Result;

            string responseString = message.Content.ReadAsStringAsync().Result;

            JObject obj = JObject.Parse(responseString);
            oauthToken = (string)obj["access_token"];
            serviceUrl = (string)obj["instance_url"];

            //print response values
            Console.WriteLine(string.Format("The token value is {0}", responseString));
            Console.WriteLine(string.Format("URL is  {0}", serviceUrl));
            Console.WriteLine("");

            //create url using package name in URL
            string restQuery = serviceUrl + "REST endpoint goes here";
            Console.WriteLine(restQuery);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, restQuery);

            //add token to header
            request.Headers.Add("Authorization", "Bearer " + oauthToken);

            //return JSON to the caller
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string requestMessage = "JSON goes here";
            request.Content = new StringContent(requestMessage, Encoding.UTF8, "application/json");

            //call endpoint async
            HttpResponseMessage response = authClient.SendAsync(request).Result;

            string result = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine(result);
            Console.WriteLine("");
            Console.WriteLine("Query complete.");
            Console.ReadLine();
        }
    }
}
