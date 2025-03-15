using CommandSystem;
using Exiled.API.Features;
using MEC;
using RemoteAdmin;
using System;

namespace DeceitSL
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class InfoCommand : ICommand
    {
        public string Command => "S-Info";
        public string[] Aliases => new string[] { };
        public string Description => "Info";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is PlayerCommandSender playerSender)
            {
                Player player = Player.Get(playerSender.ReferenceHub);
                
                response = "Данный режим представляет собой попытку повторить опыт геймплея от таких игр, как Deceit, Friday 13, Dead By Daylight и т.д. Так как это всего лишь кривой порт, сделанный на коленках каким то геем(кто понял тот понял), то прошу, не хуярьте меня лопатой, я правда старался(хоть и вышло говно) \n Удачной Игры!";
                return true;
            }

            response = "This command can only be used by a player.";
            return false;
        }
    }
}
