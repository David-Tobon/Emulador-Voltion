using Stump.DofusProtocol.Enums;
using Stump.Core.Reflection;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using System;
using System.Collections.Generic;
using Stump.Server.WorldServer.Game;
using System.Text.RegularExpressions;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;

namespace Stump.Server.WorldServer.Commands.Commands.Administrador
{
    public class Quest : InGameCommand
    {
        public Quest()
        {
            Aliases = new[] { "quest" };
            RequiredRole = RoleEnum.Administrator;
            Description = "Manage quest";
            AddParameter<int>("add or remove", "act", "add or remover quest", 0, false, null);
            //  AddParameter<string>("idquest", "idquesr", "id quest", null, false, null);
        }


        public override void Execute(GameTrigger trigger)
        {
            int act = trigger.Get<int>("act");
            // string idquest = trigger.Get<string>("idquest");
            var player = trigger.Character;



            player.StartQuest(act); // int.Parse(idquest)
            trigger.Reply("Quest id {0} spawned", act);//idquest);

        }
    }


}