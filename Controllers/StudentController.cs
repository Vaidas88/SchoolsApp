using Microsoft.AspNetCore.Mvc;
using SchoolsApp.Dtos;
using SchoolsApp.Models;
using SchoolsApp.Repositories;
using System.Collections.Generic;

namespace SchoolsApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepo _studentRepo;
        private readonly GenderRepo _genderRepo;
        private readonly SchoolRepo _schoolRepo;

        public StudentController(StudentRepo studentRepo, GenderRepo genderRepo, SchoolRepo schoolRepo)
        {
            _studentRepo = studentRepo;
            _genderRepo = genderRepo;
            _schoolRepo = schoolRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var studentsDto = new List<StudentDto>();

            var students = _studentRepo.GetAll();

            students.ForEach(student => studentsDto.Add(
                new StudentDto()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Gender = new StudentGenderDto() { Id = student.Gender.Id, Name = student.Gender.Name },
                    School = new StudentSchoolDto() { Id = student.School.Id, Name = student.School.Name, Created = student.School.Created }
                }));
            if (studentsDto != null)
            {
                return Ok(studentsDto);
            }

            return NotFound("No entries was found.");
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {
            var student = _studentRepo.GetSingle(id);

            if (student != null)
            {
                var studentDto = new StudentDto()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Gender = new StudentGenderDto() { Id = student.Gender.Id, Name = student.Gender.Name },
                    School = new StudentSchoolDto() { Id = student.School.Id, Name = student.School.Name, Created = student.School.Created }
                };

                return Ok(studentDto);
            }

            return NotFound("Entry was not found.");
        }

        [HttpPost]
        public IActionResult Create(StudentDto studentDto)
        {
            if (studentDto.Id != 0)
            {
                return BadRequest("Only possible value for Id: 0");
            }

            try
            {
                var student = new Student()
                {
                    Name = studentDto.Name,
                    Gender = _genderRepo.GetSingle(studentDto.Gender.Id),
                    School = _schoolRepo.GetSingle(studentDto.School.Id)
                };

                _studentRepo.Add(student);
                _studentRepo.SaveChanges();

                return Ok("Entry created successfully.");
            }
            catch
            {
                return BadRequest("Wrong values supplied.");
            }
        }

        [HttpPut]
        public IActionResult Edit(StudentDto studentDto)
        {
            var student = _studentRepo.GetSingle(studentDto.Id);

            if (student == null)
            {
                return BadRequest("Such student doesn't exist.");
            }

            try
            {
                student.Name = studentDto.Name ?? student.Name;
                student.Gender = _genderRepo.GetSingle(studentDto.Gender.Id) ?? student.Gender;
                student.School = _schoolRepo.GetSingle(studentDto.School.Id) ?? student.School;

                _studentRepo.Add(student);
                _studentRepo.SaveChanges();

                return Ok("Entry was edited.");
            }
            catch
            {
                return BadRequest("Wrong values supplied.");
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var student = _studentRepo.GetSingle(id);

            if (student != null)
            {
                _studentRepo.Delete(student);
                _studentRepo.SaveChanges();

                return Ok("Entry deleted successfully.");
            }

            return NotFound($"Entry with Id: {id} was not found. Nothing to delete.");
        }
    }
}
