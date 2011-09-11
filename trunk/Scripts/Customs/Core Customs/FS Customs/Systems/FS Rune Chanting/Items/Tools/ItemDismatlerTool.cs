using System;
using Server.Items;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	public class ItemDismatlerTool : Item
	{
		private int m_Uses;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Uses
		{
			get{ return m_Uses; }
			set{ m_Uses = value; InvalidateProperties(); }
		}

		[Constructable]
		public ItemDismatlerTool() : base( 0x1EBA )
		{
			Name = "an item dismantler tool";
			Uses = Utility.RandomMinMax( 50, 100 );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.Target = new DismantalTarget( this );
				from.SendMessage( "Select the item you wish to dismantle." );
			}
			else
			{
				from.SendLocalizedMessage( 1062334 ); // This item must be in your backpack to be used.
			}
		}

		public ItemDismatlerTool( Serial serial ) : base( serial )
		{
		}


		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060584, m_Uses.ToString() ); // uses remaining: ~1_val~
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			// Release
			writer.Write( (int) m_Uses );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch( version )
			{
				case 0:
				{
					m_Uses = reader.ReadInt();
					break;
				}
			}
		
		}

		private class DismantalTarget : Target
		{
			private Item m_Item;

			public DismantalTarget( Item item ) : base( -1, false, TargetFlags.None )
			{
				m_Item = item;
			}

			protected override void OnTarget( Mobile from, object target )
			{
				Item item = target as Item;

				if ( target is Item && item.IsChildOf( from.Backpack ) )
				{
					if ( from.CheckSkill( SkillName.Imbuing, 0, 50 ) )
					{
						if ( Utility.Random( 1000 ) < from.Luck / 100 )
						{
							from.SendMessage( "You dismantle the item and get some runescribing materials plus something extreamly rare." );
							from.AddToBackpack( new CosmicCrystal() );
						}
						else
							from.SendMessage( "You dismantle the item and get some runescribing materials." );

						ItemDismatlerTool tool = m_Item as ItemDismatlerTool;

						tool.Uses -= 1;

						if ( tool.Uses == 0 )
						{
							tool.Delete();
							from.SendMessage( "Your tool has worn out." );
						}

						if ( target is BaseArmor || target is BaseWeapon || target is BaseJewel || target is BaseTalisman || target is BaseClothing || target is Spellbook || target is BaseQuiver )
						{
							int props = Runescribing.GetProps( item );

							if ( item.ItemValue == ItemValue.OOAK )
							{
								Bag cont = new Bag();
						
								cont.DropItem( new MagicalDust( 20 ) );
								cont.DropItem( new LesserMysticEssence( 20 ) );
								cont.DropItem( new MysticEssence( 20 ) );
								cont.DropItem( new CosmicOil( 20 ) );
								cont.DropItem( new MagicalRelic( 20 ) );

								cont.Hue = 1150;
								cont.Name = "a bag of runescribing resources";
								cont.ItemValue = ItemValue.Uncommon;

								from.AddToBackpack( cont );

								item.Delete();
							}
							else if ( props == 0 )
							{
								item.Delete();
								from.AddToBackpack( new MagicalDust( 1 ) );
							}
							else if ( props == 1 )
							{
								item.Delete();
								from.AddToBackpack( new MagicalDust( Utility.Random( 3 ) ) );
							}
							else if ( props == 2 )
							{
								if ( Utility.Random( 100 ) < 5 )
									from.AddToBackpack( new LesserMysticEssence( 1 ) );
								else
									from.AddToBackpack( new MagicalDust( Utility.Random( 3 ) ) );

								item.Delete();
							}
							else if ( props == 3 )
							{
								if ( Utility.Random( 100 ) < 5 )
									from.AddToBackpack( new MysticEssence( 1 ) );
								else
									from.AddToBackpack( new LesserMysticEssence( Utility.Random( 3 ) ) );

								item.Delete();
							}
							else if ( props == 4 )
							{
								if ( Utility.Random( 100 ) < 5 )
									from.AddToBackpack( new MysticEssence( 1 ) );
								else
									from.AddToBackpack( new LesserMysticEssence( Utility.Random( 3 ) ) );

								item.Delete();
							}
							else if ( props == 5 )
							{
								if ( Utility.Random( 100 ) < 5 )
									from.AddToBackpack( new CosmicOil( 1 ) );
								else
									from.AddToBackpack( new MysticEssence( Utility.Random( 3 ) ) );
	
								item.Delete();
							}
							else if ( props == 6 )
							{
								if ( Utility.Random( 100 ) < 5 )
									from.AddToBackpack( new CosmicOil( 1) );
								else
									from.AddToBackpack( new MysticEssence( Utility.Random( 3 ) ) );

								item.Delete();
							}
							else if ( props == 7 )
							{
								if ( Utility.Random( 100 ) < 5 )
									from.AddToBackpack( new MagicalRelic( 1 ) );
								else
									from.AddToBackpack( new CosmicOil( Utility.Random( 3 ) ) );

								item.Delete();
							}
							else
							{
								from.AddToBackpack( new MagicalRelic( 1 ) );
								item.Delete();
							}
						}
						else
						{
							ItemValue ivalue = item.ItemValue;

							if ( ivalue == ItemValue.Trash )
							{
								if ( Utility.Random( 100 ) < 5 )
									from.AddToBackpack( new LesserMysticEssence( 1 ) );
								else
									from.AddToBackpack( new MagicalDust( Utility.Random( 3 ) ) );

								item.Delete();
							}
							else if ( ivalue == ItemValue.Common )
							{
								if ( Utility.Random( 100 ) < 5 )
									from.AddToBackpack( new MysticEssence( 1 ) );
								else
									from.AddToBackpack( new LesserMysticEssence( Utility.Random( 3 ) ) );

								item.Delete();
							}
							else if ( ivalue == ItemValue.Uncommon )
							{
								if ( Utility.Random( 100 ) < 5 )
									from.AddToBackpack( new MysticEssence( 1 ) );
								else
									from.AddToBackpack( new LesserMysticEssence( Utility.Random( 3 ) ) );

								item.Delete();
							}
							else if ( ivalue == ItemValue.Rare )
							{
								if ( Utility.Random( 100 ) < 5 )
									from.AddToBackpack( new CosmicOil( 1 ) );
								else
									from.AddToBackpack( new MysticEssence( Utility.Random( 3 ) ) );

								item.Delete();
							}
							else if ( ivalue == ItemValue.Epic )
							{
								if ( Utility.Random( 100 ) < 5 )
									from.AddToBackpack( new MagicalRelic( 1 ) );
								else
									from.AddToBackpack( new CosmicOil( Utility.Random( 3 ) ) );

								item.Delete();
							}
							else if ( ivalue == ItemValue.Legendary )
							{
								from.AddToBackpack( new MagicalRelic( 1 ) );
								item.Delete();
							}
							else if ( ivalue == ItemValue.OOAK )
							{
								Bag cont = new Bag();
						
								cont.DropItem( new MagicalDust( 20 ) );
								cont.DropItem( new LesserMysticEssence( 20 ) );
								cont.DropItem( new MysticEssence( 20 ) );
								cont.DropItem( new CosmicOil( 20 ) );
								cont.DropItem( new MagicalRelic( 20 ) );

								cont.Hue = 1150;
								cont.Name = "a bag of runescribing resources";
								cont.ItemValue = ItemValue.Uncommon;
	
								from.AddToBackpack( cont );

								item.Delete();
							}
							else if ( ivalue == ItemValue.Fabled )
							{
								from.AddToBackpack( new MagicalRelic( 3 ) );
								item.Delete();
							}
							else if ( ivalue == ItemValue.Crafted )
							{
								if ( Utility.Random( 100 ) < 5 )
									from.AddToBackpack( new LesserMysticEssence( 1 ) );
								else
									from.AddToBackpack( new MagicalDust( Utility.Random( 3 ) ) );

								item.Delete();
							}
							else if ( ivalue == ItemValue.Reagent )
							{
								if ( Utility.Random( 100 ) < 5 )
									from.AddToBackpack( new LesserMysticEssence( 1 ) );
								else
									from.AddToBackpack( new MagicalDust( Utility.Random( 3 ) ) );
	
								item.Delete();
							}
							else if ( ivalue == ItemValue.Resource )
							{
								if ( Utility.Random( 100 ) < 5 )
									from.AddToBackpack( new LesserMysticEssence( 1 ) );
								else
									from.AddToBackpack( new MagicalDust( Utility.Random( 3 ) ) );

								item.Delete();
							}
							else if ( ivalue == ItemValue.Ancient )
							{
								from.AddToBackpack( new MagicalRelic( 3 ) );
								item.Delete();
							}
							else if ( ivalue == ItemValue.Fabulous )
							{
								from.AddToBackpack( new MagicalRelic( 3 ) );
								item.Delete();
							}
							else
							{
								if ( Utility.Random( 100 ) < 5 )
									from.AddToBackpack( new LesserMysticEssence( 1 ) );
								else
									from.AddToBackpack( new MagicalDust( Utility.Random( 3 ) ) );

								item.Delete();
							}
						}
					}
					else
					{
						from.SendMessage( "You fail to dismantle this item." );
					}
				}
				else
				{
					from.SendMessage( "You can only dismantle items in your backpack." );
				}
			}
		}
	}
}