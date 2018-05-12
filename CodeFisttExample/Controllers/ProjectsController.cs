using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeFisttExample.Models;

namespace CodeFisttExample.Controllers
{
    public class ProjectsController : Controller,IDisposable 
    {
        private readonly CustomerDbContext _dbContext;

        public ProjectsController()
        {
            _dbContext = new CustomerDbContext();
        }

        // GET: Projects
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetProjectsFor(int custId)
        {
            try
            {
              
                    var projectsData = (from c in _dbContext.Projects
                                        where c.CustomerId == custId
                                        select c).ToList();

                   var myProjectData = projectsData.Select(c => new ProjectsDTO { ProjectId  = c.ProjectId, CustomerId = c.CustomerId, Name = c.Name }).ToList();
               

                  

                    return Json(new { data = myProjectData }, JsonRequestBehavior.AllowGet);
             
            }
            catch (Exception)
            {
                throw;
            }
        }



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