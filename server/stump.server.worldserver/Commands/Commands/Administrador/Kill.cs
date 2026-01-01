using Stump.Server.WorldServer.Commands.Commands.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.Server.BaseServer.Commands;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game.Actors.Fight;

namespace Stump.Server.WorldServer.Commands.Commands
{
    public class KillCommand : TargetCommand
    {
        public KillCommand()
        {
            Aliases = new[] { "kill" };
            RequiredRole = RoleEnum.Administrator;
            Description = "Mata todos los monstruos.";
        }

        public override void Execute(TriggerBase trigger)
        {
            if (trigger == null)
                return;

            var character = (trigger as GameTrigger).Character;

            if (character == null || !character.IsInFight())
                return;

            foreach (var monster in character.Fight.GetAllFighters().OfType<MonsterFighter>())
                monster.Die();
        }
    }
}


