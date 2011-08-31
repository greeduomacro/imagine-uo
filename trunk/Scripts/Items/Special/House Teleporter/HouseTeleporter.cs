using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Multis;
using Server.ContextMenus;
using Server.Targeting;
using Server.Engines.VeteranRewards;

namespace Server.Items
{
	public class PlayerHouseTeleporter2 : Item, IRewardItem
	{
		private SecureLevel m_Level;
		private PlayerHouseTeleporter2 m_Link;
		private int m_Charges;
		private bool m_IsRewardItem;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public PlayerHouseTeleporter2 Link
		{
			get{ return m_Link; }
			set{ m_Link = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsRewardItem
		{
			get{ return m_IsRewardItem; }
			set{ m_IsRewardItem = value; InvalidateProperties(); }
		}

		[Constructable]
		public PlayerHouseTeleporter2() : base( 0x40AC )
		{
			Name = "a house teleporter";
			Weight = 1.0;
			LootType = LootType.Blessed;
			m_Charges = 5;
		}

		public override bool HandlesOnMovement { get { return true; } }

		public override bool OnMoveOver( Mobile m )
		{
			Timer.DelayCall( TimeSpan.FromSeconds( 2.0 ), new TimerStateCallback( DoTeleport_Callback ), m );

			return true;
		}

		private void DoTeleport_Callback( object state )
		{
			DoTeleport( (Mobile) state );
		}

		public virtual void DoTeleport( Mobile m )
		{
			if ( this.Charges == 0 && !IsRewardItem )
			{
				m.SendLocalizedMessage( 1115120 ); //There are no charges left in this teleporter.
			}
			else if ( this.Link.Charges == 0 && !IsRewardItem )
			{
				m.SendLocalizedMessage( 1115121 ); //There are no more charges left in the remote teleporter.
			}
			else if ( this.Link.Movable == false )
			{
				m.SendLocalizedMessage( 1113858 ); //This teleporter does not have a valid destination.
			}
			if ( m.Map != Map.Felucca && this.Link.Map == Map.Felucca )
			{
				m.CloseGump( typeof( PlayerHouseTeleporter2ConfirmGump ) );
				m.SendGump( new PlayerHouseTeleporter2ConfirmGump( m, this ) );
			}
			else
			{
				m.Location = Link.Location;

				if ( !IsRewardItem )
				{
					Link.Charges -= 1;
					this.Charges -= 1;
				}
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1114918 ); //Select a House Teleporter to link to.
				from.Target = new LinkTarget( this );
			}
			else
			{
				from.SendMessage( "This must be in your backpack to use it" );
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			
			if ( !m_IsRewardItem )
				list.Add( 1075217, this.Charges.ToString() ); // ~1_val~ charges remaining

			if ( m_IsRewardItem )
				list.Add( 1113802 ); // 12th Year Veteran Reward
		}

		public class LinkTarget : Target
		{
			private PlayerHouseTeleporter2 m_Pad;

			public LinkTarget( PlayerHouseTeleporter2 pad ) : base( 1, false, TargetFlags.None )
			{
				m_Pad = pad;
			}

			protected override void OnTarget( Mobile from, object target )
			{
				if ( target is PlayerHouseTeleporter2 )
				{
					PlayerHouseTeleporter2 mt = (PlayerHouseTeleporter2)target;
					
					m_Pad.Link = mt;
					mt.Link = m_Pad;

					if ( m_Pad.Link.Charges > mt.Charges || mt.Charges > m_Pad.Link.Charges )
					{
						int c1 = mt.Charges;
						int c2 = mt.Link.Charges;
						int total = c1 + c2 / 2;
					
						mt.Link.Charges = total;
						mt.Charges = total;

						from.SendLocalizedMessage( 1115119 ); //The two House Teleporters are now linked and the charges remaining have been rebalanced.
					}
					else
					{
						from.SendLocalizedMessage( 1114919 ); //The two House Teleporters are now linked.
					}
						
				}
				else
				{
					from.SendMessage( "That is not a valid target." );
				}
			}
		}

		public PlayerHouseTeleporter2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class PlayerHouseTeleporter2ConfirmGump : Gump
	{
		private Mobile m_From;
		private PlayerHouseTeleporter2 m_Pad;

		public PlayerHouseTeleporter2ConfirmGump( Mobile from, PlayerHouseTeleporter2 pad ) : base( Core.AOS ? 110 : 20, Core.AOS ? 100 : 30 )
		{
			m_From = from;
			m_Pad = pad;

			Closable = false;

			AddPage( 0 );

			AddBackground( 0, 0, 420, 280, 5054 );

			AddImageTiled( 10, 10, 400, 20, 2624 );
			AddAlphaRegion( 10, 10, 400, 20 );

			AddHtmlLocalized( 10, 10, 400, 20, 1019005, 30720, false, false ); // WARNING

			AddImageTiled( 10, 40, 400, 200, 2624 );
			AddAlphaRegion( 10, 40, 400, 200 );

			AddHtmlLocalized( 10, 40, 400, 200, 1115595, 32512, false, true ); // You are about to teleport to a non-consensual PvP area. Are you certain you wish to do this?

			AddImageTiled( 10, 250, 400, 20, 2624 );
			AddAlphaRegion( 10, 250, 400, 20 );

			AddButton( 10, 250, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 40, 250, 170, 20, 1011036, 32767, false, false ); // OKAY

			AddButton( 210, 250, 4005, 4007, 0, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 240, 250, 170, 20, 1011012, 32767, false, false ); // CANCEL
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( info.ButtonID == 1 )
			{
				m_From.Location = m_Pad.Location;
		
				if ( !m_Pad.IsRewardItem )
				{
					m_Pad.Charges -= 1;
					m_Pad.Link.Charges -= 1;
				}
			}
		}
	}
}