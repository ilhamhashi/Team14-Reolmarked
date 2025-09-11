using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reolmarked.MVVM.Model.Classes
{
    internal class Renter : User
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        public Renter(int userId, string firstName, string lastName, DateTime creationDate,
                      string phoneNumber, string email, string streetName,
                      string streetNumber, string zipCode, string city)
            : base(userId, firstName, lastName, creationDate)
        {
            PhoneNumber = phoneNumber;
            Email = email;
            StreetName = streetName;
            StreetNumber = streetNumber;
            ZipCode = zipCode;
            City = city;
        }

    }
}
