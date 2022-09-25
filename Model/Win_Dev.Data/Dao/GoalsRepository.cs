using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

namespace Win_Dev.Data
{
    public class GoalsRepository
    {
        public HttpClient _client;
        private bool disposed = false;

        public GoalsRepository()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", NetworkClient.Token);
        }

        public GoalsRepository(HttpClient client)
        {
            _client = client;
        }

        public virtual Goal FindByID(Guid id)
        {
            var response = _client.GetAsync(NetworkClient.ServerPath + $"/api/Goals/{id}").Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var goal = JsonConvert.DeserializeObject<Goal>(result);

            if (goal != null) return goal;
            return null;
        }

        public IEnumerable<Goal> FindAll()
        {
            IEnumerable<Goal> list = new List<Goal>();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", NetworkClient.Token);
            var response = _client.GetAsync(NetworkClient.ServerPath + "/api/Goals").Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var valueSet = JsonConvert.DeserializeObject<List<Goal>>(result);
            list = valueSet.ToList();

            return list;
        }
        
        public virtual void Insert(Goal goal)
        {
            string json = new JavaScriptSerializer().Serialize(goal);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(NetworkClient.ServerPath + "/api/Goals");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + NetworkClient.Token);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }


        public virtual void Delete(Guid id)
        {
            var request = (HttpWebRequest)WebRequest.Create(NetworkClient.ServerPath + "/api/Goals/" + $"?id={id}");
            request.Method = "DELETE";
            request.Headers.Add("Authorization", "Bearer " + NetworkClient.Token);

            var response = (HttpWebResponse)request.GetResponse();
        }

        public virtual void Update(Goal goal)
        {
            Insert(goal);
        }

        public IEnumerable<Goal> FindGoalsFor(Guid id)
        {
            IEnumerable<Goal> list = new List<Goal>();

            var response = _client.GetAsync(NetworkClient.ServerPath + $"/api/Goals/ForProject/{id}").Result;
            var result = response.Content.ReadAsStringAsync().Result;

            if (result != "null")
            {
                var valueSet = JsonConvert.DeserializeObject<List<Goal>>(result);
                list = valueSet.ToList();
            }

            return list;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {

            }
            disposed = true;
        }


        ~GoalsRepository()
        {
            Dispose(false);
        }
    }

}

