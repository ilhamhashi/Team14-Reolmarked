using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Reolmarked.MVVM.Model.Classes
{
    internal abstract class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationDate { get; set; }

        protected User(int userId, string firstName, string lastName, DateTime creationDate)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            CreationDate = creationDate;
        }
    }
}
