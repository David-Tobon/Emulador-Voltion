using System.Drawing;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;

namespace Stump.Server.WorldServer.Commands.Commands
{

    public class HonorCommand : SubCommandContainer
    {
        public HonorCommand()
        {
            Aliases = new[] { "honor" };
            RequiredRole = RoleEnum.Administrator;
            Description = "Otorga Honor.";
        }
    }

    public class HonorAddCommand : TargetSubCommand
    {
        public HonorAddCommand()
        {
            Aliases = new[] { "add" };
            RequiredRole = RoleEnum.Administrator;
            ParentCommandType = typeof(HonorCommand);
            Description = "Add Honor to a target";
            AddParameter<ushort>("amount", "amount", "Amount of honor to add", 1);
            AddTargetParameter(true);
        }

        public override void Execute(TriggerBase trigger)
        {
            foreach (var target in GetTargets(trigger))
            {
                target.AddHonor(trigger.Get<ushort>("amount"));
                target.Map.Refresh(target);
                trigger.Reply("Felicitaciones Has obtenido " + trigger.Get<ushort>("amount") + " Puntos de Honor !", Color.OrangeRed);
            }
        }
    }
}