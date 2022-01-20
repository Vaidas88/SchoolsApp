using Microsoft.AspNetCore.Mvc;
using SchoolsApp.Dtos;
using SchoolsApp.Models;
using SchoolsApp.Repositories;
using System.Collections.Generic;

namespace SchoolsApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchoolController : ControllerBase
    {
        private readonly SchoolRepo _schoolRepo;
        public SchoolController(SchoolRepo schoolRepo)
        {
            _schoolRepo = schoolRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var schoolsDto = new List<SchoolDto>();

            var schools = _schoolRepo.GetAll();

            schools.ForEach(school => schoolsDto.Add(
                new SchoolDto()
                {
                    Id = school.Id,
                    Name = school.Name,
                    Created = school.Created,
                    Students = getSchoolStudents(school.Students),
                }));
            if (schoolsDto != null)
            {
                return Ok(schoolsDto);
            }

            return NotFound("No entries was found.");
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {
            var school = _schoolRepo.GetSingle(id);

            if (school != null)
            {
                var schoolDto = new SchoolDto()
                {
                    Id = school.Id,
                    Name = school.Name,
                    Created = school.Created,
                    Students = getSchoolStudents(school.Students),
                };

                return Ok(schoolDto);
            }

            return NotFound("Entry was not found.");
        }

        [HttpPost]
        public IActionResult Create(SchoolDto schoolDto)
        {
            if (schoolDto.Id != 0)
            {
                return BadRequest("Only possible value for Id: 0");
            }

            try
            {
                var school = new School()
                {
                    Name = schoolDto.Name,
                    Created = schoolDto.Created
                };

                _schoolRepo.Add(school);
                _schoolRepo.SaveChanges();

                return Ok("Entry created successfully.");
            }
            catch
            {
                return BadRequest("Wrong values supplied.");
            }

        }

        [HttpPut]
        public IActionResult Edit(SchoolDto schoolDto)
        {
            var school = _schoolRepo.GetSingle(schoolDto.Id);

            if (school == null)
            {
                return BadRequest("Such school doesn't exist.");
            }

            try
            {
                school.Name = schoolDto.Name;
                school.Created = schoolDto.Created;

                _schoolRepo.Update(school);
                _schoolRepo.SaveChanges();

                return Ok($"Entry was edited.");
            }
            catch
            {
                return NotFound("Wrong values supplied.");
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var school = _schoolRepo.GetSingle(id);

            if (school != null)
            {
                _schoolRepo.Delete(school);
                _schoolRepo.SaveChanges();

                return Ok("Entry deleted successfully.");
            }

            return NotFound($"Entry with Id: {id} was not found. Nothing to delete.");
        }
        private List<SchoolStudentDto> getSchoolStudents(List<Student> students)
        {
            var studentsDto = new List<SchoolStudentDto>();

            students.ForEach(student => studentsDto.Add(new SchoolStudentDto()
            {
                Id = student.Id,
                Name = student.Name,
                Gender = new StudentGenderDto() { Id = student.Gender.Id, Name = student.Gender.Name }
            }));

            return studentsDto;
        }

    }
}
