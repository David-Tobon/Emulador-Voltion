using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using System.Drawing;

namespace Stump.Server.WorldServer.Commands.Commands.Jugadores
{
    public class Scroll : TargetCommand
    {
        public Scroll()
        {
            base.Aliases = new string[]
            {
                "Scroll","parcho"
            };
            RequiredRole = RoleEnum.Player;
            Description = "Scrollea completamente a tu Personaje.";
        }
        public override void Execute(TriggerBase trigger)
        {
            Character[] targets = GetTargets(trigger);
            for (int i = 0; i < targets.Length; i++)
            {
                Character character = targets[i];
                character.Stats.Vitality.Additional = 100;
                character.Stats.Chance.Additional = 100;
                character.Stats.Intelligence.Additional = 100;
                character.Stats.Wisdom.Additional = 100;
                character.Stats.Agility.Additional = 100;
                character.Stats.Strength.Additional = 100;
                character.RefreshStats();


                if (character.Stats.Strength.Additional == 100)
                {
                    character.SendServerMessage("Tu personaje ya se ha scrolleado");
                }
                if (character.Stats.Strength.Additional != 100)
                {
                    character.SendServerMessage("Tu personaje" + "<b> " + character.Name + " </b>" + "ha obtenido<b> 100 puntos característicos adicionales</b>.");
                }

                
            }
        }
    }
}

