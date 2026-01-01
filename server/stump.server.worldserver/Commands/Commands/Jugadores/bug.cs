using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Database.Bug;
using Stump.Server.WorldServer.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Commands.Commands.PlayersCommands
{
    class BugCommand : InGameCommand
    {
        public BugCommand()
        {
            Aliases = new string[]
            {
                "bug"
            };
            Description = "Usa este comando para reportar bugs de la manera más clara posible ( Ayudate de los comandos /MAPID y /CELLID )";
            RequiredRole = Stump.DofusProtocol.Enums.RoleEnum.Player;
            AddParameter<string>("saisie");
        }
        public override void Execute(GameTrigger trigger)
        {
            string saisie = trigger.Get<string>("saisie");
            if (saisie != null && saisie != string.Empty)
            {
                BugRecord record = new BugRecord();
                record.OwnerAccount = trigger.Character.Account.Id;
                record.OwnerId = trigger.Character.Id;
                record.OwnerName = trigger.Character.Name;
                record.Message = saisie;
                record.Time = DateTime.Now;
                World.Instance.Database.Insert(record);
                trigger.Character.SendServerMessage("Sú mensaje ha sido enviado exitosamente, Muchas Gracias!");
            }
        }
    }
}