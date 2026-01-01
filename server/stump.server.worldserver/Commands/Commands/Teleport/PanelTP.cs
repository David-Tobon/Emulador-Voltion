using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Commands;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Dialogs.Interactives;
using Stump.Server.WorldServer.Game.Interactives;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Handlers.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Commands.Commands.Teleport
{
    public class PanelTP : CommandBase
    {
        public PanelTP()
        {
            Aliases = new[] { "tp", "TP", "zonas","zona" };
            RequiredRole = RoleEnum.Player;
            Description = "Téleportación a Mapas del servidor.";
        }

        public override void Execute(TriggerBase trigger)
        {
            Dictionary<Map, int> destinations = new Dictionary<Map, int>();


            destinations.Add(World.Instance.GetMap(191105026), 260);         //Astrub
            destinations.Add(World.Instance.GetMap(188744199), 214);         //Cercado
            destinations.Add(World.Instance.GetMap(147767), 355);            //Forja Bonta
            destinations.Add(World.Instance.GetMap(154010883), 383);         //Incarnam
            destinations.Add(World.Instance.GetMap(185863169), 386);         //Gremios
            destinations.Add(World.Instance.GetMap(139724802), 369);         //Feria Troll
            destinations.Add(World.Instance.GetMap(99090957), 441);          //Kanojedo
            destinations.Add(World.Instance.GetMap(81789952), 437);          //Kolizeo
            destinations.Add(World.Instance.GetMap(88212759), 286);          //PVP
            destinations.Add(World.Instance.GetMap(196215812), 286);         //Tienda
            destinations.Add(World.Instance.GetMap(196083718), 367);         // Kamas




            var gameTrigger = trigger as GameTrigger;
            DungsDialog s = new DungsDialog(gameTrigger.Character, destinations);
            s.Open();
        }
    }
}