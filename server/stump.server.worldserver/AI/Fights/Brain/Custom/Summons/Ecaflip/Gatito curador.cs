/*using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Spells;
using TreeSharp;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Summons
{
    [BrainIdentifier ((int) MonsterIdEnum.CHATON_GUERISSEUR)]
    class Gatito : Brain {
        public Gatito(AIFighter fighter) : base (fighter)
        {
            fighter.Team.FighterAdded += OnFighterAdded;

        }

        void OnFighterAdded (FightTeam team, FightActor fighter)
        {
            if (fighter is SummonedMonster && (fighter as SummonedMonster).Monster.MonsterId == (int) MonsterIdEnum.CHATON_GUERISSEUR) fighter.CastAutoSpell (new Spell (9433, (byte) fighter.Level), fighter.Cell);
            //if (fighter is SummonedMonster && (fighter as SummonedMonster).Monster.MonsterId == (int) MonsterIdEnum.LAPINO_PROTECTEUR) fighter.CastAutoSpell (new Spell ((int) SpellIdEnum.STIMULATING_WORD, (byte) fighter.Level), fighter.Cell);
        }

        public override void Play () {
            foreach (var spell in Fighter.Spells.Values) {
                FightActor target = null;
                if (Fighter.Id == (int) MonsterIdEnum.LAPINO_PROTECTEUR) {
                    target = Environment.GetNearestFighter (x => x.IsFriendlyWith (Fighter) && x != Fighter);
                } else {
                    target = Environment.GetNearestFighter (x => x.IsFriendlyWith (Fighter) && x != Fighter && x.LifePoints < x.MaxLifePoints);

                }
                var ennemy = Environment.GetNearestEnemy ();

                var selector = new PrioritySelector ();
                selector.AddChild (new Decorator (ctx => target == null, new DecoratorContinue (new RandomMove (Fighter))));
                selector.AddChild (new Decorator (ctx => spell == null, new DecoratorContinue (new FleeAction (Fighter))));

                if (target != null && spell != null)
                {
                    selector.AddChild (new PrioritySelector (
                        new Decorator (ctx => Fighter.CanCastSpell (spell, target.Cell) == SpellCastResult.OK,
                            new Sequence (
                                new SpellCastAction (Fighter, spell, target.Cell, true),
                                new PrioritySelector (
                                    new Decorator (
                                        ctx => Fighter.Stats.Health.TotalMax / 2 > Fighter.LifePoints,
                                        new FleeAction (Fighter)),
                                    new Decorator (new MoveFarFrom (Fighter, ennemy))))),
                        new Sequence (
                            new MoveNearTo (Fighter, target),
                            new Decorator (
                                ctx => Fighter.CanCastSpell (spell, target.Cell) == SpellCastResult.OK,
                                new Sequence (
                                    new SpellCastAction (Fighter, spell, target.Cell, true))))));
                }

                foreach (var action in selector.Execute (this)) {

                }
            }
        }
    }
}*/