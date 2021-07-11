using Abstractions;
using Abstractions.Entities;
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

        public async Task<IEnumerable<PersonEntity>> GetAll()
        {
            FilterDefinition<PersonEntity> filter = Builders<PersonEntity>.Filter.Ne(s => s.IsDeleted, true);
            List<PersonEntity> people = await _context.People.Find(filter).ToListAsync();
            return people;
        }

        public async Task<PersonEntity> GetOne(int id)
        {
            var filter = Builders<PersonEntity>.Filter.Eq("_id", id);
            var person = (await _context.People.FindAsync(filter)).FirstOrDefault();
            return person;
        }

        public async Task<int> Save(PersonEntity entity)
        {
            FilterDefinition<PersonEntity> filter = Builders<PersonEntity>.Filter.Eq("_id", entity.Id);
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
