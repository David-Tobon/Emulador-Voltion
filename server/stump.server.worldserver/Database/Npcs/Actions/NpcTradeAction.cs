using System;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Exchanges;
using Stump.Server.WorldServer.Game.Exchanges.Trades.Npcs;

namespace Stump.Server.WorldServer.Database.Npcs.Actions
{
    [Discriminator(Discriminator, typeof(NpcActionDatabase), new System.Type[]
    {
        typeof(NpcActionRecord)
    })]
    public class NpcTradeAction : NpcActionDatabase
    {
        public const string Discriminator = "Trade";
        public override NpcActionTypeEnum[] ActionType => new NpcActionTypeEnum[] { NpcActionTypeEnum.ACTION_EXCHANGE };
        public int Kamas
        {
            get
            {
                return base.Record.GetParameter<int>(0u, false);
            }
            set
            {
                base.Record.SetParameter<int>(0u, value);
            }
        }
        public int ItemIdToGive
        {
            get
            {
                return base.Record.GetParameter<int>(1u, false);
            }
            set
            {
                base.Record.SetParameter<int>(1u, value);
            }
        }

        public NpcTradeAction(NpcActionRecord record)
            : base(record)
        {
        }

        public override void Execute(Npc npc, Character character)
        {
            NpcTradese npcDialog = new NpcTradese(character, npc, Kamas, ItemIdToGive);
            npcDialog.Open();

            if (ItemIdToGive == 30000)
            {
                character.OpenPopup("Este Npc Cambia Tus Monedas por Kamas, Pon Atención, el limite de kamas es de 2 Billones, Sí superas esa cantidad, tus kamas podran desaparecer, Por Favor guarda o gasta tus Kamas antes de superar la cantidad.");
            }
        }
    }
}