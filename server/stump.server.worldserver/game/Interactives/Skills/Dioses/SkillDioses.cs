using Stump.DofusProtocol.Enums;
using Stump.ORM.SubSonic.Extensions;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Interactives;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Handlers.Visual;
using System;

namespace Stump.Server.WorldServer.Game.Interactives.Skills
{
    [Discriminator("Dioses", typeof(Skill), typeof(int), typeof(InteractiveCustomSkillRecord), typeof(InteractiveObject))]
    public class SkillDioses : CustomSkill
    {
        public SkillDioses(int id, InteractiveCustomSkillRecord skillTemplate, InteractiveObject interactiveObject)
            : base(id, skillTemplate, interactiveObject)
        {
        }

        public override int StartExecute(Character character)
        {
            var miniGame = MiniGamesManager.Instance.GetCharacterMiniGames(character);

            if(miniGame.Dioses != null)
            {
                if (miniGame.Dioses != null && miniGame.Dioses.Date == DateTime.Now.Date)
                {
                    character.OpenPopup("Solo puedes dar Oraciones a los Dioses una Vez al dia.");
                    return -1;
                }
            } 
            


            var lvl = character.Level;

            if (lvl <= 19)
            {
                character.OpenPopup("Debes ser almenos nivel 20 para dar Oraciones a los Dioses");
                return -1;
            }



            Random rnd = new Random();
          

            var bendiciones = rnd.Next(1,5);
            character.Inventory.AddItem(ItemManager.Instance.TryGetTemplate(1749), bendiciones);

            character.OpenPopup("Tu Dios agradece tus oraciones y te ha dado:   " + bendiciones + "  Bendiciones...");
            character.AddExperience(10000);

            
            


            return base.StartExecute(character);
        }
    }
}
