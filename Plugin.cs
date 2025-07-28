using Exiled.API.Features;
using System;
using System.IO;

namespace CustomizationSL
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "CustomizationSL";
        public override string Prefix => "CustomizationSL";
        public override string Author => "KapustO4ka";
        public override Version Version { get; } = new Version(1, 0, 0);

        public static Plugin Instance { get; private set; }

        public string CustomSchematicsFolder => Path.Combine(Paths.Configs, Config.CustomSchematicsFolder);
        public string MERSchematicsFolder => Path.Combine(Paths.Configs, "MapEditorReborn", "Schematics");

        public override void OnEnabled()
        {
            Instance = this;

            if (!Directory.Exists(CustomSchematicsFolder))
                Directory.CreateDirectory(CustomSchematicsFolder);

            if (!Directory.Exists(MERSchematicsFolder))
                Directory.CreateDirectory(MERSchematicsFolder);

            foreach (var file in Directory.GetFiles(CustomSchematicsFolder, "*.json"))
            {
                string dest = Path.Combine(MERSchematicsFolder, Path.GetFileName(file));
                File.Copy(file, dest, true);
            }

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            base.OnDisabled();
        }
    }
}
