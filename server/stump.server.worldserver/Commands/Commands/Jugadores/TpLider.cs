using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using System.Drawing;
using Stump.Core.Attributes;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using System.Drawing;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.BaseServer.IPC.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Core.IPC;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;


namespace Stump.Server.WorldServer.Commands.Commands
{
    public class TpLiderTeamCommand : CommandBase
    {
        public TpLiderTeamCommand()
        {
            Aliases = new[] { "tplider", "tptolider","golider" };
            RequiredRole = RoleEnum.Player;
            Description = "Teletransporta al lider del grupo";
          
            AddParameter("from", "from", "The character that teleport", isOptional: true, converter: ParametersConverter.CharacterConverter);
        }

        public override void Execute(TriggerBase trigger)
        {
            Character from = null;

            if (!(trigger as GameTrigger).Character.IsInParty())
            {
                (trigger as GameTrigger).Character.DisplayNotification("No puedes usar este comando porque no estás en un grupo.");
                return;
            }

            if ((trigger as GameTrigger).Character.IsPartyLeader())
            {
                (trigger as GameTrigger).Character.DisplayNotification("No puedes usar este comando porque eres el lider del grupo.");
                return;
            }
            if ((trigger as GameTrigger).Character.Party.Leader.Map.IsDungeon())
            {
                (trigger as GameTrigger).Character.DisplayNotification("No puedes usar este comando porque tu lider se encuentra en una mazmorra.");
                return;

            }

            var to = (trigger as GameTrigger).Character.Party.Leader;

            if (to.Map.Id == 196215814/* && (trigger as GameTrigger).Character.Map.Id != 196215814*/)
            {
                /*(trigger as GameTrigger).Character.DisplayNotification("Así que intentando hacer trampa... no se permiten tramposos aquí.");
                World.Instance.SendAnnounce("<b>[ANTI-CHEAT] :</b> El jugador " + (trigger as GameTrigger).Character.Name + " fué encontrado haciendo trampa y ha sido baneado.", Color.Crimson);
                var banIPMessage = new BanIPMessage {IPRange = (trigger as GameTrigger).Character.Client.IP, BanReason = "inestablebug", BanEndDate = null, BannerAccountId = 1};
                IPCAccessor.Instance.SendRequest(banIPMessage, ok => trigger.Reply("IP {0} banned", (trigger as GameTrigger).Character.Client.IP), error => trigger.ReplyError("IP {0} not banned : {1}", (trigger as GameTrigger).Character.Client.IP, error.Message));
                (trigger as GameTrigger).Character.Client.Disconnect();
                return;*/
                (trigger as GameTrigger).Character.DisplayNotification("El lider de tu grupo está en un mapa innacesible para ti.");
                return;
            }

            if (trigger is GameTrigger)
                from = (trigger as GameTrigger).Character;

            from.Teleport(to.Position);

            switch ((trigger as GameTrigger).Character.Account.Lang)
            {
                case "es":
                    (trigger as GameTrigger).Character.DisplayNotification(
                        "Has sido teletransportado con tu lider del grupo, para mayor facilidad de juego. Disfrutalo!");
                    break;

                case "fr":
                    (trigger as GameTrigger).Character.DisplayNotification(
                        "Has sido teletransportado con tu lider del grupo, para mayor facilidad de juego. Disfrutalo!");
                    break;

                case "pt":
                    (trigger as GameTrigger).Character.DisplayNotification(
                        "Has sido teletransportado con tu lider del grupo, para mayor facilidad de juego. Disfrutalo!");
                    break;
                default:
                    (trigger as GameTrigger).Character.DisplayNotification(
                        "Has sido teletransportado con tu lider del grupo, para mayor facilidad de juego. Disfrutalo!");
                    break;
            }
        }
    }
}