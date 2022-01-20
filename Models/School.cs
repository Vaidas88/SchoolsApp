using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolsApp.Models
{
    public class School : Entity
    {
        public DateTime Created { get; set; }

        public List<Student> Students { get; set; }
    }
}
