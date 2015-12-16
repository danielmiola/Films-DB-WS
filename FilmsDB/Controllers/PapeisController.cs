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
    public class PapeisController : ApiController
    {
        private IPapeisRepository papeisRepository;

        public PapeisController()
        {
            this.papeisRepository = new PapeisRepository(new WEBApplicationEntities());
        }

        // GET api/Papeis
        public IEnumerable<Papeis> GetPapeis()
        {
            return papeisRepository.Get();
        }

        // GET api/Papeis/5
        [ResponseType(typeof(Papeis))]
        public IHttpActionResult GetPapeis(int Fid, int Aid)
        {
            Papeis papeis = papeisRepository.GetByID(Fid, Aid);
            if (papeis == null)
            {
                return NotFound();
            }

            return Ok(papeis);
        }

        // PUT api/Papeis/5
        public IHttpActionResult PutPapeis(int Fid, int Aid, Papeis papeis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Fid != papeis.FilmeID || Aid != papeis.AtorID)
            {
                return BadRequest();
            }

            papeisRepository.Update(papeis);

            try
            {
                papeisRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PapeisExists(Fid, Aid))
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

        // POST api/Papeis
        [ResponseType(typeof(Papeis))]
        public IHttpActionResult PostPapeis(Papeis papeis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            papeisRepository.Insert(papeis);

            try
            {
                papeisRepository.Save();
            }
            catch (DbUpdateException)
            {
                if (PapeisExists(papeis.FilmeID, papeis.AtorID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = papeis.FilmeID }, papeis);
        }

        // DELETE api/Papeis/5
        [ResponseType(typeof(Papeis))]
        public IHttpActionResult DeletePapeis(int Fid, int Aid)
        {
            Papeis papeis = papeisRepository.GetByID(Fid, Aid);
            if (papeis == null)
            {
                return NotFound();
            }

            papeisRepository.Delete(papeis.FilmeID, papeis.AtorID);
            papeisRepository.Save();

            return Ok(papeis);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                papeisRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PapeisExists(int Fid, int Aid)
        {
            return papeisRepository.Count(Fid, Aid) > 0;
        }
    }
}