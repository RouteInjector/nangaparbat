using MongoDB.Entities;
using NangaParbat.Controllers;

namespace NangaParbat.Models
{
    [GenericCrud("/v1/users", "Users")]
    public class User : Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}