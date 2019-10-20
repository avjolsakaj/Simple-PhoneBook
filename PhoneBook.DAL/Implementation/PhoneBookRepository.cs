using Newtonsoft.Json.Linq;
using PhoneBook.DAL.Interface;
using PhoneBook.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PhoneBook.DAL.Implementation
{
    public class PhoneBookRepository : IPhoneBookRepository
    {
        readonly string DatabasePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "PhoneBook.DAL", "Database");

        readonly string _typeJson;
        readonly string _userTypeJson;
        readonly string _userJson;

        public PhoneBookRepository()
        {
            _typeJson = Path.Combine(DatabasePath, "type.json");
            _userTypeJson = Path.Combine(DatabasePath, "usertype.json");
            _userJson = Path.Combine(DatabasePath, "user.json");
        }

        public bool Delete(int id)
        {
            var userJson = File.ReadAllText(_userJson);
            var users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(userJson).ToList();

            users.Where(x => x.Id == id).ToList().ForEach(x =>
            {
                x.Deleted = true;
            });

            string outputUser = Newtonsoft.Json.JsonConvert.SerializeObject(users, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_userJson, outputUser);

            var userTypeJson = File.ReadAllText(_userTypeJson);
            var usertypes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserType>>(userTypeJson).ToList();

            usertypes.Where(x => x.UserId == id).ToList().ForEach(x =>
            {
                x.Deleted = true;
            });

            string outputUserType = Newtonsoft.Json.JsonConvert.SerializeObject(usertypes, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_userTypeJson, outputUserType);

            return true;
        }

        public User Get(int id)
        {
            var userJson = File.ReadAllText(_userJson);
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(userJson).FirstOrDefault(x => !x.Deleted && x.Id == id);

            var userTypeJson = File.ReadAllText(_userTypeJson);
            var usertypes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserType>>(userTypeJson).Where(x => !x.Deleted && x.UserId == user.Id).ToList();

            var typeJson = File.ReadAllText(_typeJson);
            var types = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Models.Type>>(typeJson).Where(x => !x.Deleted).ToList();

            usertypes.ForEach(x => x.Type = types.FirstOrDefault(y => y.Id == x.TypeId));
            user.UserTypes = usertypes;

            return user;
        }

        public List<User> GetAll()
        {
            var userJson = File.ReadAllText(_userJson);
            var users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(userJson).Where(x => !x.Deleted).ToList();

            var userTypeJson = File.ReadAllText(_userTypeJson);
            var usertypes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserType>>(userTypeJson).Where(x => !x.Deleted).ToList();

            var typeJson = File.ReadAllText(_typeJson);
            var types = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Models.Type>>(typeJson).Where(x => !x.Deleted).ToList();

            usertypes.ForEach(x => x.Type = types.FirstOrDefault(y => y.Id == x.TypeId));
            users.ForEach(x => x.UserTypes = usertypes.Where(y => y.UserId == x.Id));

            return users;
        }

        public List<Models.Models.Type> GetTypes()
        {
            try
            {
                var json = File.ReadAllText(_typeJson);
                var types = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Models.Type>>(json);

                var result = types.Where(x => !x.Deleted).ToList();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public User Post(User phoneBook)
        {
            throw new NotImplementedException();
        }

        public User Put(User user)
        {
            throw new NotImplementedException();
        }
    }
}
