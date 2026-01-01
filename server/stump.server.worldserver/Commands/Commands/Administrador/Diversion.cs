using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game.Actors.Look;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Commands.Commands.adicionais
{          
    class Emotemapa : InGameCommand
    {
        public Emotemapa()
        {
            Aliases = new string[]
            {
                "emotemapa"
            };
            RequiredRole = RoleEnum.Administrator;
            AddParameter<int>("id", "id", "Emote id", isOptional: false);
            Description = "Regala un Emote a todos los personajes del mapa!";

        }
        public override void Execute(GameTrigger trigger)
        {
            if (!trigger.IsArgumentDefined("id"))
            {
                trigger.ReplyError("Specify an emote or -all");
                return;
            }
            var clients = trigger.Character.Map.GetAllCharacters().Where(x => Game.HavenBags.HavenBagManager.Instance.CanBeSeenInHavenBag(x, trigger.Character)).Select(v => v.Client);
            foreach (var target in clients)
            {
                target.Character.AddEmote((EmotesEnum)trigger.Get<int>("id"));

            }
        }
    }

    class Gfx : InGameCommand
    {
        public Gfx()
        {
            Aliases = new string[]
            {
                "gfx"
            };
            RequiredRole = RoleEnum.Administrator;
            AddParameter<string>("gfx", "gfx", "GFX", isOptional: false);
            Description = "Prueba los gfx!";

        }
        public override void Execute(GameTrigger trigger)
        {
            if (!trigger.IsArgumentDefined("gfx"))
            {
                trigger.ReplyError("Specify an gfx or -all");
                return;
            }
            trigger.Character.Client.Send(new GameRolePlaySpellAnimMessage((ulong)trigger.Character.Id, (ushort)trigger.Character.Record.CellId, ushort.Parse(trigger.Get<string>("gfx")), 6));

        }
    }
   
    class Emoteplaymapa : InGameCommand
    {
        public Emoteplaymapa()
        {
            Aliases = new string[]
            {
                "emoteplaymapa"
            };
            RequiredRole = RoleEnum.Administrator;
            AddParameter<int>("id", "id", "Emote id", isOptional: false);
            Description = "Todos los Jugadores del mapa harán un Emote";

        }
        public override void Execute(GameTrigger trigger)
        {
            if (!trigger.IsArgumentDefined("id"))
            {
                trigger.ReplyError("Specify an emote or -all");
                return;
            }
            var clients = trigger.Character.Map.GetAllCharacters().Where(x => Game.HavenBags.HavenBagManager.Instance.CanBeSeenInHavenBag(x, trigger.Character)).Select(v => v.Client);
            foreach (var target in clients)
            {
                target.Character.PlayEmote((EmotesEnum)trigger.Get<int>("id"), true);

            }
        }
    }
    class Mapasay : InGameCommand
    {
        public Mapasay()
        {
            Aliases = new string[]
            {
                "mapasay"
            };
            RequiredRole = RoleEnum.Administrator;
            base.AddParameter<string>("Texto", "txt", "Texto para todos los players del mapa", null, true, null);
            Description = "Obligar a todos en el mapa a decir el texto!";

        }
        public override void Execute(GameTrigger trigger)
        {
            if (!trigger.IsArgumentDefined("txt"))
            {
                trigger.ReplyError("Specify an text");
                return;
            }
            var clients = trigger.Character.Map.GetAllCharacters().Where(x => Game.HavenBags.HavenBagManager.Instance.CanBeSeenInHavenBag(x, trigger.Character)).Select(v => v.Client);
            foreach (var target in clients)
            {
                target.Character.Say(trigger.Get<string>("txt"));

            }
        }
    }
}
