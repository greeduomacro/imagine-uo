using System; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Engines.Craft 
{
	public enum RunescribingRecipe
	{
		SlayerOfOneRune = 	10001,
		BattleLustRune =	10002,
		BloodDrinkerRune =	10003,
		SlayerRune =		10004,
		MagicalBloodDust =	10005
	}

	public class DefRunescribing : CraftSystem 
	{ 
		public override SkillName MainSkill 
		{ 
			get{ return SkillName.Imbuing; } 
		} 

		public override string GumpTitleString
		{
			get { return "<BASEFONT COLOR=#FFFFFF><CENTER>Runescribing</CENTER></BASEFONT>"; }
		}

		private static CraftSystem m_CraftSystem; 

		public static CraftSystem CraftSystem 
		{ 
			get 
			{ 
				if ( m_CraftSystem == null ) 
					m_CraftSystem = new DefRunescribing(); 

				return m_CraftSystem; 
			} 
		} 

		public override double GetChanceAtMin( CraftItem item ) 
		{ 
			return 0.0; // 0% 
		} 

		private DefRunescribing() : base( 1, 1, 1.25 )// base( 1, 2, 1.7 ) 
		{ 
		}

		private static Type typeofForge = typeof( SoulForgeAttribute );

		public static void CheckForge( Mobile from, int range, out bool forge )
		{
			forge = false;

			Map map = from.Map;

			if ( map == null )
				return;

			IPooledEnumerable eable = map.GetItemsInRange( from.Location, range );

			foreach ( Item item in eable )
			{
				Type type = item.GetType();

				bool isForge = ( type.IsDefined( typeofForge, false ) || ( item.ItemID >= 17015 && item.ItemID <= 17030 ) );

				if ( isForge )
				{
					if ( (from.Z + 16) < item.Z || (item.Z + 16) < from.Z || !from.InLOS( item ) )
						continue;

					forge = forge || isForge;

					if ( forge )
						break;
				}
			}

			eable.Free();

			for ( int x = -range; ( !forge ) && x <= range; ++x )
			{
				for ( int y = -range; ( !forge ) && y <= range; ++y )
				{
					StaticTile[] tiles = map.Tiles.GetStaticTiles( from.X+x, from.Y+y, true );

					for ( int i = 0; ( !forge ) && i < tiles.Length; ++i )
					{
						int id = tiles[i].ID;

						bool isForge = ( (id >= 17015 && id <= 17030 ) );

						if ( isForge )
						{
							if ( (from.Z + 16) < tiles[i].Z || (tiles[i].Z + 16) < from.Z || !from.InLOS( new Point3D( from.X+x, from.Y+y, tiles[i].Z + (tiles[i].Height/2) + 1 ) ) )
								continue;

							forge = forge || isForge;
						}
					}
				}
			}
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			bool forge;
			CheckForge( from, 2, out forge );

			if ( forge )
				return 0;

			return 1079787; // You must be near a soulforge to imbue an item.
		} 

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x237 );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item ) 
		{ 
			if ( toolBroken ) 
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool 

			if ( failed ) 
			{ 
				if ( lostMaterial ) 
					return 1044043; // You failed to create the item, and some of your materials are lost. 
				else 
					return 1044157; // You failed to create the item, but no materials were lost. 
			} 
			else 
			{ 
				if ( quality == 0 ) 
					return 502785; // You were barely able to make this item.  It's quality is below average. 
				else if ( makersMark && quality == 2 ) 
					return 1044156; // You create an exceptional quality item and affix your maker's mark. 
				else if ( quality == 2 ) 
					return 1044155; // You create an exceptional quality item. 
				else             
					return 1044154; // You create the item. 
			} 
		} 

		public override void InitCraftList()
		{
			int index = -1;

			// Components
			index = AddCraft( typeof( RecallRune ), "Components", "Recall Rune", 15.0, 45.0, typeof( RefinedStoneBrick ), "Refined Stone Brick", 1 );
			index = AddCraft( typeof( BlankScribingRune ), "Components", "Blank Scribing Rune", 30.0, 60.0, typeof( RefinedStoneBrick ), "Refined Stone Brick", 1 );


			// Resorces
			index = AddCraft( typeof( RefinedStoneBrick ), "Resources", "Refined Stone Brick", 0.0, 30.0, typeof( UnrefinedStone ), "Unrefined Stone", 2 );

			index = AddCraft( typeof( MagicalBloodDust ), "Resources", "Magical Blood Dust", 15.0, 45.0, typeof( MagicalDust ), "Magical Dust", 5 );
			AddRecipe( index, (int)RunescribingRecipe.MagicalBloodDust );
			AddRes( index, typeof( LesserMysticEssence ), "Lesser Mystic Essence", 10 );
			AddRes( index, typeof( Bloodmoss ), "Blood Moss", 25 );
			AddRes( index, typeof( DaemonBlood ), "Daemon's Blood", 25 );


			// Tools
			index = AddCraft( typeof( ItemDismatlerTool ), "Tools", "Item Dismatler Tool", 0.0, 50.0, typeof( Board ), "Board", 15 );
			AddRes( index, typeof( RefinedStoneBrick ), "Refined Stone Brick", 25 );
			AddRes( index, typeof( IronIngot ), "IronIngot", 25 );
			AddRes( index, typeof( Leather ), "Leather", 15 );


			// Minor Runes
			index = AddCraft( typeof( AttackChanceRune ), "Minor Runes", "Hit Chance Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );

			index = AddCraft( typeof( BattleLustRune ), "Minor Runes", "Battle Lust Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRecipe( index, (int)RunescribingRecipe.BattleLustRune );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( AncientMoonstone ), "Ancient Moonstone", 2 );

			index = AddCraft( typeof( BloodDrinkerRune ), "Minor Runes", "Blood Drinker Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRecipe( index, (int)RunescribingRecipe.BloodDrinkerRune );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( BoneDust ), "Bone Dust", 2 );

			index = AddCraft( typeof( BonusDexRune ), "Minor Runes", "Dexterity Bonus Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( PerfectSkull ), "Perfect Skull", 2 );

			index = AddCraft( typeof( BonusHitsRune ), "Minor Runes", "Health Bonus Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( DeepSeaScroll ), "Deep Sea Scroll", 2 );

			index = AddCraft( typeof( BonusIntRune ), "Minor Runes", "Intelligence Bonus Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( FreshBrain ), "Fresh Brain", 2 );

			index = AddCraft( typeof( BonusManaRune ), "Minor Runes", "Mana Bonus Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( FreshBrain ), "Fresh Brain", 2 );

			index = AddCraft( typeof( BonusStamRune ), "Minor Runes", "Stamina Bonus Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( PerfectSkull ), "Perfect Skull", 2 );

			index = AddCraft( typeof( BonusStrRune ), "Minor Runes", "Strength Bonus Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( DeepSeaScroll ), "Deep Sea Scroll", 2 );

			index = AddCraft( typeof( CastRecoveryRune ), "Minor Runes", "Faster Cast Recovery Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( SpicedRum ), "Spiced Rum", 2 );

			index = AddCraft( typeof( CastSpeedRune ), "Minor Runes", "Faster Casting Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( SpicedRum ), "Spiced Rum", 2 );

			index = AddCraft( typeof( ColdBonusRune ), "Minor Runes", "Cold Resist Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( Acorn ), "Acorn", 2 );

			index = AddCraft( typeof( DefendChanceRune ), "Minor Runes", "Defense Chance Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( CureAllTonic ), "Cure All Tonic", 2 );

			index = AddCraft( typeof( DurabilityBonusRune ), "Minor Runes", "Durability Bonus Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( BrokenLockpicks ), "Broken Lockpicks", 2 );

			index = AddCraft( typeof( EnergyBonusRune ), "Minor Runes", "Energy Resist Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( Acorn ), "Acorn", 2 );

			index = AddCraft( typeof( EnhancePotionsRune ), "Minor Runes", "Enhanced Potions Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( CureAllTonic ), "Cure All Tonic", 2 );

			index = AddCraft( typeof( FireBonusRune ), "Minor Runes", "Fire Resist Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( Acorn ), "Acorn", 2 );

			index = AddCraft( typeof( HitColdAreaRune ), "Minor Runes", "Hit Cold Area Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( Acorn ), "Acorn", 2 );

			index = AddCraft( typeof( HitCurseRune ), "Minor Runes", "Hit Curse Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( DustBunny ), "Dust Bunny", 2 );

			index = AddCraft( typeof( HitDispelRune ), "Minor Runes", "Hit Dispel Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( DustBunny ), "Dust Bunny", 2 );

			index = AddCraft( typeof( HitEnergyAreaRune ), "Minor Runes", "Hit Energy Area Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( Acorn ), "Acorn", 2 );

			index = AddCraft( typeof( HitFatigueRune ), "Minor Runes", "Hit Fatigue Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( DustBunny ), "Dust Bunny", 2 );

			index = AddCraft( typeof( HitFireAreaRune ), "Minor Runes", "Hit Fire Area Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( Acorn ), "Acorn", 2 );

			index = AddCraft( typeof( HitFireballRune ), "Minor Runes", "Hit Fireball Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( DustBunny ), "Dust Bunny", 2 );

			index = AddCraft( typeof( HitHarmRune ), "Minor Runes", "Hit Harm Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( DustBunny ), "Dust Bunny", 2 );

			index = AddCraft( typeof( HitLeechHitsRune ), "Minor Runes", "Hit Life Leech Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( DustBunny ), "Dust Bunny", 2 );

			index = AddCraft( typeof( HitLeechManaRune ), "Minor Runes", "Hit Mana Leech Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( DustBunny ), "Dust Bunny", 2 );

			index = AddCraft( typeof( HitLeechStamRune ), "Minor Runes", "Hit Stamina Leech Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( DustBunny ), "Dust Bunny", 2 );

			index = AddCraft( typeof( HitLightningRune ), "Minor Runes", "Hit Lightning Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( DustBunny ), "Dust Bunny", 2 );

			index = AddCraft( typeof( HitLowerAttackRune ), "Minor Runes", "Hit Lower Attack Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( DustBunny ), "Dust Bunny", 2 );

			index = AddCraft( typeof( HitLowerDefendRune ), "Minor Runes", "Hit Lower Defense Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( DustBunny ), "Dust Bunny", 2 );

			index = AddCraft( typeof( HitMagicArrowRune ), "Minor Runes", "Hit Magic Arrow Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( DustBunny ), "Dust Bunny", 2 );

			index = AddCraft( typeof( HitManaDrainRune ), "Minor Runes", "Hit Mana Drain Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( DustBunny ), "Dust Bunny", 2 );

			index = AddCraft( typeof( HitPhysicalAreaRune ), "Minor Runes", "Hit Physical Area Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( Acorn ), "Acorn", 2 );

			index = AddCraft( typeof( HitPoisonAreaRune ), "Minor Runes", "Hit Poison Area Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( Acorn ), "Acorn", 2 );

			index = AddCraft( typeof( UseBestSkillRune ), "Minor Runes", "Use Best Weapon Skill Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( DemonHand ), "Demon Hand", 2 );

			index = AddCraft( typeof( LowerStatReqRune ), "Minor Runes", "Lower Requirements Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( GamonHorn ), "Gamon Horn", 2 );

			index = AddCraft( typeof( MageArmorRune ), "Minor Runes", "Mage Armor Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( GamonHorn ), "Gamon Horn", 2 );

			index = AddCraft( typeof( PhysicalBonusRune ), "Minor Runes", "Physical Resist Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( Acorn ), "Acorn", 2 );

			index = AddCraft( typeof( PoisonBonusRune ), "Minor Runes", "Poison Resist Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( Acorn ), "Acorn", 2 );

			index = AddCraft( typeof( SelfRepairRune ), "Minor Runes", "Self Repair Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( BleachedRemains ), "Bleached Remains", 2 );

			index = AddCraft( typeof( LowerManaCostRune ), "Minor Runes", "Lower Mana Cost Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( BleachedRemains ), "Bleached Remains", 2 );

			index = AddCraft( typeof( LowerRegCostRune ), "Minor Runes", "Lower Reagent Cost Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( BleachedRemains ), "Bleached Remains", 2 );

			index = AddCraft( typeof( LuckRune ), "Minor Runes", "Luck Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( BleachedRemains ), "Bleached Remains", 2 );

			index = AddCraft( typeof( NightSightRune ), "Minor Runes", "Night Sight Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( BleachedRemains ), "Bleached Remains", 2 );

			index = AddCraft( typeof( ReflectPhysicalRune ), "Minor Runes", "Reflect Physical Damage Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( BrokenHeart ), "Broken Heart", 2 );

			index = AddCraft( typeof( RegenHitsRune ), "Minor Runes", "Hit Point Regeneration Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( DeepSeaScroll ), "Deep Sea Scroll", 2 );

			index = AddCraft( typeof( RegenManaRune ), "Minor Runes", "Mana Regeneration Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( FreshBrain ), "Fresh Brain", 2 );

			index = AddCraft( typeof( RegenStamRune ), "Minor Runes", "Stamina Regeneration Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( PerfectSkull ), "Perfect Skull", 2 );

			index = AddCraft( typeof( SlayerRune ), "Minor Runes", "Random Slayer Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRecipe( index, (int)RunescribingRecipe.SlayerRune );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( WrappedBody ), "Wrapped Body", 2 );

			index = AddCraft( typeof( SpellChannelingRune ), "Minor Runes", "Spell Channeling Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( BleachedRemains ), "Bleached Remains", 2 );

			index = AddCraft( typeof( SpellDamageRune ), "Minor Runes", "Spell Damage Increase Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( DemonHand ), "Demon Hand", 2 );

			index = AddCraft( typeof( WeaponDamageRune ), "Minor Runes", "Weapon Damage Increase Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( DemonHand ), "Demon Hand", 2 );

			index = AddCraft( typeof( WeaponSpeedRune ), "Minor Runes", "Swing Speed Increase Rune", 50.0, 80.0, typeof( BlankScribingRune ), "Blank Scribing Rune", 1 );
			AddRes( index, typeof( MagicalDust ), "Magical Dust", 25 );
			AddRes( index, typeof( MysticEssence ), "Mystic Essence", 5 );
			AddRes( index, typeof( DemonHand ), "Demon Hand", 2 );


			// Major Runes
			index = AddCraft( typeof( RecallRune ), "Major Runes", "Recall Rune", 80.0, 120.0, typeof( RefinedStoneBrick ), "Refined Stone Brick", 1 );


			// Supreme Runes
			index = AddCraft( typeof( SlayerOfOneRune ), "Supreme Runes", "Slayer Of One Rune", 100.0, 130.0, typeof( SlayerRune ), "Slayer Rune", 1 );
			AddRecipe( index, (int)RunescribingRecipe.SlayerOfOneRune );
			AddRes( index, typeof( CosmicCrystal ), "Cosmic Crystal", 5 );
			AddRes( index, typeof( MagicalRelic ), "Magical Relic", 10 );
			AddRes( index, typeof( PerfectSaltWaterPearl ), "Perfect Salt Water Pearl", 20 );

			//AddRecipe( index, (int)RunescribingingRecipe.RecallRune );
			//AddSkill( index, SkillName.Inscribe, 80.0, 100.0 );
			//AddRes( index, typeof( DestroyingAngel ), "Destroying Angel", 25 );
			//AddRes( index, typeof( Bloodmoss ), "Blood Moss", 15 );
			//SetUseAllRes( index, true );

			MarkOption = true;
			CanEnhance = true;

		}
	}

	public class SoulForgeAttribute : Attribute
	{
		public SoulForgeAttribute()
		{
		}
	}
}