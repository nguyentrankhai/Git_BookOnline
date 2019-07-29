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
    public class tbl_AccountController : ApiController
    {
        private BookOnlineEntities db = new BookOnlineEntities();

        // GET: api/tbl_Account
        public IQueryable<tbl_Account> Gettbl_Account()
        {
            return db.tbl_Account;
        }

        // GET: api/tbl_Account/5
        [ResponseType(typeof(tbl_Account))]
        public IHttpActionResult Gettbl_Account(string id)
        {
            tbl_Account tbl_Account = db.tbl_Account.Find(id);
            if (tbl_Account == null)
            {
                return NotFound();
            }

            return Ok(tbl_Account);
        }

        public IHttpActionResult Gettbl_Account(string id, string pwd)
        {
            pwd = EncodingText.Base64Decode(pwd);
            tbl_Account tbl_Account = db.tbl_Account.Find(id);
            if (tbl_Account == null || tbl_Account.PWD != pwd)
            {
                return NotFound();
            }

            return Ok(tbl_Account);
        }

        // PUT: api/tbl_Account/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Account(string id, tbl_Account tbl_Account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Account.AccountID)
            {
                return BadRequest();
            }

            db.Entry(tbl_Account).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_AccountExists(id))
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

        // POST: api/tbl_Account
        [ResponseType(typeof(tbl_Account))]
        public IHttpActionResult Posttbl_Account(tbl_Account tbl_Account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Account.Add(tbl_Account);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tbl_AccountExists(tbl_Account.AccountID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbl_Account.AccountID }, tbl_Account);
        }

        // DELETE: api/tbl_Account/5
        [ResponseType(typeof(tbl_Account))]
        public IHttpActionResult Deletetbl_Account(string id)
        {
            tbl_Account tbl_Account = db.tbl_Account.Find(id);
            if (tbl_Account == null)
            {
                return NotFound();
            }

            db.tbl_Account.Remove(tbl_Account);
            db.SaveChanges();

            return Ok(tbl_Account);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_AccountExists(string id)
        {
            return db.tbl_Account.Count(e => e.AccountID == id) > 0;
        }
    }
}