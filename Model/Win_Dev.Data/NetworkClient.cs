using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace Win_Dev.Data
{
    public class NetworkClient : INetworkClient
    {
        public static string ServerPath = "https://localhost:44396";
        public static string Token;

        private ApplicationState _state;
        public ApplicationState State 
        { get
            {
                return _state;
            }
            private set
            {
                _state = value;          
            }
        }

        public Action<string> TokenChanged;
        public Action<ApplicationState> ApplicationStateChanged;

        public NetworkClient()
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
                var responce = client.GetAsync(ServerPath + "/api/State").Result;
                return responce.Content.ReadAsStringAsync().Result;
            }
        }


        public void LogIn(string login, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>();
            var content = new FormUrlEncodedContent(pairs);
            
            using (var client = new HttpClient())
            {
                var response =
                   client.PostAsync(ServerPath + "/SignIn?" + $"username={login}&password={password}", content).Result;

                var result = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, string> tokenDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                if ((response.StatusCode == System.Net.HttpStatusCode.OK)&&(tokenDictionary.TryGetValue("access_token", out Token)))
                {
                    
                    State.DoesUserLoggedIn = true;

                } else
                {
                    State.LoginAttemptFailed = true;
                    State.DoesUserLoggedIn = false;

                }
            }

            if (ApplicationStateChanged != null) ApplicationStateChanged.Invoke(_state);
            if (TokenChanged != null) TokenChanged.Invoke(Token);

            State.LoginAttemptFailed = false;
        }

        public void LogOut()
        {
            State.DoesUserLoggedIn = false;
            if (ApplicationStateChanged != null) ApplicationStateChanged.Invoke(_state);

        }

    }
}

