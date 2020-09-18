using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZoomIntegrationMicroservice.Models
{
    public interface IAccessToken
    {
        List<AccessTokenResponse> GetTokens();
        bool AddToken(AccessTokenResponse token);
        bool CreateCode(string code, string state);
        CodeResponse GetCodeToken(string state);
    }
}
