using Abstractions.Models;
using Abstractions.Repositories;
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

        IPersonRepository _repository;

        public PeopleController(IPersonRepository repository)
        {
            _repository = repository;
        }



        /// <summary>
        /// get all people
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Person>> Get()
        {
            return await _repository.GetAll();
        }

        /// <summary>
        /// get a person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<Person> Get(int id)
        {
            return await _repository.GetOne(id);
        }

        /// <summary>
        /// add a person
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Post([FromBody] Person value)
        {
            await _repository.Save(value);
        }

        /// <summary>
        /// update a person's details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task Put([FromBody] Person value)
        {
            var person = await _repository.GetOne(value.Id);
            if (person != null)
            {
                person = value;
                await _repository.Save(person);
            }

        }

        /// <summary>
        /// remove person
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var person = await _repository.GetOne(id);
            if (person != null)
            {
                person.IsDeleted = true;
                await _repository.Save(person);
            }
        }
    }
}
