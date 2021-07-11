using Abstractions.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Services
{
   public interface IPeopleService
    {
        Task<List<string>> AddPerson(Person person);
        Task<List<string>> UpdatePerson(Person person);
        Task<List<string>> DeletePerson(int id);
        Task<Person> GetPerson(int id);
        Task<IEnumerable<Person>> GetPeople();
    }
}
