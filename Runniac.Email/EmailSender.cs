using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Email
{
    public class EmailSender : IEmailSender
    {
        private string _apiKey;
        private string _domain;
        private string BASE_URL = "https://api.mailgun.net/v2";

        public EmailSender(string apiKey, string domain)
        {
            _apiKey = apiKey;
            _domain = domain;
        }

        public void SendMessage(string from, string to, string subject, string body)
        {
            RestClient client = new RestClient();
            client.BaseUrl = BASE_URL;
            client.Authenticator = new HttpBasicAuthenticator("api", _apiKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", _domain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", from);
            request.AddParameter("to", to);
            request.AddParameter("subject", subject);
            request.AddParameter("html", body);
            request.Method = Method.POST;
            client.Execute(request);
        }
    }
}
