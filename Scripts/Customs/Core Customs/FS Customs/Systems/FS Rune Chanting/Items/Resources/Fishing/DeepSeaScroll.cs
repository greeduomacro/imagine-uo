using System;
using Server.Items;

namespace Server.Items
{
	public class DeepSeaScroll : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		[Constructable]
		public DeepSeaScroll() : this( 1 )
		{
		}

		[Constructable]
		public DeepSeaScroll( int amount ) : base( 0x2DAF )
		{
			Name = "deep sea scroll";
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
			ItemValue = ItemValue.Resource;
		}

		public DeepSeaScroll( Serial serial ) : base( serial )
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