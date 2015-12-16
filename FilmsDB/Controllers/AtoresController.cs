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
    public class AtoresController : ApiController
    {
        private IAtoresRepository atoresRepository;
        
        public AtoresController()
        {
            this.atoresRepository = new AtoresRepository(new WEBApplicationEntities());
        }

        // GET api/Atores
        public IEnumerable<Atores> GetAtores()
        {
            return atoresRepository.Get();
        }

        // GET api/Atores/5
        [ResponseType(typeof(Atores))]
        public IHttpActionResult GetAtores(int id)
        {
            Atores atores = atoresRepository.GetByID(id);
            if (atores == null)
            {
                return NotFound();
            }

            return Ok(atores);
        }

        // PUT api/Atores/5
        public IHttpActionResult PutAtores(int id, Atores atores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != atores.AtoresID)
            {
                return BadRequest();
            }

            atoresRepository.Update(atores);

            try
            {
                atoresRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AtoresExists(id))
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

        // POST api/Atores
        [ResponseType(typeof(Atores))]
        public IHttpActionResult PostAtores(Atores atores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            atoresRepository.Insert(atores);
            atoresRepository.Save();

            return CreatedAtRoute("DefaultApi", new { id = atores.AtoresID }, atores);
        }

        // DELETE api/Atores/5
        [ResponseType(typeof(Atores))]
        public IHttpActionResult DeleteAtores(int id)
        {
            Atores atores = atoresRepository.GetByID(id);
            
            if (atores == null)
            {
                return NotFound();
            }

            atoresRepository.Delete(atores.AtoresID);
            atoresRepository.Save();

            return Ok(atores);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                atoresRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AtoresExists(int id)
        {
            return atoresRepository.Count(id) > 0;
        }
    }
}