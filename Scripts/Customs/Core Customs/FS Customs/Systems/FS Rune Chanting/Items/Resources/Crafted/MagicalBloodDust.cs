using System;
using Server.Items;

namespace Server.Items
{
	public class MagicalBloodDust : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		[Constructable]
		public MagicalBloodDust() : this( 1 )
		{
		}

		[Constructable]
		public MagicalBloodDust( int amount ) : base( 0x2DB1 )
		{
			Name = "magical blood dust";
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
		}

		public MagicalBloodDust( Serial serial ) : base( serial )
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