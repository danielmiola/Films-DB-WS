using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FilmsDB.Models;
using FilmsDB.DAL;

namespace FilmsDB.Controllers
{
    public class ReviewsController : ApiController
    {
        private IReviewsRepository reviewsRepository;

        public ReviewsController()
        {
            this.reviewsRepository = new ReviewsRepository(new WEBApplicationEntities());
        }

        // GET api/Reviews
        public IEnumerable<Reviews> GetReviews()
        {
            return reviewsRepository.Get();
        }

        // GET api/Reviews/5
        [ResponseType(typeof(Reviews))]
        public IHttpActionResult GetReviews(int id)
        {
            Reviews reviews = reviewsRepository.GetByID(id);
            if (reviews == null)
            {
                return NotFound();
            }

            return Ok(reviews);
        }

        // PUT api/Reviews/5
        public IHttpActionResult PutReviews(int id, Reviews reviews)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reviews.ReviewID)
            {
                return BadRequest();
            }

            reviewsRepository.Update(reviews);

            try
            {
                reviewsRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Reviews
        [ResponseType(typeof(Reviews))]
        public IHttpActionResult PostReviews(Reviews reviews)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            reviewsRepository.Insert(reviews);
            reviewsRepository.Save();

            return CreatedAtRoute("DefaultApi", new { id = reviews.ReviewID }, reviews);
        }

        // DELETE api/Reviews/5
        [ResponseType(typeof(Reviews))]
        public IHttpActionResult DeleteReviews(int id)
        {
            Reviews reviews = reviewsRepository.GetByID(id);
            if (reviews == null)
            {
                return NotFound();
            }

            reviewsRepository.Delete(reviews.ReviewID);
            reviewsRepository.Save();

            return Ok(reviews);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                reviewsRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReviewsExists(int id)
        {
            return reviewsRepository.Count(id) > 0;
        }
    }
}