using System;
using Server;

namespace Server.Items
{
	public class ReagentRing : GoldRing
	{
		[Constructable]
		public ReagentRing()
		{
			Name = "a reagent ring";
			Hue = 1161;
			Attributes.LowerRegCost = 100;
		}

		public ReagentRing( Serial serial ) : base( serial )
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