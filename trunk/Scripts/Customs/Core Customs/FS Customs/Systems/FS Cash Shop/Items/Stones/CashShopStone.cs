using System;
using Server.Gumps;
using Server.Items;

namespace Server.Items
{
	public class CashShopStone : Item
	{
		[Constructable]
		public CashShopStone() : base( 0xED4 )
		{
			Name = "Cash Shop Stone";
			Movable = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.CloseGump( typeof( CashShopGump ) );
			from.SendGump( new CashShopGump() );
		}

		public CashShopStone( Serial serial ) : base( serial )
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