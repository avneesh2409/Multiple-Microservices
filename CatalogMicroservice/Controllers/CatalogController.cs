using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CatalogMicroservice.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalog _catalog;

        public CatalogController(ICatalog catalog)
        {
            _catalog = catalog;
        }
        // GET: api/<CatalogController>
        [HttpGet]
        public List<Catalog> Get()
        {
            return _catalog.GetCatalogs();
        }

        // GET api/<CatalogController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CatalogController>
        [HttpPost]
        public bool Post(Catalog catalog)
        {
            return _catalog.AddCatalog(catalog);
        }

        // PUT api/<CatalogController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CatalogController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
