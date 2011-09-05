using System;
using Server;

namespace Server.Items
{
	public class ManaBracelet : GoldBracelet
	{
		[Constructable]
		public ManaBracelet()
		{
			Name = "a mana bracelet";
			Hue = 1161;
			Attributes.LowerManaCost = 50;
		}

		public ManaBracelet( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}