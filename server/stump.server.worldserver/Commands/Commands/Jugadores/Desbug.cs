using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Maps.Cells;

namespace FreakPlugin.Game
{
    public class DebugMap : InGameCommand
    {
        Cell DebugCell { get; set; }
        public DebugMap()
        {
            Aliases = new[] { "desbug","arreglar","desbugear","desbugg","desbuggear" };
            RequiredRole = RoleEnum.Player;
            Description = "Desplaza a tu personaje a una Casilla libre en el mapa.";
        }

        public override void Execute(GameTrigger trigger)
        {

            var cells = trigger.Character.Map.Cells;

            foreach (var item in cells)
            {
                if (item.Walkable == true)
                    DebugCell = item;
            }
            trigger.Character.Teleport(new ObjectPosition(trigger.Character.Map, DebugCell));
            trigger.Reply("Debug [" + DebugCell.Id + "]"+" Disculpa las Molestias Ocasionadas. <3");
            return;



        }
    }
}
