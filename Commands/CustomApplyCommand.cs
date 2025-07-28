using CommandSystem;
using Exiled.API.Features;
using CustomizationSL.Handlers;
using System;
using System.Linq;

namespace CustomizationSL.Commands
{
    public class CustomApplyCommand : ICommand
    {
        public string Command => "apply";
        public string[] Aliases => Array.Empty<string>();
        public string Description => "Применить схематику по имени или ID.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var player = Player.Get(sender);
            var config = Plugin.Instance.Config;

            if (arguments.Count == 0)
            {
                response = "Укажите имя или ID схематика. Пример: .custom apply hat1 или .custom apply 1";
                return false;
            }

            string input = arguments.At(0);
            string schematicName = input;

            int playerLevel = config.PlayerLevels.TryGetValue(player.UserId, out int lvl) ? lvl : 1;

            // Если ввели число — ищем имя по ID
            if (int.TryParse(input, out int id))
            {
                var availableSchematics = config.Schematics
                    .Where(kv => playerLevel >= kv.Value.rank)
                    .OrderBy(kv => kv.Key)
                    .Select(kv => kv.Key)
                    .ToList();

                if (id < 1 || id > availableSchematics.Count)
                {
                    response = $"Схематик с ID {id} не найден.";
                    return false;
                }

                schematicName = availableSchematics[id - 1];
            }

            if (!config.Schematics.TryGetValue(schematicName, out var data))
            {
                response = $"Схематик '{schematicName}' не найден.";
                return false;
            }

            return SchematicHandler.ApplySchematic(player, schematicName, data, playerLevel, out response);
        }
    }
}
