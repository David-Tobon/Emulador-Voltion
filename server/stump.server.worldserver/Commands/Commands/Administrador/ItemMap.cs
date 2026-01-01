using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game.Actors.Look;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Stump.Server.WorldServer.Commands.Commands.adicionais
{
    class ItemMapa : InGameCommand
    {
        public ItemMapa()
        {
            Aliases = new string[]
            {
                "ItemMapa"
            };
            RequiredRole = RoleEnum.Administrator;
            AddParameter<int>("id", "id", "Emote id", isOptional: false);
            AddParameter<int>("cantidad", isOptional: false);
            Description = "Regala un Item a todos los personajes del mapa";

        }
        public override void Execute(GameTrigger trigger)
        {
            if (!trigger.IsArgumentDefined("id"))
            {
                trigger.ReplyError("Especifica iD del  Item");
                return;
            }
            if (!trigger.IsArgumentDefined("cantidad"))
            {
                trigger.ReplyError("Especifica Cantidad");
                return;
            }
            var clients = trigger.Character.Map.GetAllCharacters().Where(x => Game.HavenBags.HavenBagManager.Instance.CanBeSeenInHavenBag(x, trigger.Character)).Select(v => v.Client);
            // aunque este es solo del mapa en cuestion


            var nombre = ItemManager.Instance.TryGetItemType(trigger.Get<int>("id"));
            foreach (var target in clients)
            {
                target.Character.Inventory.AddItem(ItemManager.Instance.TryGetTemplate(trigger.Get<int>("id")), trigger.Get<int>("cantidad"));
                //target.Character.OpenPopup("El Administrador [" + ((GameTrigger)trigger).Character.Name + "] te ha regalado el item: "+ nombre.Name.ToString() + " revisalo en tu inventario");
                target.Character.OpenPopup("El Administrador [" + ((GameTrigger)trigger).Character.Name + "] te ha regalado un item, revisalo en tu inventario.");

            }
        }
    }
}

 