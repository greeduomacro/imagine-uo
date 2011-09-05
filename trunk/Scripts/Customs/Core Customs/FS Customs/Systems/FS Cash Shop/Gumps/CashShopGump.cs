using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.Commands;

namespace Server.Gumps
{
	public class CashShopGump : Gump
	{
		public CashShopGump() : base( 0, 0 )
		{
			string title = "<CENTER>Donation Cash Shop</CENTER>";
			string news = "<CENTER><BASEFONT COLOR=black>9/2/2011<BR><BR>No news to report.</CENTER>";

			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage( 0 );

			AddBackground( 7, 6, 748, 486, 5120 );
			AddAlphaRegion( 20, 15, 725, 30 );
			AddHtml( 20, 15, 725, 30, title, (bool)false, (bool)false );
			AddImageTiled( 18, 52, 731, 4, 5121 );
			AddImageTiled( 148, 63, 6, 417, 5123 );
			AddLabel( 43, 59, 1160, @"Catagories" );

			AddButton( 20, 95, 9903, 9904, 1, GumpButtonType.Page, 10 );
			AddButton( 20, 125, 9903, 9904, 1, GumpButtonType.Page, 20 );
			AddButton( 20, 155, 9903, 9904, 1, GumpButtonType.Page, 30 );
			AddButton( 20, 185, 9903, 9904, 1, GumpButtonType.Page, 40 );
			AddButton( 20, 215, 9903, 9904, 1, GumpButtonType.Page, 50 );
			AddButton( 20, 245, 9903, 9904, 1, GumpButtonType.Page, 60 );
			AddButton( 20, 275, 9903, 9904, 1, GumpButtonType.Page, 70 );
			AddButton( 20, 305, 9903, 9904, 1, GumpButtonType.Page, 80 );
			AddButton( 20, 335, 9903, 9904, 1, GumpButtonType.Page, 90 );
			AddButton( 20, 365, 9903, 9904, 1, GumpButtonType.Page, 100 );
			AddButton( 20, 395, 9903, 9904, 1, GumpButtonType.Page, 110 );
			AddButton( 20, 425, 9903, 9904, 1, GumpButtonType.Page, 120 );
			AddButton( 20, 455, 9903, 9904, 1, GumpButtonType.Page, 130 );

			AddLabel( 50, 95, 1152, @"Tokens" );
			AddLabel( 50, 125, 1152, @"Weapons" );
			AddLabel( 50, 155, 1152, @"Armor" );
			AddLabel( 50, 185, 1152, @"Clothing" );
			AddLabel( 50, 215, 1152, @"Jewelery" );
			AddLabel( 50, 245, 1152, @"Talismans" );
			AddLabel( 50, 275, 1152, @"Glasses" );
			AddLabel( 50, 305, 1152, @"House Decore" );
			AddLabel( 50, 335, 1152, @"Crafting" );
			AddLabel( 50, 365, 1152, @"Spellbooks" );
			AddLabel( 50, 395, 1152, @"Pets");
			AddLabel( 50, 425, 1152, @"Shields" );
			AddLabel( 50, 455, 1152, @"Misc." );
			AddAlphaRegion( 168, 66, 570, 412 );
			AddImageTiled( 170, 94, 566, 10, 5124 );
			AddImageTiled( 220, 67, 10, 27, 5124 );
			AddImageTiled( 550, 67, 10, 30, 5124 );
			AddLabel( 180, 70, 1152, @"Buy" );
			AddLabel( 242, 70, 1152, @"Name" );
			AddLabel( 570, 70, 1152, @"Cost" );

			AddPage( 1 );

			AddImage( 207, 66, 5400 );
			AddLabel( 409, 102, 1152, @"Latest News" );
			AddHtml( 317, 204, 266, 198, news, ( bool )false, (bool)true );

			// TOKENS
			AddPage( 10 );

			AddButton( 180, 110, 4029, 4030, 100, GumpButtonType.Reply, 0 );
			AddLabel( 240, 112, 1152, @"Skill Cap Token (Extra 100.0 To Cap)" );
			AddLabel(570, 112, 1152, @"50 Platinum");

			AddButton( 180, 140, 4029, 4030, 101, GumpButtonType.Reply, 0 );
			AddLabel( 240, 142, 1152, @"Follower Cap Token" );
			AddLabel( 570, 142, 1152, @"250 Platinum" );

			AddButton( 180, 170, 4029, 4030, 102, GumpButtonType.Reply, 0 );
			AddLabel( 240, 172, 1152, @"Extra House Slot Token" );
			AddLabel( 570, 172, 1152, @"100 Platinum" );

			AddButton( 180, 200, 4029, 4030, 103, GumpButtonType.Reply, 0 );
			AddLabel( 240, 202, 1152, @"Extra Character Slot Token" );
			AddLabel( 570, 202, 1152, @"100 Platinum" );

			AddButton( 180, 230, 4029, 4030, 104, GumpButtonType.Reply, 0 );
			AddLabel( 240, 232, 1152, @"20% More House & Bank Storage Token" );
			AddLabel( 570, 232, 1152, @"100 Platinum" );

			AddButton( 180, 260, 4029, 4030, 105, GumpButtonType.Reply, 0 );
			AddLabel( 240, 262, 1152, @"Character Rename Token" );
			AddLabel( 570, 262, 1152, @"10 Platinum" );

			AddButton( 180, 290, 4029, 4030, 106, GumpButtonType.Reply, 0 );
			AddLabel( 240, 292, 1152, @"Sex Change Token" );
			AddLabel( 570, 292, 1152, @"10 Platinum" );

			AddButton( 180, 320, 4029, 4030, 107, GumpButtonType.Reply, 0 );
			AddLabel( 240, 322, 1152, @"Heritage Token" );
			AddLabel( 570, 322, 1152, @"30 Platinum" );

			AddButton( 180, 350, 4029, 4030, 108, GumpButtonType.Reply, 0 );
			AddLabel( 240, 352, 1152, @"Personal Attendant Token" );
			AddLabel( 570, 352, 1152, @"30 Platinum" );

			AddButton( 180, 380, 4029, 4030, 109, GumpButtonType.Reply, 0 );
			AddLabel( 240, 382, 1152, @"Soulstone Fragment Token" );
			AddLabel( 570, 382, 1152, @"30 Platinum" );

			AddButton( 180, 410, 4029, 4030, 110, GumpButtonType.Reply, 0 );
			AddLabel( 240, 412, 1152, @"Charger Of The Fallen Token" );
			AddLabel( 570, 412, 1152, @"100 Platinum" );

			AddButton( 580, 440, 0xFA5, 0xFA6, 1, GumpButtonType.Page, 11 );
			AddLabel( 640, 442, 1152, @"Next Page" );

			// TOKENS PAGE 2
			AddPage( 11 );

			AddButton( 180, 110, 4029, 4030, 111, GumpButtonType.Reply, 0 );
			AddLabel( 240, 112, 1152, @"Blue Soulstone Token" );
			AddLabel(570, 112, 1152, @"150 Platinum");

			AddButton( 180, 140, 4029, 4030, 112, GumpButtonType.Reply, 0 );
			AddLabel( 240, 142, 1152, @"Red Soulstone Token" );
			AddLabel( 570, 142, 1152, @"150 Platinum" );

			AddButton( 180, 440, 0xFAE, 0xFAF, 1, GumpButtonType.Page, 10 );
			AddLabel( 240, 442, 1152, @"Last Page" );

			//AddButton( 580, 440, 0xFA5, 0xFA6, 1, GumpButtonType.Page, 12 );
			//AddLabel( 640, 442, 1152, @"Next Page" );

			// WEAPONS
			AddPage( 20 );

			AddButton( 180, 110, 4029, 4030, 200, GumpButtonType.Reply, 0 );
			AddLabel( 240, 112, 1152, @"Razor's Edge (Phoenix Melee Set)" );
			AddLabel(570, 112, 1152, @"200 Platinum");

			// ARMOR
			AddPage( 30 );

			AddButton( 180, 110, 4029, 4030, 300, GumpButtonType.Reply, 0 );
			AddLabel( 240, 112, 1152, @"Blazing Phoenix Helm (Phoenix Caster Set)" );
			AddLabel(570, 112, 1152, @"200 Platinum");

			AddButton( 180, 140, 4029, 4030, 301, GumpButtonType.Reply, 0 );
			AddLabel( 240, 142, 1152, @"Blazing Phoenix Gorget (Phoenix Caster Set)" );
			AddLabel( 570, 142, 1152, @"200 Platinum" );

			AddButton( 180, 170, 4029, 4030, 302, GumpButtonType.Reply, 0 );
			AddLabel( 240, 172, 1152, @"Blazing Phoenix Tunic (Phoenix Caster Set)" );
			AddLabel( 570, 172, 1152, @"200 Platinum" );

			AddButton( 180, 200, 4029, 4030, 303, GumpButtonType.Reply, 0 );
			AddLabel( 240, 202, 1152, @"Blazing Phoenix Leggings (Phoenix Caster Set)" );
			AddLabel( 570, 202, 1152, @"200 Platinum" );

			AddButton( 180, 230, 4029, 4030, 304, GumpButtonType.Reply, 0 );
			AddLabel( 240, 232, 1152, @"Blazing Phoenix Sleeves (Phoenix Caster Set)" );
			AddLabel( 570, 232, 1152, @"200 Platinum" );

			AddButton( 180, 260, 4029, 4030, 305, GumpButtonType.Reply, 0 );
			AddLabel( 240, 262, 1152, @"Blazing Phoenix Gloves (Phoenix Caster Set)" );
			AddLabel( 570, 262, 1152, @"200 Platinum" );

			AddButton( 180, 290, 4029, 4030, 306, GumpButtonType.Reply, 0 );
			AddLabel( 240, 292, 1152, @"Solid Phoenix Helm (Phoenix Melee Set)" );
			AddLabel( 570, 292, 1152, @"200 Platinum" );

			AddButton( 180, 320, 4029, 4030, 307, GumpButtonType.Reply, 0 );
			AddLabel( 240, 322, 1152, @"Solid Phoenix Gorget (Phoenix Melee Set)" );
			AddLabel( 570, 322, 1152, @"200 Platinum" );

			AddButton( 180, 350, 4029, 4030, 308, GumpButtonType.Reply, 0 );
			AddLabel( 240, 352, 1152, @"Solid Phoenix Tunic (Phoenix Melee Set)" );
			AddLabel( 570, 352, 1152, @"200 Platinum" );

			AddButton( 180, 380, 4029, 4030, 309, GumpButtonType.Reply, 0 );
			AddLabel( 240, 382, 1152, @"Solid Phoenix Leggings (Phoenix Melee Set)" );
			AddLabel( 570, 382, 1152, @"200 Platinum" );

			AddButton( 180, 410, 4029, 4030, 310, GumpButtonType.Reply, 0 );
			AddLabel( 240, 412, 1152, @"Solid Phoenix Sleeves (Phoenix Melee Set)" );
			AddLabel( 570, 412, 1152, @"200 Platinum" );

			AddButton( 580, 440, 0xFA5, 0xFA6, 1, GumpButtonType.Page, 31 );
			AddLabel( 640, 442, 1152, @"Next Page" );

			// ARMOR PAGE 2
			AddPage( 31 );

			AddButton( 180, 110, 4029, 4030, 311, GumpButtonType.Reply, 0 );
			AddLabel( 240, 112, 1152, @"Solid Phoenix Gloves (Phoenix Melee Set)" );
			AddLabel(570, 112, 1152, @"200 Platinum");

			AddButton( 180, 440, 0xFAE, 0xFAF, 1, GumpButtonType.Page, 30 );
			AddLabel( 240, 442, 1152, @"Last Page" );

			//AddButton( 580, 440, 0xFA5, 0xFA6, 1, GumpButtonType.Page, 32 );
			//AddLabel( 640, 442, 1152, @"Next Page" );

			AddPage( 40 );

			AddButton( 180, 110, 4029, 4030, 400, GumpButtonType.Reply, 0 );
			AddLabel( 240, 112, 1152, @"A Legendary Robe (2% To All Resists)" );
			AddLabel(570, 112, 1152, @"50 Platinum");

			AddButton( 180, 140, 4029, 4030, 401, GumpButtonType.Reply, 0 );
			AddLabel( 240, 142, 1152, @"Shroud Of Embers (Phoenix Caster Set)" );
			AddLabel( 570, 142, 1152, @"200 Platinum" );

			AddButton( 180, 170, 4029, 4030, 402, GumpButtonType.Reply, 0 );
			AddLabel( 240, 172, 1152, @"Robe of Ashes (Phoenix Melee Set)" );
			AddLabel( 570, 172, 1152, @"200 Platinum" );

			AddButton( 180, 200, 4029, 4030, 403, GumpButtonType.Reply, 0 );
			AddLabel( 240, 202, 1152, @"Burning Sandals (Phoenix Caster Set)" );
			AddLabel( 570, 202, 1152, @"200 Platinum" );

			AddButton( 180, 230, 4029, 4030, 404, GumpButtonType.Reply, 0 );
			AddLabel( 240, 232, 1152, @"Magma Boots (Phoenix Melee Set)" );
			AddLabel( 570, 232, 1152, @"200 Platinum" );

			AddButton( 180, 260, 4029, 4030, 405, GumpButtonType.Reply, 0 );
			AddLabel( 240, 262, 1152, @"Boiled Apron (Phoenix Caster Set)" );
			AddLabel( 570, 262, 1152, @"200 Platinum" );

			AddButton( 180, 290, 4029, 4030, 406, GumpButtonType.Reply, 0 );
			AddLabel( 240, 292, 1152, @"Steamed Aporn (Phoenix Melee Set)" );
			AddLabel( 570, 292, 1152, @"200 Platinum" );

			//AddButton( 580, 440, 0xFA5, 0xFA6, 1, GumpButtonType.Page, 31 );
			//AddLabel( 640, 442, 1152, @"Next Page" );

			AddPage( 50 );

			AddButton( 180, 110, 4029, 4030, 500, GumpButtonType.Reply, 0 );
			AddLabel( 240, 112, 1152, @"A Reagent Ring (100% LRC)" );
			AddLabel(570, 112, 1152, @"50 Platinum");

			AddButton( 180, 140, 4029, 4030, 501, GumpButtonType.Reply, 0 );
			AddLabel( 240, 142, 1152, @"A Mana Bracelet (50% LMC)" );
			AddLabel( 570, 142, 1152, @"50 Platinum" );
            
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			Mobile from = sender.Mobile;

        		if ( info.ButtonID == 0 ) // Close
         		{
			}

        		if ( info.ButtonID == 100 )
         		{
				if ( from.Backpack.ConsumeTotal( typeof( Platinum ), 50, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new SkillCapToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else if ( from.BankBox.ConsumeTotal( typeof( Platinum ), 50, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new SkillCapToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else
				{
					from.SendMessage( "You do not have enough platinum for this item." );
				}
			}

        		if ( info.ButtonID == 101 )
         		{
				if ( from.Backpack.ConsumeTotal( typeof( Platinum ), 250, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new FollowerCapToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else if ( from.BankBox.ConsumeTotal( typeof( Platinum ), 250, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new FollowerCapToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else
				{
					from.SendMessage( "You do not have enough platinum for this item." );
				}
			}

        		if ( info.ButtonID == 102 )
         		{
				if ( from.Backpack.ConsumeTotal( typeof( Platinum ), 100, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new HouseSlotToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else if ( from.BankBox.ConsumeTotal( typeof( Platinum ), 50, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new HouseSlotToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else
				{
					from.SendMessage( "You do not have enough platinum for this item." );
				}
			}

        		if ( info.ButtonID == 103 )
         		{
				if ( from.Backpack.ConsumeTotal( typeof( Platinum ), 100, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new CharacterSlotToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else if ( from.BankBox.ConsumeTotal( typeof( Platinum ), 100, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new CharacterSlotToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else
				{
					from.SendMessage( "You do not have enough platinum for this item." );
				}
			}

        		if ( info.ButtonID == 104 )
         		{
				if ( from.Backpack.ConsumeTotal( typeof( Platinum ), 100, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new StorageToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else if ( from.BankBox.ConsumeTotal( typeof( Platinum ), 100, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new StorageToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else
				{
					from.SendMessage( "You do not have enough platinum for this item." );
				}
			}

        		if ( info.ButtonID == 105 )
         		{
				if ( from.Backpack.ConsumeTotal( typeof( Platinum ), 10, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new RenameToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else if ( from.BankBox.ConsumeTotal( typeof( Platinum ), 10, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new RenameToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else
				{
					from.SendMessage( "You do not have enough platinum for this item." );
				}
			}

        		if ( info.ButtonID == 106 )
         		{
				if ( from.Backpack.ConsumeTotal( typeof( Platinum ), 10, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new SexChangeToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else if ( from.BankBox.ConsumeTotal( typeof( Platinum ), 10, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new SexChangeToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else
				{
					from.SendMessage( "You do not have enough platinum for this item." );
				}
			}

        		if ( info.ButtonID == 107 )
         		{
				if ( from.Backpack.ConsumeTotal( typeof( Platinum ), 30, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new HeritageToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else if ( from.BankBox.ConsumeTotal( typeof( Platinum ), 30, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new HeritageToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else
				{
					from.SendMessage( "You do not have enough platinum for this item." );
				}
			}

        		if ( info.ButtonID == 108 )
         		{
				if ( from.Backpack.ConsumeTotal( typeof( Platinum ), 30, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new PersonalAttendantToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else if ( from.BankBox.ConsumeTotal( typeof( Platinum ), 30, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new PersonalAttendantToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else
				{
					from.SendMessage( "You do not have enough platinum for this item." );
				}
			}

        		if ( info.ButtonID == 109 )
         		{
				if ( from.Backpack.ConsumeTotal( typeof( Platinum ), 30, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new SoulstoneFragmentToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else if ( from.BankBox.ConsumeTotal( typeof( Platinum ), 30, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new SoulstoneFragmentToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else
				{
					from.SendMessage( "You do not have enough platinum for this item." );
				}
			}

        		if ( info.ButtonID == 110 )
         		{
				if ( from.Backpack.ConsumeTotal( typeof( Platinum ), 100, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new ChargerOfTheFallenToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else if ( from.BankBox.ConsumeTotal( typeof( Platinum ), 30, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new ChargerOfTheFallenToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else
				{
					from.SendMessage( "You do not have enough platinum for this item." );
				}
			}

        		if ( info.ButtonID == 111 )
         		{
				if ( from.Backpack.ConsumeTotal( typeof( Platinum ), 150, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new BlueSoulstoneToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else if ( from.BankBox.ConsumeTotal( typeof( Platinum ), 150, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new BlueSoulstoneToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else
				{
					from.SendMessage( "You do not have enough platinum for this item." );
				}
			}

        		if ( info.ButtonID == 112 )
         		{
				if ( from.Backpack.ConsumeTotal( typeof( Platinum ), 150, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new RedSoulstoneToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else if ( from.BankBox.ConsumeTotal( typeof( Platinum ), 150, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new RedSoulstoneToken() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else
				{
					from.SendMessage( "You do not have enough platinum for this item." );
				}
			}

        		if ( info.ButtonID == 500 )
         		{
				if ( from.Backpack.ConsumeTotal( typeof( Platinum ), 50, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new ReagentRing() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else if ( from.BankBox.ConsumeTotal( typeof( Platinum ), 50, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new ReagentRing() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else
				{
					from.SendMessage( "You do not have enough platinum for this item." );
				}
			}

        		if ( info.ButtonID == 501 )
         		{
				if ( from.Backpack.ConsumeTotal( typeof( Platinum ), 50, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new ManaBracelet() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else if ( from.BankBox.ConsumeTotal( typeof( Platinum ), 50, true ) )
				{
					BankBox bank = from.BankBox;
					bank.DropItem( new ManaBracelet() );

					from.SendMessage( "Your item has been placed in your bankbox." );
				}
				else
				{
					from.SendMessage( "You do not have enough platinum for this item." );
				}
			}
		}
	}
}