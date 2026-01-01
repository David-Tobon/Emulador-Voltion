using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Maps.Cells;

namespace Stump.Server.WorldServer.Commands.Commands.Teleport
{
    public class TeleportIncarnam : CommandBase
    {
        public static void Teleport(Character player, int mapId, short cellId, DirectionsEnum playerDirection)
        {
            player.Teleport(new ObjectPosition(Singleton<World>.Instance.GetMap(mapId), cellId, playerDirection));
        }

        public TeleportIncarnam()
        {
            Aliases = new[] { "Incarnam" };
            RequiredRole = RoleEnum.Player;
            Description = "Te has teletransportado a incarnam";
        }

        public override void Execute(TriggerBase trigger)
        {
            var gameTrigger = trigger as GameTrigger;
            if (gameTrigger != null)
            {
                var player = gameTrigger.Character;
                Teleport(player, 154010883, 383, DirectionsEnum.DIRECTION_SOUTH_EAST);
            }
        }
    }
}