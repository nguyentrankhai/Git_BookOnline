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
    public class tbl_BookController : ApiController
    {
        private BookOnlineEntities db = new BookOnlineEntities();

        // GET: api/tbl_Book
        public IQueryable<tbl_Book> Gettbl_Book()
        {
            return db.tbl_Book;
        }

        // GET: api/tbl_Book/5
        [ResponseType(typeof(tbl_Book))]
        public IHttpActionResult Gettbl_Book(string id)
        {
            tbl_Book tbl_Book = db.tbl_Book.Find(id);
            if (tbl_Book == null)
            {
                return NotFound();
            }

            return Ok(tbl_Book);
        }

        // PUT: api/tbl_Book/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Book(string id, tbl_Book tbl_Book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Book.BookID)
            {
                return BadRequest();
            }

            db.Entry(tbl_Book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_BookExists(id))
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

        // POST: api/tbl_Book
        [ResponseType(typeof(tbl_Book))]
        public IHttpActionResult Posttbl_Book(tbl_Book tbl_Book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Book.Add(tbl_Book);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tbl_BookExists(tbl_Book.BookID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbl_Book.BookID }, tbl_Book);
        }

        // DELETE: api/tbl_Book/5
        [ResponseType(typeof(tbl_Book))]
        public IHttpActionResult Deletetbl_Book(string id)
        {
            tbl_Book tbl_Book = db.tbl_Book.Find(id);
            if (tbl_Book == null)
            {
                return NotFound();
            }

            db.tbl_Book.Remove(tbl_Book);
            db.SaveChanges();

            return Ok(tbl_Book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_BookExists(string id)
        {
            return db.tbl_Book.Count(e => e.BookID == id) > 0;
        }
    }
}