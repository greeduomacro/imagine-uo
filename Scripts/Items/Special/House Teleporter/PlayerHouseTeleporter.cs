using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Multis;
using Server.Targeting;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Engines.VeteranRewards;

namespace Server.Items
{
	public class PlayerHouseTeleporter : Item, ISecurable
	{
		private Item m_Target;
		private SecureLevel m_Level;
		private int m_Charges;
		private bool m_IsRewardItem;

		[CommandProperty( AccessLevel.GameMaster )]
		public Item Target
		{
			get{ return m_Target; }
			set{ m_Target = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
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
		public PlayerHouseTeleporter( int itemID ) : this( itemID, null )
		{
		}

		public PlayerHouseTeleporter( int itemID, Item target ) : base( itemID )
		{
			m_Level = SecureLevel.Anyone;

			m_Target = target;
		}

		public bool CheckAccess( Mobile m )
		{
			BaseHouse here = BaseHouse.FindHouseAt( this );
			BaseHouse there = BaseHouse.FindHouseAt( m_Target );

			if ( here != null && ( here.Public ? here.IsBanned( m ) : !here.HasAccess( m )) )
				return false;

			if ( there != null && ( there.Public ? there.IsBanned( m ) : !there.HasAccess( m )) )
				return false;

			return ( here != null && here.HasSecureAccess( m, m_Level ) );
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( m_Target != null && !m_Target.Deleted )
			{
				if ( CheckAccess( m ) )
				{
					if ( !m.Hidden || m.AccessLevel == AccessLevel.Player )
						new EffectTimer( Location, Map, 2023, 0x1F0, TimeSpan.FromSeconds( 0.4 ) ).Start();

					PlayerHouseTeleporter pht = m_Target as PlayerHouseTeleporter;

					if ( this.Charges == 0 && !IsRewardItem )
					{
						m.SendLocalizedMessage( 1115120 ); //There are no charges left in this teleporter.
					}
					else if ( pht.Charges == 0 && !IsRewardItem )
					{
						m.SendLocalizedMessage( 1115121 ); //There are no more charges left in the remote teleporter.
					}
					else if ( m_Target.Movable == false )
					{
						m.SendLocalizedMessage( 1113858 ); //This teleporter does not have a valid destination.
					}
					else if ( this.Map != Map.Felucca && pht.Map == Map.Felucca )
					{
						m.CloseGump( typeof( PlayerHouseTeleporterConfirmGump ) );
						m.SendGump( new PlayerHouseTeleporterConfirmGump( m, this ) );
					}
					else
					{
						this.ConsumeCharges( this );
						new DelayTimer( this, m ).Start();
					}
				}
				else
				{
					m.SendLocalizedMessage( 1061637 ); // You are not allowed to access this.
				}
			}

			return true;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public PlayerHouseTeleporter( Serial serial ) : base( serial )
		{
		}

		public void ConsumeCharges( PlayerHouseTeleporter pad )
		{
			PlayerHouseTeleporter target = pad.Target as PlayerHouseTeleporter;

			pad.Charges -= 1;
		
			if ( pad.Target != null )
				target.Charges -= 1;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (Item) m_Target );
			writer.Write( (int) m_Level );
			writer.Write( (int) m_Charges );
			writer.Write( (bool) m_IsRewardItem );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Target = reader.ReadItem();
					m_Level = (SecureLevel)reader.ReadInt();
					m_Charges = reader.ReadInt();
					m_IsRewardItem = reader.ReadBool();

					if ( version < 0 )
						m_Level = SecureLevel.Anyone;

					break;
				}
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1114918 ); //Select a House Teleporter to Target to.
				from.Target = new TargetTarget( this );
			}
			else
			{
				from.SendMessage( "This must be in your backpack to use it" );
			}
		}

		public class TargetTarget : Target
		{
			private PlayerHouseTeleporter m_Pad;

			public TargetTarget( PlayerHouseTeleporter pad ) : base( 1, false, TargetFlags.None )
			{
				m_Pad = pad;
			}

			protected override void OnTarget( Mobile from, object target )
			{
				if ( target is PlayerHouseTeleporter )
				{
					PlayerHouseTeleporter mt = target as PlayerHouseTeleporter;

					m_Pad.Target = mt;
					mt.Target = m_Pad;

					if ( m_Pad.Charges > mt.Charges || mt.Charges > m_Pad.Charges )
					{
						int c1 = mt.Charges;
						int c2 = m_Pad.Charges;
						int total = c1 + c2 / 2;
					
						m_Pad.Charges = total;
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

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			
			if ( !m_IsRewardItem )
				list.Add( 1075217, this.Charges.ToString() ); // ~1_val~ charges remaining

			if ( m_IsRewardItem )
				list.Add( 1113802 ); // 12th Year Veteran Reward
		}

		private class EffectTimer : Timer
		{
			private Point3D m_Location;
			private Map m_Map;
			private int m_EffectID;
			private int m_SoundID;

			public EffectTimer( Point3D p, Map map, int effectID, int soundID, TimeSpan delay ) : base( delay )
			{
				m_Location = p;
				m_Map = map;
				m_EffectID = effectID;
				m_SoundID = soundID;
			}

			protected override void OnTick()
			{
				Effects.SendLocationParticles( EffectItem.Create( m_Location, m_Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, m_EffectID, 0 );

				if ( m_SoundID != -1 )
					Effects.PlaySound( m_Location, m_Map, m_SoundID );
			}
		}

		private class DelayTimer : Timer
		{
			private PlayerHouseTeleporter m_Teleporter;
			private Mobile m_Mobile;

			public DelayTimer( PlayerHouseTeleporter tp, Mobile m ) : base( TimeSpan.FromSeconds( 1.0 ) )
			{
				m_Teleporter = tp;
				m_Mobile = m;
			}

			protected override void OnTick()
			{
				Item target = m_Teleporter.m_Target;

				if ( target != null && !target.Deleted )
				{
					Mobile m = m_Mobile;

					if ( m.Location == m_Teleporter.Location && m.Map == m_Teleporter.Map )
					{
						Point3D p = target.GetWorldTop();
						Map map = target.Map;

						Server.Mobiles.BaseCreature.TeleportPets( m, p, map );

						m.MoveToWorld( p, map );

						if ( !m.Hidden || m.AccessLevel == AccessLevel.Player )
						{
							Effects.PlaySound( target.Location, target.Map, 0x1FE );

							Effects.SendLocationParticles( EffectItem.Create( m_Teleporter.Location, m_Teleporter.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023, 0 );
							Effects.SendLocationParticles( EffectItem.Create( target.Location, target.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 5023, 0 );

							new EffectTimer( target.Location, target.Map, 2023, -1, TimeSpan.FromSeconds( 0.4 ) ).Start();
						}
					}
				}
			}
		}
	}

	public class PlayerHouseTeleporterConfirmGump : Gump
	{
		private Mobile m_From;
		private Item m_Pad;

		public PlayerHouseTeleporterConfirmGump( Mobile from, Item pad ) : base( Core.AOS ? 110 : 20, Core.AOS ? 100 : 30 )
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
				PlayerHouseTeleporter pht = m_Pad as PlayerHouseTeleporter;
				PlayerHouseTeleporter target = pht.Target as PlayerHouseTeleporter;

				Point3D p = target.GetWorldTop();
				Map map = target.Map;

				Server.Mobiles.BaseCreature.TeleportPets( m_From, p, map );
				m_From.MoveToWorld( p, map );

				if ( !pht.IsRewardItem )
				{
					pht.ConsumeCharges( pht );
				}
			}
		}
	}
}