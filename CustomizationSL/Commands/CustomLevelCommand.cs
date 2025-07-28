using CommandSystem;
using Exiled.API.Features;
using System;

namespace CustomizationSL.Commands
{
    public class CustomLevelCommand : ICommand
    {
        public string Command => "lvl";
        public string[] Aliases => Array.Empty<string>();
        public string Description => "Показать уровень игрока.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var player = Player.Get(sender);
            var config = Plugin.Instance.Config;

            int level = config.PlayerLevels.TryGetValue(player.UserId, out int lvl) ? lvl : 1;
            response = $"Ваш уровень кастомизации: {level}";
            return true;
        }
    }
}
