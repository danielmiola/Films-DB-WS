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
    public class UsuariosController : ApiController
    {
        private IUsuariosRepository usuariosRepository;

        public UsuariosController()
        {
            this.usuariosRepository = new UsuariosRepository(new WEBApplicationEntities());
        }

        // GET api/Usuarios
        public IEnumerable<Usuarios> GetUsuarios()
        {
            return usuariosRepository.Get();
        }

        // GET api/Usuarios/5
        [ResponseType(typeof(Usuarios))]
        public IHttpActionResult GetUsuarios(int id)
        {
            Usuarios usuarios = usuariosRepository.GetByID(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return Ok(usuarios);
        }

        // PUT api/Usuarios/5
        public IHttpActionResult PutUsuarios(int id, Usuarios usuarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuarios.UsuarioID)
            {
                return BadRequest();
            }

            usuariosRepository.Update(usuarios);

            try
            {
                usuariosRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosExists(id))
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

        // POST api/Usuarios
        [ResponseType(typeof(Usuarios))]
        public IHttpActionResult PostUsuarios(Usuarios usuarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            usuariosRepository.Insert(usuarios);

            try
            {
                usuariosRepository.Save();
            }
            catch (DbUpdateException)
            {
                if (UsuariosExists(usuarios.UsuarioID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = usuarios.UsuarioID }, usuarios);
        }

        // DELETE api/Usuarios/5
        [ResponseType(typeof(Usuarios))]
        public IHttpActionResult DeleteUsuarios(int id)
        {
            Usuarios usuarios = usuariosRepository.GetByID(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            usuariosRepository.Delete(usuarios.UsuarioID);
            usuariosRepository.Save();

            return Ok(usuarios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                usuariosRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuariosExists(int id)
        {
            return usuariosRepository.Count(id) > 0;
        }
    }
}