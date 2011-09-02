using System;
using System.Globalization;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Engines.Quests;
using Necro = Server.Engines.Quests.Necro;
using Haven = Server.Engines.Quests.Haven;

namespace Server.Items
{
	public class PlatinumBankCheck : Item
	{
		private int m_Worth;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Worth
		{
			get{ return m_Worth; }
			set{ m_Worth = value; InvalidateProperties(); }
		}

		public PlatinumBankCheck( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_Worth );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			LootType = LootType.Blessed;

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Worth = reader.ReadInt();
					break;
				}
			}
		}

		[Constructable]
		public PlatinumBankCheck( int worth ) : base( 0x14F0 )
		{
			Name = "A Platinum Bank Check";
			Weight = 1.0;
			Hue = 2101;
			LootType = LootType.Blessed;

			m_Worth = worth;

			ItemValue = ItemValue.Common;
		}

		public override bool DisplayLootType{ get{ return Core.AOS; } }

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			string worth;

			if ( Core.ML )
				worth = m_Worth.ToString( "N0", CultureInfo.GetCultureInfo( "en-US" ) );
			else
				worth = m_Worth.ToString();

			list.Add( 1060738, worth ); // value: ~1_val~

			decimal amount = m_Worth * 0.10m;

			string newAmount = amount.ToString( "C", CultureInfo.GetCultureInfo( "en-US" ) );

			list.Add( 1053099, "{0}\t{1}", newAmount, "USD" );
		}

		public override void OnSingleClick( Mobile from )
		{
			from.Send( new MessageLocalizedAffix( Serial, ItemID, MessageType.Label, 0x3B2, 3, 1041361, "", AffixType.Append, String.Concat( " ", m_Worth.ToString() ), "" ) ); // A bank check:
		}

		public override void OnDoubleClick( Mobile from )
		{
			BankBox box = from.FindBankNoCreate();

			if ( box != null && IsChildOf( box ) )
			{
				Delete();

				int deposited = 0;

				int toAdd = m_Worth;

				Platinum Platinum;

				while ( toAdd > 60000 )
				{
					Platinum = new Platinum( 60000 );

					if ( box.TryDropItem( from, Platinum, false ) )
					{
						toAdd -= 60000;
						deposited += 60000;
					}
					else
					{
						Platinum.Delete();

						from.AddToBackpack( new PlatinumBankCheck( toAdd ) );
						toAdd = 0;

						break;
					}
				}

				if ( toAdd > 0 )
				{
					Platinum = new Platinum( toAdd );

					if ( box.TryDropItem( from, Platinum, false ) )
					{
						deposited += toAdd;
					}
					else
					{
						Platinum.Delete();

						from.AddToBackpack( new PlatinumBankCheck( toAdd ) );
					}
				}

				// Platinum was deposited in your account:
				from.SendLocalizedMessage( 1042672, true, " " + deposited.ToString() );
			}
			else
			{
				from.SendLocalizedMessage( 1047026 ); // That must be in your bank box to use it.
			}
		}
	}
}