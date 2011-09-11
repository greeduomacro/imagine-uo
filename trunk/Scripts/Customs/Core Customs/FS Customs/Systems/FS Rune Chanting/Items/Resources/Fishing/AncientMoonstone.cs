using System;
using Server.Items;

namespace Server.Items
{
	public class AncientMoonstone : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		[Constructable]
		public AncientMoonstone() : this( 1 )
		{
		}

		[Constructable]
		public AncientMoonstone( int amount ) : base( 0x0F8B )
		{
			Name = "ancient moonstone";
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
			ItemValue = ItemValue.Resource;
		}

		public AncientMoonstone( Serial serial ) : base( serial )
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