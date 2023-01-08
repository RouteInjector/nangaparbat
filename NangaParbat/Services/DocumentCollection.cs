using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using NangaParbat.Attributes;
using NangaParbat.Models;

namespace NangaParbat.Services
{
    public class DocumentCollection<T> : IDocumentStore<T> where T : Document
    {
        private readonly IMongoCollection<T> _collection;
        
        public DocumentCollection(IMongoDatabase database)
        {
            /*
            var attribute =
                (CollectionAttribute) Attribute.GetCustomAttribute(typeof(T), typeof (CollectionAttribute))!;
            _collection = database.GetCollection<T>(attribute.Collection);
            */

            String collection = typeof(T).Name;
            _collection = database.GetCollection<T>(collection);
        }
        

        public async Task<IEnumerable<T>> Get()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T?> Get(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<T> Create(T document)
        {
            await _collection.InsertOneAsync(document);
            return document;
        }

        public async Task<T> Update(string id, T document)
        {
            document.Id = id;
            await _collection.ReplaceOneAsync(x => x.Id == id, document);
            return document;
        }

        public async Task Delete(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }
    }
}