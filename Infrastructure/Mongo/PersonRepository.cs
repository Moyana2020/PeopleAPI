using Abstractions;
using Abstractions.Models;
using Abstractions.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mongo
{
    public class PersonRepository : IPersonRepository
    {
        private readonly MongoContext _context;

        public PersonRepository(IOptions<AppSettings> config)
        {
            _context = new MongoContext(config);
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            FilterDefinition<Person> filter = Builders<Person>.Filter.Ne(s => s.IsDeleted, true);
            List<Person> people = await _context.People.Find(filter).ToListAsync();
            return people;
        }

        public async Task<Person> GetOne(int id)
        {
            var filter = Builders<Person>.Filter.Eq("_id", id);
            var person = (await _context.People.FindAsync(filter)).FirstOrDefault();
            return person;
        }

        public async Task<int> Save(Person entity)
        {
            FilterDefinition<Person> filter = Builders<Person>.Filter.Eq("_id", entity.Id);
            var result = await _context.People.FindAsync(filter);

            if (result.Any())
            {
                await _context.People.ReplaceOneAsync(filter, entity);
            }
            else
            {
                await _context.People.InsertOneAsync(entity);
            }

            return entity.Id;
        }
    }
}
