using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auction
{
    public class Person
    {
        public string Login { get; private set; }
        public string FirstName { get; private set; }
        public string SecondName { get; private set; }

        public Person(string login, string firstName, string secondName)
        {
            Login = login;
            FirstName = firstName;
            SecondName = secondName;
        }
    }
}
