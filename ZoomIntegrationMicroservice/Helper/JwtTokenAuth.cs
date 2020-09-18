using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZoomIntegrationMicroservice.Models;

namespace ZoomIntegrationMicroservice.Helper
{
    public class JwtTokenAuth
    {
        private readonly IConfiguration _config;

        public JwtTokenAuth(IConfiguration config)
        {
            _config = config;
        }
        


    }
}
