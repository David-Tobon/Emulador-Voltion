using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Maps.Cells;

namespace Stump.Server.WorldServer.Commands.Commands.Teleport
{
    class StartCommand : CommandBase
    {
        public static void Teleport(Character player, int mapId, short cellId, DirectionsEnum playerDirection)
        {
            player.Teleport(new ObjectPosition(Singleton<World>.Instance.GetMap(mapId), cellId, playerDirection));
        }

        public StartCommand()
        {
            Aliases = new[] { "astrub", "start", "inicio" };
            RequiredRole = RoleEnum.Player;
            Description = "Teletransporta al zaap de astrub.";
        }

        public override void Execute(TriggerBase trigger)
        {
            var gameTrigger = trigger as GameTrigger;
            if (gameTrigger != null)
            {
                var player = gameTrigger.Character;
                Teleport(player, 191105026, 260, DirectionsEnum.DIRECTION_SOUTH_EAST);
            }
        }
    }
}