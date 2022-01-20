using SchoolsApp.Data;
using SchoolsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolsApp.Repositories
{
    public class GenderRepo : GenericRepo<Gender>
    {
        public GenderRepo(DataContext context) : base(context)
        {
        }
    }
}
