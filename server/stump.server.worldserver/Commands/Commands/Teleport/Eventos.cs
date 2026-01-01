using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Maps.Cells;

namespace Stump.Server.WorldServer.Commands.Commands.Teleport
{
    class ShopEventos : CommandBase
    {
        public static void Teleport(Character player, int mapId, short cellId, DirectionsEnum playerDirection)
        {
            player.Teleport(new ObjectPosition(Singleton<World>.Instance.GetMap(mapId), cellId, playerDirection));
        }

        public ShopEventos()
        {
            Aliases = new[] { "evento", "eventos"};
            RequiredRole = RoleEnum.Player;
            Description = "Teletransporta a la Tienda de Eventos";
        }

        public override void Execute(TriggerBase trigger)
        {
            var gameTrigger = trigger as GameTrigger;
            if (gameTrigger != null)
            {
                var player = gameTrigger.Character;
                Teleport(player, 195821572, 257, DirectionsEnum.DIRECTION_SOUTH_EAST);
            }
        }
    }
}
