using System;
using Server.Items;

namespace Server.Items
{
	public class BlankScribingRune : Item
	{
		[Constructable]
		public BlankScribingRune() : base( 0x1F17 )
		{
			Name = "a blank scribing rune";
			Hue = 743;
			Weight = 1.0;
			ItemValue = ItemValue.Resource;
		}

		public BlankScribingRune( Serial serial ) : base( serial )
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