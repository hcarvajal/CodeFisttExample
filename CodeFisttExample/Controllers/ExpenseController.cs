using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeFisttExample.Models;

namespace CodeFisttExample.Controllers
{
    public class ExpenseController : Controller
    {
        // GET: Expense
        public ActionResult ShowGrid()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LoadData()
        {

            try
            {
                //Creating instance of DatabaseContext class  
                using (CustomerDbContext _context = new CustomerDbContext())
                {

                    var expenseData = (from e in _context.Expenses
                                       join p in _context.Projects on e.ProjectId equals p.ProjectId
                                       select new
                                       {
                                           ExpenseId = e.ExpenseId,
                                           Name = e.Name,
                                           ProjectName = p.Name,
                                           Amout = e.Amout
                                       }).ToList();
                                       

                    return Json(new { data = expenseData }, JsonRequestBehavior.AllowGet);


                }
            }
            catch (Exception)
            {
                throw;
            }


        }


        [HttpPost]
        public ActionResult SaveExpense(Expenses e)
        {
            using (CustomerDbContext _context = new CustomerDbContext())
            {
                _context.Expenses.Add(e);
                _context.SaveChanges();
            }

            return Json(new { data = "ok" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult EditExpense(Expenses e)
        {
            using (CustomerDbContext _context = new CustomerDbContext())
            {
                var editExpense = (from c in _context.Expenses
                                   where c.ExpenseId == e.ExpenseId
                                   select c).FirstOrDefault();
                // _context.Entry(e).State = System.Data.Entity.EntityState.Modified;
                editExpense.ExpenseDate = e.ExpenseDate;
                editExpense.Name = e.Name;
                editExpense.ExpenseName = e.ExpenseName;
                editExpense.Amout = e.Amout;
                editExpense.Description = e.Description;
                editExpense.ProjectId = e.ProjectId;
                _context.SaveChanges();

            }

            return Json(new { data = "ok" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteExpense(int id)
        {
           try
            {
                using (CustomerDbContext _context = new CustomerDbContext())
                {
                    var expenseDelete = _context.Expenses.Find(id);
                    _context.Expenses.Remove(expenseDelete);
                    _context.SaveChanges();

                    return Json(new { data = "ok" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
           
 
        }


        public ActionResult GetExpenseBy(int id)
        {
            try
            {
                using (CustomerDbContext _context = new CustomerDbContext())
                {
                    var expenseData = _context.Expenses.Find(id);

                     

                    return Json(new { data = expenseData }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}