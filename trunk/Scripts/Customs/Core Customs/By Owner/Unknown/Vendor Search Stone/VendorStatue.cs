using System;
using Server;
using Server.Gumps;

namespace Server.Items
{
    class VendorStatue : Item
    {
        [Constructable]
        public VendorStatue()
            : base(0xEDC)
        {
            Weight = 1000.0;
            Name = "Vendor Search Stone";
            Movable = false;

        }

        public VendorStatue(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public override void OnDoubleClick(Mobile e)
        {
            Type[] types;
            types = Type.EmptyTypes;
            e.SendGump(new VendorSearchGump(e, "", 0, types, false));
        }
    }
}

