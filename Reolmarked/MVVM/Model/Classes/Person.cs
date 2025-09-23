namespace Reolmarked.MVVM.Model.Classes
{
    public abstract class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationDate { get; set; }

        protected Person(int personId, string firstName, string lastName, DateTime creationDate)
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
            CreationDate = creationDate;
        }

        protected Person(string firstName, string lastName, DateTime creationDate)
        {
            FirstName = firstName;
            LastName = lastName;
            CreationDate = creationDate;
        }
    }
}
