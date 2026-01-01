using System;
using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.BaseServer.Commands.Patterns;
using Stump.Server.WorldServer.AI.Fights.Spells;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Database.I18n;
using Stump.Server.WorldServer.Database.Monsters;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Maps.Spawns;
using Monster = Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters.Monster;
using System.Drawing;
using Stump.Core.Attributes;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Commands.Commands.Patterns;

namespace Stump.Server.WorldServer.Commands.Commands.Vips
{


    public class MonsterStarsCommand : InGameCommand
    {
        [Variable(true)]
        public static string AnnounceColor = ColorTranslator.ToHtml(Color.White);

        public MonsterStarsCommand()
        {
            Aliases = new[] { "Estrellas" };
            RequiredRole = RoleEnum.Moderator;
            Description = "Otorga Estrellas a los Monstruos";
            
        }

        public override void Execute(GameTrigger trigger)
        {
            
                foreach (var monster in World.Instance.GetMaps().SelectMany(x => x.Actors.OfType<MonsterGroup>()))
                {
                    monster.AgeBonus = 200;

                }


                // Poner Mensaje en el chat
            var color = ColorTranslator.FromHtml(AnnounceColor);
            var msg = " : Ha Subido las extrellas de los Monstruos al Máximo.";
            var formatMsg = trigger is GameTrigger
                                ? string.Format("El jugador <b>★{0}</b>: {1}", ((GameTrigger)trigger).Character.Name, msg)
                                : string.Format("{0}", msg);

            World.Instance.SendAnnounce(formatMsg, color);

        }

        /*
        public override void Execute(TriggerBase trigger)
        {
            if (!(trigger is GameTrigger))
                return;

            GameTrigger gameTrigger = trigger as GameTrigger;
            if (MonsterManager.Instance.GetMonsterDungeonsSpawns().Where(x => x.MapId == gameTrigger.Character.Map.Id).Count() == 0)
            {
                foreach (var map in World.Instance.GetMaps())
                {
                    foreach (MonsterGroup monster in map.Actors.Where(x => x is MonsterGroup))
                    {
                        monster.CreationDate = new DateTime(monster.CreationDate.Year,
                            monster.CreationDate.Month, monster.CreationDate.Day - 1,
                            monster.CreationDate.Hour, monster.CreationDate.Minute, monster.CreationDate.Second);
                        map.Refresh(monster);
                    }
                }

                var color = ColorTranslator.FromHtml(AnnounceColor);

                var msg = " : Ha Subido las extrellas de los Monstruos al Máximo.";
                var formatMsg = trigger is GameTrigger
                                    ? string.Format("El jugador <b>★{0}</b>: {1}", ((GameTrigger)trigger).Character.Name, msg)
                                    : string.Format("{0}", msg);

                World.Instance.SendAnnounce(formatMsg, color);



            }


            else
            {
                gameTrigger.Character.SendServerMessage("No puedes usar este comando en este lugar");
            }
        }

        */


        /*
        public override void Execute1(GameTrigger trigger)
        {
            if (!(trigger is GameTrigger))
                return;

            GameTrigger gameTrigger = trigger as GameTrigger;
            if (MonsterManager.Instance.GetMonsterDungeonsSpawns().Where(x => x.MapId == gameTrigger.Character.Map.Id).Count() == 0)
            {
                foreach (var map in World.Instance.GetMaps())
                {
                    foreach (MonsterGroup monster in map.Actors.Where(x => x is MonsterGroup))
                    {
                        monster.CreationDate = new DateTime(monster.CreationDate.Year,
                            monster.CreationDate.Month, monster.CreationDate.Day - 1,
                            monster.CreationDate.Hour, monster.CreationDate.Minute, monster.CreationDate.Second);
                        map.Refresh(monster);
                    }
                }

                var color = ColorTranslator.FromHtml(AnnounceColor);

                var msg = " : Ha Subido las extrellas de los Monstruos al Máximo.";
                var formatMsg = trigger is GameTrigger
                                    ? string.Format("El jugador <b>★{0}</b>: {1}", ((GameTrigger)trigger).Character.Name, msg)
                                    : string.Format("{0}", msg);

                World.Instance.SendAnnounce(formatMsg, color);



            }


            else
            {
                gameTrigger.Character.SendServerMessage("No puedes usar este comando en este lugar");
            }
        }


        */
    }
    }

