/*
namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightSpectatorJoinMessage : GameFightJoinMessage
    {
        public new const uint Id = 6504;
        public override uint MessageId
        {
            get { return Id; }
        }
        public NamedPartyTeam[] NamedPartyTeams { get; set; }

        public GameFightSpectatorJoinMessage(bool isTeamPhase, bool canBeCancelled, bool canSayReady, bool isFightStarted, short timeMaxBeforeFightStart, sbyte fightType, NamedPartyTeam[] namedPartyTeams)
        {
            this.IsTeamPhase = isTeamPhase;
            this.CanBeCancelled = canBeCancelled;
            this.CanSayReady = canSayReady;
            this.IsFightStarted = isFightStarted;
            this.TimeMaxBeforeFightStart = timeMaxBeforeFightStart;
            this.FightType = fightType;
            this.NamedPartyTeams = namedPartyTeams;
        }

        public GameFightSpectatorJoinMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)NamedPartyTeams.Count());
            for (var namedPartyTeamsIndex = 0; namedPartyTeamsIndex < NamedPartyTeams.Count(); namedPartyTeamsIndex++)
            {
                var objectToSend = NamedPartyTeams[namedPartyTeamsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var namedPartyTeamsCount = reader.ReadUShort();
            NamedPartyTeams = new NamedPartyTeam[namedPartyTeamsCount];
            for (var namedPartyTeamsIndex = 0; namedPartyTeamsIndex < namedPartyTeamsCount; namedPartyTeamsIndex++)
            {
                var objectToAdd = new NamedPartyTeam();
                objectToAdd.Deserialize(reader);
                NamedPartyTeams[namedPartyTeamsIndex] = objectToAdd;
            }
        }

    }
}
*/

namespace Stump.DofusProtocol.Messages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class GameFightSpectatorJoinMessage : GameFightJoinMessage
    {
        public new const uint Id = 6504;
        public override uint MessageId
        {
            get { return Id; }
        }
        public IEnumerable<NamedPartyTeam> namedPartyTeams;

        public GameFightSpectatorJoinMessage(bool isTeamPhase, bool canBeCancelled, bool canSayReady, bool isFightStarted, short timeMaxBeforeFightStart, sbyte fightType, IEnumerable<NamedPartyTeam> namedPartyTeams)
        {
            this.IsTeamPhase = isTeamPhase;
            this.CanBeCancelled = canBeCancelled;
            this.CanSayReady = canSayReady;
            this.IsFightStarted = isFightStarted;
            this.TimeMaxBeforeFightStart = timeMaxBeforeFightStart;
            this.FightType = fightType;
            this.namedPartyTeams = namedPartyTeams;

        }

        public GameFightSpectatorJoinMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)namedPartyTeams.Count());
            foreach (var objectToSend in namedPartyTeams)
            {
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var namedPartyTeamsCount = reader.ReadUShort();
            var namedPartyTeams_ = new NamedPartyTeam[namedPartyTeamsCount];
            for (var namedPartyTeamsIndex = 0; namedPartyTeamsIndex < namedPartyTeamsCount; namedPartyTeamsIndex++)
            {
                var objectToAdd = new NamedPartyTeam();
                objectToAdd.Deserialize(reader);
                namedPartyTeams_[namedPartyTeamsIndex] = objectToAdd;
            }
            namedPartyTeams = namedPartyTeams_;
        }

    }
}