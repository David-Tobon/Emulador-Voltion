using System.Linq;
using NLog;
using Stump.Core.Attributes;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Items;

namespace Stump.Server.WorldServer.Game.Actors.RolePlay.Characters
{
    public class PrestigeManager : Singleton<PrestigeManager>
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public const int ItemForBonus = 14230;

        public static ItemTemplate BonusItem;

        [Variable]
        public static short[] PrestigeTitles =
        {
            // 527, 528, 529, 530, 531, 532, 533, 534, 535, 536
        };

        private static readonly EffectInteger[][] m_prestigesBonus =
        {
          //Prestigio 1
            new[] {new EffectInteger(EffectsEnum.Effect_AddVitality, 50)}, 
               
            //Prestigio 2

            new[]
            {
                new EffectInteger(EffectsEnum.Effect_AddAirElementReduction, 20),
                new EffectInteger(EffectsEnum.Effect_AddEarthElementReduction, 20),
                new EffectInteger(EffectsEnum.Effect_AddFireElementReduction, 20),
                new EffectInteger(EffectsEnum.Effect_AddWaterElementReduction, 20),
                new EffectInteger(EffectsEnum.Effect_AddNeutralElementReduction, 20)
            },


            //Prestigio 3

            new[] {new EffectInteger(EffectsEnum.Effect_IncreaseDamage_138, 25)},

            //Prestigio 4
            new[] {new EffectInteger(EffectsEnum.Effect_AddCriticalHit, 5)},



            //Prestigio 5
            new[] {new EffectInteger(EffectsEnum.Effect_AddVitality, 100)},

            //Prestigio 6
            new[]
            {
                new EffectInteger(EffectsEnum.Effect_AddChance, 50),
                new EffectInteger(EffectsEnum.Effect_AddIntelligence, 50),
                new EffectInteger(EffectsEnum.Effect_AddWisdom, 50),
                new EffectInteger(EffectsEnum.Effect_AddAgility, 50),
                new EffectInteger(EffectsEnum.Effect_AddStrength, 50)
            },

            //Prestigio 7
            new[] {new EffectInteger(EffectsEnum.Effect_AddDamageBonus, 15)},

            //Prestigio 8
            
            new[] {new EffectInteger(EffectsEnum.Effect_IncreaseDamage_138, 100)},


            //Prestigio 9
            
            new[]
            {
                new EffectInteger(EffectsEnum.Effect_AddChance, 100),
                new EffectInteger(EffectsEnum.Effect_AddIntelligence, 100),
                new EffectInteger(EffectsEnum.Effect_AddWisdom, 100),
                new EffectInteger(EffectsEnum.Effect_AddAgility, 100),
                new EffectInteger(EffectsEnum.Effect_AddStrength, 100)
            },

            //Prestigio 10
            new[] {
            new EffectInteger(EffectsEnum.Effect_AddRange, 1),
            new EffectInteger(EffectsEnum.Effect_AddMP_128, 1),
            new EffectInteger(EffectsEnum.Effect_AddAP_111, 1),
            },
            

            //Prestigio +
            new[] {new EffectInteger(EffectsEnum.Effect_IncreaseDamage_138, 20)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddVitality, 50)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddCriticalHit, 7)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddRange, 1)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddMP_128, 1)},


            };

        private bool m_disabled;

        public bool PrestigeEnabled
        {
            get { return !m_disabled; }
        }

        [Initialization(typeof(ItemManager), Silent = true)]
        public void Initialize()
        {
            BonusItem = ItemManager.Instance.TryGetTemplate(ItemForBonus);

            if (BonusItem != null)
                return;

            logger.Error("Item {0} not found, prestiges disabled", ItemForBonus);
            m_disabled = true;
        }

        public EffectInteger[] GetPrestigeEffects(int rank)
        {
            return m_prestigesBonus.Take(rank).SelectMany(x => x.Select(y => (EffectInteger)y.Clone())).ToArray();
        }

        public short GetPrestigeTitle(int rank)
        {
            return PrestigeTitles[rank - 1];
        }
    }
}