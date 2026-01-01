using System;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Commands.Commands.Patterns
{
    public abstract class TargetCommand : CommandBase
    {
        protected void AddTargetParameter(bool optional = false, string description = "Defined target")
        {
            AddParameter("target", "t", description, isOptional: optional, converter: ParametersConverter.CharactersConverter);
        }

        public Character[] GetTargets(TriggerBase trigger)
        {
            Character[] targets = null;
            if (trigger.IsArgumentDefined("target"))
                targets = trigger.Get<Character[]>("target");
            else if (trigger is GameTrigger)
                targets = new []{(trigger as GameTrigger).Character};

            if (targets == null)
                throw new Exception("Target no ha sido definido");

            if (targets.Length == 0)
                throw new Exception("El Target no se ha encontrado");

            return targets;
        }

        public Character GetTarget(TriggerBase trigger)
        {
            var targets = GetTargets(trigger);

            if (targets.Length > 1)
                throw new Exception("Solo puedes elegir un Target");

            return targets[0];
        }
    }

    public abstract class TargetSubCommand : SubCommand
    {
        protected void AddTargetParameter(bool optional = false, string description = "Defined target")
        {
            AddParameter("target", "t", description, isOptional: optional, converter: ParametersConverter.CharactersConverter);
        }

        public Character[] GetTargets(TriggerBase trigger)
        {
            Character[] targets = null;
            if (trigger.IsArgumentDefined("target"))
                targets = trigger.Get<Character[]>("target");
            else if (trigger is GameTrigger)
                targets = new []{(trigger as GameTrigger).Character};

            if (targets == null)
                throw new Exception("El target no ha sido Definido");

            if (targets.Length == 0)
                throw new Exception("El target no se ha encontrado");

            return targets;
        }

        public Character GetTarget(TriggerBase trigger)
        {
            var targets = GetTargets(trigger);

            if (targets.Length > 1)
                throw new Exception("Solo puedes elegir un target");

            return targets[0];
        }
    }
}