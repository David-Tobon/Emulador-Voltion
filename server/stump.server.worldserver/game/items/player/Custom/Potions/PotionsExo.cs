
/*
using NLog;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Conditions.Criterions;
using Stump.Server.WorldServer.Game.Effects;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom
{
    [ItemType(ItemTypeEnum.EXO_POTION)]
    public class PotionsExo : BasePlayerItem
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public PotionsExo(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
        }

        public override bool CanDrop(BasePlayerItem item) => true;

        public override bool Drop(BasePlayerItem dropOnItem)
        {
            if (Owner.IsInFight())
                return false;

            var potionEffect = Record.Effects[0];

            if (dropOnItem.Record.IsExo || dropOnItem.Effects.Contains(potionEffect))
            {
                Owner.SendServerMessage("Sú objeto ya tiene un Exomagueo...", System.Drawing.Color.DarkOrange);
                return false;
            }

            else
            {
                dropOnItem.Effects.Add(potionEffect);
                EffectManager.Instance.GetItemEffectHandler(potionEffect, Owner, dropOnItem).Apply();
                Owner.Inventory.RefreshItem(dropOnItem);
                Owner.Inventory.MoveItem(dropOnItem, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);
                Owner.Client.Send(new ExchangeCraftInformationObjectMessage((sbyte)2, (ushort)dropOnItem.Template.Id, (ulong)Owner.Id));
                switch (potionEffect.EffectId)
                {
                    case EffectsEnum.Effect_AddAP_111:
                        dropOnItem.Record.IsExoPa = false; break;

                    case EffectsEnum.Effect_AddMP_128:
                    case EffectsEnum.Effect_AddMP:
                        dropOnItem.Record.IsExoPm = false; break;

                    case EffectsEnum.Effect_AddRange:
                    case EffectsEnum.Effect_AddRange_136:
                        dropOnItem.Record.IsExoPo = false; break;

                    case EffectsEnum.Effect_AddSummonLimit:
                        dropOnItem.Record.IsExoInvoc = false; break;
                }
                dropOnItem.Record.IsExo = true;
                Owner.RefreshStats();
                Owner.SendServerMessage("¡Exo realizado con éxito en el objeto!", System.Drawing.Color.DarkOrange);
                return true;
        }
    } 

        public override bool CanEquip()
        {
            return false;
        }
    }
}


using NLog;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Conditions.Criterions;
using Stump.Server.WorldServer.Game.Effects;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom
{
    [ItemType(ItemTypeEnum.EXO_POTION)]
    public class PotionsExo : BasePlayerItem
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public PotionsExo(Character owner, PlayerItemRecord record) : base(owner, record) { }

        public override bool CanDrop(BasePlayerItem item) => true;

        public override bool Drop(BasePlayerItem dropOnItem)
        {
            if (Owner.IsInFight())
                return false;

            var potionEffect = Record.Effects[0];

            //var effect = GetEffectToImprove(potionEffect);

            if (dropOnItem.Effects.Exists(x => x.EffectId == EffectsEnum.Effect_AddAP_111) ||
                dropOnItem.Effects.Exists(x => x.EffectId == EffectsEnum.Effect_AddRange_136) ||
                dropOnItem.Effects.Exists(x => x.EffectId == EffectsEnum.Effect_AddRange) ||
                dropOnItem.Effects.Exists(x => x.EffectId == EffectsEnum.Effect_AddMP_128) ||
                dropOnItem.Effects.Exists(x => x.EffectId == EffectsEnum.Effect_AddMP) ||
                dropOnItem.Effects.Exists(x => x.EffectId == EffectsEnum.Effect_AddSummonLimit))
            {
                Owner.SendServerMessage("Su artículo ya tiene un efecto de Alcance, PA, PM o Invocación.", System.Drawing.Color.Crimson);
                return false;

            }
            else
            {
                dropOnItem.Effects.Add(potionEffect);
                EffectManager.Instance.GetItemEffectHandler(potionEffect, Owner, dropOnItem).Apply();
                Owner.Inventory.RefreshItem(dropOnItem);
                Owner.Inventory.MoveItem(dropOnItem, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);
                Owner.Client.Send(new ExchangeCraftInformationObjectMessage((sbyte)2, (ushort)dropOnItem.Template.Id, (ulong)Owner.Id));
                switch (potionEffect.EffectId)
                {
                    case EffectsEnum.Effect_AddAP_111:
                        dropOnItem.Record.IsExoPa = true;
                        break;

                    case EffectsEnum.Effect_AddMP_128:
                    case EffectsEnum.Effect_AddMP:
                        dropOnItem.Record.IsExoPm = true;
                        break;

                    case EffectsEnum.Effect_AddRange:
                    case EffectsEnum.Effect_AddRange_136:
                        dropOnItem.Record.IsExoPo = true;
                        break;

                    case EffectsEnum.Effect_AddSummonLimit:
                        dropOnItem.Record.IsExoInvoc = true;
                        break;
                }
                dropOnItem.Record.IsExo = true;
                Owner.RefreshStats();
                Owner.SendServerMessage("¡Exo realizado con éxito en el objeto!", System.Drawing.Color.White);
                return false;
            }
        }

        public override bool CanEquip()
        {
            return false;
        }
    }
}

*/