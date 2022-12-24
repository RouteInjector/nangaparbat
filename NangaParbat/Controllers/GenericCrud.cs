using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using NangaParbat.Services;

namespace NangaParbat.Controllers
{

    public class GenericCrud<T> : ControllerBase where T : Entity
    {
        private readonly Storage<T> _storage;
 
        public GenericCrud(Storage<T> storage)
        {
            _storage = storage;
        }
        
        [HttpGet]
        public Task<IEnumerable<T>> Get()
        {
            return _storage.GetAll();
        }
        
        [HttpPost]
        public Task<T> Create([FromBody]T value)
        {
            return _storage.Add(value);
        }
 
        [HttpGet("{id}")]
        public Task<T> Get(string id)
        {
            return _storage.GetById(id);
        }
 
        [HttpPut("{id}")]
        public async Task Update(string id, [FromBody]T value)
        {
            await _storage.Add(value);
        }
        
        [HttpDelete("{id}")]
        public Task Delete(string id)
        {
            return _storage.Delete(id);
        }
    }
}