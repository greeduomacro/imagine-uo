using System;
using Server;

namespace Server.Items
{
    public class Spiderweb : Item
    {
        //public override int LabelNumber { get { return 1023814; } } // spiderweb
	
		[Constructable]
		public Spiderweb() : base( 0x10DD )
		{
			Weight = 1.0;	
			ItemValue = ItemValue.Rare;		
		}

        public Spiderweb(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}

    public class EggCaseWeb : Item
    {
        //public override int LabelNumber { get { return 1024312; } } // egg case web

        [Constructable]
        public EggCaseWeb()
            : base(0x10D8)
        {
			ItemValue = ItemValue.Rare;
        }

        public EggCaseWeb(Serial serial)
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
}