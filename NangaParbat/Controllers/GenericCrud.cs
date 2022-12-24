using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NangaParbat.Models;
using NangaParbat.Services;

namespace NangaParbat.Controllers
{
    public class GenericCrud<T> : ControllerBase where T : Document
    {
        private readonly IDocumentStore<T> _store;

        public GenericCrud(IDocumentStore<T> store)
        {
            _store = store;
        }

        /// <summary>
        /// Gets all entries
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IEnumerable<T>> Get()
        {
            return _store.Get();
        }

        /// <summary>
        /// Creates a new entry
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<T> Create([FromBody] T value)
        {
            return _store.Create(value);
        }
        /// <summary>
        /// Get entry by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<T?> Get(string id)
        {
            return _store.Get(id);
        }
        /// <summary>
        /// Updates an entry by ID
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<T> Update(string id, [FromBody] T value)
        {
            return _store.Update(id, value);
        }

        /// <summary>
        /// Deletes an entry by ID
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task Delete(string id)
        {
            return _store.Delete(id);
        }
    }
}