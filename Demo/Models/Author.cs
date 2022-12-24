using System.Collections.Generic;
using NangaParbat.Attributes;
using NangaParbat.Controllers;
using NangaParbat.Models;

namespace Demo.Models
{
    [GenericCrud("/v1/authors", "Author")]
    [Collection("authors")]
    public class Author : Document
    {
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}