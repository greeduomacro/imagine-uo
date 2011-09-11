using System;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
	public class SlayerRune : BaseMinorRune
	{
		SlayerName m_Slayer;

		[CommandProperty( AccessLevel.GameMaster )]
		public SlayerName Slayer
		{
			get{ return m_Slayer; }
			set{ m_Slayer = value; InvalidateProperties(); }
		}

		[Constructable]
		public SlayerRune() : base()
		{
			Slayer = (SlayerName) Utility.RandomList( 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27 );
			MaxAmount = 1;
		}

		public SlayerRune( Serial serial ) : base( serial )
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

			SlayerEntry entry = SlayerGroup.GetEntryByName( m_Slayer );

			if ( entry != null )
				list.Add(entry.Title);
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_Slayer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();

			switch( version )
			{
				case 0:
				{
					m_Slayer = (SlayerName)reader.ReadInt();
					break;
				}
			}
		}

		private class RuneTarget : Target
		{
			private SlayerRune m_Rune;

			public RuneTarget( SlayerRune rune ) : base( -1, false, TargetFlags.None )
			{
				m_Rune = rune;
			}

			protected override void OnTarget( Mobile from, object target )
			{
				Item item = target as Item;
				Type type = item.GetType();

				if ( item is BaseWeapon || item is Spellbook )
				{
					if ( Runescribing.GetProps( item ) >= 7 )
					{
						from.SendMessage( "This item cannot be enhanced any further" );
					}
					else if ( item.ChantSlots >= 3 )
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

							if ( i.Slayer == SlayerName.None )
							{
								i.Slayer = m_Rune.Slayer;
								item.ChantSlots += 1;
								m_Rune.Delete();
							}
							else if ( i.Slayer2 == SlayerName.None )
							{
								i.Slayer2 = m_Rune.Slayer;
								item.ChantSlots += 1;
								m_Rune.Delete();
							}
							else
							{
								from.SendMessage( "The rune fails to enhance the item any further." );
							}
						}

						if ( item is Spellbook )
						{
							Spellbook i = item as Spellbook;

							if ( i.Slayer == SlayerName.None )
							{
								i.Slayer = m_Rune.Slayer;
								item.ChantSlots += 1;
								m_Rune.Delete();
							}
							else if ( i.Slayer2 == SlayerName.None )
							{
								i.Slayer2 = m_Rune.Slayer;
								item.ChantSlots += 1;
								m_Rune.Delete();
							}
							else
							{
								from.SendMessage( "The rune fails to enhance the item any further." );
							}
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

