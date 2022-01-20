using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolsApp.Dtos
{
    public class SchoolStudentDto : EntityDto
    {
        public StudentGenderDto Gender { get; set; }
    }
}
