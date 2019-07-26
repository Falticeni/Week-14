using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        void AddUser(string username, string email, string city, string description, string street);
        void EditUser(int id, string userName, string email, string description, string city, string street);
        int GetLastUserId();
        User GetUserById(int id);
        void DeleteUser(int id);
        IList<User> GetAllUsers(int id);
    }
}
