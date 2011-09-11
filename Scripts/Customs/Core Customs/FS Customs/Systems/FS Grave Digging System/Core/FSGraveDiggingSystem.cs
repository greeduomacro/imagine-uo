using Server;
using System;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Targets;
using System.Collections;

namespace Server.FSGraveDiggingSystem
{
	public class FSGraveSystem
	{
		private static Hashtable m_IsDigging = new Hashtable();

		public static bool IsDigging( Mobile m )
		{
			return m_IsDigging.Contains( m );
		}

		private class InternalTimer : Timer
		{
			private int m_Tally;
			private int m_Count = 3;
			private Mobile m_Mobile;
			private Item m_Tool;

			public InternalTimer( Mobile from, Item tool ) : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.0 ) )
			{
				m_Mobile = from;
				m_Tool = tool;
			}

			protected override void OnTick()
			{
				if ( m_Tally < m_Count )
				{
					m_Tally += 1;
					m_Mobile.RevealingAction();

					m_Mobile.PlaySound( Utility.RandomMinMax( 0x125, 0x126 ) );
					m_Mobile.Animate( 11, 5, 1, true, false, 0 );
				}

				if ( m_Tally >= m_Count )
				{
					EndDig( m_Mobile, m_Tool );
				}
			}
		}

		public static void EndDig( Mobile from, Item tool )
		{
			Timer it = (Timer)m_IsDigging[from];

			if ( it != null )
				it.Stop();

			m_IsDigging.Remove( from );


			BaseHarvestTool t = (BaseHarvestTool)tool;
			t.UsesRemaining -= 1;
	
			if ( t.UsesRemaining == 0 )
			{
				tool.Delete();
				from.SendMessage( "You have worn out your tool!" );
			}

			if ( from.CheckSkill( SkillName.Forensics, 0, 100 ) )
			{
				int bonus = 1;

				if ( tool is SturdyShovel )
					bonus = 2;
				
				if ( tool is GraveDiggersShovel )
					bonus = 4;

				if ( tool is GoldenShovel )
					bonus = 6;

				if ( tool is DiamondShovel )
					bonus = 8;

				if ( tool is SummonShovel )
					bonus = 10;

				Double chance = from.Skills[SkillName.Forensics].Value / 10 * bonus;

				if ( tool is SummonShovel && Utility.Random( 1000 ) < chance )
				{
					int roll = Utility.Random( 100 );
					Mobile spawn;
	
					if ( roll < 50 )
					{
						spawn = new FailSafe();
						spawn.MoveToWorld( from.Location, from.Map );

						spawn.Combatant = from;
						spawn.Z++;

						World.Broadcast( 0x35, true, "{0} has summoned Fail Safe!", from.Name );
					}
					else
					{
						spawn = new Grobbubatus();
						spawn.MoveToWorld( from.Location, from.Map );

						spawn.Combatant = from;
						spawn.Z++;

						World.Broadcast( 0x35, true, "{0} has summoned Grobbubatus!", from.Name );
					}
				}
				else if ( Utility.Random( 100 ) < chance )
				{
					int roll = Utility.Random( 2 );

					switch ( roll )
					{
						case 0: from.AddToBackpack( GiveReward( from, tool ) ); break;
						case 1: from.AddToBackpack( Runescribing.GetResoureDrop( false, true ) ); break;
					}
				}
				else
				{
					if ( Utility.Random( 100 ) < 25 - bonus )
					{
						from.SendMessage( "You have disturbed the dead!" );

						BaseCreature m = null;

						try
						{
							m = Activator.CreateInstance( m_Dead[Utility.Random( m_Dead.Length )] ) as BaseCreature;
						}
						catch
						{
						}

        					m.Location = from.Location;
        					m.Map = from.Map;

						if ( m.IsParagon )
							m.IsParagon = false;

        					World.AddMobile( m );

						m.Combatant = from;
						m.Z++;
					}
					else
					{
						from.SendMessage( "You skillfully dig the area but do not find anything." );
					}
				}
			}
			else
			{
				if ( Utility.Random( 100 ) < 50 )
				{
					from.SendMessage( "You have disturbed the dead!" );

					BaseCreature m = null;

					try
					{
						m = Activator.CreateInstance( m_Dead[Utility.Random( m_Dead.Length )] ) as BaseCreature;
					}
					catch
					{
					}

        				m.Location = from.Location;
        				m.Map = from.Map;

					if ( m.IsParagon )
						m.IsParagon = false;

        				World.AddMobile( m );

					m.Combatant = from;
					m.Z++;	
				}
				else
				{
					from.SendMessage( "You fail to discover anything of interest." );
				}
			}
		}

		private static Type[] m_Dead = new Type[]
			{
				typeof( Skeleton ),
				typeof( Ghoul ),
				typeof( Zombie ),
				typeof( Bogle ),
				typeof( Spectre ),
				typeof( Lich ),
				typeof( Mummy ),
				typeof( SkeletalMage ),
				typeof( Shade ),
				typeof( Wraith ),
				typeof( BoneMagi ),
				typeof( BoneKnight ),
				typeof( RottingCorpse ),
				typeof( SkeletalKnight ),
				typeof( SpectralArmour ),
				typeof( RestlessSoul ),
				typeof( LichLord )
			};

		private static Type[] m_GenericLoot = new Type[]
			{
				typeof( Amber ),
				typeof( Amethyst ),
				typeof( Citrine ),
				typeof( Diamond ),
				typeof( Emerald ),
				typeof( Ruby ),
				typeof( Sapphire ),
				typeof( StarSapphire ),
				typeof( BonePile ),
				typeof( Skull ),
				typeof( Head ),
				typeof( LeftArm ),
				typeof( LeftLeg ),
				typeof( RibCage ),
				typeof( RightArm ),
				typeof( RightLeg ),
				typeof( Torso ),
				typeof( BlackPearl ),
				typeof( Bloodmoss ),
				typeof( Garlic ),
				typeof( Ginseng ),
				typeof( SpidersSilk ),
				typeof( Nightshade ),
				typeof( MandrakeRoot ),
				typeof( SulfurousAsh ),
				typeof( BatWing ),
				typeof( PigIron ),
				typeof( NoxCrystal ),
				typeof( GraveDust ),
				typeof( DaemonBlood ),
				typeof( Gold ),
				typeof( FertileDirt ),
				typeof( FertileDirt ),
				typeof( FertileDirt ),
				typeof( FertileDirt ),
				typeof( FertileDirt ),
				typeof( IronOre ),
				typeof( DullCopperOre ),
				typeof( ShadowIronOre ),
				typeof( CopperOre ),
				typeof( BronzeOre ),
				typeof( AgapiteOre ),
				typeof( GoldOre ),
				typeof( VeriteOre ),
				typeof( ValoriteOre ),
				typeof( IronOre ),
				typeof( DullCopperOre ),
				typeof( ShadowIronOre ),
				typeof( CopperOre ),
				typeof( BronzeOre ),
				typeof( AgapiteOre ),
				typeof( GoldOre ),
				typeof( VeriteOre ),
				typeof( ValoriteOre ),
				typeof( IronOre ),
				typeof( DullCopperOre ),
				typeof( ShadowIronOre ),
				typeof( CopperOre ),
				typeof( BronzeOre ),
				typeof( AgapiteOre ),
				typeof( GoldOre ),
				typeof( VeriteOre ),
				typeof( ValoriteOre ),
				typeof( DecoRocks2 ),
				typeof( Granite ),
				typeof( DullCopperGranite ),
				typeof( ShadowIronGranite ),
				typeof( CopperGranite ),
				typeof( BronzeGranite ),
				typeof( AgapiteGranite ),
				typeof( GoldGranite ),
				typeof( VeriteGranite ),
				typeof( ValoriteGranite )

			};

		private static Type[] m_EnhancedLoot = new Type[] // Add Artifacts
			{
				typeof( SummonShovel ),
				typeof( ButchersResolve ),
				typeof( PlayerDeathShroud ),
				typeof( GraveDiggerRing ),
				typeof( GraveDiggerBracelet ),
				typeof( GraveDiggerEarrings )
			};

		public static void StartDig( Item tool, Mobile from, Object target )
		{
			if ( IsDigging( from ) )
			{
				from.SendMessage( "You are already digging." );
			}
			else if ( !from.InRange( ((StaticTarget)target).Location, 3 ) )
			{
				from.SendMessage( "You must be closer to the grave to dig." );
			}
			else if ( tool is Pickaxe || tool is GargoylesPickaxe || tool is SturdyPickaxe )
			{
				from.PlaySound( Utility.RandomMinMax( 0x125, 0x126 ) );
				from.Animate( 11, 5, 1, true, false, 0 );
				from.SendMessage( "You dig and dig but cant seem to move enough dirt!" );
			}
			else if ( from.CheckSkill( SkillName.Mining, 0, 100 ) )
			{
				from.PlaySound( Utility.RandomMinMax( 0x125, 0x126 ) );
				from.Animate( 11, 5, 1, true, false, 0 );

				Timer it = (Timer)m_IsDigging[from];
	
				if ( it != null )
					it.Stop();

				it = new InternalTimer( from, tool );
				m_IsDigging[from] = it;

				it.Start();
			}
			else
			{
				from.PlaySound( Utility.RandomMinMax( 0x125, 0x126 ) );
				from.Animate( 11, 5, 1, true, false, 0 );

				if ( Utility.Random( 100 ) < 50 )
				{
					from.SendMessage( "You have disturbed the dead!" );

					BaseCreature m = null;

					try
					{
						m = Activator.CreateInstance( m_Dead[Utility.Random( m_Dead.Length )] ) as BaseCreature;
					}
					catch
					{
					}

        				m.Location = from.Location;
        				m.Map = from.Map;

					if ( m.IsParagon )
						m.IsParagon = false;

        				World.AddMobile( m );

					m.Combatant = from;
					m.Z++;
				}
				else
				{
					from.SendMessage( "You are unable to find anything in the grave." );
				}
			}
		}

		public static Item GiveReward( Mobile from, Item tool )
		{
			int bonus = 1;

			if ( tool is SturdyShovel )
				bonus = 2;

			if ( tool is GraveDiggersShovel )
				bonus = 4;

			if ( tool is GoldenShovel )
				bonus = 6;

			if ( tool is DiamondShovel )
				bonus = 8;

			if ( tool is SummonShovel )
				bonus = 10;

			Item i = null;

			try
			{
				Double chance = from.Skills[SkillName.Forensics].Value / 50 * bonus;
				
				if ( Utility.Random( 200 ) <= chance && from.Skills[SkillName.Forensics].Value >= 80 )
				{
					i = Activator.CreateInstance( m_EnhancedLoot[Utility.Random( m_EnhancedLoot.Length )] ) as Item;
					from.SendMessage( "You find something extraordinary!" );
				}
				else if ( Utility.Random( 100 ) < 15 )
				{

					i = new TreasureMap( Utility.RandomMinMax( 1, 7 ), from.Map );
					from.SendMessage( "You find a treasure map!" );
				}
				else
				{
					i = Activator.CreateInstance( m_GenericLoot[Utility.Random( m_GenericLoot.Length )] ) as Item;
					from.SendMessage( "You find something buried in the grave!" );
				}
			}
			catch
			{
			}

			if ( i.Stackable == true )
				i.Amount = Utility.RandomMinMax( 2, 10 ) * bonus;

			//TODO: Add chance to spawn mini boss from grave digging.

			return i;
		}
	}
}