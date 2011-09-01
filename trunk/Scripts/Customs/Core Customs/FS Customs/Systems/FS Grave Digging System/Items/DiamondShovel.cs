using System;
using Server;
using Server.Engines.Harvest;

namespace Server.Items
{
	public class DiamondShovel : BaseHarvestTool
	{
		public override HarvestSystem HarvestSystem{ get{ return Mining.System; } }

		[Constructable]
		public DiamondShovel() : this( Utility.RandomList( 5, 10 ) )
		{
		}

		[Constructable]
		public DiamondShovel( int uses ) : base( uses, 0xF39 )
		{
			Weight = 5.0;
			Hue = 1154;
			Name = "a diamond shovel";

			ItemValue = ItemValue.Epic;
		}

		public DiamondShovel( Serial serial ) : base( serial )
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