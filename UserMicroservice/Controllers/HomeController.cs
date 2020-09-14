using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUser _userContext;

        public HomeController(IUser userContext)
        {
            _userContext = userContext;
        }
        // GET: api/<HomeController>
        [HttpGet]
        public List<User> Get()
        {
            return _userContext.GetUsers();
        }
        // GET api/<HomeController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _userContext.GetSingleUser(id);
        }
        // POST api/<HomeController>
        [HttpPost]
        public bool Post(User user)
        {
            return _userContext.AddUser(user);
        }
        // PUT api/<HomeController>/5
        [HttpPut()]
        public bool Put(User user)
        {
           return _userContext.UpdateUser(user);
        }
        // DELETE api/<HomeController>/5
        [HttpDelete("{id}")]
        public User Delete(int id)
        {
            return _userContext.DeleteUser(id);
        }
    }
}
