using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Models
{
    public class UserRepository : IUser
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool AddUser(User user)
        {
            try
            {
                _context.users.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool UpdateUser(User user)
        {
            try {
                _context.users.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public User DeleteUser(int id)
        {
            var entity = _context.users.FirstOrDefault(t => t.Id == id);
            _context.users.Remove(entity);
            _context.SaveChanges();
            return entity;
        }

        public User GetSingleUser(int id)
        {
            return _context.users.FirstOrDefault(t => t.Id == id);
        }
        public List<User> GetUsers()
        {
            var users = _context.users.ToList();
            return users;
        }
    }
}
