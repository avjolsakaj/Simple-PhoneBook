using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models.Models
{
    public class User
    {
        public User()
        {
            UserTypes = new List<UserType>();
        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public bool Deleted { get; set; }

        public IEnumerable<UserType> UserTypes { get; set; }
    }
}
