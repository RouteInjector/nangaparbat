using MongoDB.Entities;
using NangaParbat.Controllers;

namespace Demo.Models
{
    [GenericCrud("/v1/authors", "Author")]
    public class Author : Entity
    {
        public string Name { get; set; }
    }
}