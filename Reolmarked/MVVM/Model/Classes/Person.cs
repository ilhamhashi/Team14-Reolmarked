namespace Reolmarked.MVVM.Model.Classes
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationDate { get; set; }

        public Person(int personId, string firstName, string lastName, DateTime creationDate)
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
            CreationDate = creationDate;
        }

        public Person(string firstName, string lastName, DateTime creationDate)
        {
            FirstName = firstName;
            LastName = lastName;
            CreationDate = creationDate;
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
