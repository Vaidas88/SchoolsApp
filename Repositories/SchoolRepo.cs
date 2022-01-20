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
    public class SchoolRepo : GenericRepo<School>
    {
        public SchoolRepo(DataContext context) : base(context)
        {
        }

        public override List<School> GetAll()
        {
            return _dbSet.Include(s => s.Students).ThenInclude(s => s.Gender).ToList();
        }
    }
}
