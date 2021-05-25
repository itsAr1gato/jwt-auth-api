using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using Webproj.Models;
using Webproj.Helpers;

namespace Webproj.Repositories
{
    public interface IUserRepository
    {
        List<User> Get();

        User Get(string id);

        User Create(User user);

        void Update(string id, User user);

        void Delete(string id);
    }

    public class UserRepository : IUserRepository
    {
        private IMongoCollection<User> _users;

        public UserRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>("Users");
        }

        public List<User> Get() =>
            _users.Find(user => true).ToList();

        public User Get(string id) =>
            _users.Find<User>(user => user.Id == id).FirstOrDefault();

        public User Create(User user)
        {
            _users.InsertOne(user);

            return user;
        }

        public void Update(string id, User userIn) =>
            _users.ReplaceOne(user => user.Id == id, userIn);

        public void Delete(string id) =>
            _users.DeleteOne(user => user.Id == id);
    }
}