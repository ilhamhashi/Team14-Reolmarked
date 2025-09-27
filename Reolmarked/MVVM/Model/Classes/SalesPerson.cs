using System.ComponentModel;

namespace Reolmarked.MVVM.Model.Classes
{
    public enum Role
    {
        [Description("Medarbejder")]
        Employee,
        [Description("Ejer")]
        Owner
    }
    public class SalesPerson : Person
    {
        public Role Role { get; set; }

        public SalesPerson(int personId, string firstName, string lastName, DateTime creationDate, Role role) : base(personId, firstName, lastName, creationDate)
        {
            Role = role;
        }

        public SalesPerson(string firstName, string lastName, DateTime creationDate, Role role) : base(firstName, lastName, creationDate)
        {
            Role = role;
        }
    }
}
