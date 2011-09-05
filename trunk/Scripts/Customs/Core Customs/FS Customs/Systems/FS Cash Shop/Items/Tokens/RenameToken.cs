using System;
using Server.Items;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public class RenameToken : Item
	{
		[Constructable]
		public RenameToken() : base( 0x2AAA )
		{
			Name = "a reward token";
			LootType = LootType.Blessed;
			Light = LightType.Circle300;
			Weight = 5.0;
			ItemValue = ItemValue.Legendary;
		}

		public RenameToken( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1070998, "new name" ); // Use this to redeem<br>your ~1_PROMO~
		}

		public override void OnDoubleClick( Mobile from )
		{
			string msg = "<CENTER><BASEFONT COLOR=#FFFFFF>Type the new name you wish to be known by below<BR>Standard nameing rules apply</CENTER>";

			if( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1062334 ); // This item must be in your backpack to be used.
			}
			else
			{
				from.CloseGump( typeof( RenameGump ) );
				from.SendGump( new RenameGump( from, msg, true ) );
				this.Delete();
			}
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