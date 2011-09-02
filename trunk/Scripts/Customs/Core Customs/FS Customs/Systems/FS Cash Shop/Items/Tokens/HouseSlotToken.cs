using System;
using Server.Items;
using Server.Network;
using Server.Accounting;

namespace Server.Items
{
	public class HouseSlotToken : Item
	{
		public static int MaxHouses = 100;

		[Constructable]
		public HouseSlotToken() : base( 0x2AAA )
		{
			Name = "a reward token";
			LootType = LootType.Blessed;
			Light = LightType.Circle300;
			Weight = 5.0;
			ItemValue = ItemValue.Legendary;
		}

		public HouseSlotToken( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1070998, "extra house slot" ); // Use this to redeem<br>your ~1_PROMO~
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1062334 ); // This item must be in your backpack to be used.
			}
			else
			{
				Account acct = from.Account as Account;
				int houses = Convert.ToInt32( acct.GetTag( "maxHouses" ) );

				if ( houses < 1 )
					houses = 1;

				if ( houses < MaxHouses )
				{
					houses += 1;

					acct.SetTag( "maxHouses", houses.ToString() );
					from.SendMessage( "You now have {0} house slots.", houses.ToString() );

					this.Delete();
				}
				else
				{
					from.SendMessage( "You cannot increase your max houses slot any further." );
				}
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