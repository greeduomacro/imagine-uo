using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.FSBountyHunterSystem;

namespace Server.Gumps
{
	public class CreateBountyGump : Gump
	{
		private Mobile m_From;
		private Mobile m_Killer;

		public CreateBountyGump( Mobile from, Mobile killer ) : base( 100, 100 )
		{

			m_From = from;
			m_Killer = killer;

			int balance = 0;

			BankBox box = from.BankBox; 

			if ( box != null ) 
				balance = box.TotalGold;

			Closable = false;
			Disposable = false;
			Dragable = false;
			Resizable = false;

			AddPage(0);

			AddBackground(28, 22, 346, 139, 9200);
			AddHtml( 33, 31, 334, 55, @"<BASEFONT COLOR=WHITE><CENTER>Would you like to place a bounty on your killers head?</CENTER></BASEFONT>", (bool)false, (bool)false);

			AddLabel(33, 89, 1149, @"Current Bank Balance: " + balance.ToString() );

			AddImageTiled(33, 130, 130, 20, 2524);
			AddLabel(33, 108, 1149, @"Amount Of Bounty");
			AddTextEntry(37, 130, 122, 20, 0, 1, @"1000");
			AddButton(190, 128, 247, 248, 1, GumpButtonType.Reply, 0);
			AddButton(270, 128, 241, 242, 2, GumpButtonType.Reply, 0);
		}

		public override void OnResponse( NetState state, RelayInfo info ) 
		{ 
			if ( info.ButtonID == 1 )
			{
				Mobile from = state.Mobile;
				BankBox box = from.BankBox; 
				string text = (string)info.GetTextEntry( 1 ).Text;

				if ( text.Length > 0 )
				{
                  			try 
                  			{ 
                     				int amount = Convert.ToInt32( text );

						if ( amount < 1 )
						{
							from.SendMessage( "Thats to low of an amount." );
							from.SendGump( new CreateBountyGump( from, m_Killer ) );
						}
						else if ( box.TotalGold < amount )
						{
							from.SendMessage( "You lack the gold for that bounty." );
							from.SendGump( new CreateBountyGump( from, m_Killer ) );
						}
						else
						{
							box.ConsumeTotal( typeof( Gold ), amount );
							FSBountySystem.CreateBounty( m_Killer, amount );
							from.SendMessage( "{0} has been removed from your bank.", amount );
							from.SendMessage( "The bounty has been placed on {0}'s head.", m_Killer.Name );
						}
					}
                 	 		catch 
                 			{ 
						from.SendMessage( "You must enter a number amount." );
						from.SendGump( new CreateBountyGump( from, m_Killer ) );
                  			} 
				}
			}
		}
	}
}