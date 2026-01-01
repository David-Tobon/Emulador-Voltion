using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database;
using Stump.Server.WorldServer.Database.Accounts;

namespace Stump.Server.WorldServer.Game.Actors.RolePlay.Characters
{
    public class Manager_Dioses_aniripsa : DataManager<Manager_Dioses_aniripsa>, ISaveable
    {
        readonly Dictionary<int, DB_Dioses_aniripsa> m_records = new Dictionary<int, DB_Dioses_aniripsa>();

        [Initialization(InitializationPass.Ninth)]



        public override void Initialize()
        {
            foreach (
                var record in Database.Query<DB_Dioses_aniripsa>(DB_Dioses_aniripsa_Relator.FetchQuery))
            {

                m_records.Add((ushort)record.CharacterId, record);
            }
            World.Instance.RegisterSaveableInstance(this);
        }

        public DB_Dioses_aniripsa GetCharacterDioses(Character character)
        {
            return m_records.FirstOrDefault(x => x.Key == character.Id).Value;
        }

        public void RecordDiosesaniripsa(Character character)
        {
            var record = GetCharacterDioses(character);

            if (record != null)
            {
                record.Tiempo_Dios = DateTime.Now;
            }
            else
            {
                var dioses = new DB_Dioses_aniripsa()
                {
                    CharacterId = character.Id,
                    Tiempo_Dios = DateTime.Now,
                };
                m_records.Add(character.Id, dioses);
            }
        }

      

        public DB_Dioses_aniripsa GetMiniGamesRecordByCharacterId(int id)
        {
            WorldServer.Instance.IOTaskPool.EnsureContext();
            return Database.Query<DB_Dioses_aniripsa>(string.Format(DB_Dioses_aniripsa_Relator.FetchById, id)).FirstOrDefault();
        }

        public void Save()
        {
            Database.BeginTransaction();
            var dbIds = m_records.Values;

            foreach (var id in dbIds.Distinct())
            {
                DB_Dioses_aniripsa record = GetMiniGamesRecordByCharacterId(id.CharacterId);
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