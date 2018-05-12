using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeFisttExample.Models
{
    public class Expenses
    {
        [Key]
        public int ExpenseId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public DateTime ExpenseDate { get; set; }

        [StringLength(150)]
        public string ExpenseName { get; set; }

        public Double Amout { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public int ProjectId { get; set; }
        public Projects Projects { get; set; }
    }
}