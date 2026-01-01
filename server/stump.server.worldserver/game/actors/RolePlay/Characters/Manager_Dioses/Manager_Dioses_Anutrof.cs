using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database;
using Stump.Server.WorldServer.Database.Accounts;

namespace Stump.Server.WorldServer.Game.Actors.RolePlay.Characters
{
    public class Manager_Dioses_anutrof : DataManager<Manager_Dioses_anutrof>, ISaveable
    {
        readonly Dictionary<int, DB_Dioses_anutrof> m_records = new Dictionary<int, DB_Dioses_anutrof>();

        [Initialization(InitializationPass.Ninth)]



        public override void Initialize()
        {
            foreach (
                var record in Database.Query<DB_Dioses_anutrof>(DB_Dioses_anutrof_Relator.FetchQuery))
            {

                m_records.Add((ushort)record.CharacterId, record);
            }
            World.Instance.RegisterSaveableInstance(this);
        }

        public DB_Dioses_anutrof GetCharacterDioses(Character character)
        {
            return m_records.FirstOrDefault(x => x.Key == character.Id).Value;
        }

        public void RecordDiosesanutrof(Character character)
        {
            var record = GetCharacterDioses(character);

            if (record != null)
            {
                record.Tiempo_Dios = DateTime.Now;
            }
            else
            {
                var dioses = new DB_Dioses_anutrof()
                {
                    CharacterId = character.Id,
                    Tiempo_Dios = DateTime.Now,
                };
                m_records.Add(character.Id, dioses);
            }
        }

      

        public DB_Dioses_anutrof GetMiniGamesRecordByCharacterId(int id)
        {
            WorldServer.Instance.IOTaskPool.EnsureContext();
            return Database.Query<DB_Dioses_anutrof>(string.Format(DB_Dioses_anutrof_Relator.FetchById, id)).FirstOrDefault();
        }

        public void Save()
        {
            Database.BeginTransaction();
            var dbIds = m_records.Values;

            foreach (var id in dbIds.Distinct())
            {
                DB_Dioses_anutrof record = GetMiniGamesRecordByCharacterId(id.CharacterId);
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