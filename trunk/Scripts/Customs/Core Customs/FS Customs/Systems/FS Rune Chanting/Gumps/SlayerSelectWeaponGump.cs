using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;
using Server.Commands;

namespace Server.Gumps
{
	public class SlayerSelectWeaponGump : Gump
	{
		BaseWeapon m_Item;

		public SlayerSelectWeaponGump( BaseWeapon item ) : base( 0, 0 )
		{
			m_Item = item;

			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddBackground(131, 90, 185, 292, 9200);
			AddAlphaRegion(137, 96, 172, 25);
			AddAlphaRegion(137, 130, 172, 247);
			AddHtml( 137, 96, 172, 25, @"<BASEFONT COLOR=#FFFFFF><CENTER>Select A Slayer</CENTER></BASEFONT>", (bool)false, (bool)false);
			AddButton(145, 140, 4023, 4024, 1, GumpButtonType.Reply, 0);
			AddLabel(180, 140, 1152, @"Repond Slayer");
			AddButton(145, 170, 4023, 4024, 2, GumpButtonType.Reply, 0);
			AddLabel(180, 170, 1152, @"Reptile Slayer");
			AddButton(145, 200, 4023, 4024, 3, GumpButtonType.Reply, 0);
			AddLabel(180, 200, 1152, @"Demon Slayer");
			AddButton(145, 230, 4023, 4024, 4, GumpButtonType.Reply, 0);
			AddLabel(180, 230, 1152, @"Elemental Slayer");
			AddButton(145, 260, 4023, 4024, 5, GumpButtonType.Reply, 0);
			AddLabel(180, 260, 1152, @"Undead Slayer");
			AddButton(145, 290, 4023, 4024, 6, GumpButtonType.Reply, 0);
			AddLabel(180, 290, 1152, @"Arachnid Slayer");
			AddButton(145, 320, 4023, 4024, 7, GumpButtonType.Reply, 0);
			AddLabel(180, 320, 1152, @"Fey Slayer");
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			Mobile from = sender.Mobile;
			BaseWeapon item = m_Item;

			if ( info.ButtonID == 1 )
         		{
				item.Slayer = SlayerName.Repond;
				from.SendMessage( "Your slayer has been changed." );
			}

			if ( info.ButtonID == 2 )
         		{
				item.Slayer = SlayerName.ReptilianDeath;
				from.SendMessage( "Your slayer has been changed." );
			}

			if ( info.ButtonID == 3 )
         		{
				item.Slayer = SlayerName.Exorcism;
				from.SendMessage( "Your slayer has been changed." );
			}

			if ( info.ButtonID == 4 )
         		{
				item.Slayer = SlayerName.ElementalBan;
				from.SendMessage( "Your slayer has been changed." );
			}

			if ( info.ButtonID == 5 )
         		{
				item.Slayer = SlayerName.Silver;
				from.SendMessage( "Your slayer has been changed." );
			}

			if ( info.ButtonID == 6 )
         		{
				item.Slayer = SlayerName.ArachnidDoom;
				from.SendMessage( "Your slayer has been changed." );
			}

			if ( info.ButtonID == 7 )
         		{
				item.Slayer = SlayerName.Fey;
				from.SendMessage( "Your slayer has been changed." );
			}

			if ( info.ButtonID == 0 )
         		{
			}
        	}
    	}
}