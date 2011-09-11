using System;
using Server.Items;

namespace Server.Items
{
	public class PerfectSkull : Item
	{
		[Constructable]
		public PerfectSkull() : base( 6880 + Utility.RandomMinMax( 0, 4) )
		{
			Name = "a perfect skull";
			Weight = 1.0;
			ItemValue = ItemValue.Resource;
		}

		public PerfectSkull( Serial serial ) : base( serial )
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
}