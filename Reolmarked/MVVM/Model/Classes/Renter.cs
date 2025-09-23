namespace Reolmarked.MVVM.Model.Classes
{
    public class Renter : Person
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        public Renter(int personId, string firstName, string lastName, DateTime creationDate,
                      string phone, string email, string streetName,
                      string streetNumber, string zipCode, string city)
            : base(personId, firstName, lastName, creationDate)
        {
            Phone = phone;
            Email = email;
            StreetName = streetName;
            StreetNumber = streetNumber;
            ZipCode = zipCode;
            City = city;
        }

        public Renter(string firstName, string lastName, DateTime creationDate,
              string phone, string email, string streetName,
              string streetNumber, string zipCode, string city)
    : base(firstName, lastName, creationDate)
        {
            Phone = phone;
            Email = email;
            StreetName = streetName;
            StreetNumber = streetNumber;
            ZipCode = zipCode;
            City = city;
        }
    }
}
