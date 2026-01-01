using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Commands.Commands
{
    class VieCommand : InGameCommand
    {
        public VieCommand()
        {
            base.Aliases = new[] { "hp","vida","curar","curarse" };
            base.Description = "Devuelve los puntos de salud completos.";
            base.RequiredRole = RoleEnum.Moderator; 
        }
        public override void Execute(GameTrigger trigger)
        {

            var character = trigger.Character;
            Character player = trigger.Character;
            if (!player.IsFighting())
            {


                foreach (var perso in WorldServer.Instance.FindClients(x => x.IP == character.Client.IP && x.Character != character))
                {
                
                    if (perso.Character.Map.Id == character.Map.Id)
                    {
                        perso.Character.Record.DamageTaken = 0;
                        perso.Character.Stats.Health.DamageTaken = 0;
                        perso.Character.RefreshStats();
                        perso.Character.SaveLater();
                    }
                }




                
                player.Record.DamageTaken = 0;
                player.Stats.Health.DamageTaken = 0;
                player.RefreshStats();
                player.SaveLater(); 
                

            }
            else
            {
                if (player.Fight.State == Game.Fights.FightState.Placement)
                {
                    player.Record.DamageTaken = 0;
                    player.Stats.Health.DamageTaken = 0;
                    player.RefreshStats();
                    player.SaveLater();


                    foreach (var perso in WorldServer.Instance.FindClients(x => x.IP == character.Client.IP && x.Character != character))
                    {

                        if (perso.Character.Map.Id == character.Map.Id)
                        {
                            perso.Character.Record.DamageTaken = 0;
                            perso.Character.Stats.Health.DamageTaken = 0;
                            perso.Character.RefreshStats();
                            perso.Character.SaveLater();
                        }
                    }



                }
                player.SendServerMessage("Acción imposible en Combate.");
            }

        }
    }
}
