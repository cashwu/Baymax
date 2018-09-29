using System;

namespace Baymax.Attribute
{
    public sealed class ConfigSectionAttribute : System.Attribute
    {
        public ConfigSectionAttribute(string name, bool isCollections = false, Type collectionType = null)
        {
            Name = name;
            IsCollections = isCollections;
            CollectionType = collectionType;
        }

        public string Name { get; }
        
        public bool IsCollections { get; }
        
        public Type CollectionType { get; }
    }
}