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
    class KamasMapa : InGameCommand
    {
        public KamasMapa()
        {
            Aliases = new string[]
            {
                "KamasMapa"
            };
            RequiredRole = RoleEnum.Administrator;
            //AddParameter<int>("id", "id", "Emote id", isOptional: false);
            AddParameter<int>("cantidad", isOptional: false);
            Description = "Regala Kamas a todos los personajes del mapa";

        }
        public override void Execute(GameTrigger trigger)
        {
            
            if (!trigger.IsArgumentDefined("cantidad"))
            {
                trigger.ReplyError("Especifica Cantidad");
                return;
            }
            var clients = trigger.Character.Map.GetAllCharacters().Where(x => Game.HavenBags.HavenBagManager.Instance.CanBeSeenInHavenBag(x, trigger.Character)).Select(v => v.Client);


            int CantidadKamas = trigger.Get<int>("cantidad");
            
                foreach (var target in clients)
                {
                    target.Character.Inventory.AddKamas((ulong)CantidadKamas);
                    
                    target.Character.OpenPopup("El Administrador [" + ((GameTrigger)trigger).Character.Name + "] te ha regalado: " + CantidadKamas + " Kamas.");

                }
            
            
        }
    }
}

 