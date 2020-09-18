using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZoomIntegrationMicroservice.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<AccessTokenResponse> userTokens { get; set; }
        public DbSet<CodeResponse> codeTokens { get; set; }
    }
}
