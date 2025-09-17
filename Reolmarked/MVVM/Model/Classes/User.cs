namespace Reolmarked.MVVM.Model.Classes
{
    public abstract class User
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

        protected User(string firstName, string lastName, DateTime creationDate)
        {
            FirstName = firstName;
            LastName = lastName;
            CreationDate = creationDate;
        }
    }
}
