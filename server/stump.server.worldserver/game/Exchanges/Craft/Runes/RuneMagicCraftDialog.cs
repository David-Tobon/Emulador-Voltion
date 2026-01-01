using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.Extensions;
using Stump.Core.Mathematics;
using Stump.Core.Reflection;
using Stump.Core.Timers;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Exchanges.Trades;
using Stump.Server.WorldServer.Game.Exchanges.Trades.Players;
using Stump.Server.WorldServer.Game.Interactives;
using Stump.Server.WorldServer.Game.Interactives.Skills;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Game.Jobs;
using Stump.Server.WorldServer.Handlers.Basic;



namespace Stump.Server.WorldServer.Game.Exchanges.Craft.Runes
{
    public abstract class RuneMagicCraftDialog : BaseCraftDialog
    {
        public const int MAX_STAT_POWER = 100;
        public const int AUTOCRAFT_INTERVAL = 1000;

        private TimedTimerEntry m_autoCraftTimer;

        public RuneMagicCraftDialog(InteractiveObject interactive, Skill skill, Job job)
            : base(interactive, skill, job)
        {
        }

        public RuneCrafter RuneCrafter => Crafter as RuneCrafter;

        public PlayerTradeItem ItemToImprove
        {
            get;
            private set;
        }
        public PlayerTradeItem SpecialRune
        {
            get;
            private set;
        }

        public PlayerTradeItem SignatureRune
        {
            get;
            private set;
        }

        public IEnumerable<EffectInteger> ItemEffects => ItemToImprove.Effects.OfType<EffectInteger>();

        public PlayerTradeItem Rune
        {
            get;
            private set;
        }

        //public BasePlayerItem Item
        //{
        //    get;
        //    private set;
        //}



        public PlayerTradeItem Potion
        {
            get; private set;
        }

        public PlayerTradeItem Orbe
        {
            get; private set;
        }

        public virtual void Open()
        {
            FirstTrader.ItemMoved += OnItemMoved;
            SecondTrader.ItemMoved += OnItemMoved;
        }

        public override void Close()
        {
            StopAutoCraft();
        }

        public void StopAutoCraft(ExchangeReplayStopReasonEnum reason = ExchangeReplayStopReasonEnum.STOPPED_REASON_USER)
        {
            if (m_autoCraftTimer != null)
            {
                m_autoCraftTimer.Stop();
                m_autoCraftTimer = null;

                OnAutoCraftStopped(reason);
                ChangeAmount(1);
            }
        }

        protected virtual void OnAutoCraftStopped(ExchangeReplayStopReasonEnum reason)
        {

        }

        protected virtual void OnItemMoved(Trader trader, TradeItem item, bool modified, int difference)
        {
            var playerItem = item as PlayerTradeItem;

            if (playerItem == null)
                return;

            if (item.Template.Type.ItemType == ItemTypeEnum.RUNE_DE_FORGEMAGIE && (playerItem != Rune || playerItem.Stack == 0))
            {
                Rune = playerItem.Stack > 0 ? playerItem : null;
            }
            if (item.Template.Type.ItemType == ItemTypeEnum.POTION_DE_FORGEMAGIE && (playerItem != Potion || playerItem.Stack == 0))
            {
                Potion = playerItem.Stack > 0 ? playerItem : null;
            }
            if (item.Template.Type.ItemType == ItemTypeEnum.ORBE_DE_FORGEMAGIE && (playerItem != Orbe || playerItem.Stack == 0))
            {
                Orbe = playerItem.Stack > 0 ? playerItem : null;
            }
            else if (IsItemEditable(item) && (playerItem != ItemToImprove || playerItem.Stack == 0))
            {
                ItemToImprove = playerItem.Stack > 0 ? playerItem : null;
            }
            else if (item.Template.Id == (int)ItemIdEnum.RUNE_DE_SIGNATURE_7508 && (playerItem != SignatureRune || playerItem.Stack == 0))
            {
                SignatureRune = playerItem.Stack > 0 ? playerItem : null;
            }
            else if ((item.Template.Type.ItemType == ItemTypeEnum.RUNE_DE_TRANSCENDANCE || item.Template.Type.ItemType == ItemTypeEnum.RUNE_DE_CORUPTION) && (playerItem != SpecialRune || playerItem.Stack == 0))
            {
                SpecialRune = playerItem.Stack > 0 ? playerItem : null;
            }
        }

        public bool IsItemEditable(IItem item)
        {
            return Skill.SkillTemplate.ModifiableItemTypes.Contains((int)item.Template.TypeId);
        }


        public override bool CanMoveItem(BasePlayerItem item)
        {
            return item.Template.TypeId == (int)ItemTypeEnum.RUNE_DE_FORGEMAGIE || item.Template.TypeId == (int)ItemTypeEnum.POTION_DE_FORGEMAGIE || item.Template.TypeId == (int)ItemTypeEnum.ORBE_DE_FORGEMAGIE || Skill.SkillTemplate.ModifiableItemTypes.Contains((int)item.Template.TypeId);
        }

        protected virtual void OnRuneApplied(CraftResultEnum result, MagicPoolStatus poolStatus)
        {
        }

        public void ApplyAllRunes()
        {
            if (m_autoCraftTimer != null)
                StopAutoCraft();

            if (Amount == 1 || Amount == 0)
                ApplyRune();
            else
                AutoCraft();
        }

        private void AutoCraft()
        {
            ApplyRune();
            if (ItemToImprove != null && Rune != null && Amount == -1)
                m_autoCraftTimer = Crafter.Character.Area.CallDelayed(AUTOCRAFT_INTERVAL, AutoCraft);
            else
                StopAutoCraft(ExchangeReplayStopReasonEnum.STOPPED_REASON_OK);
        }

        private static bool CheckEffects(BasePlayerItem item)
        {
            foreach (var effect in item.Template.Effects)
            {
                var final = item.Effects.FirstOrDefault(x => x.EffectId == effect.EffectId);
                if (final == null)
                    return false;
            }
            return true;
        }

        public void ApplyRune()
        {
            //if (ItemToImprove == null || Rune == null || Potion == null || Orbe == null)
            //    return;


            //Objeto de Apariencia
            if (ItemToImprove.Effects.Exists(x => x.EffectId == EffectsEnum.Effect_LivingObjectId))
            {
                Crafter.Character.SendServerMessage("[Voltion] No puedes maguear con un objeto de apariencia vinculado, por favor liberelo e intentelo de nuevo.");
                return;
            }

            if (ItemToImprove.Effects.Exists(x => x.EffectId == EffectsEnum.Effect_Appearance || x.EffectId == EffectsEnum.Effect_Apparence_Wrapper))
            {
                Crafter.Character.SendServerMessage("[Voltion] No puedes maguear con un objeto de apariencia vinculado, por favor liberelo e intentelo de nuevo.");
                return;
            }

            //Forja Futura Imposible
            if (ItemToImprove.Effects.FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_CantFM) != null)
            {
                Crafter.Character.OpenPopup("[Voltion] El objeto que esta intentando maguear tiene un encantamiento que no le permite seguirlo mejorando.");
                return;
            }








            var rune = Rune;
            var potion = Potion;
            var orbe = Orbe;

            var specialrune = SpecialRune;
            var signature = SignatureRune;


            #region RunaFirma 

            if (signature != null)
            {

                if (signature != null)
                {
                    signature.Owner.Inventory.RemoveItem(signature.PlayerItem, 1);
                    if (signature.Owner.Id == Crafter.Id)
                        Crafter.MoveItem((uint)signature.Guid, -1);
                    else
                    {
                        if (signature.Stack <= 1)
                        {
                            signature.Stack = 0;
                            signature.Owner.Inventory.RemoveItem(signature.PlayerItem);
                        }
                        else
                        {
                            signature.Stack -= 1;
                        }
                        (this as MultiRuneMagicCraftDialog)?.OnItemMoved(Crafter, signature, true, 1);
                    }

                    if (ItemToImprove.Effects.Exists(x => x.EffectId == EffectsEnum.Effect_985))
                    {
                        ItemToImprove.Effects.RemoveAll(x => x.EffectId == EffectsEnum.Effect_985);
                        ItemToImprove.Effects.Add(new EffectString(EffectsEnum.Effect_985, Crafter.Character.Name));
                    }
                    else
                    {
                        ItemToImprove.Effects.Add(new EffectString(EffectsEnum.Effect_985, Crafter.Character.Name));
                    }
                    Crafter.Character.SendServerMessage("[Voltion] Gracias a tu Runa de firma dejaste tu nombre marcado en este objeto.");
                }
            }
            #endregion

            #region Orbe
            if (orbe != null)
            {
                if (orbe.Template.Level < ItemToImprove.Template.Level)
                {
                    Crafter.Character.OpenPopup("[Voltion] El nivel de este orbe es demasiado bajo para este objeto");

                    OnRuneApplied(CraftResultEnum.CRAFT_SUCCESS, MagicPoolStatus.UNMODIFIED);
                }
                else
                {
                    //delete rune
                    orbe.Owner.Inventory.RemoveItem(orbe.PlayerItem, 1);
                    if (orbe.Owner.Id == Crafter.Id)
                        Crafter.MoveItem((uint)orbe.Guid, -1);
                    else
                    {
                        if (orbe.Stack <= 1)
                        {
                            orbe.Stack = 0;
                            orbe.Owner.Inventory.RemoveItem(orbe.PlayerItem);
                        }
                        else
                        {
                            orbe.Stack -= 1;
                        }
                        (this as MultiRuneMagicCraftDialog)?.OnItemMoved(Crafter, orbe, true, 1);
                    }
                    //final
                    var effects = Singleton<ItemManager>.Instance.GenerateItemEffects(ItemToImprove.Template);
                    ItemToImprove.PlayerItem.Effects = effects;
                    OnRuneApplied(CraftResultEnum.CRAFT_SUCCESS, MagicPoolStatus.UNMODIFIED);
                }
            }
            #endregion
            #region Runas Especiales
            else if (specialrune != null)
            {
                if (ItemToImprove.Effects.Where(x => x.EffectId != EffectsEnum.Effect_PowerSink).Any(x => IsExotic(x) || IsOverMax(x as EffectInteger)))
                {
                    Crafter.Character.OpenPopup("No puedes aplicar esta runa a un Objeto con Exomagueo o Runa de Firma.");
                    return;
                }

                specialrune.Owner.Inventory.RemoveItem(specialrune.PlayerItem, 1);
                if (specialrune.Owner.Id == Crafter.Id)
                    Crafter.MoveItem((uint)specialrune.Guid, -1);
                else
                {
                    if (specialrune.Stack <= 1)
                    {
                        specialrune.Stack = 0;
                        specialrune.Owner.Inventory.RemoveItem(specialrune.PlayerItem);
                    }
                    else
                    {
                        specialrune.Stack -= 1;
                    }
                    (this as MultiRuneMagicCraftDialog)?.OnItemMoved(Crafter, specialrune, true, 1);
                }

                foreach (var effect in specialrune.Effects.OfType<EffectInteger>().Where(x => x.EffectId != EffectsEnum.Effect_FMPercentOfChance))
                {
                    var percent = (specialrune.Effects.FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_FMPercentOfChance) as EffectInteger);
                    if (percent == null)
                        return;

                    var existantEffect = GetEffectToImprove(effect);

                    var rand = new CryptoRandom();
                    var randNumber = (int)(rand.NextDouble() * 100);

                    if (randNumber <= percent.Value)
                    {
                        if (effect.Template.Operator != "-")
                            BoostEffect(effect);
                        else
                            BoostEffect(effect);   //Linea 345

                        OnRuneApplied(CraftResultEnum.CRAFT_SUCCESS, MagicPoolStatus.UNMODIFIED);
                    }
                }
            }


            #endregion
            else if (potion != null)
            {
                try
                {
                    ApliedEffectsPotion(potion);
                }
                catch { }

            }

            #region Runas % Daños con Armas
            else if (rune != null)
            {

                // OnRuneApplied(CraftResultEnum.CRAFT_SUCCESS, MagicPoolStatus.UNMODIFIED);


                if (RunesShields.Contains(rune.Template.Id))
                {


                    if (ItemToImprove.PlayerItem.Template.TypeId == 82)
                    {
                        ApliedEffectsRune(rune);
                    }
                    else
                    {

                        switch (this.ItemToImprove.Owner.Account.Lang)
                        {
                            case "es":
                                Crafter.Character.OpenPopup("Solo puedes aplicar este efecto a un item de tipo escudo.");
                                break;

                        }
                        // OnRuneApplied(CraftResultEnum.CRAFT_SUCCESS, MagicPoolStatus.UNMODIFIED);
                    }


                }

                else
                {
                    ApliedEffectsRune(rune);
                }




            }
            else
                return;

            ItemToImprove.PlayerItem.Invalidate();

        }
        #endregion

        private void ApliedEffectsRune(PlayerTradeItem rune)
        {
            rune.Owner.Inventory.RemoveItem(rune.PlayerItem, 1);
            Crafter.MoveItem((uint)rune.Guid, -1);
            foreach (var effect in rune.Effects.OfType<EffectInteger>())
            {
                var existantEffect = GetEffectToImprove(effect);

                double criticalSuccess, neutralSuccess, criticalFailure;
                GetChances(existantEffect, effect, out criticalSuccess, out neutralSuccess, out criticalFailure);

                var rand = new CryptoRandom();
                var randNumber = (int)(rand.NextDouble() * 100);

                if (randNumber <= criticalSuccess)
                {
                    BoostEffect(effect);

                    OnRuneApplied(CraftResultEnum.CRAFT_SUCCESS, MagicPoolStatus.UNMODIFIED);
                }
                else if (randNumber <= criticalSuccess + neutralSuccess)
                {
                    BoostEffect(effect);
                    int residual = DeBoostEffect(effect);

                    OnRuneApplied(CraftResultEnum.CRAFT_SUCCESS, GetMagicPoolStatus(residual));
                }
                else
                {
                    int residual = DeBoostEffect(effect);

                    OnRuneApplied(CraftResultEnum.CRAFT_FAILED, GetMagicPoolStatus(residual));
                }

            }
        }
        private MagicPoolStatus GetMagicPoolStatus(int residual)
        {
            return residual == 0 ? MagicPoolStatus.UNMODIFIED : (residual > 0 ? MagicPoolStatus.INCREASED : MagicPoolStatus.DECREASED);
        }

        private void BoostEffect(EffectInteger runeEffect)
        {
            var effect = GetEffectToImprove(runeEffect);

            if (effect != null)
            {
                foreach (EffectDice item in ItemToImprove.Template.Effects)
                {

                    if (effect.Value < (item.DiceFace + 5) && effect.EffectId == item.EffectId)
                    {
                        if ((int)effect.EffectId == 111 && effect.Value == 1)
                        {
                            ItemToImprove.Effects.Remove(effect);
                            ItemToImprove.Effects.Add(new EffectInteger((EffectsEnum)effect.Template.OppositeId, effect.Value));
                        }
                        else if ((int)effect.EffectId == 128 && effect.Value == 1)
                        {
                            ItemToImprove.Effects.Remove(effect);
                            ItemToImprove.Effects.Add(new EffectInteger((EffectsEnum)effect.Template.OppositeId, effect.Value));
                        }
                        else if (effect.EffectId == item.EffectId)
                        {
                            effect.Value += (short)((effect.Template.BonusType == -1 ? -1 : 1) * runeEffect.Value);

                            if (effect.Value == 0)
                                ItemToImprove.Effects.Remove(effect);
                            else if (effect.Value > 0 && effect.Value <= runeEffect.Value && effect.Template.OppositeId > 0) // from negativ to positiv
                            {
                                ItemToImprove.Effects.Remove(effect);
                                ItemToImprove.Effects.Add(new EffectInteger((EffectsEnum)effect.Template.OppositeId, effect.Value));
                            }
                        }

                    }
                }
            }
            else
            {
                ItemToImprove.Effects.Add(new EffectInteger(runeEffect.EffectId, runeEffect.Value));
            }
        }


        private void ApliedEffectsPotion(PlayerTradeItem potion)
        {

            foreach (var effect in ItemToImprove.Effects.OfType<EffectInteger>())
            {
                if (effect.EffectId == EffectsEnum.Effect_DamageNeutral)
                {
                    var existantEffect = GetEffectToImprove(effect);



                    double criticalSuccess, neutralSuccess, criticalFailure;
                    GetChances(existantEffect, effect, out criticalSuccess, out neutralSuccess, out criticalFailure);

                    var rand = new CryptoRandom();
                    var randNumber = (int)(rand.NextDouble() * 100);

                    if (randNumber <= criticalSuccess)
                    {
                        BoostEffectPotin(effect);

                        OnRuneApplied(CraftResultEnum.CRAFT_SUCCESS, MagicPoolStatus.UNMODIFIED);
                    }
                    else if (randNumber <= criticalSuccess + neutralSuccess)
                    {
                        BoostEffectPotin(effect);
                        int residual = DeBoostEffect(effect);

                        OnRuneApplied(CraftResultEnum.CRAFT_SUCCESS, GetMagicPoolStatus(residual));
                    }
                    else
                    {
                        int residual = DeBoostEffect(effect);

                        OnRuneApplied(CraftResultEnum.CRAFT_FAILED, GetMagicPoolStatus(residual));
                    }
                }
            }
            potion.Owner.Inventory.RemoveItem(potion.PlayerItem, 1);
            Crafter.MoveItem((uint)potion.Guid, -1);
        }

        private void BoostEffectPotin(EffectInteger potionEffect)
        {
            //var effect = GetEffectToImprove(potionEffect);
            //var effect_ = (EffectDice)effect;
            var effect = ItemToImprove.Effects.Find(x => x.EffectId == EffectsEnum.Effect_DamageNeutral);
            var effectCac = (EffectsEnum)GetWeaponEffect(Potion.PlayerItem.Template.Id);

            if (effect != null)
            {

                //ItemToImprove.Effects.Remove(effect);
                //ItemToImprove.Effects.Add(new EffectDice((short)effectCac, 0, effect_.DiceNum, effect_.DiceFace, new EffectBase()));

                ItemToImprove.Effects.Remove(effect);
                var effect_ = (EffectDice)effect;
                ItemToImprove.Effects.Add(new EffectDice((short)effectCac, 0, effect_.DiceNum, effect_.DiceFace, new EffectBase()));
            }
        }

        private int DeBoostEffect(EffectInteger runeEffect)
        {
            var pwrToLose = (int)Math.Ceiling(EffectManager.Instance.GetEffectPower(runeEffect));
            short residual = 0;

            if (ItemToImprove.PlayerItem.PowerSink > 0)
            {
                residual = (short)-Math.Min(pwrToLose, ItemToImprove.PlayerItem.PowerSink);
                ItemToImprove.PlayerItem.PowerSink += residual;
                pwrToLose += residual;
            }

            if (pwrToLose == 0)
                return residual;

            while (pwrToLose > 0)
            {
                var effect = GetEffectToDown(runeEffect);

                if (effect == null)
                    break;

                var maxLost = (int)Math.Ceiling(EffectManager.Instance.GetEffectBasePower(runeEffect) / Math.Abs(EffectManager.Instance.GetEffectBasePower(effect)));

                var rand = new CryptoRandom();
                var lost = rand.Next(1, maxLost + 1);

                var oldValue = effect.Value;

                effect.Value -= (short)((effect.Template.BonusType == -1 ? -1 : 1) * lost);
                pwrToLose -= (int)Math.Ceiling(lost * Math.Abs(EffectManager.Instance.GetEffectBasePower(effect)));

                if (effect.Value == 0 || (effect.Value < 0 && oldValue > 0))
                    ItemToImprove.Effects.Remove(effect);
                else if (effect.Value < 0 && effect.Value >= -lost && effect.Template.OppositeId > 0) // from positiv to negativ stat
                {
                    ItemToImprove.Effects.Remove(effect);
                    ItemToImprove.Effects.Add(new EffectInteger((EffectsEnum)effect.Template.OppositeId, (short)-effect.Value));
                }

            }

            residual = (short)(pwrToLose < 0 ? -pwrToLose : 0);
            ItemToImprove.PlayerItem.PowerSink += residual;

            return residual;
        }

        private EffectInteger GetEffectToImprove(EffectInteger runeEffect)
        {
            return ItemEffects.FirstOrDefault(x => x.EffectId == runeEffect.EffectId || (x.Template.OppositeId != 0 && x.Template.OppositeId == runeEffect.Id));
        }

        private EffectInteger GetEffectToDown(EffectInteger runeEffect)
        {
            var effectToImprove = GetEffectToImprove(runeEffect);
            // recherche de jet exotique
            var exoticEffect = ItemEffects.Where(x => IsExotic(x) && x != effectToImprove).RandomElementOrDefault();

            if (exoticEffect != null)
                return exoticEffect;

            // recherche de jet overmax
            var overmaxEffect = ItemEffects.Where(x => IsOverMax(x, runeEffect) && x != effectToImprove).RandomElementOrDefault();

            if (overmaxEffect != null)
                return overmaxEffect;

            var rand = new CryptoRandom();
            foreach (var effect in ItemEffects.ShuffleLinq().Where(x => x != effectToImprove))
            {
                if (EffectManager.Instance.GetEffectPower(effect) - EffectManager.Instance.GetEffectPower(runeEffect) < MAX_STAT_POWER)
                    continue;

                if (rand.NextDouble() <= EffectManager.Instance.GetEffectPower(runeEffect) / Math.Abs(EffectManager.Instance.GetEffectBasePower(effect)))
                    return effect;
            }

            return ItemEffects.FirstOrDefault(x => x != effectToImprove);
        }

        private bool IsExotic(EffectBase effect)
        {
            return ItemToImprove.Template.Effects.All(x => x.EffectId != effect.EffectId);
        }

        private double GetExoticPower()
        {
            return ItemToImprove.Effects.Where(IsExotic).OfType<EffectInteger>().Sum(x => EffectManager.Instance.GetEffectPower(x));
        }

        private bool IsOverMax(EffectInteger effect)
        {
            var template = GetTemplateEffect(effect);

            return effect.Template.BonusType > -1 && effect.Value > template?.Max;
        }

        private bool IsOverMax(EffectInteger effect, EffectInteger runeEffect)
        {
            var template = GetTemplateEffect(effect);

            return effect.Template.BonusType > -1 && effect.Value + runeEffect.Value > template?.Max;
        }

        private EffectDice GetTemplateEffect(EffectBase effect)
        {
            return ItemToImprove.Template.Effects.OfType<EffectDice>().FirstOrDefault(x => x.EffectId == effect.EffectId || (x.Template.OppositeId > 0 && x.Template.OppositeId == (int)effect.EffectId));
        }

        private void GetChances(EffectInteger effectToImprove, EffectInteger runeEffect, out double criticalSuccess, out double neutralSuccess, out double criticalFailure)
        {
            var minPwr = EffectManager.Instance.GetItemMinPower(ItemToImprove);
            var maxPwr = EffectManager.Instance.GetItemMaxPower(ItemToImprove);
            var pwr = EffectManager.Instance.GetItemPower(ItemToImprove);

            double itemStatus = Math.Max(0, GetProgress(pwr, maxPwr, minPwr) * 100);
            var parentEffect = GetTemplateEffect(runeEffect);

            if (effectToImprove != null &&
                (EffectManager.Instance.GetEffectPower(effectToImprove) + EffectManager.Instance.GetEffectPower(runeEffect) > MAX_STAT_POWER ||
                GetExoticPower() > MAX_STAT_POWER))

            {
                neutralSuccess = 0;
                criticalSuccess = 0;
                criticalFailure = 100;
                return;
            }

            double effectStatus;
            double diceFactor;
            double itemFactor;
            double levelSuccess;
            double effectSuccess;
            double itemSuccess;
            if (parentEffect == null) // exo
            {
                effectStatus = 100;
                itemStatus = 89 + Math.Sqrt(EffectManager.Instance.GetEffectPower(runeEffect)) + Math.Sqrt(itemStatus);
                diceFactor = 40;
                itemFactor = 54;
                levelSuccess = 5;
            }
            else
            {
                effectStatus = Math.Max(0, GetProgress(effectToImprove?.Value ?? 0, parentEffect.Max, parentEffect.Min) * 100);

                if (effectToImprove != null && IsOverMax(effectToImprove, runeEffect))
                {

                    itemStatus = Math.Max(itemStatus, effectStatus / 2);
                    effectStatus += EffectManager.Instance.GetEffectPower(runeEffect);
                }

                diceFactor = 30;
                itemFactor = 50;
                levelSuccess = 5;
            }

            effectStatus = Math.Min(100, effectStatus);
            itemStatus = Math.Min(99, itemStatus);

            if (effectStatus >= 80)
                effectSuccess = diceFactor * effectStatus / 100;
            else
                effectSuccess = effectStatus / 4;

            if (itemStatus >= 50)
                itemSuccess = itemFactor * itemStatus / 100;
            else
                itemSuccess = itemStatus;

            neutralSuccess = 50d;
            criticalSuccess = Math.Max(1, 100 - Math.Ceiling(effectSuccess + itemSuccess
                ));

            if (criticalSuccess > 50)
                neutralSuccess = 100 - criticalSuccess;
            else if (criticalSuccess < 25)
                neutralSuccess = 25 + criticalSuccess;

            criticalFailure = 100 - (criticalSuccess + neutralSuccess);


        }

        private double GetProgress(double value, double max, double min)
        {
            if (min < 0 || max < 0)
            {
                var x = max;
                max = -min;
                min = -x;
            }

            if (max == min && max != 0) return value / max;
            else if (max == 0) return 1d;
            else return (value - min) / (max - min);
        }

        //public static Dictionary<int, int> RunesShields = new Dictionary<int, int>()
        //{
        //    {1, 18724 },
        //    {2, 18723 },
        //    {3, 18722 },
        //    {4, 18720 },
        //    {5, 18719 },
        //    {6, 18721 }
        //};

        public static List<int> RunesShields = new List<int>()
        {
            {18724 },
            {18723 },
            {18722 },
            {18720 },
            {18719 },
            {18721 }
        };

        #region Get Effect
        protected int GetWeaponEffect(int integerEffect)
        {
            switch (integerEffect)
            {
                case 1345:
                    return 99;
                case 1346:
                    return 96;
                case 1347:
                    return 98;
                case 1348:
                    return 97;
                default:
                    return 2;
            }
        }
        #endregion
    }
}