using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Win_Dev.Business
{
    public class ClientObject : INetworkClient
    {
        private const string _serverPath = "https://localhost:44399";
        private static string _token;

        private ApplicationState _state;
        public ApplicationState State 
        { get
            {
                return _state;
            }
            private set
            {
                _state = value;
                if (applicationStateChanged != null) applicationStateChanged.Invoke(_state);
            }
        }

        public event Action<ApplicationState> applicationStateChanged;

        public ClientObject()
        {
            State = new ApplicationState();
        }

        public void Initialise()
        {
            try
            {
                if (CheckServerState() == "true")
                {
                    State.IsServerFound = true;
                }
            }
            catch (Exception ex)
            {
                State.IsServerFound = false;
            }
        }

        private string CheckServerState()
        {
            using (var client = new HttpClient())
            {
                var responce = client.GetAsync(_serverPath + "/api/State").Result;
                return responce.Content.ReadAsStringAsync().Result;
            }
        }


        public void LogIn(string login, string password)
        {
            /*
            try
            {
                var registerModel = new
                {
                    Login = login,
                    Password = password,
                    ConfirmPassword = password
                };
                using (var client = new HttpClient())
                {
                    var response = client.PostAsJsonAsync(_serverPath + "/api/Account/Register", registerModel).Result;                  
                }

                state.DoesUserLoggedIn = true;
            }
            catch 
            {
                state.DoesUserLoggedIn = false;
            }*/
            State.DoesUserLoggedIn = true;
        }

        public void LogOut()
        {
            State.DoesUserLoggedIn = false;
            
        }


        // получение токена
        public Dictionary<string, string> GetTokenDictionary(string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( "grant_type", "password" ),
                    new KeyValuePair<string, string>( "username", userName ),
                    new KeyValuePair<string, string> ( "Password", password )
                };
            var content = new FormUrlEncodedContent(pairs);

            using (var client = new HttpClient())
            {
                var response =
                    client.PostAsync(_serverPath + "/Token", content).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                // Десериализация полученного JSON-объекта
                Dictionary<string, string> tokenDictionary =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                return tokenDictionary;
            }
        }

        public HttpClient CreateClient(string accessToken = "")
        {
            var client = new HttpClient();
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }
            return client;
        }

        public string GetValues(string token)
        {
            using (var client = CreateClient(token))
            {
                var response = client.GetAsync(_serverPath + "/api/State").Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}

