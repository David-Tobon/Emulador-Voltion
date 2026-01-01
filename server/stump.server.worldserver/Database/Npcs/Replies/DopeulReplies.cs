/*
using System;
using System.Drawing;
using System.Linq;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Dopple;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Dopple;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Handlers.Context;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("Dopeul", typeof(NpcReply), typeof(NpcReplyRecord))]
    internal class DopeulReplies : NpcReply
    {
        public DopeulReplies(NpcReplyRecord record) : base(record)
        {
        }

        public int MonsterId
        {
            get { return Record.GetParameter<int>(0u); }
            set { Record.SetParameter(0u, value); }
        }

        public override bool Execute(Npc npc, Character character)
        {
            var DeleteDopeul = new DoppleRecord();
            var compareTime = DateTime.Now;
            foreach (var dopeul in character.DoppleCollection.Dopeul.Where(dopeul => dopeul.DopeulId == MonsterId))
            {
                DeleteDopeul = dopeul;
                compareTime = dopeul.Time;
                break;
            }
            if (!(compareTime <= DateTime.Now))
            {
                switch (character.Account.Lang)
                {
                    default:
                        character.SendServerMessage(
                    $"No puedes lanzar un combate con este Dopeul ahora mismo, debes esperar  <b>{compareTime.Subtract(DateTime.Now).Hours} Horas, {compareTime.Subtract(DateTime.Now).Minutes} Minutos</b>",
                    Color.White);
                        break;
                }
                character.LeaveDialog();
                return false;
            }
            else if (compareTime <= DateTime.Now)
            {
                if (DeleteDopeul != null)
                {
                    character.DoppleCollection.DeleteDopeul.Add(DeleteDopeul);
                }


                var monsterGradeId = 1;

                if (character.Level <=20)
                {
                    monsterGradeId = 1;
                }
                if (character.Level <= 40)
                {
                    monsterGradeId = 2;
                }
                if (character.Level <= 60)
                {
                    monsterGradeId = 3;
                }
                if (character.Level <= 80)
                {
                    monsterGradeId = 4;
                }
                if (character.Level <= 100)
                {
                    monsterGradeId = 5;
                }
                if (character.Level <= 120)
                {
                    monsterGradeId = 6;
                }
                if (character.Level <= 140)
                {
                    monsterGradeId = 7;
                }
                if (character.Level <= 160)
                {
                    monsterGradeId = 8;
                }
                if (character.Level <= 180)
                {
                    monsterGradeId = 9;
                }

                if (character.Level >= 181)
                {
                    monsterGradeId = 10;
                }


                
                var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(MonsterId, monsterGradeId);
                var position = new ObjectPosition(character.Map, character.Cell, (DirectionsEnum)5);
                var monster = new Monster(grade, new MonsterGroup(0, position));

                var fight = Singleton<FightManager>.Instance.CreatePvDFight(character.Map);
                fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));
                fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
                fight.StartPlacement();

                ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
                    new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
                character.SaveLater();
                return true;
            }
            switch (character.Account.Lang)
            {
                default:
                    character.SendServerMessage(
                $" No puedes lanzar un combate con este Dopeul ahora mismo, debes esperar <b>{compareTime.Subtract(DateTime.Now).Hours} Horas, {compareTime.Subtract(DateTime.Now).Minutes} Minutos</b>",
                Color.White);
                    break; 
            }
            character.LeaveDialog();
            return false;
        }
    }
}
*/


using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Dopple;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Database.Npcs.Replies;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Dopple;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Handlers.Context;
using System;
using System.Drawing;
using System.Linq;

namespace Database.Npcs.Replies
{
    [Discriminator("Dopeul", typeof(NpcReply), typeof(NpcReplyRecord))]
    internal class DopeulReplies : NpcReply
    {
        public DopeulReplies(NpcReplyRecord record) : base(record)
        {
        }

        public int MonsterId
        {
            get
            {
                var result = 0;
                try
                {
                    result = Record.GetParameter<int>(0u);
                }
                catch { }
                return result;
            }
            set { Record.SetParameter(0u, value); }
        }

        public override bool Execute(Npc npc, Character character)
        {
            DoppleRecord EditDopple = null;
            var compareTime = DateTime.Now;
            character.DoppleCollection.Load(character.Id);
            foreach (var dopeul in character.DoppleCollection.Dopeul.Where(dopeul => dopeul.DopeulId == MonsterId))
            {
                EditDopple = dopeul;
                compareTime = dopeul.Time;
                break;
            }
            if (compareTime <= DateTime.Now)
            {
                var dopplesIp = new DoppleCollection();
                dopplesIp.Load(character.Client.IP);
                foreach (var dopple in dopplesIp.Dopeul.Where(dopeul => dopeul.DopeulId == MonsterId))
                {
                    EditDopple = dopple;
                    compareTime = dopple.Time;
                    break;
                }
                if (!(compareTime <= DateTime.Now))
                {
                    switch (character.Account.Lang)
                    {
                        case "es":
                            character.SendServerMessage(
                        $"No puedes lanzar un combate con este Dopeul ahora mismo, Tienes que esperar <b>{compareTime.Subtract(DateTime.Now).Hours} horas, {compareTime.Subtract(DateTime.Now).Minutes} minutos</b>",
                        Color.White);
                            break;
                        case "fr":
                            character.SendServerMessage(
                        $"No puedes lanzar un combate con este Dopeul ahora mismo, Tienes que esperar <b>{compareTime.Subtract(DateTime.Now).Hours} horas, {compareTime.Subtract(DateTime.Now).Minutes} minutos</b>",
                        Color.White);
                            break;
                        default:
                            character.SendServerMessage(
                        $"No puedes lanzar un combate con este Dopeul ahora mismo, Tienes que esperar <b>{compareTime.Subtract(DateTime.Now).Hours} horas, {compareTime.Subtract(DateTime.Now).Minutes} minutos</b>",
                        Color.White);
                            break;
                    }
                    character.LeaveDialog();
                    return false;
                }
                var monsterGradeId = 1;
                while (monsterGradeId < 12)
                {
                    if (character.Level > monsterGradeId * 20 + 10)
                        monsterGradeId++;
                    else
                        break;
                }

                if (character.Level > 221)
                {
                    monsterGradeId = 11;
                }
                var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(MonsterId, monsterGradeId);
                var position = new ObjectPosition(character.Map, character.Cell, (DirectionsEnum)5);
                var monster = new Monster(grade, new MonsterGroup(0, position));

                var fight = Singleton<FightManager>.Instance.CreatePvDFight(character.Map);
                fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));
                fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
                fight.StartPlacement();

                ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
                    new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
                if (EditDopple != null)
                {
                    EditDopple.Ip = character.Client.IP;
                    EditDopple.IsUpdated = true;
                    EditDopple.Time = DateTime.Now.AddHours(6);
                }
                else
                {
                    Singleton<ItemManager>.Instance.CreateDopeul(character, MonsterId);
                }
                character.SaveLater();
                return true;
            }
            switch (character.Account.Lang)
            {
                case "es":
                    character.SendServerMessage(
                $"Ya peleaste con un dopeul en alguna de tus cuentas!, Tienes que esperar <b>{compareTime.Subtract(DateTime.Now).Hours} horas, {compareTime.Subtract(DateTime.Now).Minutes} minutos</b>",
                Color.White);
                    break;
                case "fr":
                    character.SendServerMessage(
                $"Ya peleaste con un dopeul en alguna de tus cuentas!, Tienes que esperar <b>{compareTime.Subtract(DateTime.Now).Hours} horas, {compareTime.Subtract(DateTime.Now).Minutes} minutos</b>",
                Color.White);
                    break;
                default:
                    character.SendServerMessage(
                $"Ya peleaste con un dopeul en alguna de tus cuentas!, Tienes que esperar <b>{compareTime.Subtract(DateTime.Now).Hours} horas, {compareTime.Subtract(DateTime.Now).Minutes} minutos</b>",
                Color.White);
                    break;
            }
            character.LeaveDialog();
            return false;
        }
    }
}
