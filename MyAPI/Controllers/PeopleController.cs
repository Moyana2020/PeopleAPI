using Abstractions.DTOs;
using Abstractions.Repositories;
using Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyAPI.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PeopleController : ControllerBase
    {

        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }



        /// <summary>
        /// get all people
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Person>> Get()
        {
            var result = await _peopleService.GetPeople();
            return result;
        }

        /// <summary>
        /// get a person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<Person> Get(int id)
        {
            return await _peopleService.GetPerson(id);
        }

        /// <summary>
        /// add a person
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person value)
        {
            var result = await _peopleService.AddPerson(value);
            if(result.Count < 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// update a person's details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Person value)
        {
            var result = await _peopleService.UpdatePerson(value);
            if (result.Count < 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result);
            }

        }

        /// <summary>
        /// remove person
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _peopleService.DeletePerson(id);
            if (result.Count < 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
