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
    public class GenerosController : ApiController
    {
        private IGenerosRepository generosRepository;

        public GenerosController()
        {
            generosRepository = new GenerosRepository(new WEBApplicationEntities());
        }

        // GET api/Generos
        public IEnumerable<Generos> GetGeneros()
        {
            return generosRepository.Get();
        }

        // GET api/Generos/5
        [ResponseType(typeof(Generos))]
        public IHttpActionResult GetGeneros(int id)
        {
            Generos generos = generosRepository.GetByID(id);
            if (generos == null)
            {
                return NotFound();
            }

            return Ok(generos);
        }

        // PUT api/Generos/5
        public IHttpActionResult PutGeneros(int id, Generos generos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != generos.GeneroID)
            {
                return BadRequest();
            }

            generosRepository.Update(generos);

            try
            {
                generosRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenerosExists(id))
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

        // POST api/Generos
        [ResponseType(typeof(Generos))]
        public IHttpActionResult PostGeneros(Generos generos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            generosRepository.Insert(generos);
            generosRepository.Save();

            return CreatedAtRoute("DefaultApi", new { id = generos.GeneroID }, generos);
        }

        // DELETE api/Generos/5
        [ResponseType(typeof(Generos))]
        public IHttpActionResult DeleteGeneros(int id)
        {
            Generos generos = generosRepository.GetByID(id);
            if (generos == null)
            {
                return NotFound();
            }

            generosRepository.Delete(generos.GeneroID);
            generosRepository.Save();

            return Ok(generos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                generosRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GenerosExists(int id)
        {
            return generosRepository.Count(id) > 0;
        }
    }
}