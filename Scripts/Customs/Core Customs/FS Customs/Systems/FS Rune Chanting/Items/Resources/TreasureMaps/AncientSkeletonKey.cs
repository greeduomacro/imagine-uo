using System;
using Server.Items;

namespace Server.Items
{
	public class AncientSkeletonKey : Item
	{
		[Constructable]
		public AncientSkeletonKey() : base( 0x1010 )
		{
			Name = "ancient skeleton key";
			Weight = 1.0;
			ItemValue = ItemValue.Resource;
		}

		public AncientSkeletonKey( Serial serial ) : base( serial )
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