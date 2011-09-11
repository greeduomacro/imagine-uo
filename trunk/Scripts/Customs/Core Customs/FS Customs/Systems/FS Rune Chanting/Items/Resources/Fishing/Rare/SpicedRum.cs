using System;
using Server.Items;

namespace Server.Items
{
	public class SpicedRum : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		[Constructable]
		public SpicedRum() : this( 1 )
		{
		}

		[Constructable]
		public SpicedRum( int amount ) : base( 0x099B )
		{
			Name = "spiced rum";
			Hue = 1150;
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
			ItemValue = ItemValue.Resource;
		}

		public SpicedRum( Serial serial ) : base( serial )
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