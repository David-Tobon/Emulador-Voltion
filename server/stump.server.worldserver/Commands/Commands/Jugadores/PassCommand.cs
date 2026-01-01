using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Exchanges.Bank;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Handlers.Inventory;

namespace Stump.Server.WorldServer.Commands.Commands
{
    public class PassCommand : InGameCommand
    {
        public PassCommand()
        {
            Aliases = new[] { "pass","pasar" };
            Description = "Permite Pasar automaticamente tus turnos. (Solo 1 Personaje por IP)";
            RequiredRole = RoleEnum.Player;
        }

        CharacterFighter Fighter;

        public override void Execute(GameTrigger trigger)
        {
            if (!trigger.Character.ForcePassTurn)
            {
                if(trigger.Character.Fighter != null)
                {
                    Fighter = trigger.Character.Fighter;
                    trigger.Character.ContextChanged += OnContextChanged;
                    trigger.Character.Fighter.Fight.TurnStarted += OnTurnStarted;
                }
                else
                {
                    trigger.Character.ContextChanged += OnContextChanged;
                }
                trigger.Character.SendServerMessage("Ahora Pasará automáticamente sus turnos.");
                trigger.Character.ForcePassTurn = true;
            }
            else
            {
                if (trigger.Character.Fighter != null)
                {
                    Fighter = trigger.Character.Fighter;
                    trigger.Character.ContextChanged -= OnContextChanged;
                    trigger.Character.Fighter.Fight.TurnStarted -= OnTurnStarted;
                }
                else
                {
                    trigger.Character.ContextChanged -= OnContextChanged;
                }
                trigger.Character.SendServerMessage("Comando desactivado. Ahora jugaras normalmente.");
                trigger.Character.ForcePassTurn = false;
            }
        }

        private void OnContextChanged(Character character, bool inFight)
        {
            if(character.Fighter != null)
            {
                Fighter = character.Fighter;
                character.Fighter.Fight.TurnStarted += OnTurnStarted;
            }
        }

        private void OnTurnStarted(IFight fight, FightActor actor)
        {
            if (Fighter != null && actor != Fighter)
                return;

            Fighter.PassTurn();
        }
    }
}