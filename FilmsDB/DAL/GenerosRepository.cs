using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilmsDB.Models;
using System.Data.Entity;

namespace FilmsDB.DAL
{
    public class GenerosRepository : IGenerosRepository, IDisposable
    {
        private WEBApplicationEntities context;

        public GenerosRepository(WEBApplicationEntities context)
        {
            this.context = context;
        }

        public IEnumerable<Generos> Get()
        {
            return context.Generos.ToList();
        }

        public Generos GetByID(int id)
        {
            return context.Generos.Find(id);
        }

        public void Insert(Generos genero)
        {
            context.Generos.Add(genero);
        }

        public void Delete(int id)
        {
            Generos genero = context.Generos.Find(id);
            context.Generos.Remove(genero);
        }

        public void Update(Generos genero)
        {
            context.Entry(genero).State = EntityState.Modified;
        }

        public int Count(int id)
        {
            return context.Generos.Count(e => e.GeneroID == id);
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