using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Enums.Extensions;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Debuffs;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Move;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Fights.Buffs;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Ecaflip
{
    [SpellCastHandler(SpellIdEnum.FELINE_SPIRIT_108)]
    public class FelineSpiritCastHandler : DefaultSpellCastHandler
    {
        public FelineSpiritCastHandler(SpellCastInformations cast)
            : base(cast)
        {
        }
        public FightActor fighters;
        public Character character;

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();

            if (Fight.IsCellFree(TargetedCell))
                return;

            Buff[] array = base.Caster.GetBuffs((Buff entry) => entry.Spell == base.Spell).ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                Buff buff = array[i];
                base.Caster.RemoveBuff(buff);
            }
            base.Execute();
        }
    }
}