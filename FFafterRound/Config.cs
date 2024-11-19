using Exiled.API.Features;
using Exiled.API.Interfaces;

namespace FFafterRound
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
}