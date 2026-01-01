using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Maps.Cells;

namespace Stump.Server.WorldServer.Commands.Commands.Teleport
{
    public class TeleportPadoque : CommandBase
    {
        public static void Teleport(Character player, int mapId, short cellId, DirectionsEnum playerDirection)
        {
            player.Teleport(new ObjectPosition(Singleton<World>.Instance.GetMap(mapId), cellId, playerDirection));
        }

        public TeleportPadoque()
        {
            Aliases = new[] { "cercado","enclos","paddock"};
            RequiredRole = RoleEnum.Player;
            Description = "Transporte rápido a un cercado.";
        }

        public override void Execute(TriggerBase trigger)
        {
            var gameTrigger = trigger as GameTrigger;
            if (gameTrigger != null)
            {
                
                var player = gameTrigger.Character;
                Teleport(player, 150328, 191, DirectionsEnum.DIRECTION_SOUTH_EAST);
            }
        }
    }
}