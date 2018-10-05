using Baymax.Attribute;
using Baymax.Model.Config;

namespace Baymax.Tests.Config
{
    [ConfigSection("SingleConfig")]
    public class SingleConfig : IConfig
    {
        public int Id { get; set; }
    }
}