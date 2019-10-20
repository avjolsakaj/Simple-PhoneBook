using PhoneBook.DTO.DTO;
using System.Collections.Generic;

namespace PhoneBook.BLL.Interface
{
    public interface IPhoneBookService
    {
        IEnumerable<PhoneBookOutDTO> GetAllOrderedBy(bool orderByFirstName, bool asc);

        PhoneBookOutDTO Get(int id);

        PhoneBookOutDTO Post(PhoneBookInDTO phoneBook);

        PhoneBookOutDTO AddNumber(PhoneBookInDTO phoneBook);

        PhoneBookOutDTO Put(PhoneBookInDTO phoneBook);

        bool Delete(int id);

        List<TypesOutDTO> GetTypes();
    }
}
