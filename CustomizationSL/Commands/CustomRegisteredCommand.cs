using CommandSystem;
using Exiled.API.Features;
using CustomizationSL.Handlers;
using System;
using System.Linq;

namespace CustomizationSL.Commands
{
    public class CustomRegisteredCommand : ICommand
    {
        public string Command => "registered";
        public string[] Aliases => new[] { "reg" };
        public string Description => "Показать схематики, применённые на вас.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var player = Player.Get(sender);

            var list = SchematicHandler.GetRegisteredSchematics(player);

            if (list.Count == 0)
            {
                response = "На вас нет активных схем.";
                return true;
            }

            response = "Активные схематики:\n" + string.Join("\n", list.Select(name => $"- {name.Remove(0, 16)}"));
            return true;
        }
    }
}
