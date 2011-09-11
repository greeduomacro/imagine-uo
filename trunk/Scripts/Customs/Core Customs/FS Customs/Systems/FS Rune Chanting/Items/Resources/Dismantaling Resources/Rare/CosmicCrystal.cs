using System;
using Server.Items;

namespace Server.Items
{
	public class CosmicCrystal : Item
	{
		[Constructable]
		public CosmicCrystal() : base( 0x1F19 )
		{
			Name = "a cosmic crystal";
			Hue = 1462;
			Weight = 1.0;
			ItemValue = ItemValue.Legendary;
		}

		public CosmicCrystal( Serial serial ) : base( serial )
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