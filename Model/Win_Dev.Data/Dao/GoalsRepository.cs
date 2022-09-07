using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

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
        /*
        public virtual void Insert(TEntity entity)
        {         
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(Guid id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            _dbSet.Remove(entityToDelete);
            _context.SaveChanges();
        }

        public void Delete(TEntity entityToDelete)
        {
            _context.Entry(entityToDelete).State = EntityState.Deleted;
            _dbSet.Remove(entityToDelete);
            _context.SaveChanges();
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        */
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

