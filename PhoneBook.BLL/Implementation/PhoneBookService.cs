using PhoneBook.BLL.Interface;
using PhoneBook.DAL.Interface;
using PhoneBook.DTO.DTO;
using PhoneBook.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace PhoneBook.BLL.Implementation
{
    public class PhoneBookService : IPhoneBookService
    {
        private readonly IPhoneBookRepository _phoneBookRepository;

        public PhoneBookService(IPhoneBookRepository phoneBookRepository)
        {
            _phoneBookRepository = phoneBookRepository;
        }

        public PhoneBookOutDTO Get(int id)
        {
            var user = _phoneBookRepository.Get(id);

            return UserToPhoneBookConverter(user);
        }

        public IEnumerable<PhoneBookOutDTO> GetAllOrderedBy(bool orderByFirstName, bool asc)
        {
            var user = _phoneBookRepository.GetAll();

            IEnumerable<PhoneBookOutDTO> result = null;

            var users = user.Select(x => new PhoneBookOutDTO
            {
                Id = x.Id,
                Firstname = x.FirstName,
                Lastname = x.LastName,
                NumberInfo = x.UserTypes.Select(y => new NumberInfoOutDTO
                {
                    Id = y.Id,
                    Number = y?.Number ?? string.Empty,
                    Type = y?.Type?.Name ?? string.Empty
                }).ToList()
            }).ToList();


            if (!asc)
            {
                if (!orderByFirstName)
                {
                    result = users.OrderByDescending(x => x.Lastname);
                }
                else
                {
                    result = users.OrderByDescending(x => x.Firstname);
                }
            }
            else
            {
                if (!orderByFirstName)
                {
                    result = users.OrderBy(x => x.Lastname);
                }
                else
                {
                    result = users.OrderBy(x => x.Firstname);
                }
            }

            return result;
        }

        public List<TypesOutDTO> GetTypes()
        {
            var types = _phoneBookRepository.GetTypes();

            var result = types.Select(x => new TypesOutDTO
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            return result;
        }

        public PhoneBookOutDTO Post(PhoneBookInDTO phoneBook)
        {
            var user = PhoneBookToUser(phoneBook);

            var createdUser = _phoneBookRepository.Post(user);

            return UserToPhoneBookConverter(createdUser);
        }

        public PhoneBookOutDTO Put(PhoneBookInDTO phoneBook)
        {
            var user = PhoneBookToUser(phoneBook);

            var updatedUser = _phoneBookRepository.Put(user);

            return UserToPhoneBookConverter(updatedUser);
        }

        public bool Delete(int id)
        {
            bool deleted = _phoneBookRepository.Delete(id);

            return deleted;
        }
        private static PhoneBookOutDTO UserToPhoneBookConverter(User user)
        {
            return new PhoneBookOutDTO
            {
                Id = user.Id,
                Firstname = user.FirstName,
                Lastname = user.LastName,
                NumberInfo = user.UserTypes.Select(x => new NumberInfoOutDTO
                {
                    Id = x.Id,
                    Number = x?.Number ?? string.Empty,
                    Type = x?.Type?.Name ?? string.Empty
                }).ToList()
            };
        }

        private static User PhoneBookToUser(PhoneBookInDTO phoneBook)
        {
            return new User
            {
                Id = phoneBook.Id,
                FirstName = phoneBook.Firstname,
                LastName = phoneBook.Lastname,
                UserTypes = phoneBook.NumberInfo.Select(x => new UserType
                {
                    Id = x.Id,
                    Number = x.Number,
                    TypeId = x.TypeId
                }).ToList(),
                Deleted = phoneBook.Deleted
            };
        }

        public PhoneBookOutDTO AddNumber(PhoneBookInDTO phoneBook)
        {
            var user = PhoneBookToUser(phoneBook);

            var createdUser = _phoneBookRepository.AddNumber(user);

            return UserToPhoneBookConverter(createdUser);
        }
    }
}
