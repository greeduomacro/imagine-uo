using System;
using Server.Items;

namespace Server.Items
{
	public class MagicalDust : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		[Constructable]
		public MagicalDust() : this( 1 )
		{
		}

		[Constructable]
		public MagicalDust( int amount ) : base( 0x2DB5 )
		{
			Name = "magical dust";
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
			ItemValue = ItemValue.Uncommon;
		}

		public MagicalDust( Serial serial ) : base( serial )
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