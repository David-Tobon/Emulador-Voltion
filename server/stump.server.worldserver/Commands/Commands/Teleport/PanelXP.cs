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
    public class PanelXP : CommandBase
    {
        public PanelXP()
        {
            Aliases = new[] { "xp", "XP", "farm" };
            RequiredRole = RoleEnum.Player;
            Description = "Teletransporta a la Multiples Zonas de XP.";
        }

        public override void Execute(TriggerBase trigger)
        {
            Dictionary<Map, int> destinations = new Dictionary<Map, int>();
            
            // Deshabilitado
             destinations.Add(World.Instance.GetMap(196083718), 367);
            

            var gameTrigger = trigger as GameTrigger;
            DungsDialog s = new DungsDialog(gameTrigger.Character, destinations);
            s.Open();
        }
    }
}