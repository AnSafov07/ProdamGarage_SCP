using CommandSystem;
using System;

namespace CustomizationSL.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class BaseCustomizationCommand : ParentCommand
    {
        public BaseCustomizationCommand()
        {
            LoadGeneratedCommands();
        }

        public override string Command => "customization";
        public override string[] Aliases => new[] { "custom", "customize" };
        public override string Description => "Управление пользовательскими схематиками.";

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new CustomListCommand());
            RegisterCommand(new CustomDeleteCommand());
            RegisterCommand(new CustomLevelCommand());
            RegisterCommand(new CustomApplyCommand());
            RegisterCommand(new CustomHelpCommand());
            RegisterCommand(new CustomRegisteredCommand());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "Используйте '.custom help' для списка доступных команд.";
            return false;
        }
    }
}
