using Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abstractions.Repositories
{
    public interface IPersonRepository : IRepository<Person, int>
    {

    }
}
