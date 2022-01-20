using Microsoft.EntityFrameworkCore;
using SchoolsApp.Data;
using SchoolsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolsApp.Repositories
{
    public class StudentRepo : GenericRepo<Student>
    {
        public StudentRepo(DataContext context) : base(context)
        {
        }

        public override List<Student> GetAll()
        {
            return _dbSet.Include(s => s.Gender).Include(s => s.School).ToList();
        }
    }
}
