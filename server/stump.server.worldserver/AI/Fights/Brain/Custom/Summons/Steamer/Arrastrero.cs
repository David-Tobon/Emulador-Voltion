using System.Collections.Generic;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Game.Fights;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Summons
{
    [BrainIdentifier((int)MonsterIdEnum.CHALUTIER)]
    public class Arrastrero : Brain
    {
        public Arrastrero(AIFighter fighter) : base(fighter)
        {
            //fighter.Team.FighterAdded += OnFighterAdded;
            fighter.Fight.TurnStarted += OnTurnStarted;
            fighter.Fight.FightStarted += Fight_FightStarted;
        }

        void OnFighterAdded(FightTeam team, FightActor fighter)
        {

        }

        private void Fight_FightStarted(IFight obj)
        {

        }
        private void OnTurnStarted(IFight obj, FightActor fighter)
        {

            if (fighter != Fighter)
                return;

            var spell = new Spell(9879, 1); // Atraer
            var spell2 = new Spell(9881, 1); // Sub MP

            var spellCastHandler = SpellManager.Instance.GetSpellCastHandler(fighter, spell, fighter.Cell, false);
            var spellCastHandler2 = SpellManager.Instance.GetSpellCastHandler(fighter, spell2, fighter.Cell, false);

            spellCastHandler.Initialize();
            spellCastHandler2.Initialize();

            foreach (var handler in spellCastHandler.GetEffectHandlers())
            {
                List<Cell> m_cell = new List<Cell> {
                    fighter.Cell
                };

                handler.AffectedCells = m_cell;
            }

            spellCastHandler.Execute();

            foreach (var handler in spellCastHandler2.GetEffectHandlers())
            {
                List<Cell> m_cell = new List<Cell> {
                    fighter.Cell
                };

                handler.AffectedCells = m_cell;
            }

            spellCastHandler2.Execute();

        }

        public override void Play()
        {

        }
    }
}