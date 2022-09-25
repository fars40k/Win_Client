using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Win_Dev.Data
{
    
    /// <summary>
    /// Contains navigation properties data work-logic
    /// </summary>
    public partial class LinkedDataWorker
    {
        public HttpClient _client;
        private bool _disposed = false;

        public LinkedDataWorker()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", NetworkClient.Token);
        }
        public LinkedDataWorker(HttpClient client)
        {
            _client = client;
        }

        public void AddPersonToProject(Guid PersonGUID, Guid ProjectGUID)
        {
            var request = (HttpWebRequest)WebRequest.Create(NetworkClient.ServerPath + "/api/Personel/Assign/" + $"{PersonGUID}/{ProjectGUID}");
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + NetworkClient.Token);

            var response = (HttpWebResponse)request.GetResponse();
        }

        public void RemovePersonFromProject(Guid PersonGUID, Guid ProjectGUID)
        {
            var request = (HttpWebRequest)WebRequest.Create(NetworkClient.ServerPath + "/api/Personel/Unassign/" + $"{PersonGUID}/{ProjectGUID}");
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + NetworkClient.Token);

            var response = (HttpWebResponse)request.GetResponse();
        }
        
        public void AddGoalToProject(Guid GoalGUID, Guid ProjectGUID)
        {
            var request = (HttpWebRequest)WebRequest.Create(NetworkClient.ServerPath + "/api/Projects/AddTaskTo/" + $"{ProjectGUID}/{GoalGUID}");
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + NetworkClient.Token);

            var response = (HttpWebResponse)request.GetResponse();
        }

        public void RemoveGoalFromProject(Guid GoalGUID, Guid ProjectGUID)
        {
            var request = (HttpWebRequest)WebRequest.Create(NetworkClient.ServerPath + "/api/Projects/RemoveTaskFrom/" + $"{ProjectGUID}/{GoalGUID}");
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + NetworkClient.Token);

            var response = (HttpWebResponse)request.GetResponse();
        }

        public void AddPersonToGoal(Guid PersonGUID, Guid GoalGUID)
        {
            var request = (HttpWebRequest)WebRequest.Create(NetworkClient.ServerPath + "/api/Personel/Appoint/" + $"{PersonGUID}/{GoalGUID}");
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + NetworkClient.Token);

            var response = (HttpWebResponse)request.GetResponse();
        }

        public void RemovePersonFromGoal(Guid PersonGUID, Guid GoalGUID)
        {
            var request = (HttpWebRequest)WebRequest.Create(NetworkClient.ServerPath + "/api/Personel/Dismiss/" + $"{PersonGUID}/{GoalGUID}");
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + NetworkClient.Token);

            var response = (HttpWebResponse)request.GetResponse();
        }       

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {

            }
            _disposed = true;
        }

        ~LinkedDataWorker()
        {
            Dispose(false);
        }
     
    }

}

