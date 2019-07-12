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
using STUBookOnline.Models;

namespace STUBookOnline.Controllers
{
    public class tbl_BookofUserController : ApiController
    {
        private BookOnlineEntities db = new BookOnlineEntities();

        // GET: api/tbl_BookofUser
        public IQueryable<tbl_BookofUser> Gettbl_BookofUser()
        {
            return db.tbl_BookofUser;
        }

        // GET: api/tbl_BookofUser/5
        [ResponseType(typeof(tbl_BookofUser))]
        public IHttpActionResult Gettbl_BookofUser(int id)
        {
            tbl_BookofUser tbl_BookofUser = db.tbl_BookofUser.Find(id);
            if (tbl_BookofUser == null)
            {
                return NotFound();
            }

            return Ok(tbl_BookofUser);
        }
        // GET: api/tbl_BookofUser/5
        [ResponseType(typeof(tbl_BookofUser))]
        public IHttpActionResult Gettbl_BookofUser(string userid)
        {
            var tbl_BookofUser = db.tbl_BookofUser.Where(x =>x.USERID == userid).ToList();
            if (tbl_BookofUser == null)
            {
                return NotFound();
            }

            return Ok(tbl_BookofUser);
        }

        // PUT: api/tbl_BookofUser/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_BookofUser(int id, tbl_BookofUser tbl_BookofUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_BookofUser.ID)
            {
                return BadRequest();
            }

            db.Entry(tbl_BookofUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_BookofUserExists(id))
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

        // POST: api/tbl_BookofUser
        [ResponseType(typeof(tbl_BookofUser))]
        public IHttpActionResult Posttbl_BookofUser(tbl_BookofUser tbl_BookofUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_BookofUser.Add(tbl_BookofUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tbl_BookofUserExists(tbl_BookofUser.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbl_BookofUser.ID }, tbl_BookofUser);
        }

        // DELETE: api/tbl_BookofUser/5
        [ResponseType(typeof(tbl_BookofUser))]
        public IHttpActionResult Deletetbl_BookofUser(int id)
        {
            tbl_BookofUser tbl_BookofUser = db.tbl_BookofUser.Find(id);
            if (tbl_BookofUser == null)
            {
                return NotFound();
            }

            db.tbl_BookofUser.Remove(tbl_BookofUser);
            db.SaveChanges();

            return Ok(tbl_BookofUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_BookofUserExists(int id)
        {
            return db.tbl_BookofUser.Count(e => e.ID == id) > 0;
        }
    }
}