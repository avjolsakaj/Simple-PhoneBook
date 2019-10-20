using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models.Models
{
    public class UserType
    {
        public UserType()
        {
            User = new User();
            Type = new Type();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Number { get; set; }

        public bool Deleted { get; set; }

        public User User { get; set; }

        public Type Type { get; set; }
    }
}
