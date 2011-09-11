using System;
using Server.Items;

namespace Server.Items
{
	public class BrokenHeart : Item
	{
		[Constructable]
		public BrokenHeart() : base( 0x1CED )
		{
			Name = "a broken heart";
			Weight = 1.0;
			ItemValue = ItemValue.Resource;
		}

		public BrokenHeart( Serial serial ) : base( serial )
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