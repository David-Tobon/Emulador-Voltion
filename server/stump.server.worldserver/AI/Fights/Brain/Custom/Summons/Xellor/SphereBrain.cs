using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Spells;
using TreeSharp;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Summons
{
    [BrainIdentifier((int)MonsterIdEnum.CADRAN_DE_XELOR_264)]
    [BrainIdentifier((int)MonsterIdEnum.CADRAN_DE_XELOR_3960)]
    public class SphereBrain : Brain
    {
        public SphereBrain(AIFighter fighter) : base(fighter)
        {
            fighter.Team.FighterAdded += OnFighterAdded;
        }

        void OnFighterAdded(FightTeam team, FightActor fighter)
        {
            if (fighter != Fighter)
                return;
        }

        public override void Play()
        {
            var spell = new Spell((int)SpellIdEnum.OSCILLATION, 1);
            var target = Environment.GetNearestEnemy();
            var selector = new PrioritySelector();
            
            selector.AddChild(new Decorator(ctx => target == null, new DecoratorContinue(new RandomMove(Fighter))));
            selector.AddChild(new Decorator(ctx => spell == null, new DecoratorContinue(new FleeAction(Fighter))));

            if (target != null && spell != null)
            {

                
                //selector.AddChild(new PrioritySelector(
                //new Decorator(ctx => Fighter.CanCastSpell(spell, target.Cell) == SpellCastResult.OK,
                //    new Sequence(
                //        new SpellCastAction(Fighter, spell, target.Cell, true),
                //        new Decorator(new MoveNearTo(Fighter, target))))
                //));
                selector.AddChild(new PrioritySelector(
                    new Sequence(
                        new Decorator(
                            ctx => Fighter.CanCastSpell(spell, Fighter.Cell) == SpellCastResult.OK,
                            new Sequence(
                                new SpellCastAction(Fighter, spell, Fighter.Cell, true),
                                new FleeAction(Fighter))))));
            }

            foreach (var action in selector.Execute(this))
            {

            }
        }
    }
}