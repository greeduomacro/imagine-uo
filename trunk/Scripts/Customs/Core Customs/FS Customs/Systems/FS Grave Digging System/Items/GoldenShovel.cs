using System;
using Server;
using Server.Engines.Harvest;

namespace Server.Items
{
	public class GoldenShovel : BaseHarvestTool
	{
		public override HarvestSystem HarvestSystem{ get{ return Mining.System; } }

		[Constructable]
		public GoldenShovel() : this( Utility.RandomList( 5, 10, 15 ) )
		{
		}

		[Constructable]
		public GoldenShovel( int uses ) : base( uses, 0xF39 )
		{
			Weight = 5.0;
			Hue = 2213;
			Name = "a golden shovel";

			ItemValue = ItemValue.Rare;
		}

		public GoldenShovel( Serial serial ) : base( serial )
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