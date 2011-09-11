using System;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
	public class SlayerOfOneRune : BaseSupremeRune
	{
		[Constructable]
		public SlayerOfOneRune() : base()
		{
		}

		public SlayerOfOneRune( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.Target = new RuneTarget( this );
				from.SendMessage( "Select the item you wish to enhance." );
			}
			else
			{
				from.SendLocalizedMessage( 1062334 ); // This item must be in your backpack to be used.
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( "Slayer Of One" );
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

		private class RuneTarget : Target
		{
			private SlayerOfOneRune m_Rune;

			public RuneTarget( SlayerOfOneRune rune ) : base( -1, false, TargetFlags.None )
			{
				m_Rune = rune;
			}

			protected override void OnTarget( Mobile from, object target )
			{
				Type type = target as Type;
				Item item = target as Item;

				if ( item is BaseWeapon || item is Spellbook )
				{
					if ( Runescribing.GetProps( item ) >= 7 )
					{
						from.SendMessage( "This item cannot be enhanced any further" );
					}
					else if ( item.ChantSlots > 0 )
					{
						from.SendMessage( "This item cannot handle any more enhancments." );
					}
					else if ( Runescribing.CheckBlacklist( type ) == true )
					{
						from.SendMessage( "This item cannot be enhanced." );
					}
					else
					{
						if ( item is BaseWeapon )
						{
							BaseWeapon i = item as BaseWeapon;

							i.Slayer = SlayerName.Silver;
							i.Slayer2 = SlayerName.None;

							i.SlayerSelect = true;

							item.ChantSlots += 3;

							m_Rune.Delete();
						}

						if ( item is Spellbook )
						{
							Spellbook i = item as Spellbook;

							i.Slayer = SlayerName.Silver;
							i.Slayer2 = SlayerName.None;

							//i.SlayerSelect = true;

							item.ChantSlots += 3;

							m_Rune.Delete();
						}
					}
				}
				else
				{
					from.SendMessage( "You cannot use this enhancement on that." );
				}
			}
		}
	}
}

