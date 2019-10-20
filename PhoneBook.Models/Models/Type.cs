using System.Collections.Generic;

namespace PhoneBook.Models.Models
{
    public class Type
    {
        public Type()
        {
            UserTypes = new List<UserType>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool Deleted { get; set; }

        public IEnumerable<UserType> UserTypes { get; set; }
    }
}
