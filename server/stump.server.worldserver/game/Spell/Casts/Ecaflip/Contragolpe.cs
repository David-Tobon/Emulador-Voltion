using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Buffs;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Ecaflip
{
    [SpellCastHandler(SpellIdEnum.REPERCUSSION)]
    [SpellCastHandler(SpellIdEnum.REPERCUSSION_111)]
    [SpellCastHandler(SpellIdEnum.REPERCUSSION_7467)]
    public class Contragolpe : DefaultSpellCastHandler
    {
        public Contragolpe(SpellCastInformations cast) : base(cast) { }

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();


            List<SpellEffectHandler> effects = base.Handlers.ToList();


            Handlers = effects.ToArray();

            var Target = Fight.GetOneFighter(TargetedCell);
            if (Target != null)
            {

                var h = Handlers;
                short shieldAmount = (short)(Target.Level * 6);

                var effect = new EffectDice(EffectsEnum.Effect_AddShieldPercent, shieldAmount, shieldAmount, shieldAmount);

                var actorBuffId = Caster.PopNextBuffId();
                var handler = EffectManager.Instance.GetSpellEffectHandler(effect, Caster, this, TargetedCell, Critical);

                var escudo = new StatBuff(actorBuffId, Target, Caster, handler, Spell, shieldAmount, PlayerFields.Shield, false, FightDispellableEnum.DISPELLABLE_BY_DEATH, Target);
                escudo.Duration = 1;

                if (Target.Level > 200)
                {
                    shieldAmount = (short)(200 * 6);
                }

                if (Critical == true)
                {
                    shieldAmount = (short)(200 * 6.9);

                }

                escudo.Value = shieldAmount;
                escudo.Dice.Value = shieldAmount;
                escudo.Dice.DiceNum = shieldAmount;
                Target.AddBuff(escudo);

                base.Execute();
            }
        }
    }
}