using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Entities;

namespace NangaParbat.Services
{
    public class Storage<T> where T : Entity
    {
        public async Task<IEnumerable<T>> GetAll()
        {
            return await DB.Find<T>()
                .Match(_ => true)
                .ExecuteAsync();
        }

        public async Task<T> GetById(string id)
        {
            return await DB.Find<T>().OneAsync(id.ToString());
        }

        public async Task<T> Add(T value)
        {
            await value.SaveAsync();
            return value;
        }

        public async Task Delete(string id)
        {
            await DB.DeleteAsync<T>(id.ToString());
        }
    }
}