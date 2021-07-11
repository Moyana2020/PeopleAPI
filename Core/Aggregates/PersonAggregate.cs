using Abstractions.DTOs;
using Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Aggregates
{
    public class PersonAggregate : BaseAggregate<PersonEntity>
    {
        public PersonAggregate(PersonEntity entity) : base(entity)
        {

        }

        /// <summary>
        /// saves a persons details 
        /// </summary>
        /// <param name="person"></param>
        public void SavePerson(Person person)
        {
            PopulateEntity(person);
        }

        /// <summary>
        /// removes person details
        /// </summary>
        public void DeletePerson()
        {
            Entity.IsDeleted = true;
        }


        /// <summary>
        /// validates persons details
        /// </summary>
        /// <param name="person"></param>
        public void ValidatePerson(Person person)
        {
            if (string.IsNullOrEmpty(person.FirstName))
            {
                AddMessage("First name is required");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="person"></param>
        private void PopulateEntity(Person person)
        {
            Entity.FirstName = person.FirstName;
            Entity.LastName = person.LastName;
            Entity.Telephone = person.Telephone;
        }
    }
}
