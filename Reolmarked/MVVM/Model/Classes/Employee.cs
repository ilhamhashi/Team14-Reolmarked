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

    public class Employee : Person
    {
        public Role Role { get; set; }

        public Employee(int personId, string firstName, string lastName, DateTime creationDate, Role role) : base(personId, firstName, lastName, creationDate)
        {
            Role = role;
        }

        public Employee(string firstName, string lastName, DateTime creationDate, Role role) : base(firstName, lastName, creationDate)
        {
            Role = role;
        }
    }
}
