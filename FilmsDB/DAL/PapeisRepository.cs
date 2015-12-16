using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilmsDB.Models;
using System.Data.Entity;

namespace FilmsDB.DAL
{
    public class PapeisRepository: IPapeisRepository, IDisposable
    {
        private WEBApplicationEntities context;

        public PapeisRepository(WEBApplicationEntities context)
        {
            this.context = context;
        }

        public IEnumerable<Papeis> Get()
        {
            return context.Papeis.ToList();
        }

        public Papeis GetByID(int Fid, int Aid)
        {
            return context.Papeis.Where(e => e.FilmeID == Fid && e.AtorID == Aid).FirstOrDefault();
        }

        public void Insert(Papeis papel)
        {
            context.Papeis.Add(papel);
        }

        public void Delete(int Fid, int Aid)
        {
            Papeis papel = context.Papeis.Where(e => e.FilmeID == Fid && e.AtorID == Aid).FirstOrDefault();
            context.Papeis.Remove(papel);
        }

        public void Update(Papeis papel)
        {
            context.Entry(papel).State = EntityState.Modified;
        }

        public int Count(int Fid, int Aid)
        {
            return context.Papeis.Count(e => e.FilmeID == Fid && e.AtorID == Aid);
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