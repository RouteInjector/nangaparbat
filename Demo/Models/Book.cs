using MongoDB.Entities;
using NangaParbat.Controllers;

namespace Demo.Models
{
    [GenericCrud("/v1/books", "Book")]
    public class Book : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}