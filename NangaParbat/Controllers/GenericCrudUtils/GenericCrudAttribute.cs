using System;

namespace NangaParbat.Controllers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class GenericCrudAttribute : Attribute
    {
        public GenericCrudAttribute(string route, string group)
        {
            Route = route;
            Group = group;
        }

        public string Route { get; set; }
        public string Group { get; set; }
    }
}