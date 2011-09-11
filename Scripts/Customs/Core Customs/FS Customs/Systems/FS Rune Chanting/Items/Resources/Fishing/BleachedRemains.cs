using System;
using Server.Items;

namespace Server.Items
{
	public class BleachedRemains : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		[Constructable]
		public BleachedRemains() : this( 1 )
		{
		}

		[Constructable]
		public BleachedRemains( int amount ) : base( 0x318C )
		{
			Name = "bleached remains";
			Hue = 1150;
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
			ItemValue = ItemValue.Resource;
		}

		public BleachedRemains( Serial serial ) : base( serial )
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