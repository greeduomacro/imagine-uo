using System;
using Server.Items;

namespace Server.Items
{
	public class MysticEssence : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		[Constructable]
		public MysticEssence() : this( 1 )
		{
		}

		[Constructable]
		public MysticEssence( int amount ) : base( 0x2DB2 )
		{
			Name = "mystic essence";
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
			ItemValue = ItemValue.Rare;
		}

		public MysticEssence( Serial serial ) : base( serial )
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