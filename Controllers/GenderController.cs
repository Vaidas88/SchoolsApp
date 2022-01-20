using Microsoft.AspNetCore.Mvc;
using SchoolsApp.Dtos;
using SchoolsApp.Models;
using SchoolsApp.Repositories;
using System.Collections.Generic;

namespace SchoolsApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenderController : ControllerBase
    {
        private readonly GenderRepo _genderRepo;
        public GenderController(GenderRepo genderRepo)
        {
            _genderRepo = genderRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var gendersDto = new List<GenderDto>();

            var genders = _genderRepo.GetAll();

            genders.ForEach(gender => gendersDto.Add(
                new GenderDto()
                {
                    Id = gender.Id,
                    Name = gender.Name
                }));

            if (gendersDto != null)
            {
                return Ok(gendersDto);
            }

            return NotFound("No entries was found.");
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {
            var gender = _genderRepo.GetSingle(id);

            if (gender != null)
            {
                var genderDto = new GenderDto() { Id = gender.Id, Name = gender.Name };

                return Ok(genderDto);
            }

            return NotFound("Entry was not found.");
        }

        [HttpPost]
        public IActionResult Create(GenderDto genderDto)
        {
            if (genderDto.Id != 0)
            {
                return BadRequest("Only possible value for Id: 0");
            }

            try
            {
                var gender = new Gender() { Name = genderDto.Name };

                _genderRepo.Add(gender);
                _genderRepo.SaveChanges();

                return Ok("Entry created successfully.");
            }
            catch
            {
                return BadRequest("Wrong values supplied.");
            }
        }

        [HttpPut]
        public IActionResult Edit(GenderDto genderDto)
        {
            var gender = _genderRepo.GetSingle(genderDto.Id);

            if (gender == null)
            {
                return BadRequest("Such gender doesn't exist.");
            }

            try
            {
                gender.Name = genderDto.Name;

                _genderRepo.Update(gender);
                _genderRepo.SaveChanges();

                return Ok($"Entry was edited.");
            }
            catch
            {
                return BadRequest("Wrong values supplied.");
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var gender = _genderRepo.GetSingle(id);

            if (gender != null)
            {
                _genderRepo.Delete(gender);
                _genderRepo.SaveChanges();

                return Ok("Entry deleted successfully.");
            }

            return NotFound($"Entry with Id: {id} was not found. Nothing to delete.");
        }
    }
}
