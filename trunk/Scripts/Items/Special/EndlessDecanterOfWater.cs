using System;
using Server;

namespace Server.Items
{
	public class EndlessDecanterOfWater : Pitcher
	{
		private Item m_Link;

		[CommandProperty( AccessLevel.GameMaster )]
		public Item Link
		{
			get{ return m_Link; }
			set{ m_Link = value; }
		}

		public override int MaxQuantity{ get{ return 40; } }

		[Constructable]
		public EndlessDecanterOfWater()
		{
			Name = "an endless decanter of water";
		}

		public EndlessDecanterOfWater( Serial serial ) : base( serial )
		{
		}

		public override void Fill_OnTarget( Mobile from, object targ )
		{
			if ( targ is Item )
			{
				Item item = (Item)targ;
				IWaterSource src;

				src = ( item as IWaterSource );

				if ( src == null && item is AddonComponent )
					src = ( ((AddonComponent)item).Addon as IWaterSource );

				if ( src != null )
				{
					m_Link = item;
					base.Fill_OnTarget( from, targ );
				}
				else
				{
					from.SendMessage( "You may only fill this with water." );
				}
			}
		}

		public override void Pour_OnTarget( Mobile from, object targ )
		{
			if ( this.Quantity == 1 )
			{
				if ( m_Link != null )
				{
					if ( from.InRange( m_Link.GetWorldLocation(), 20 ) )
					{
						this.Quantity = 40;
						from.SendMessage( "The decanter has refilled its self." );
						base.Pour_OnTarget( from, targ );
					}
					else
					{
						from.SendMessage( "You are to far away from the linked water source for the decanter to refill its self." );
						base.Pour_OnTarget( from, targ );
					}
				}
				else
				{
					base.Pour_OnTarget( from, targ );
				}
			}
			else
			{
				base.Pour_OnTarget( from, targ );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );

			// Version 1
			writer.Write( (Item) m_Link );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch( version )
			{
				case 0:
				{
					m_Link = reader.ReadItem();
					break;
				}
			}
		}
	}
}