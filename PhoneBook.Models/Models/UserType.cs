namespace PhoneBook.Models.Models
{
    public class UserType
    {
        public UserType()
        {
            User = new User();
            Type = new Type();
        }

        public int Id { get; set; }

        public int TypeId { get; set; }

        public int UserId { get; set; }

        public string Number { get; set; }

        public bool Deleted { get; set; }

        public User User { get; set; }

        public Type Type { get; set; }
    }
}
