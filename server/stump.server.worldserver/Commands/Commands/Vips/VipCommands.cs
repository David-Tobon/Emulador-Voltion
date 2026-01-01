using System;
using System.Drawing;
using Stump.Core.Attributes;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Maps.Cells;

namespace Stump.Server.WorldServer.Commands.Commands
{

    class Vip : SubCommandContainer
    {
        public Vip()
        {
            Aliases = new[] { "VIP" };
            Description = "Lista de comandos para usuarios VIP";
            RequiredRole = RoleEnum.Player;
        }

        public static void Teleport(Character player, int mapId, short cellId, DirectionsEnum playerDirection)
        {
            player.Teleport(new ObjectPosition(Singleton<World>.Instance.GetMap(mapId), cellId, playerDirection));
        }

        public class vipinfo : TargetSubCommand
        {
            public vipinfo()
            {
                Aliases = new[] { "info" };
                RequiredRole = RoleEnum.Player;
                Description = "Información sobre los beneficios de Ser Usuario VIP";
                ParentCommandType = typeof(Vip);
            }
            public override void Execute(TriggerBase trigger)
            {
                var gameTrigger = trigger as GameTrigger;
                if (gameTrigger != null)
                {

                    gameTrigger.Character.SendServerMessage("<b>★ La Lista de algunos beneficios como usuario VIP: ★</b><br><br>" +
                        "<b>-</b> Chat Exclusivo de Usuarios VIP<br>" +
                        "<b>-</b> Ornamentos exclusivos<br>" +
                        "<b>-</b> Titulos exclusivos<br>" +
                        "<b>-</b> Rango Exclusivo en Discord<br>" +
                        "<b>-</b> Comandos Exclusivos desbloqueados<br>" +
                        "<b>-</b> Apariencias Exclusivas<br>" +
                        "<b>-</b> ¡Y Mucho más! Consulta nuestra página web o Discord para saber más y adquirir el Servicio VIP<br>", Color.Aqua);

                }
            }

            public class viptp : TargetSubCommand
            {
                public viptp()
                {
                    Aliases = new[] { "mapa" };
                    RequiredRole = RoleEnum.Administrator;
                    Description = "Teletransporta a un mapa exclusivo para usuarios VIP";
                    ParentCommandType = typeof(Vip);
                }
                public override void Execute(TriggerBase trigger)

                {
                    var gameTrigger = trigger as GameTrigger;
                    if (gameTrigger != null && gameTrigger.UserRole >= RoleEnum.Moderator)
                    {
                        var player = gameTrigger.Character;
                        Teleport(player, 196086786, 272, DirectionsEnum.DIRECTION_WEST);
                    }
                    else
                    {
                        gameTrigger.Character.OpenPopup("Actualmente no eres un usuario VIP");
                    }
                }
            }
            public static string AnnounceColor = ColorTranslator.ToHtml(Color.Gold);
            public class vipchat : TargetSubCommand

            {
                public vipchat()
                {
                    Aliases = new[] { "chat" };
                    RequiredRole = RoleEnum.Moderator;
                    Description = "Te permite hablar de forma prioritaria en el chat. EJ: .vip chat ¡Hola!";
                    ParentCommandType = typeof(Vip);
                    AddParameter<string>("message", "msg", "Anuncio");
                }
                public override void Execute(TriggerBase trigger)
                {
                    var gameTrigger = trigger as GameTrigger;
                    if (gameTrigger != null && gameTrigger.UserRole >= RoleEnum.Moderator)
                    {
                        var color = ColorTranslator.FromHtml(AnnounceColor);

                        var msg = trigger.Get<string>("msg");
                        var formatMsg = trigger is GameTrigger ?
                            string.Format("<b>★{0}</b>: {1}", ((GameTrigger)trigger).Character.Name, msg) :
                            string.Format("{0}", msg);

                        World.Instance.SendAnnounce(formatMsg, color);
                    }
                    else
                    {
                        gameTrigger.Character.OpenPopup("Actualmente no eres un usuario VIP");
                    }
                }
            }

        }
    }
}
