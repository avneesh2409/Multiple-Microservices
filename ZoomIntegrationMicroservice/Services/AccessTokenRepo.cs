using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZoomIntegrationMicroservice.Models;

namespace ZoomIntegrationMicroservice.Services
{
    public class AccessTokenRepo : IAccessToken
    {
        private readonly AppDbContext _context;

        public AccessTokenRepo(AppDbContext context)
        {
            _context = context;
        }

        public bool AddToken(AccessTokenResponse token)
        {
            try {
                _context.userTokens.Add(token);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool CreateCode(string code, string state)
        {
            
           _context.codeTokens.Add(new CodeResponse()
            {
                code=code,
                state=state
            });
            _context.SaveChanges();
            return true;
        }

        public CodeResponse GetCodeToken(string state)
        {
            try {
                var result = _context.codeTokens.Where(e => e.state == state).FirstOrDefault();
                return result;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return null;
            }     
        }

        public List<AccessTokenResponse> GetTokens()
        {
            return _context.userTokens.ToList();
        }
       
    }
}
