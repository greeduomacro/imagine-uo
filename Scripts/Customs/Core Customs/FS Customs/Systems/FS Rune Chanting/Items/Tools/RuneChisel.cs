using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class RuneChisel : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefRunescribing.CraftSystem; } }

		[Constructable]
		public RuneChisel() : base( 0x1026 )
		{
			Weight = 1.0;
			Name = "a rune chisel";
		}

		[Constructable]
		public RuneChisel( int uses ) : base( uses, 0x1026 )
		{
			Weight = 1.0;
			Name = "a rune chisel";
		}

		public RuneChisel( Serial serial ) : base( serial )
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