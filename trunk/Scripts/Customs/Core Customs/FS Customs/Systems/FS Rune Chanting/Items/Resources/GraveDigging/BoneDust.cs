using System;
using Server.Items;

namespace Server.Items
{
	public class BoneDust : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		[Constructable]
		public BoneDust() : this( 1 )
		{
		}

		[Constructable]
		public BoneDust( int amount ) : base( 0x2DB5 )
		{
			Name = "bone dust";
			Hue = 1150;
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
			ItemValue = ItemValue.Uncommon;
		}

		public BoneDust( Serial serial ) : base( serial )
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