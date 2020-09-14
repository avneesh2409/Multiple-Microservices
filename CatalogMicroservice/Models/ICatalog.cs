using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogMicroservice.Models
{
    public interface ICatalog
    {
        List<Catalog> GetCatalogs();
        bool AddCatalog(Catalog catalog);
    }
}
