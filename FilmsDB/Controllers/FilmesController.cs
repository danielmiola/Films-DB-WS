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
    public class FilmesController : ApiController
    {
        private IFilmesRepository filmesRepository;

        public FilmesController()
        {
            this.filmesRepository = new FilmesRepository(new WEBApplicationEntities());
        }
        
        // GET api/Filmes
        public IEnumerable<Filmes> GetFilmes()
        {
            return filmesRepository.Get();
        }

        // GET api/Filmes/5
        [ResponseType(typeof(Filmes))]
        public IHttpActionResult GetFilmes(int id)
        {
            Filmes filmes = filmesRepository.GetByID(id);
            if (filmes == null)
            {
                return NotFound();
            }

            return Ok(filmes);
        }

        // PUT api/Filmes/5
        public IHttpActionResult PutFilmes(int id, Filmes filmes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != filmes.FilmeID)
            {
                return BadRequest();
            }

            filmesRepository.Update(filmes);

            try
            {
                filmesRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmesExists(id))
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

        // POST api/Filmes
        [ResponseType(typeof(Filmes))]
        public IHttpActionResult PostFilmes(Filmes filmes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            filmesRepository.Insert(filmes);
            filmesRepository.Save();

            return CreatedAtRoute("DefaultApi", new { id = filmes.FilmeID }, filmes);
        }

        // DELETE api/Filmes/5
        [ResponseType(typeof(Filmes))]
        public IHttpActionResult DeleteFilmes(int id)
        {
            Filmes filmes = filmesRepository.GetByID(id);
            if (filmes == null)
            {
                return NotFound();
            }

            filmesRepository.Delete(filmes.FilmeID);
            filmesRepository.Save();

            return Ok(filmes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                filmesRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FilmesExists(int id)
        {
            return filmesRepository.Count(id) > 0;
        }
    }
}