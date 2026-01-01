using NLog;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;


namespace Stump.Server.WorldServer.Commands.Commands.adicionais
{
    class On : InGameCommand
    {
        public readonly Logger logger = LogManager.GetCurrentClassLogger();
        public On()
        {
            Aliases = new string[]
            {
                "on"
            };
            RequiredRole = RoleEnum.Player;
            Description = "Muestra información acerca del servidor!";
          

        }
        public override void Execute(GameTrigger trigger)
        {
            switch (trigger.Character.Account.Lang)
            {
                case "fr":
                    trigger.Reply("Tiempo en Linea : " + trigger.Bold("{0}") + " Jugadores : " + trigger.Bold("{1}"), WorldServer.Instance.UpTime.ToString(@"dd\.hh\:mm\:ss"), WorldServer.Instance.ClientManager.Count);
                    break;
                case "es":
                    trigger.Reply("Tiempo en Linea : " + trigger.Bold("{0}") + " Jugadores : " + trigger.Bold("{1}"), WorldServer.Instance.UpTime.ToString(@"dd\.hh\:mm\:ss"), WorldServer.Instance.ClientManager.Count);
                    break;
                case "en":
                    trigger.Reply("Tiempo en Linea : " + trigger.Bold("{0}") + " Jugadores : " + trigger.Bold("{1}"), WorldServer.Instance.UpTime.ToString(@"dd\.hh\:mm\:ss"), WorldServer.Instance.ClientManager.Count);
                    break;
                default:
                    trigger.Reply("Tiempo en Linea : " + trigger.Bold("{0}") + " Jugadores : " + trigger.Bold("{1}"), WorldServer.Instance.UpTime.ToString(@"dd\.hh\:mm\:ss"), WorldServer.Instance.ClientManager.Count);
                    break;
            }


        }
    }
}
