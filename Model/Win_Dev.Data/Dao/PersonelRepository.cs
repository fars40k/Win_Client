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
    public class PersonelRepository
    {
        private HttpClient _client;
        private bool disposed = false;

        public PersonelRepository()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", NetworkClient.Token);
        }

        public virtual Person FindByID(Guid id)
        {
            var response = _client.GetAsync(NetworkClient.ServerPath + $"/api/Personel/{id}").Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var person = JsonConvert.DeserializeObject<Person>(result);

            if (person != null) return person;
            return null;
        }

        public IEnumerable<Person> FindAll()
        {
            IEnumerable<Person> list = new List<Person>();

            var response = _client.GetAsync(NetworkClient.ServerPath + "/api/Personel").Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var valueSet = JsonConvert.DeserializeObject<List<Person>>(result);
            list = valueSet.ToList();
            
            return list;
        }
        
        public virtual void Insert(Person person)
        {
            string json = new JavaScriptSerializer().Serialize(person);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(NetworkClient.ServerPath + "/api/Personel");
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
            
            var request = (HttpWebRequest)WebRequest.Create(NetworkClient.ServerPath + "/api/Personel/" + $"?id={id}");
            request.Method = "DELETE";
            request.Headers.Add("Authorization", "Bearer " + NetworkClient.Token);

            var response = (HttpWebResponse)request.GetResponse();
        }
        
        public virtual void Update(Person person)
        {
            Insert(person);
        }

        public IEnumerable<Person> FindForProject(Guid id)
        {
            IEnumerable<Person> list = new List<Person>();
            var url = NetworkClient.ServerPath + $"/api/Personel/ForProject/{id.ToString()}";

            var response = _client.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;

            if (result != "null")
            {

                var valueSet = JsonConvert.DeserializeObject<List<Person>>(result);
                list = valueSet.ToList();

            }
            return list;
        }

        public IEnumerable<Person> FindForGoal(Guid id)
        {
            IEnumerable<Person> list = new List<Person>();
            var url = NetworkClient.ServerPath + $"/api/Personel/ForGoal/{id.ToString()}";

            var response = _client.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;

            if (result != "null")
            {

                var valueSet = JsonConvert.DeserializeObject<List<Person>>(result);
                list = valueSet.ToList();

            }
            return list;
        }

        public IEnumerable<Person> FindForGoals(Guid id)
        {
            IEnumerable<Person> list = new List<Person>();

            var response = _client.GetAsync(NetworkClient.ServerPath + $"/api/Personel/ForGoal/{id}").Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var valueSet = JsonConvert.DeserializeObject<List<Person>>(result);
            list = valueSet.ToList();

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


        ~PersonelRepository()
        {
            Dispose(false);
        }
    }

}

