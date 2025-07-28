using CommandSystem;
using Exiled.API.Features;
using CustomizationSL.Handlers;
using System;

namespace CustomizationSL.Commands
{
    public class CustomDeleteCommand : ICommand
    {
        public string Command => "delete";
        public string[] Aliases => Array.Empty<string>();
        public string Description => "Удалить все активные схематики.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var player = Player.Get(sender);
            response = SchematicHandler.DeleteSchematics(player);
            return true;
        }
    }
}
