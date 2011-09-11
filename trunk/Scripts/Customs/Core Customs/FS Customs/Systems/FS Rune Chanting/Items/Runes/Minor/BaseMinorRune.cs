using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Engines.Craft;

namespace Server.Items
{
	public enum RuneQuality
	{
		Low,
		Regular,
		Exceptional
	}

	public class BaseMinorRune : Item, ICraftable
	{
		private Mobile m_Crafter;
		private int m_MaxAmount;
		private int m_BaseAmount;
		private RuneQuality m_Quality;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Crafter
		{
			get{ return m_Crafter; }
			set{ m_Crafter = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxAmount
		{
			get{ return m_MaxAmount; }
			set{ m_MaxAmount = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int BaseAmount
		{
			get{ return m_BaseAmount; }
			set{ m_BaseAmount = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public RuneQuality Quality
		{
			get{ return m_Quality; }
			set{ m_Quality = value; InvalidateProperties(); }
		}

		public BaseMinorRune() : base( 0x1F17 )
		{
			Name = "a minor enchanted rune";
			Hue = 1150;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Quality == RuneQuality.Exceptional )
				list.Add( 1063341 ); // exceptional

			if ( m_Crafter != null )
				list.Add( 1050043, m_Crafter.Name ); // crafted by ~1_NAME~
		}

		public BaseMinorRune( Serial serial ) : base( serial )
		{
		}

		public virtual int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			Double bonus = (Double)quality * 5;
			Double imbuing = from.Skills.Imbuing.Value / 4 + bonus;
			Double amount = imbuing / 100.0 * (Double)this.MaxAmount;

			this.BaseAmount = (int)amount;

			if ( this.BaseAmount == 0 )
				this.BaseAmount = 1;

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
			writer.Write( (int) m_MaxAmount );
			writer.Write( (int) m_BaseAmount );
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
					m_MaxAmount = reader.ReadInt();
					m_BaseAmount = reader.ReadInt();
					m_Quality = (RuneQuality)reader.ReadEncodedInt();
					break;
				}
			}
		}
	}
}