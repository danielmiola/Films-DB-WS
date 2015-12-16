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
    public class StudiosController : ApiController
    {
        private IStudiosRepository studiosRepository;

        public StudiosController()
        {
            this.studiosRepository = new StudiosRepository(new WEBApplicationEntities());
        }

        // GET api/Studios
        public IEnumerable<Studios> GetStudios()
        {
            return studiosRepository.Get();
        }

        // GET api/Studios/5
        [ResponseType(typeof(Studios))]
        public IHttpActionResult GetStudios(int id)
        {
            Studios studios = studiosRepository.GetByID(id);
            if (studios == null)
            {
                return NotFound();
            }

            return Ok(studios);
        }

        // PUT api/Studios/5
        public IHttpActionResult PutStudios(int id, Studios studios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studios.StudioID)
            {
                return BadRequest();
            }

            studiosRepository.Update(studios);

            try
            {
                studiosRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudiosExists(id))
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

        // POST api/Studios
        [ResponseType(typeof(Studios))]
        public IHttpActionResult PostStudios(Studios studios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            studiosRepository.Insert(studios);
            studiosRepository.Save();

            return CreatedAtRoute("DefaultApi", new { id = studios.StudioID }, studios);
        }

        // DELETE api/Studios/5
        [ResponseType(typeof(Studios))]
        public IHttpActionResult DeleteStudios(int id)
        {
            Studios studios = studiosRepository.GetByID(id);
            if (studios == null)
            {
                return NotFound();
            }

            studiosRepository.Delete(studios.StudioID);
            studiosRepository.Save();

            return Ok(studios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                studiosRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudiosExists(int id)
        {
            return studiosRepository.Count(id) > 0;
        }
    }
}