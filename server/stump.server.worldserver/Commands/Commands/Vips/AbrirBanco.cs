using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Exchanges.Bank;
using Stump.Server.WorldServer.Handlers.Inventory;

namespace Stump.Server.WorldServer.Commands.Commands
{
    public class AbrirBanco : InGameCommand
    {
        public AbrirBanco()
        {
            Aliases = new[] { "banco" };
            Description = "Abre el Banco.";
            RequiredRole = RoleEnum.Moderator;
        }
        public override void Execute(GameTrigger trigger)
        {
            Character character = trigger.Character;
            if (!character.IsFighting())
            {
                var dialog = new BankDialog(character);
                dialog.Open();
            }
        }
    }
}