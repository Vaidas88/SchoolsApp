using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolsApp.Models
{
    public class Student : Entity
    {
        public Gender Gender { get; set; }

        public School School { get; set; }
    }
}
