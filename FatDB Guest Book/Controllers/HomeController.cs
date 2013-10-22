using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FatCloud.Client.FatDB;
using FatCloud.Client.QueryProvider;
using FatDBGuestBook.Models;

namespace FatDBGuestBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly FatDBConnection _fatDbConnection;

        public HomeController(FatDBConnection fatDbConnection)
        {
            _fatDbConnection = fatDbConnection;
        }

        public ActionResult Index()
        {
            var factory = new FatDBQueryableFactory(_fatDbConnection);
            IEnumerable<GuestBookEntry> entries;
            try
            {
                entries = factory.Queryable<GuestBookEntry>().ToList();
            }
            catch (NullReferenceException)
            {
                /* If the database hasn't been created factory.Queryable throws a NPE */
                entries = Enumerable.Empty<GuestBookEntry>();
            }
            var model = new GuestBookEntries {Entries = entries};
            return View(model);
        }

        [HttpPost]
        public ActionResult AddComment(string comment)
        {
            var c = _fatDbConnection.CreateBasicClient<GuestBookEntry>();
            c.InsertRecord(new GuestBookEntry { Comment = comment });
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _fatDbConnection.Dispose();
        }
    }
}
