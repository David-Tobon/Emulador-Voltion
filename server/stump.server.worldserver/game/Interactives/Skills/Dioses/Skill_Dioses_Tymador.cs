using Stump.DofusProtocol.Enums;
using Stump.ORM.SubSonic.Extensions;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Interactives;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Handlers.Visual;
using System;
using Stump.Server.WorldServer.Game.Interactives.Skills;
using Stump.Server.WorldServer.Game.Interactives.Skills.Dioses;

namespace Stump.Server.WorldServer.Game.Interactives.Skills
{
    [Discriminator("Dioses_tymador", typeof(Skill), typeof(int), typeof(InteractiveCustomSkillRecord), typeof(InteractiveObject))]
    public class Skill_Dioses_tymador : CustomSkill
    {
        public Skill_Dioses_tymador(int id, InteractiveCustomSkillRecord skillTemplate, InteractiveObject interactiveObject)
            : base(id, skillTemplate, interactiveObject)
        {
        }






        public override int StartExecute(Character character)
        {
            Variables_Dioses variables = new Variables_Dioses();

            int sacramentos = variables.sacramentos;
            int xp_50 = variables.xp_50;
            int xp_100= variables.xp_100;
            int xp_150= variables.xp_150;
            int xp_190 = variables.xp_190;


            var miniGame = Manager_Dioses_tymador.Instance.GetCharacterDioses(character);

            if (miniGame != null)
            {
                if (miniGame.Tiempo_Dios != null && miniGame.Tiempo_Dios.Date == DateTime.Now.Date)
                {
                    character.OpenPopup("Solo puedes dirigir tus palabras a este Dios Una Vez al Dia. <br> ¡Vuelve Mañana!");
                    return -1;
                }
            }

            

            Manager_Dioses_tymador.Instance.RecordDiosestymador(character);


            if (character.Level <=49)
            {
                character.OpenPopup("Solo aquellos aventureros valerosos que han alcanzado el nivel 50 podrán hacerle una ofrenda a los Dioses. Vuelve Mañana con el nivel Necesario.");
                return -1;
            }
            if (character.Level >= 50)
            {

                Random rnd = new Random();


                var bendiciones = rnd.Next(1, 6);
                character.Inventory.AddItem(ItemManager.Instance.TryGetTemplate(sacramentos), bendiciones);

                character.OpenPopup("Tu Dios agradece tus oraciones y te ha dado:   " + bendiciones + "  Sacramentos...");


                VisualHandler.SendGameRolePlaySpellAnimMessage(character.Client, character, InteractiveObject.Cell.Id, (int)SpellIdEnum.LARGE_MULTICOLOURED_FAIRYWORK);
                VisualHandler.SendGameRolePlaySpellAnimMessage(character.Client, character, InteractiveObject.Cell.Id, (int)SpellIdEnum.LARGE_CRACKLING_MULTICOLOURED_FAIRYWORK);
                VisualHandler.SendGameRolePlaySpellAnimMessage(character.Client, character, InteractiveObject.Cell.Id, (int)SpellIdEnum.LARGE_SPRINKLING_MULTICOLOURED_FAIRYWORK);


                if (character.Level >= 50 )
                {
                    character.AddExperience(xp_50);
                }
                if (character.Level >= 100)
                {
                    character.AddExperience(xp_100);
                }
                if (character.Level >= 150)
                {
                    character.AddExperience(xp_150);
                }
                if (character.Level >= 190)
                {
                    character.AddExperience(xp_190);
                }


                return base.StartExecute(character);

            } 
            return -1;
            




        }
    }
}
