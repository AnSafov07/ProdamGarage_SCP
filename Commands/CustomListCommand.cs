using CommandSystem;
using Exiled.API.Features;
using System;
using System.Linq;

namespace CustomizationSL.Commands
{
    public class CustomListCommand : ICommand
    {
        public string Command => "list";
        public string[] Aliases => Array.Empty<string>();
        public string Description => "Показать доступные схематики.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var player = Player.Get(sender);
            if (player == null)
            {
                response = "Игрок не найден.";
                return false;
            }

            var config = Plugin.Instance.Config;
            int playerLevel = config.PlayerLevels.TryGetValue(player.UserId, out var lvl) ? lvl : 1;

            var available = config.Schematics
                .Where(kv => playerLevel >= kv.Value.rank)
                .OrderBy(kv => kv.Key)
                .Select(kv => kv.Key)
                .ToList();

            if (available.Count == 0)
            {
                response = "Нет доступных схем.";
                return true;
            }

            response = string.Join("\n", available.Select((name, index) => $"{index + 1}. {name}"));
            return true;
        }

    }
}
