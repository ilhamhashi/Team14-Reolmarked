using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reolmarked.MVVM.Model.Classes
{
    public class Employee : User
    {
        //public string UserName { get; set; }
        //public string Password { get; set; }
        public string Role { get; set; }

        public Employee(int userId, string firstName, string lastName, DateTime creationDate,
                        /*string userName, string password,*/ string role)
            : base(userId, firstName, lastName, creationDate)
        {
            //UserName = userName;
            //Password = password;
            Role = role;
        }
    }
}
