using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace Abstractions.Models
{

    [DataContract]
   public class Person : IEntity
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Telephone { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
        [DataMember]
        public int Id { get; set; }
    }
}
