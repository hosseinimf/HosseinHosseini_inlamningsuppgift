using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Person
    {

        [JsonProperty(propertyName: "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(propertyName: "lastName")]
        public string LastName { get; set; }

        [JsonProperty(propertyName: "age")]
        public int Age { get; set; }

        [JsonProperty(propertyName: "email")]
        public string Email { get; set; }


        public string DisplayName => $"{FirstName} {LastName}";


        public Person(string firstName, string lastName, int age, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Email = email;
        }


        public Person()
        {

        }
    }
}
