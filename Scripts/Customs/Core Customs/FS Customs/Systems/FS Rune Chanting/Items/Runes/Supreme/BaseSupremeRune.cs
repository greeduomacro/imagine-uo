using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Engines.Craft;

namespace Server.Items
{
	public class BaseSupremeRune : Item, ICraftable
	{
		private Mobile m_Crafter;
		private RuneQuality m_Quality;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Crafter
		{
			get{ return m_Crafter; }
			set{ m_Crafter = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public RuneQuality Quality
		{
			get{ return m_Quality; }
			set{ m_Quality = value; InvalidateProperties(); }
		}

		public BaseSupremeRune() : base( 0x1F17 )
		{
			Name = "a supreme enchanted rune";
			Hue = 1161;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Quality == RuneQuality.Exceptional )
				list.Add( 1063341 ); // exceptional

			if ( m_Crafter != null )
				list.Add( 1050043, m_Crafter.Name ); // crafted by ~1_NAME~
		}

		public BaseSupremeRune( Serial serial ) : base( serial )
		{
		}

		public virtual int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			if ( makersMark )
				Crafter = from;

			m_Quality = (RuneQuality)quality;

			return quality;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			// Release
			writer.Write( (Mobile) m_Crafter );
			writer.WriteEncodedInt( (int) m_Quality );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch( version )
			{
				case 0:
				{
					m_Crafter = reader.ReadMobile();
					m_Quality = (RuneQuality)reader.ReadEncodedInt();
					break;
				}
			}
		}
	}
}