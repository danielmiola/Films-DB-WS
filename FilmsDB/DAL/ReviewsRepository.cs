using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilmsDB.Models;
using System.Data.Entity;

namespace FilmsDB.DAL
{
    public class ReviewsRepository: IReviewsRepository, IDisposable
    {
        private WEBApplicationEntities context;

        public ReviewsRepository(WEBApplicationEntities context)
        {
            this.context = context;
        }

        public IEnumerable<Reviews> Get()
        {
            return context.Reviews.ToList();
        }

        public Reviews GetByID(int id)
        {
            return context.Reviews.Find(id);
        }

        public void Insert(Reviews review)
        {
            context.Reviews.Add(review);
        }

        public void Delete(int id)
        {
            Reviews review = context.Reviews.Find(id);
            context.Reviews.Remove(review);
        }

        public void Update(Reviews review)
        {
            context.Entry(review).State = EntityState.Modified;
        }

        public int Count(int id)
        {
            return context.Atores.Count(e => e.AtoresID == id);
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