using System.Collections.Generic;
using System.Threading.Tasks;
using NangaParbat.Models;

namespace NangaParbat.Services
{
    public interface IDocumentStore<T> where T : Document
    {
        public Task<IEnumerable<T>> Get();
        public Task<T?> Get(string id);
        public Task<T> Create(T document);
        public Task<T> Update(string id, T document);
        public Task Delete(string id);
    }
}