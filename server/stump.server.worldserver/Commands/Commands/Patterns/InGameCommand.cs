using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Trigger;

namespace Stump.Server.WorldServer.Commands.Commands.Patterns
{
    public abstract class InGameCommand : CommandBase
    {
        public override void Execute(TriggerBase trigger)
        {
            if (!( trigger is GameTrigger ))
            {
                trigger.ReplyError("Este comando solo puede ser ejecutado dentro del juego");
                return;
            }

            Execute(trigger as GameTrigger);
        }

        public abstract void Execute(GameTrigger trigger);
    }

    public abstract class InGameSubCommand : SubCommand
    {
        public override void Execute(TriggerBase trigger)
        {
            if (!( trigger is GameTrigger ))
            {
                trigger.ReplyError("Este comando solo puede ser ejecutado dentro del juego");
                return;
            }

            Execute(trigger as GameTrigger);
        }

        public abstract void Execute(GameTrigger trigger);
    }
}