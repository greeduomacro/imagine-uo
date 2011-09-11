using System;
using Server.Items;

namespace Server.Items
{
	public class WrappedBody : Item
	{
		[Constructable]
		public WrappedBody() : base( 0x1C21 )
		{
			Name = "Wrapped Body";
			Weight = 1.0;
			ItemValue = ItemValue.Resource;
		}

		public WrappedBody( Serial serial ) : base( serial )
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