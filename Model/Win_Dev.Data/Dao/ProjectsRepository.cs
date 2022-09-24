using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

namespace Win_Dev.Data
{
    public class ProjectsRepository
    {
        private HttpClient _client;
        private bool disposed = false;

        public ProjectsRepository()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", NetworkClient.Token);
        }

        public virtual Project FindByID(Guid id)
        {
            var response = _client.GetAsync(NetworkClient.ServerPath + $"/api/Projects/{id}").Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var project = JsonConvert.DeserializeObject<Project>(result);

            if (project != null) return project;
            return null;
        }

        public IEnumerable<Project> FindAll()
        {
            IEnumerable<Project> list = new List<Project>();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", NetworkClient.Token);
            var response = _client.GetAsync(NetworkClient.ServerPath + "/api/Projects").Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var valueSet = JsonConvert.DeserializeObject<List<Project>>(result);
            list = valueSet.ToList();

            return list;
        }

        public virtual void Insert(Project project)
        {
            string json = new JavaScriptSerializer().Serialize(project);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(NetworkClient.ServerPath + "/api/Projects");
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
            var request = (HttpWebRequest)WebRequest.Create(NetworkClient.ServerPath + "/api/Projects/" + $"?id={id}");
            request.Method = "DELETE";
            request.Headers.Add("Authorization", "Bearer " + NetworkClient.Token);

            var response = (HttpWebResponse)request.GetResponse();
        }


        public virtual void Update(Project project)
        {
            Insert(project);
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

        ~ProjectsRepository()
        {
            Dispose(false);
        }
    }

}

