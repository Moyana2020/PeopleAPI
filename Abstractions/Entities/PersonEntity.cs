using Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abstractions.Entities
{
    public class PersonEntity : IEntity
    {
       
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
       
        public string Telephone { get; set; }
       
        public bool IsDeleted { get; set; }
       
        public int Id { get; set; }
       
    }
}
