using System;
using Server.Items;

namespace Server.Items
{
	public class LesserMysticEssence : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		[Constructable]
		public LesserMysticEssence() : this( 1 )
		{
		}

		[Constructable]
		public LesserMysticEssence( int amount ) : base( 0x2DAF )
		{
			Name = "lesser mystic essence";
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
			ItemValue = ItemValue.Uncommon;
		}

		public LesserMysticEssence( Serial serial ) : base( serial )
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