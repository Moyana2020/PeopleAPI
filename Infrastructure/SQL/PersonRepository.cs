using Abstractions;
using Abstractions.Models;
using Abstractions.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.SQL
{
    public class PersonRepository : IPersonRepository
    {
        SQLContext _context;
       


        public PersonRepository(IOptions<AppSettings> config)
        {
            _context = new SQLContext(config);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Person>> GetAll()
        {
            var people = _context.People.Select(s => s);
            return people;
        }

        public async Task<Person> GetOne(int id)
        {
            var person = _context.People.Where(s=>s.Id == id).Select(s => s).FirstOrDefault();
            return person;
        }

        public async Task<int> Save(Person entity)
        {
            if(entity.Id != 0)
            {
                var person = await GetOne(entity.Id);
                if(person != null)
                {
                    _context.People.Remove(person);
                    _context.People.Add(entity);
                }
            }
            else
            {
                _context.People.Add(entity);
               
            }

            _context.SaveChanges();
            return 1;
        }
    }
}
