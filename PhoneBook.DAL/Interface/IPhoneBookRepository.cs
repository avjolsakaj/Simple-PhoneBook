using PhoneBook.Models.Models;
using System.Collections.Generic;

namespace PhoneBook.DAL.Interface
{
    public interface IPhoneBookRepository
    {
        User Get(int id);

        List<User> GetAll();

        User Post(User phoneBook);

        User Put(User user);

        bool Delete(int id);

        List<Type> GetTypes();
    }
}
