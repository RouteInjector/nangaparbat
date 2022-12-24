using System;

namespace NangaParbat.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GenericCrudAttribute : Attribute
    {
        public GenericCrudAttribute(string route, string group)
        {
            Route = route;
            Group = group;
        }

        public string Route { get; }
        public string Group { get; }
    }
}