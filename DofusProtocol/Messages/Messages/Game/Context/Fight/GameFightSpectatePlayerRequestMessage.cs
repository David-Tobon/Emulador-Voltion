/*
namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightSpectatePlayerRequestMessage : Message
    {
        public const uint Id = 6474;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong PlayerId { get; set; }

        public GameFightSpectatePlayerRequestMessage(ulong playerId)
        {
            this.PlayerId = playerId;
        }

        public GameFightSpectatePlayerRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(PlayerId);
        }

        public override void Deserialize(IDataReader reader)
        {
            PlayerId = reader.ReadVarULong();
        }

    }
}
*/
namespace Stump.DofusProtocol.Messages
{
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class GameFightSpectatePlayerRequestMessage : Message
    {
        public const uint Id = 6474;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong playerId;

        public GameFightSpectatePlayerRequestMessage(ulong playerId)
        {
            this.playerId = playerId;
        }

        public GameFightSpectatePlayerRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(playerId);
        }

        public override void Deserialize(IDataReader reader)
        {
            playerId = reader.ReadVarULong();
        }

    }
}