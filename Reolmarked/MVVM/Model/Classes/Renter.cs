using System.Net;

namespace Reolmarked.MVVM.Model.Classes
{
    public class Renter : Person
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public Renter(int personId, string firstName, string lastName, string phone, string email, string address, DateTime creationDate) : base(personId, firstName, lastName, creationDate)
        {
            Phone = phone;
            Email = email;
            Address = address;
        }

        public Renter(string firstName, string lastName, string phone, string email, string address, DateTime creationDate) : base(firstName, lastName, creationDate)
        {
            Phone = phone;
            Email = email;
            Address = address;
        }


    }
}
