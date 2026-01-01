using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Commands.Commands
{
    class IpDropCommand : InGameCommand
    {
        public IpDropCommand()
        {
            base.Aliases = new[] { "ipdrop" };
            base.Description = "El personaje que haya activado este comando recibirá todas los Drops de todos sus personajes.";
            base.RequiredRole = RoleEnum.Moderator;
        }
        public override void Execute(GameTrigger trigger)
        {
            Character player = trigger.Character;
            if (!player.IsIpDrop)
            {
                player.IsIpDrop = true;
                player.SendServerMessage("Activaste el Comando IPDrop.");
            }
            else
            {
                player.IsIpDrop = false;
                player.SendServerMessage("Comando IPDrop Desactivado");
            }
        }
    }
}
