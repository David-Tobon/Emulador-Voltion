using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Maps;

namespace Stump.Server.WorldServer.Commands.Commands.adicionais
{
    public class Anunciomensagem : CommandBase

    {
        private static Regex m_numberRegex = new Regex("^[0-9]+$", RegexOptions.Compiled);
        private static Regex m_numberRangeRegex = new Regex("^([0-9]+)-([0-9]+)$", RegexOptions.Compiled);

        public Anunciomensagem()
        {
            base.Aliases = new string[] { "anunciar" };
            base.RequiredRole = RoleEnum.Administrator;
            base.Description = "anuncia algo";
            base.AddParameter<string>("Texto", "txt", "Texto que irá passar para os players!", null, true, null);
            base.AddParameter<string>("parameters", "params", "Level (exact or range x-y), Breed, Area or Name (partial)", null, true, null);
        }
        public override void Execute(TriggerBase trigger)
        {
            PlayableBreedEnum playableBreedEnum;
            Predicate<Character> breedId = (Character x) => true;
            if (trigger.IsArgumentDefined("params"))
            {
                string str = trigger.Get<string>("params");
                if (!Anunciomensagem.m_numberRegex.IsMatch(str))
                {
                    Match match = Anunciomensagem.m_numberRangeRegex.Match(str);
                    if (match.Success)
                    {
                        int num = int.Parse(match.Groups[1].Value);
                        int num1 = int.Parse(match.Groups[2].Value);
                        breedId = (Character x) => (x.Level < num ? false : x.Level <= num1);
                    }
                    else if (!Enum.TryParse<PlayableBreedEnum>(str, true, out playableBreedEnum))
                    {
                        Area area = Singleton<World>.Instance.GetArea(str);
                        breedId = (area == null ? new Predicate<Character>((Character x) => x.Name.IndexOf(str, StringComparison.InvariantCultureIgnoreCase) != -1) : new Predicate<Character>((Character x) => x.Area == area));
                    }
                    else
                    {
                        breedId = (Character x) => x.BreedId == playableBreedEnum;
                    }
                }
                else
                {
                    int num2 = int.Parse(str);
                    breedId = (Character x) => x.Level == num2;
                }
            }
            IEnumerable<Character> characters = Singleton<World>.Instance.GetCharacters(breedId);
            int num3 = 0;
            foreach (Character character in characters)
            {
                num3++;
                if (trigger.IsArgumentDefined("txt"))
                {
                    character.OpenPopup(trigger.Get<string>("txt"));
                    character.SendServerMessage(trigger.Get<string>("txt"));
                }
            }
            trigger.Reply("Enviado para " + num3 + " personajes!");

        }
    }
    public class Anuncio2mensagem : CommandBase

    {
        private static Regex m_numberRegex = new Regex("^[0-9]+$", RegexOptions.Compiled);
        private static Regex m_numberRangeRegex = new Regex("^([0-9]+)-([0-9]+)$", RegexOptions.Compiled);

        public Anuncio2mensagem()
        {
            base.Aliases = new string[] { "anunciar2" };
            base.RequiredRole = RoleEnum.Administrator;
            base.Description = "anuncia algo";
            base.AddParameter<string>("Texto", "txt", "Texto que irá passar para os players!", null, true, null);
            base.AddParameter<string>("parameters", "params", "Level (exact or range x-y), Breed, Area or Name (partial)", null, true, null);
        }
        public override void Execute(TriggerBase trigger)
        {
            PlayableBreedEnum playableBreedEnum;
            Predicate<Character> breedId = (Character x) => true;
            if (trigger.IsArgumentDefined("params"))
            {
                string str = trigger.Get<string>("params");
                if (!Anuncio2mensagem.m_numberRegex.IsMatch(str))
                {
                    Match match = Anuncio2mensagem.m_numberRangeRegex.Match(str);
                    if (match.Success)
                    {
                        int num = int.Parse(match.Groups[1].Value);
                        int num1 = int.Parse(match.Groups[2].Value);
                        breedId = (Character x) => (x.Level < num ? false : x.Level <= num1);
                    }
                    else if (!Enum.TryParse<PlayableBreedEnum>(str, true, out playableBreedEnum))
                    {
                        Area area = Singleton<World>.Instance.GetArea(str);
                        breedId = (area == null ? new Predicate<Character>((Character x) => x.Name.IndexOf(str, StringComparison.InvariantCultureIgnoreCase) != -1) : new Predicate<Character>((Character x) => x.Area == area));
                    }
                    else
                    {
                        breedId = (Character x) => x.BreedId == playableBreedEnum;
                    }
                }
                else
                {
                    int num2 = int.Parse(str);
                    breedId = (Character x) => x.Level == num2;
                }
            }
            IEnumerable<Character> characters = Singleton<World>.Instance.GetCharacters(breedId);
            int num3 = 0;
            foreach (Character character in characters)
            {
                num3++;
                if (trigger.IsArgumentDefined("txt"))
                {

                    character.DisplayNotification(trigger.Get<string>("txt"));
                }
            }
            trigger.Reply("Enviado para " + num3 + " Personajes!");

        }
    }
}