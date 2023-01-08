using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NangaParbat.Attributes;
using NangaParbat.Models;

namespace Demo.Models
{
    //[Collection("books")]
    [GenericCrud("/v1/books", "Book")]
    public class Book : Document
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Author> Authors { get; set; }
    }
}