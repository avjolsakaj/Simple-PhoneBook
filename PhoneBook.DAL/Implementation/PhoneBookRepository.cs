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
            return GetUser(id);
        }

        private User GetUser(int id)
        {
            // Get User
            var userJson = File.ReadAllText(_userJson);
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(userJson).FirstOrDefault(x => !x.Deleted && x.Id == id);

            // Get UserTypes
            var userTypeJson = File.ReadAllText(_userTypeJson);
            var usertypes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserType>>(userTypeJson).Where(x => !x.Deleted && x.UserId == id).ToList();

            // Get Types
            var typeJson = File.ReadAllText(_typeJson);
            var types = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Models.Type>>(typeJson).Where(x => !x.Deleted).ToList();

            usertypes.ForEach(x => x.Type = types.FirstOrDefault(y => y.Id == x.TypeId));
            user.UserTypes = usertypes;

            // Return user
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

        public User Post(User user)
        {
            // Get users
            var userJson = File.ReadAllText(_userJson);
            var users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(userJson).ToList();

            var userId = users.Count() + 1;

            // Update Users
            users.Add(new User
            {
                Id = userId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Deleted = false
            });

            // Save users
            string outputUser = Newtonsoft.Json.JsonConvert.SerializeObject(users, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_userJson, outputUser);

            // Get UserTypes
            var userTypeJson = File.ReadAllText(_userTypeJson);
            var usertypes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserType>>(userTypeJson).ToList();

            int userTypesId = usertypes.Count() + 1;

            var userTypesFromDTO = user.UserTypes?.ToList().Select(x => new UserType
            {
                Id = userTypesId++,
                Number = x.Number,
                TypeId = x.TypeId,
                Deleted = false,
                UserId = userId
            }).ToList();

            // Update UserTypes
            usertypes.AddRange(userTypesFromDTO);

            // Save UserTypes
            string outputUserType = Newtonsoft.Json.JsonConvert.SerializeObject(usertypes, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_userTypeJson, outputUserType);

            // Get Updated user
            return GetUser(userId);
        }

        public User Put(User user)
        {
            // Get users
            var userJson = File.ReadAllText(_userJson);
            var users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(userJson).ToList();

            // Update Users
            users.Where(x => x.Id == user.Id).ToList().ForEach(x =>
            {
                x.FirstName = user.FirstName;
                x.LastName = user.LastName;
            });

            // Save users
            string outputUser = Newtonsoft.Json.JsonConvert.SerializeObject(users, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_userJson, outputUser);

            // Get UserTypes
            var userTypeJson = File.ReadAllText(_userTypeJson);
            var usertypes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserType>>(userTypeJson).ToList();

            List<int> userTypesDTO = user.UserTypes.Select(x => x.Id).ToList();

            // Update UserTypes
            usertypes.Where(x => userTypesDTO.Contains(x.Id)).ToList().ForEach(x =>
            {
                x.Number = user.UserTypes.FirstOrDefault(y => y.Id == x.Id)?.Number;
                x.TypeId = user.UserTypes.FirstOrDefault(y => y.Id == x.Id)?.TypeId ?? 1;
            });

            // Save UserTypes
            string outputUserType = Newtonsoft.Json.JsonConvert.SerializeObject(usertypes, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_userTypeJson, outputUserType);

            // Get Updated user
            return GetUser(user.Id);
        }

        public User AddNumber(User user)
        {
            // Get user
            var userJson = File.ReadAllText(_userJson);
            var userDB = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(userJson).FirstOrDefault(x => !x.Deleted && x.Id == user.Id);

            if (userDB == null)
            {
                // Should return error of not found
                return null;
            }

            // Get UserTypes
            var userTypeJson = File.ReadAllText(_userTypeJson);
            var usertypes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserType>>(userTypeJson).ToList();

            int userTypesId = usertypes.Count() + 1;

            var userTypesFromDTO = user.UserTypes?.ToList().Select(x => new UserType
            {
                Id = userTypesId++,
                Number = x.Number,
                TypeId = x.TypeId,
                Deleted = false,
                UserId = userDB.Id
            }).ToList();

            // Update UserTypes
            usertypes.AddRange(userTypesFromDTO);

            // Save UserTypes
            string outputUserType = Newtonsoft.Json.JsonConvert.SerializeObject(usertypes, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_userTypeJson, outputUserType);

            // Get Updated user
            return GetUser(user.Id);
        }
    }
}
