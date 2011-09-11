using System;
using Server.Mobiles;
using Server.Targeting;
using System.Collections;
using System.Collections.Generic;

namespace Server.Items
{
	public class Runescribing
	{
		// Blacklisted items
		public static Type[] Blacklist = new Type[]
		{
			typeof( Dagger ),
		};

		public static void Enhance( Mobile from, BaseTool tool )
		{
			if ( from.Skills.Imbuing.Value < 80 )
			{
				from.SendMessage( "You lack the skill to enhance anything." );
			}
			else
			{
				from.Target = new EnhanceTarget( tool );
				from.SendMessage( "Select the item you wish to enhance." );
			}
		}

		private class EnhanceTarget : Target
		{
			private BaseTool m_Tool;

			public EnhanceTarget( BaseTool tool ) : base( -1, false, TargetFlags.None )
			{
				m_Tool = tool;
			}

			protected override void OnTarget( Mobile from, object target )
			{
				if ( target is BaseArmor || target is BaseWeapon || target is BaseClothing || target is BaseJewel || target is BaseTalisman || target is BaseQuiver || target is Spellbook )
				{
				}
				else if (  target is BaseMinorRune )
				{
				}
				else
				{
					from.SendMessage( "This item cannot be enhanced." );
				}
			}
		}

		public static bool CheckBlacklist( Type type )
		{
			foreach ( Type check in Blacklist )
			{
  				if ( check == type )
    					return true;
			}

			return false;
		}

		public static Item GetResoureDrop( bool isTmap, bool isGrave )
		{
			int amount = Utility.RandomMinMax( 2, 8 );
			int roll = Utility.Random( 4 );

			if ( isTmap == true )
			{
				if ( roll == 1 )
					return new AncientSkeletonKey( amount );

				if ( roll == 2 )
					return new BrokenLockpicks( amount );

				if ( roll == 3 )
					return new CureAllTonic( amount );

				if ( roll == 4 )
					return new DustBunny( amount );

				return new DustBunny( amount );
			}
			else if ( isGrave == true )
			{
				if ( Utility.Random( 100 ) < 3 )
				{
					return new WrappedBody();
				}
				else
				{
					if ( roll == 1 )
						return new BoneDust( amount );

					if ( roll == 2 )
						return new BrokenHeart( amount );

					if ( roll == 3 )
						return new FreshBrain( amount );

					if ( roll == 4 )
						return new PerfectSkull( amount );

					return new PerfectSkull( amount );
				}
			}
			else
			{
				return new MagicalDust( amount );
			}
		}

		public static int GetProps( Item sent )
		{
			int value = 0;

			if ( sent is BaseArmor )
			{
				BaseArmor item = sent as BaseArmor;

				foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
				{
					if ( item != null && item.Attributes[ (AosAttribute)i ] > 0 )
						value += 1;
				}

				foreach( int i in Enum.GetValues(typeof( AosArmorAttribute ) ) )
				{
					if ( item.ArmorAttributes[ (AosArmorAttribute)i ] > 0 )
						value += 1;
				}

				if ( item.SkillBonuses.Skill_1_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_2_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_3_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_4_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_5_Value > 0 )
				{
					value += 1;
				}

				if ( item.ColdBonus > 0 )
				{
					value += 1;
				}

				if ( item.EnergyBonus > 0 )
				{
					value += 1;
				}

				if ( item.FireBonus > 0 )
				{
					value += 1;
				}

				if ( item.PhysicalBonus > 0 )
				{
					value += 1;
				}

				if ( item.PoisonBonus > 0 )
				{
					value += 1;
				}
			}

			if ( sent is BaseWeapon )
			{
				BaseWeapon item = sent as BaseWeapon;

				foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
				{
					if ( item != null && item.Attributes[ (AosAttribute)i ] > 0 )
						value += 1;
				}

				foreach( int i in Enum.GetValues(typeof( AosWeaponAttribute ) ) )
				{
					if ( item.WeaponAttributes[ (AosWeaponAttribute)i ] > 0 )
						value += 1;
				}

				if ( item.SkillBonuses.Skill_1_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_2_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_3_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_4_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_5_Value > 0 )
				{
					value += 1;
				}

				if ( item.Slayer != SlayerName.None )
					value += 1;

				if ( item.Slayer2 != SlayerName.None )
					value += 1;

			}

			if ( sent is BaseClothing )
			{
				BaseClothing item = sent as BaseClothing;

				foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
				{
					if ( item != null && item.Attributes[ (AosAttribute)i ] > 0 )
						value += 1;
				}

				foreach( int i in Enum.GetValues(typeof( AosArmorAttribute ) ) )
				{
					if ( item.ClothingAttributes[ (AosArmorAttribute)i ] > 0 )
						value += 1;
				}

				if ( item.SkillBonuses.Skill_1_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_2_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_3_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_4_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_5_Value > 0 )
				{
					value += 1;
				}

				if ( item.Resistances.Chaos > 0 )
					value += 1;

				if ( item.Resistances.Cold > 0 )
					value += 1;

				if ( item.Resistances.Direct > 0 )
					value += 1;

				if ( item.Resistances.Energy > 0 )
					value += 1;

				if ( item.Resistances.Fire > 0 )
					value += 1;

				if ( item.Resistances.Physical > 0 )
					value += 1;

				if ( item.Resistances.Poison > 0 )
					value += 1;
			}

			if ( sent is BaseJewel )
			{
				BaseJewel item = sent as BaseJewel;

				foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
				{
					if ( item != null && item.Attributes[ (AosAttribute)i ] > 0 )
						value += 1;
				}

				if ( item.SkillBonuses.Skill_1_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_2_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_3_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_4_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_5_Value > 0 )
				{
					value += 1;
				}

				if ( item.Resistances.Chaos > 0 )
					value += 1;

				if ( item.Resistances.Cold > 0 )
					value += 1;

				if ( item.Resistances.Direct > 0 )
					value += 1;

				if ( item.Resistances.Energy > 0 )
					value += 1;

				if ( item.Resistances.Fire > 0 )
					value += 1;

				if ( item.Resistances.Physical > 0 )
					value += 1;

				if ( item.Resistances.Poison > 0 )
					value += 1;
			}

			if ( sent is BaseQuiver )
			{
				BaseQuiver item = sent as BaseQuiver;

				foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
				{
					if ( item != null && item.Attributes[ (AosAttribute)i ] > 0 )
						value += 1;
				}

				if ( item.DamageIncrease > 0 )
					value += 1;

				if ( item.LowerAmmoCost > 0 )
					value += 1;
			}

			if ( sent is BaseTalisman )
			{
				BaseTalisman item = sent as BaseTalisman;

				foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
				{
					if ( item != null && item.Attributes[ (AosAttribute)i ] > 0 )
						value += 1;
				}

				if ( item.SkillBonuses.Skill_1_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_2_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_3_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_4_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_5_Value > 0 )
				{
					value += 1;
				}
			}

			if ( sent is Spellbook )
			{
				Spellbook item = sent as Spellbook;

				foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
				{
					if ( item != null && item.Attributes[ (AosAttribute)i ] > 0 )
						value += 1;
				}

				if ( item.SkillBonuses.Skill_1_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_2_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_3_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_4_Value > 0 )
				{
					value += 1;
				}

				if ( item.SkillBonuses.Skill_5_Value > 0 )
				{
					value += 1;
				}

				if ( item.Slayer != SlayerName.None )
					value += 1;

				if ( item.Slayer2 != SlayerName.None )
					value += 1;
			}

			return value;
		}
	}
}