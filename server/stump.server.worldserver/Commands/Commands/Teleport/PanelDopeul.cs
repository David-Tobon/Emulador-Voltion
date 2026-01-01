/* using Stump.Core.Reflection;
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
    public class PanelDopeul : CommandBase
    {
        public PanelDopeul()
        {
            Aliases = new[] { "dopeul", "dopeuls","templo","templos"};
            RequiredRole = RoleEnum.Player;
            Description = "Teletransporte a la Zona de Dopeuls";
        }

        public override void Execute(TriggerBase trigger)
        {
            Dictionary<Map, int> destinations = new Dictionary<Map, int>();
            destinations.Add(World.Instance.GetMap(67109888), 494);         //Tymador
            destinations.Add(World.Instance.GetMap(163053570), 506);         //Hipermago
            destinations.Add(World.Instance.GetMap(148636161), 487);         //Selotrop
            destinations.Add(World.Instance.GetMap(96469504), 485);         //Streamer
            destinations.Add(World.Instance.GetMap(88080660), 435);         //Yopuka
            destinations.Add(World.Instance.GetMap(177210626), 413);         //Uginak
            destinations.Add(World.Instance.GetMap(88212244), 414);         //Ocra
            destinations.Add(World.Instance.GetMap(88081686), 387);         //Xelor
            destinations.Add(World.Instance.GetMap(88082201), 540);         //Pandawa
            destinations.Add(World.Instance.GetMap(88214295), 399);         //Sram
            destinations.Add(World.Instance.GetMap(88084245), 401);         //Osamoda
            destinations.Add(World.Instance.GetMap(88086290), 303);         //Feca
            destinations.Add(World.Instance.GetMap(88212750), 512);         //Sadida
            destinations.Add(World.Instance.GetMap(17048578), 339);         //Anutrof
            destinations.Add(World.Instance.GetMap(69207040), 312);         //Zobal
            destinations.Add(World.Instance.GetMap(88083734), 538);         //Aniripsa
            destinations.Add(World.Instance.GetMap(185863684), 537);        //Zurcarak
            destinations.Add(World.Instance.GetMap(185861637), 356);        //Sacrogrito








            var gameTrigger = trigger as GameTrigger;
            DungsDialog s = new DungsDialog(gameTrigger.Character, destinations);
            s.Open();
        }
    }
}
*/