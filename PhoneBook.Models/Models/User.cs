using System.Collections.Generic;

namespace PhoneBook.Models.Models
{
    public class User
    {
        public User()
        {
            UserTypes = new List<UserType>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Deleted { get; set; }

        public IEnumerable<UserType> UserTypes { get; set; }
    }
}
