using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilmsDB.Models;
using System.Data.Entity;

namespace FilmsDB.DAL
{
    public class StudiosRepository: IStudiosRepository, IDisposable
    {
        private WEBApplicationEntities context;

        public StudiosRepository(WEBApplicationEntities context)
        {
            this.context = context;
        }

        public IEnumerable<Studios> Get()
        {
            return context.Studios.ToList();
        }

        public Studios GetByID(int id)
        {
            return context.Studios.Find(id);
        }

        public void Insert(Studios studio)
        {
            context.Studios.Add(studio);
        }

        public void Delete(int id)
        {
            Studios studio = context.Studios.Find(id);
            context.Studios.Remove(studio);
        }

        public void Update(Studios studio)
        {
            context.Entry(studio).State = EntityState.Modified;
        }

        public int Count(int id)
        {
            return context.Studios.Count(e => e.StudioID == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}