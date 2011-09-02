using System;
using Server.Items;
using Server.Network;
using Server.Accounting;

namespace Server.Items
{
	public class StorageToken : Item
	{
		public static int MaxStorage = 300;

		[Constructable]
		public StorageToken() : base( 0x2AAA )
		{
			Name = "a reward token";
			LootType = LootType.Blessed;
			Light = LightType.Circle300;
			Weight = 5.0;
			ItemValue = ItemValue.Legendary;
		}

		public StorageToken( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1070998, "20% more bank & house storage" ); // Use this to redeem<br>your ~1_PROMO~
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
				int bank = Convert.ToInt32( acct.GetTag( "maxStorage" ) );

				if ( bank < 25 )
					bank = 0;

				if ( bank < MaxStorage )
				{
					bank += 25;

					acct.SetTag( "maxStorage", bank.ToString() );

					for ( int i = 0; i < acct.Length; ++i )
					{
						Mobile mob = acct[i];

						if ( mob != null )
							mob.BankBox.MaxItems += 25;
					}

					from.SendMessage( "Your max bank & house storage has been increased by 20%." );

					this.Delete();
				}
				else
				{
					from.SendMessage( "You cannot increase your storage any further." );
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