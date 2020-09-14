using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogMicroservice.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Catalog> catalogs { get; set; }
    }
}
