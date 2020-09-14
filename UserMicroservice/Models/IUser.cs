using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Models
{
    public interface IUser
    {
        List<User> GetUsers();
        bool AddUser(User user);
        bool UpdateUser(User user);
        User DeleteUser(int id);
        User GetSingleUser(int id);
    }
}
