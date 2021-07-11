using Abstractions.DTOs;
using Abstractions.Entities;
using Abstractions.Repositories;
using Abstractions.Services;
using Core.Aggregates;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class PeopleService : IPeopleService
    {

        private readonly ILogger<PeopleService> _logger;
        private readonly IPersonRepository _repository;

        public PeopleService(ILogger<PeopleService> logger, IPersonRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// adds a new person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public async Task<List<string>> AddPerson(Person person)
        {
            //initialise aggregate
            _logger.LogInformation("Initialising.....");
            var aggregate = new PersonAggregate(new PersonEntity());
            var result = new List<string>();
            aggregate.ValidatePerson(person);
            if(aggregate.ResultMessages.Count < 1)
            {

                //save person details
                _logger.LogInformation("Saving person details.......");
                aggregate.SavePerson(person);
                await _repository.Save(aggregate.Entity);
            }
            else
            {
                result = aggregate.ResultMessages;
            }
            return result;
           
        }

        /// <summary>
        /// removes person details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<string>> DeletePerson(int id)
        {
            //load person record
            _logger.LogInformation("Loading person detials......");
            var entity = await _repository.GetOne(id);
            var result = new List<string>();

            if (entity == null)
            {
                result.Add("Person not found");
                return result;
            }

            var aggregate = new PersonAggregate(entity);
            if (aggregate.ResultMessages.Count < 1)
            {
                //save person 
                _logger.LogInformation("Saving person details.....");
                aggregate.DeletePerson();
                await _repository.Save(aggregate.Entity);
            }
            else
            {
                result = aggregate.ResultMessages;
            }
            return result;

        }

        /// <summary>
        /// gets all people details
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Person>> GetPeople()
        {
            var entities =  await _repository.GetAll();
            var people = new List<Person>();
            foreach(var entity in entities)
            {
                var person = new Person(entity);
                people.Add(person);
            }
            return people;
        }

        /// <summary>
        /// gets a person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Person> GetPerson(int id)
        {
            var entity = await _repository.GetOne(id);
            var person = new Person(entity);
            return person;
        }

        /// <summary>
        /// updates a persons details
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public async Task<List<string>> UpdatePerson(Person person)
        {
            //load person record
            _logger.LogInformation("Loading person detials......");
            var entity = await _repository.GetOne(person.Id);
            var result = new List<string>();

            if(entity == null)
            {
                result.Add("Person not found");
                return result;
            }

            var aggregate = new PersonAggregate(entity);
            aggregate.ValidatePerson(person);
            if (aggregate.ResultMessages.Count < 1)
            {
                //save person 
                _logger.LogInformation("Saving person details.....");
                aggregate.SavePerson(person);
                await _repository.Save(aggregate.Entity);
            }
            else
            {
                result = aggregate.ResultMessages;
            }
            return result;

        }
    }
}
