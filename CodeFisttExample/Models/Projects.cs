using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeFisttExample.Models
{
    public class Projects
    {

        [Key]
        public int ProjectId { get; set; }

        [StringLength(250)]
        public string CustomrId { get; set; }

        public string Name { get; set; }

        public int CustomerId { get; set; }
        public Customers Customers { get; set; }
    }
}