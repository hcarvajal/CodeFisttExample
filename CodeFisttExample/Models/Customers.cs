using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeFisttExample.Models
{
    public class Customers
    {
        public Customers()
        {
            Projects = new HashSet<Projects>();
        }

        [Key]
        public int CustomerId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }


        public virtual ICollection<Projects> Projects { get; set; }
    }
}