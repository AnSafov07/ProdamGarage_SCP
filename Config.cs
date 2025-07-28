using Exiled.API.Interfaces;
using System.Collections.Generic;

namespace CustomizationSL
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public List<string> Steams { get; set; } = new List<string>();

        public Dictionary<string, SchematicData> Schematics { get; set; } = new Dictionary<string, SchematicData>();
        public Dictionary<string, int> PlayerLevels { get; set; } = new Dictionary<string, int>();

        public string CustomSchematicsFolder { get; set; } = "CustomizationSL";
    }
}
