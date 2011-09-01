using System;
using Server;

namespace Server.Items
{
	public class PlayerDeathShroud : HoodedShroudOfShadows
	{
		public override int BasePhysicalResistance{ get{ return 10; } }

		[Constructable]
		public PlayerDeathShroud()
		{
			Name = "a death shroud";
			Hue = 0;

			ItemValue = ItemValue.Rare;
		}

		public PlayerDeathShroud( Serial serial ) : base( serial )
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