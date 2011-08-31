using Server;
using Server.Items;

namespace Server.Items
{
    #region Marlando
		
    [Flipable(0x46B4, 0x46B5)]
    public class GargishSash : BaseClothing
    {
        public override Race RequiredRace { get { return Race.Gargoyle; } }
        public override bool CanBeWornByGargoyles { get { return true; } }

        [Constructable]
        public GargishSash()
            : this(0)
        {
        }

        [Constructable]
        public GargishSash(int hue) : base(0x46B4, Layer.MiddleTorso, hue)
        {
            Weight = 1.0;
        }

        public GargishSash(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
    #region To Find lol
    /*
    [FlipableAttribute(0x153b, 0x153c)]
    public class GargishApron : BaseWaist
    {
        public override Race RequiredRace { get { return Race.Gargoyle; } }
        public override bool CanBeWornByGargoyles { get { return true; } }

        [Constructable]
        public GargishApron()
            : this(0)
        {
        }

        [Constructable]
        public GargishApron(int hue)
            : base(0x153b, hue)
        {
            Weight = 2.0;
        }

        public GargishApron(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }*/

    #endregion
	#endregion

}

