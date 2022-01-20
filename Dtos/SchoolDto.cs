using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolsApp.Dtos
{
    public class SchoolDto : EntityDto
    {
        public DateTime Created { get; set; }

        public List<SchoolStudentDto> Students { get; set; }
    }
}
