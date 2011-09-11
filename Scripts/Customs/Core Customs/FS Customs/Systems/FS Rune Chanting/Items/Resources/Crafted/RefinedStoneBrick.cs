using System;
using Server.Items;

namespace Server.Items
{
	public class RefinedStoneBrick : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		[Constructable]
		public RefinedStoneBrick() : this( 1 )
		{
		}

		[Constructable]
		public RefinedStoneBrick( int amount ) : base( 7139 )
		{
			Name = "refined stone brick";

			Stackable = true;
			Amount = amount;
			Weight = 1.0;
			ItemValue = ItemValue.Resource;
		}

		public RefinedStoneBrick( Serial serial ) : base( serial )
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