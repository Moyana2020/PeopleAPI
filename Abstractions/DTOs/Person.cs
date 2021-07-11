using Abstractions.Entities;
using System.Runtime.Serialization;

namespace Abstractions.DTOs
{

    [DataContract]
    public class Person
    {

        public Person()
        {

        }
        public Person(PersonEntity entity)
        {
            this.FirstName = entity.FirstName;
            this.LastName = entity.LastName;
            this.Telephone = entity.Telephone;
            this.Id = entity.Id;
        }

        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public int Id { get; set; }
    }
}
