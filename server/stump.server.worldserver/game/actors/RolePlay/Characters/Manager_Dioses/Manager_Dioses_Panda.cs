using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database;
using Stump.Server.WorldServer.Database.Accounts;

namespace Stump.Server.WorldServer.Game.Actors.RolePlay.Characters
{
    public class Manager_Dioses_Panda : DataManager<Manager_Dioses_Panda>, ISaveable
    {
        readonly Dictionary<int, DB_Dioses_Panda> m_records = new Dictionary<int, DB_Dioses_Panda>();

        [Initialization(InitializationPass.Ninth)]



        public override void Initialize()
        {
            foreach (
                var record in Database.Query<DB_Dioses_Panda>(DB_Dioses_Panda_Relator.FetchQuery))
            {

                m_records.Add((ushort)record.CharacterId, record);
            }
            World.Instance.RegisterSaveableInstance(this);
        }

        public DB_Dioses_Panda GetCharacterDioses(Character character)
        {
            return m_records.FirstOrDefault(x => x.Key == character.Id).Value;
        }

        public void RecordDiosesPanda(Character character)
        {
            var record = GetCharacterDioses(character);

            if (record != null)
            {
                record.Tiempo_Dios = DateTime.Now;
            }
            else
            {
                var dioses = new DB_Dioses_Panda()
                {
                    CharacterId = character.Id,
                    Tiempo_Dios = DateTime.Now,
                };
                m_records.Add(character.Id, dioses);
            }
        }

      

        public DB_Dioses_Panda GetMiniGamesRecordByCharacterId(int id)
        {
            WorldServer.Instance.IOTaskPool.EnsureContext();
            return Database.Query<DB_Dioses_Panda>(string.Format(DB_Dioses_Panda_Relator.FetchById, id)).FirstOrDefault();
        }

        public void Save()
        {
            Database.BeginTransaction();
            var dbIds = m_records.Values;

            foreach (var id in dbIds.Distinct())
            {
                DB_Dioses_Panda record = GetMiniGamesRecordByCharacterId(id.CharacterId);
                if (record != null)
                {
                    Database.Update(id);
                }
                else
                {
                    Database.Insert(id);
                }
            }

            Database.CompleteTransaction();
        }
    }
}