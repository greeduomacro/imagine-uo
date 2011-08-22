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
				Item item = targ as Item;

				if ( item is IWaterSource )
				{
					m_Link = item;
					base.Fill_OnTarget( from, targ );
				}
				else
				{
					from.SendMessage( "You may only fill this with water." );
				}
			}
			else
			{
				from.SendMessage( "You may only fill this with water." );
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
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}