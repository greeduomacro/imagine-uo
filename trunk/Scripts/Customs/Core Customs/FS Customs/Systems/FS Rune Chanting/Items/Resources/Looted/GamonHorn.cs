using System;
using Server.Items;

namespace Server.Items
{
	public class GamonHorn : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		[Constructable]
		public GamonHorn() : this( 1 )
		{
		}

		[Constructable]
		public GamonHorn( int amount ) : base( 0x2DB7 )
		{
			Name = "a gaman's horn";
			Hue = 1453;
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
			ItemValue = ItemValue.Resource;
		}

		public GamonHorn( Serial serial ) : base( serial )
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