using System.Linq;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.BaseServer.Commands.Commands;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using System.Collections.Generic;

namespace Stump.Server.WorldServer.Commands.Commands.Info
{
    public class InfoIP : TargetCommand
    {
        public InfoIP()
        {
            Aliases = new[]
            {
                "infoip",
            };
            RequiredRole = RoleEnum.Player;
            Description = "Numero de IPs Conectadas";
        }

        public override void Execute(TriggerBase trigger)
        {
            List<string> IPAlreadySeen = new List<string>();
            foreach(Character chr in Singleton<World>.Instance.GetCharacters())
            {
                if(!IPAlreadySeen.Contains(chr.Account.LastConnectionIp))
                    IPAlreadySeen.Add(chr.Account.LastConnectionIp);
            }
            trigger.Reply("Ip Conectadas Actuales: " + IPAlreadySeen.Count);
        }
    }
}