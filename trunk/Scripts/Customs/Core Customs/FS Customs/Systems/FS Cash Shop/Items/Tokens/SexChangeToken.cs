using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class SexChangeToken : Item
	{
		[Constructable]
		public SexChangeToken() : base( 0x2AAA )
		{
			Name = "a reward token";
			LootType = LootType.Blessed;
			Light = LightType.Circle300;
			Weight = 5.0;
			ItemValue = ItemValue.Legendary;
		}

		public SexChangeToken( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1070998, "sex change" ); // Use this to redeem<br>your ~1_PROMO~
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1062334 ); // This item must be in your backpack to be used.
			}
			else
			{
				if ( from.Female )
				{
					from.Female = false;
					from.SendMessage( "You are now a male." );
				}
				else
				{
					from.Female = true;
					from.SendMessage( "You are now a female." );

					if ( from.FacialHairItemID != 0 )
					{
						from.FacialHairItemID = 0;
						from.SendMessage( "You shave off your facial hair." );
					}
				}

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