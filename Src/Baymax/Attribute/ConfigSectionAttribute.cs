namespace Baymax.Attribute
{
    public sealed class ConfigSectionAttribute : System.Attribute
    {
        public ConfigSectionAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}