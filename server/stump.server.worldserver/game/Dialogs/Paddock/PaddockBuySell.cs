/*


using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Interactives;
using Stump.Server.WorldServer.Handlers.Dialogs;
using Stump.Server.WorldServer.Handlers.Guilds;
using Stump.Server.WorldServer.Handlers.Inventory;
using MapPaddock = Stump.Server.WorldServer.Game.Maps.Paddocks.Paddock;

namespace Stump.Server.WorldServer.Game.Dialogs.Paddock
{
    public class PaddockBuySell : IDialog
    {
        public PaddockBuySell(Character character, MapPaddock paddock, bool bsell, ulong price, InteractiveObject InteractiveObjecta)
        {
            Character = character;
            Paddock = paddock;
            Bsell = bsell;
            Price = price;
            m_InteractiveObjecta = InteractiveObjecta;
        }

        public Character Character
        {
            get;
            private set;
        }

        public MapPaddock Paddock
        {
            get;
            private set;
        }
        public bool Bsell
        {
            get;
            private set;
        }
        public ulong Price
        {
            get;
            private set;
        }
        private InteractiveObject m_InteractiveObjecta
        {
            get;
            set;
        }


        public DialogTypeEnum DialogType => DialogTypeEnum.DIALOG_PURCHASABLE;

        #region IDialog Members

        public void Open()
        {
            Character.SetDialog(this);

            InventoryHandler.SendExchangeStartPaddockBuySell(Character.Client, Bsell, (uint)Character.Id, Price);
        }
        public void Close()
        {
            Character.CloseDialog(this);
            DialogHandler.SendLeaveDialogMessage(Character.Client, DialogType);
        }

        public void BuyPaddock(ulong proposedPrice)
        {
            // verify price? proposedPrice
            if (this.Paddock == null)
            {
                this.Close();
                return; 
            }
            if (this.Paddock.Price != (int)proposedPrice)
            {
                this.Close();
                return;
            }

            if (Character.Kamas < (int)proposedPrice)
                {
                 //Vous n'avez pas assez de kamas pour effectuer cette action.
                 Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 82);
                this.Close();
                return ;
                }
            if (Paddock.Guild?.Boss?.Character !=null)
            Paddock.Guild.Boss.Character.Inventory.AddKamas((int)proposedPrice);
            else if(Paddock.Guild !=null) 
            Items.ItemManager.CreatItemOffline(Paddock.Guild.Boss.Id, 15399, 1, (int)proposedPrice);
            Paddock.OnSale = false;
           
            Paddock.Locked = false;
            Paddock.Price = 0;
            //send for all in map



            //Character.Map.ForEach(entry => Handlers.Context.ContextHandler.SendGameContextDestroyMessage(entry.Client));
            //Character.Map.ForEach(entry => Handlers.Context.ContextHandler.SendGameContextCreateMessage(entry.Client, 1));


            if (Paddock.Guild != null)
                Paddock.Guild.RemovePaddock(Paddock);
            Paddock.Guild = Character.Guild;
            Character.Guild.AddPaddock(Paddock);
            Character.Map.ForEach(entry => Paddock.GetPaddockPropertiesGuildMessage());
            Paddock.Map.Refresh(m_InteractiveObjecta);
            m_InteractiveObjecta.SetInteractiveState(InteractiveStateEnum.STATE_NORMAL);

            //GuildHandler.SendGuildInformationsPaddocksMessage(Character.Guild.Clients, Character.Guild);
            //GuildHandler.SendGuildInformationsPaddocksMessage(Paddock.Guild.Clients, Paddock.Guild);
            //Character.Map.ForEach(entry => Handlers.Context.RolePlay.ContextRoleplayHandler.SendMapComplementaryInformationsDataMessage(entry.Client));

            Character.Inventory.SubKamas((int)proposedPrice);
            this.Close();
        }
        public void SellPaddock(ulong Price, bool forSale)
        {
            // verify price? proposedPrice
            if (this.Paddock == null)
            {
                this.Close();
                return;
            }

            if (forSale == false)
            {
                Paddock.Locked = true;
                Paddock.OnSale = false;
                Paddock.Price = 0;
               
            }
            else
            {
                Paddock.Price = (int)Price;
                Paddock.OnSale = true;
                Paddock.Locked = false;
            }



            //send for all in map
            Paddock.Map.Refresh(m_InteractiveObjecta);
            Character.Map.ForEach(entry => Paddock.GetPaddockPropertiesGuildMessage());
            this.Close();

        }
        #endregion
    }
}

*/