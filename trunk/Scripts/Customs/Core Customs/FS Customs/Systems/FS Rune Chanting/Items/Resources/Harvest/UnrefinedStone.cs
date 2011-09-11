using System;
using Server.Items;

namespace Server.Items
{
	public class UnrefinedStone : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		[Constructable]
		public UnrefinedStone() : base( Utility.RandomList( 4964, 4965, 4966, 4968, 4969, 4971, 4972 )  )
		{
			Name = "unrefined stone";

			Weight = 1.0;
			ItemValue = ItemValue.Resource;
		}

		public UnrefinedStone( Serial serial ) : base( serial )
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