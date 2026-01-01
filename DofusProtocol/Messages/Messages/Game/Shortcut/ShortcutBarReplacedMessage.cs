/*
namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ShortcutBarReplacedMessage : Message
    {
        public const uint Id = 6706;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte BarType { get; set; }
        public Shortcut Shortcut { get; set; }

        public ShortcutBarReplacedMessage(sbyte barType, Shortcut shortcut)
        {
            this.BarType = barType;
            this.Shortcut = shortcut;
        }

        public ShortcutBarReplacedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(BarType);
            writer.WriteShort(Shortcut.TypeId);
            Shortcut.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            BarType = reader.ReadSByte();
            Shortcut = ProtocolTypeManager.GetInstance<Shortcut>(reader.ReadShort());
            Shortcut.Deserialize(reader);
        }

    }
}
*/


// Generated on 04/19/2020 03:45:08
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ShortcutBarReplacedMessage : Message
    {
        public const uint Id = 6706;
        public override uint MessageId
        {
            get { return Id; }
        }

        public sbyte barType;
        public Types.Shortcut shortcut;

        public ShortcutBarReplacedMessage()
        {
        }

        public ShortcutBarReplacedMessage(sbyte barType, Types.Shortcut shortcut)
        {
            this.barType = barType;
            this.shortcut = shortcut;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(barType);
            writer.WriteShort(shortcut.TypeId);
            shortcut.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            barType = reader.ReadSByte();
            shortcut = Types.ProtocolTypeManager.GetInstance<Types.Shortcut>(reader.ReadShort());
            shortcut.Deserialize(reader);
        }

    }

}