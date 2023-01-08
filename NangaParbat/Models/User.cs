using NangaParbat.Attributes;
using NangaParbat.Controllers;

namespace NangaParbat.Models
{
    //[Collection("users")]
    [GenericCrud("/v1/users", "Users")]
    public class User : Document
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}