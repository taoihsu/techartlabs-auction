using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auction
{
    public class Person
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public Person(string firstName, string secondName)
        {
            FirstName = firstName;
            SecondName = secondName;
        }
    }
}
