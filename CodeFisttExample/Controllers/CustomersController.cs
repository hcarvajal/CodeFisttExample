using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeFisttExample.Models;

namespace CodeFisttExample.Controllers
{
    public class CustomersController : Controller, IDisposable
    {
        private readonly CustomerDbContext _dbContext;

        public CustomersController()
        {
            _dbContext = new CustomerDbContext();
        }


        // GET: Customers
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }


        public ActionResult LoadCustomers()
        {
            var customerData = _dbContext.Customers.Select(c => new CustomerDto { Id = c.CustomerId, Name = c.Name, LastName = c.LastName }).ToList();
           
            return Json(new { data = customerData }, JsonRequestBehavior.AllowGet);
        }  // end load customers

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}