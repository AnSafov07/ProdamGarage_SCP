using Exiled.API.Features;
using Exiled.API.Interfaces;

namespace DetonationCoin
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public float SCP001Chance { get; set; } = 10f;
    }
}