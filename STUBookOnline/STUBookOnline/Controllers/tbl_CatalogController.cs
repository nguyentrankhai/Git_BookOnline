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
    public class tbl_CatalogController : ApiController
    {
        private BookOnlineEntities db = new BookOnlineEntities();

        // GET: api/tbl_Catalog
        public IQueryable<tbl_Catalog> Gettbl_Catalog()
        {
            return db.tbl_Catalog;
        }

        // GET: api/tbl_Catalog/5
        [ResponseType(typeof(tbl_Catalog))]
        public IHttpActionResult Gettbl_Catalog(string id)
        {
            tbl_Catalog tbl_Catalog = db.tbl_Catalog.Find(id);
            if (tbl_Catalog == null)
            {
                return NotFound();
            }

            return Ok(tbl_Catalog);
        }

        // PUT: api/tbl_Catalog/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Catalog(string id, tbl_Catalog tbl_Catalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Catalog.CatalogID)
            {
                return BadRequest();
            }

            db.Entry(tbl_Catalog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_CatalogExists(id))
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

        // POST: api/tbl_Catalog
        [ResponseType(typeof(tbl_Catalog))]
        public IHttpActionResult Posttbl_Catalog(tbl_Catalog tbl_Catalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Catalog.Add(tbl_Catalog);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tbl_CatalogExists(tbl_Catalog.CatalogID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbl_Catalog.CatalogID }, tbl_Catalog);
        }

        // DELETE: api/tbl_Catalog/5
        [ResponseType(typeof(tbl_Catalog))]
        public IHttpActionResult Deletetbl_Catalog(string id)
        {
            tbl_Catalog tbl_Catalog = db.tbl_Catalog.Find(id);
            if (tbl_Catalog == null)
            {
                return NotFound();
            }

            db.tbl_Catalog.Remove(tbl_Catalog);
            db.SaveChanges();

            return Ok(tbl_Catalog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_CatalogExists(string id)
        {
            return db.tbl_Catalog.Count(e => e.CatalogID == id) > 0;
        }
    }
}