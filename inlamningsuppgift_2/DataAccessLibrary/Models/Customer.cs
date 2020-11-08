using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class Customer
    {
        public Customer()
        {

        }

        public Customer(long id, string firstName, string lastName, DateTime created)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Created = created;
        }

        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Created { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
