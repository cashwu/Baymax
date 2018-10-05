using Baymax.Attribute;
using Baymax.Model.Config;

namespace Baymax.Tests.Config
{
    [ConfigSection("ArrayConfigStruct", isCollections: true, collectionType: typeof(int))]
    public class ArrayConfigStruct : IConfig
    {
    }

    [ConfigSection("ArrayConfigObject", isCollections: true, collectionType: typeof(ArrayConfigObject))]
    public class ArrayConfigObject : IConfig
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}