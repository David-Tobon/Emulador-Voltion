/*

using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Interactives;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Exchanges.Paddock;
using Stump.Server.WorldServer.Game.Maps.Paddocks;

namespace Stump.Server.WorldServer.Game.Interactives.Skills
{
    [Discriminator("Paddock", typeof(Skill), typeof(int), typeof(InteractiveCustomSkillRecord), typeof(InteractiveObject))]
    public class SkillPaddock : CustomSkill
    {
        public SkillPaddock(int id, InteractiveCustomSkillRecord record, InteractiveObject interactiveObject)
            : base (id, record, interactiveObject)
        {
        }

        public override int StartExecute(Character character)
        {
            if (character.IsBusy())
                return -1;

            if (!Record.AreConditionsFilled(character))
            {
                character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 1);
                return -1;
            }

            var paddock = PaddockManager.Instance.GetPaddockByMap(InteractiveObject.Map.Id);
            if (paddock == null)
                return -1;

            var exchange = new PaddockExchange(character, paddock);
            exchange.Open();

            return base.StartExecute(character);
        }
    }
}
*/
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Interactives;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Exchanges.Paddock;
using Stump.Server.WorldServer.Game.Maps.Paddocks;

namespace Stump.Server.WorldServer.Game.Interactives.Skills
{
    [Discriminator("Paddock", typeof(Skill), typeof(int), typeof(InteractiveCustomSkillRecord), typeof(InteractiveObject))]
    public class SkillPaddock : CustomSkill
    {
        public SkillPaddock(int id, InteractiveCustomSkillRecord record, InteractiveObject interactiveObject)
            : base(id, record, interactiveObject)
        {
        }
        public override bool CanUse(Character character)
        {
            var paddock = PaddockManager.Instance.GetPaddockByMap(InteractiveObject.Map.Id);
            if (paddock == null)
                return false;
            if (!paddock.IsPaddockOwner(character))
                return false;
            if (paddock.Abandonned == true)
                return false;

            return base.AreConditionsFilled(character);
        }
        public override int StartExecute(Character character)
        {
            if (character.IsBusy())
                return -1;

            if (!Record.AreConditionsFilled(character))
            {
                character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 1);
                return -1;
            }

            var paddock = PaddockManager.Instance.GetPaddockByMap(InteractiveObject.Map.Id);
            if (paddock == null)
                return -1;
            //if(paddock.Guild!=null)
            //    if(character.Guild != paddock.Guild)
            //        {
            //        character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 1);
            //        return -1;
            //    }



            var exchange = new PaddockExchange(character, paddock);
            exchange.Open();

            return base.StartExecute(character);
        }
    }
}
