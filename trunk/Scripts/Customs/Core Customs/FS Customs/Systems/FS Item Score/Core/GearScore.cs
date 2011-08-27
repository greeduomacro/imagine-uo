using System;
using Server.Mobiles;
using Server.Targeting;
using System.Collections;
using System.Collections.Generic;

namespace Server.Items
{
	public class GearScore
	{
		public static ItemValue GetItemValue( Item item )
		{
			if ( item.OverrideItemValue == true )
				return item.ItemValue;

			if ( item is BaseDecorationArtifact )
			{
				BaseDecorationArtifact art = item as BaseDecorationArtifact;

				if ( art.ArtifactRarity == 0 )
					return ItemValue.Common;

				if ( art.ArtifactRarity <= 3 )
					return ItemValue.Uncommon;

				if ( art.ArtifactRarity <= 7 )
					return ItemValue.Rare;

				if (art.ArtifactRarity <= 12 )
					return ItemValue.Epic;

				return ItemValue.Legendary;
			}

			if ( item is BaseDecorationContainerArtifact )
			{
				BaseDecorationContainerArtifact artCont = item as BaseDecorationContainerArtifact;

				if ( artCont.ArtifactRarity == 0 )
					return ItemValue.Common;

				if ( artCont.ArtifactRarity <= 3 )
					return ItemValue.Uncommon;

				if ( artCont.ArtifactRarity <= 7 )
					return ItemValue.Rare;

				if ( artCont.ArtifactRarity <= 12 )
					return ItemValue.Epic;

				return ItemValue.Legendary;
			}

			if ( item is BaseArmor )
			{
				BaseArmor ba = item as BaseArmor;

				if ( ba.ArtifactRarity == 0 )
					return CheckArmor( ba );;

				if ( ba.ArtifactRarity <= 10 )
					return ItemValue.Uncommon;

				if ( ba.ArtifactRarity <= 30 )
					return ItemValue.Rare;

				if ( ba.ArtifactRarity <= 99 )
					return ItemValue.Epic;

				return ItemValue.Legendary;
			}

			return ItemValue.Trash;
		}

		public static ItemValue CheckArmor( BaseArmor armor )
		{
			return ItemValue.Trash;
		}

		public static int GetArmorValue( BaseArmor armor )
		{
			// Each stat above 0 equals a 2 point gearscore value
			// Each stat gives its own gearscore value as well.

			int value = 0;

			if ( armor.ArtifactRarity > 0 )
				value = 150;

			foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
			{
				if ( armor != null && armor.Attributes[ (AosAttribute)i ] > 0 )
					value += 2;
			}

			foreach( int i in Enum.GetValues(typeof( AosArmorAttribute ) ) )
			{
				if ( armor.ArmorAttributes[ (AosArmorAttribute)i ] > 0 )
					value += 2;
			}

			//Start skill bonus

			if ( armor.SkillBonuses.Skill_1_Value > 0 )
			{
				value += (int)armor.SkillBonuses.Skill_1_Value * 4;
				value += 2;
			}

			if ( armor.SkillBonuses.Skill_2_Value > 0 )
			{
				value += (int)armor.SkillBonuses.Skill_2_Value * 4;
				value += 2;
			}

			if ( armor.SkillBonuses.Skill_3_Value > 0 )
			{
				value += (int)armor.SkillBonuses.Skill_3_Value * 4;
				value += 2;
			}

			if ( armor.SkillBonuses.Skill_4_Value > 0 )
			{
				value += (int)armor.SkillBonuses.Skill_4_Value * 4;
				value += 2;
			}

			if ( armor.SkillBonuses.Skill_5_Value > 0 )
			{
				value += (int)armor.SkillBonuses.Skill_5_Value * 4;
				value += 2;
			}

			//Start armor attributes

			if ( armor.ArmorAttributes.DurabilityBonus > 0 )
				value += armor.ArmorAttributes.DurabilityBonus / 4;

			if ( armor.ArmorAttributes.LowerStatReq > 0 )
				value += armor.ArmorAttributes.LowerStatReq / 4;

			if ( armor.ArmorAttributes.MageArmor > 0 )
				value += 10;

			if ( armor.ArmorAttributes.SelfRepair > 0 )
				value += armor.ArmorAttributes.SelfRepair * 2;

			//Start standard attributes

			if ( armor.Attributes.AttackChance > 0 )
				value += armor.Attributes.AttackChance * 2;

			if ( armor.Attributes.BonusDex > 0 )
				value += armor.Attributes.BonusDex * 4;

			if ( armor.Attributes.BonusHits > 0 )
				value += armor.Attributes.BonusHits * 2;

			if ( armor.Attributes.BonusInt > 0 )
				value += armor.Attributes.BonusInt * 4;

			if ( armor.Attributes.BonusMana > 0 )
				value += armor.Attributes.BonusMana * 2;

			if ( armor.Attributes.BonusStam > 0 )
				value += armor.Attributes.BonusStam * 2;

			if ( armor.Attributes.BonusStr > 0 )
				value += armor.Attributes.BonusStr * 4;

			if ( armor.Attributes.CastRecovery > 0 )
				value += armor.Attributes.CastRecovery * 10;

			if ( armor.Attributes.CastSpeed > 0 )
				value += armor.Attributes.CastSpeed * 10;

			if ( armor.Attributes.DefendChance > 0 )
				value += armor.Attributes.DefendChance * 2;

			if ( armor.Attributes.EnhancePotions > 0 )
				value += armor.Attributes.EnhancePotions;

			if ( armor.Attributes.LowerManaCost > 0 )
				value += armor.Attributes.LowerManaCost * 2;

			if ( armor.Attributes.LowerRegCost > 0 )
				value += armor.Attributes.LowerRegCost;

			if ( armor.Attributes.Luck > 0 )
				value += armor.Attributes.Luck / 2;

			if ( armor.Attributes.NightSight > 0 )
				value += 10;

			if ( armor.Attributes.ReflectPhysical > 0 )
				value += armor.Attributes.ReflectPhysical * 2;

			if ( armor.Attributes.RegenHits > 0 )
				value += armor.Attributes.RegenHits * 10;

			if ( armor.Attributes.RegenMana > 0 )
				value += armor.Attributes.RegenMana * 10;

			if ( armor.Attributes.RegenStam > 0 )
				value += armor.Attributes.RegenStam * 10;

			if ( armor.Attributes.SpellChanneling > 0 )
				value += 10;

			if ( armor.Attributes.SpellDamage > 0 )
				value += armor.Attributes.SpellDamage * 2;

			if ( armor.Attributes.WeaponDamage > 0 )
				value += armor.Attributes.WeaponDamage * 2;

			if ( armor.Attributes.WeaponSpeed > 0 )
				value += armor.Attributes.WeaponSpeed * 2;

			//Start Resist Bonus

			if ( armor.ColdBonus > 0 )
			{
				value += armor.ColdBonus * 2;
				value += 2;
			}

			if ( armor.EnergyBonus > 0 )
			{
				value += armor.EnergyBonus * 2;
				value += 2;
			}

			if ( armor.FireBonus > 0 )
			{
				value += armor.FireBonus * 2;
				value += 2;
			}

			if ( armor.PhysicalBonus > 0 )
			{
				value += armor.PhysicalBonus * 2;
				value += 2;
			}

			if ( armor.PoisonBonus > 0 )
			{
				value += armor.PoisonBonus * 2;
				value += 2;
			}

			return value;
		}
	}
}