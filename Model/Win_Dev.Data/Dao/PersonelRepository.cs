using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

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

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", NetworkClient.Token);
            var response = _client.GetAsync(NetworkClient.ServerPath + "/api/Personel").Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var valueSet = JsonConvert.DeserializeObject<List<Person>>(result);
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


        ~PersonelRepository()
        {
            Dispose(false);
        }
    }

}

