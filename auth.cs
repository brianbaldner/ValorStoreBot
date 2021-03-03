using System;
using System.Net;
using System.Text.RegularExpressions;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ValorantAuth
{
    class Program
    {
        public static CookieContainer cookiejar;
        public static string AccessToken { get; set; }
        public static string EntitlementToken { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter Username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            var password = Console.ReadLine();

            ShopRequest(username, password);
        }

        public static string ShopRequest(string username, string password)
        {
            cookiejar = new CookieContainer();
            Authentication.GetAuthorization(cookiejar);
            string authresult = Authentication.Authenticate(cookiejar, username, password);
            var authJson = JsonConvert.DeserializeObject(authresult);
            JToken authObj = JObject.FromObject(authJson);
            if(authresult.Contains("auth_failure"))
            {
                return "nullerror";
            }
            string authURL = authObj["response"]["parameters"]["uri"].Value<string>();
            var access_tokenVar = Regex.Match(authURL, @"access_token=(.+?)&scope=").Groups[1].Value;
            AccessToken = $"{access_tokenVar}";

            RestClient client = new RestClient(new Uri("https://entitlements.auth.riotgames.com/api/token/v1"));
            RestRequest request = new RestRequest(Method.POST);

            request.AddHeader("Authorization", $"Bearer {AccessToken}");
            request.AddJsonBody("{}");

            string response = client.Execute(request).Content;
            var entitlement_token = JsonConvert.DeserializeObject(response);
            JToken entitlement_tokenObj = JObject.FromObject(entitlement_token);

            EntitlementToken = entitlement_tokenObj["entitlements_token"].Value<string>();


            RestClient userid_client = new RestClient(new Uri("https://auth.riotgames.com/userinfo"));
            RestRequest userid_request = new RestRequest(Method.POST);

            userid_request.AddHeader("Authorization", $"Bearer {AccessToken}");
            userid_request.AddJsonBody("{}");

            string userid_response = userid_client.Execute(userid_request).Content;
            dynamic userid = JsonConvert.DeserializeObject(userid_response);
            JToken useridObj = JObject.FromObject(userid);

            //Console.WriteLine(userid_response);

            string UserID = useridObj["sub"].Value<string>();

            return Authentication.getStore(EntitlementToken, AccessToken, UserID);
        }
    }

    class Authentication
    {
        public static void GetAuthorization(CookieContainer jar)
        {
            string url = "https://auth.riotgames.com/api/v1/authorization";
            RestClient client = new RestClient(url);

            client.CookieContainer = Program.cookiejar;

            RestRequest request = new RestRequest(Method.POST);
            string body = "{\"client_id\":\"play-valorant-web-prod\",\"nonce\":\"1\",\"redirect_uri\":\"https://playvalorant.com/opt_in" + "\",\"response_type\":\"token id_token\",\"scope\":\"account openid\"}";
            request.AddJsonBody(body);
            client.Execute(request);
        }

        public static string Authenticate(CookieContainer cookie, string user, string pass)
        {
            string url = "https://auth.riotgames.com/api/v1/authorization";
            RestClient client = new RestClient(url);

            client.CookieContainer = Program.cookiejar;

            RestRequest request = new RestRequest(Method.PUT);
            string body = "{\"type\":\"auth\",\"username\":\"" + user + "\",\"password\":\"" + pass + "\"}";
            request.AddJsonBody(body);

            return client.Execute(request).Content;
        }
        public static string getStore(string entitlement, string access, string id)
        {
            string url = "https://pd.na.a.pvp.net/store/v2/storefront/" + id;
            RestClient client = new RestClient(url);
            client.CookieContainer = Program.cookiejar;
            //client.CookieContainer = new CookieContainer();

            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {access}");
            request.AddHeader("X-Riot-Entitlements-JWT", $"{entitlement}");
            //request.AddHeader("X-Riot-ClientPlatform", $"ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9");
            //request.AddHeader("X-Riot-ClientVersion", $"release-02.03-shipping-10-522677");
            //request.AddJsonBody("{}");
            string response = client.Execute(request).Content;
            return response;
        }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class playID
        {
            public string DisplayName { get; set; }
            public string Subject { get; set; }
            public string GameName { get; set; }
            public string TagLine { get; set; }
        }
        public static string getId(string entitlement, string access)
        {
            string url = "https://pd.na.a.pvp.net/name-service/v2/players/";
            RestClient client = new RestClient(url);

            client.CookieContainer = Program.cookiejar;

            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer " + access);
            request.AddHeader("X-Riot-Entitlements-JWT", entitlement);
            //request.AddHeader("X-Riot-ClientPlatform", $"ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9");
            //request.AddHeader("X-Riot-ClientVersion", $"release-02.03-shipping-10-522677");
            //request.AddJsonBody("{}");
            string responce = client.Execute(request).Content;
            playID id = JsonConvert.DeserializeObject<playID>(responce);
            return id.Subject;
        }
        public static string getSkins(string version)
        {
            string url = "https://shared.na.a.pvp.net/content-service/v2/content";
            RestClient client = new RestClient(url);

            client.CookieContainer = Program.cookiejar;

            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("X-Riot-ClientVersion", version);
            request.AddHeader("X-Riot-ClientPlatform", $"ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9");
            
            //request.AddJsonBody("{}");
            string responce = client.Execute(request).Content;
            return responce;
        }
    }
}