using System;
using Server.Items;

namespace Server.Items
{
	public class BrokenLockpicks : Item
	{
		[Constructable]
		public BrokenLockpicks() : base( 0x14FD )
		{
			Name = "broken lockpicks";
			Weight = 1.0;
			ItemValue = ItemValue.Resource;
		}

		public BrokenLockpicks( Serial serial ) : base( serial )
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