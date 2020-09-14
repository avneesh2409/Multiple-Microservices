using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogMicroservice.Models
{
    public class CatalogRepository : ICatalog
    {
        private readonly AppDbContext _catalog;

        public CatalogRepository(AppDbContext catalog)
        {
            _catalog = catalog;
        }
        public bool AddCatalog(Catalog catalog)
        {
            try
            {
                _catalog.catalogs.Add(catalog);
                _catalog.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<Catalog> GetCatalogs()
        {
            var catalogs = _catalog.catalogs.ToList();
            return catalogs;
        }
    }
}
