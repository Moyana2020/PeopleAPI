
using Abstractions.DTOs;
using Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abstractions.Repositories
{
    public interface IPersonRepository : IRepository<PersonEntity, int>
    {

    }
}
