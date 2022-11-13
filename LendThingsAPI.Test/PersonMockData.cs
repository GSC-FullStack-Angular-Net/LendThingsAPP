using LendThingsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendThingsAPI.Test
{
    public static class PersonMockData
    {
        private static List<Person> People { get; set; }

        static public List<Person> GetPersonList()
        {
            if(People is null)
            {
                People = new List<Person>()
                    {
                        new Person { Id = 1, Name = "Fist", Email = "a@a.com", PhoneNumber = "111111111" },
                        new Person { Id = 2, Name = "Second", Email = "b@b.com", PhoneNumber = "222222222" },
                        new Person { Id = 3, Name = "Third", Email = "c@c.com", PhoneNumber = "333333333" }
                    };
            }
            return People;
        }
    }
}
