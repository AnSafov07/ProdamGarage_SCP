using CommandSystem;
using System;

namespace CustomizationSL.Commands
{
    public class CustomHelpCommand : ICommand
    {
        public string Command => "help";
        public string[] Aliases => Array.Empty<string>();
        public string Description => "Показать список подкоманд кастомизации.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response =
                "Доступные команды:\n" +
                ".custom list - Показать доступные схематики\n" +
                ".custom apply <имя или id> - Применить схематик\n" +
                ".custom delete - Удалить свои схематики\n" +
                ".custom lvl - Показать ваш уровень\n" +
                ".custom reg - Показать применённые схематики";
            return true;
        }
    }
}
