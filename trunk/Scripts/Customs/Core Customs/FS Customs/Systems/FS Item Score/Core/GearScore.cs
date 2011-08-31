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

			if ( item is BaseWeapon )
			{
				BaseWeapon bw = item as BaseWeapon;

				if ( bw.ArtifactRarity == 0 )
					return CheckWeapon( bw );;

				if ( bw.ArtifactRarity <= 10 )
					return ItemValue.Uncommon;

				if ( bw.ArtifactRarity <= 30 )
					return ItemValue.Rare;

				if ( bw.ArtifactRarity <= 99 )
					return ItemValue.Epic;

				return ItemValue.Legendary;
			}

			if ( item is BaseClothing )
			{
				BaseClothing bc = item as BaseClothing;

				if ( bc.ArtifactRarity == 0 )
					return CheckClothing( bc );;

				if ( bc.ArtifactRarity <= 10 )
					return ItemValue.Uncommon;

				if ( bc.ArtifactRarity <= 30 )
					return ItemValue.Rare;

				if ( bc.ArtifactRarity <= 99 )
					return ItemValue.Epic;

				return ItemValue.Legendary;
			}

			if ( item is BaseJewel )
			{
				BaseJewel bj = item as BaseJewel;

				if ( bj.ArtifactRarity == 0 )
					return CheckJewel( bj );;

				if ( bj.ArtifactRarity <= 10 )
					return ItemValue.Uncommon;

				if ( bj.ArtifactRarity <= 30 )
					return ItemValue.Rare;

				if ( bj.ArtifactRarity <= 99 )
					return ItemValue.Epic;

				return ItemValue.Legendary;
			}

			if ( item is BaseQuiver )
			{
				BaseQuiver bq = item as BaseQuiver;

				return CheckQuiver( bq );
			}

			if ( item is BaseTalisman )
			{
				BaseTalisman bt = item as BaseTalisman;

				return CheckTalisman( bt );
			}

			return ItemValue.Trash;
		}

		public static ItemValue CheckArmor( BaseArmor item )
		{
			int value = 0;

			foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
			{
				if ( item != null && item.Attributes[ (AosAttribute)i ] > 0 )
					value += 2;
			}

			foreach( int i in Enum.GetValues(typeof( AosArmorAttribute ) ) )
			{
				if ( item.ArmorAttributes[ (AosArmorAttribute)i ] > 0 )
					value += 2;
			}

			//Start skill bonus

			if ( item.SkillBonuses.Skill_1_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_1_Value * 4;
				value += 2;
			}

			if ( item.SkillBonuses.Skill_2_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_2_Value * 4;
				value += 2;
			}

			if ( item.SkillBonuses.Skill_3_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_3_Value * 4;
				value += 2;
			}

			if ( item.SkillBonuses.Skill_4_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_4_Value * 4;
				value += 2;
			}

			if ( item.SkillBonuses.Skill_5_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_5_Value * 4;
				value += 2;
			}

			//Start armor attributes

			if ( item.ArmorAttributes.DurabilityBonus > 0 )
				value += item.ArmorAttributes.DurabilityBonus / 4;

			if ( item.ArmorAttributes.LowerStatReq > 0 )
				value += item.ArmorAttributes.LowerStatReq / 4;

			if ( item.ArmorAttributes.MageArmor > 0 )
				value += 10;

			if ( item.ArmorAttributes.SelfRepair > 0 )
				value += item.ArmorAttributes.SelfRepair * 2;

			//Start standard attributes

			if ( item.Attributes.AttackChance > 0 )
				value += item.Attributes.AttackChance * 2;

			if ( item.Attributes.BonusDex > 0 )
				value += item.Attributes.BonusDex * 4;

			if ( item.Attributes.BonusHits > 0 )
				value += item.Attributes.BonusHits * 2;

			if ( item.Attributes.BonusInt > 0 )
				value += item.Attributes.BonusInt * 4;

			if ( item.Attributes.BonusMana > 0 )
				value += item.Attributes.BonusMana * 2;

			if ( item.Attributes.BonusStam > 0 )
				value += item.Attributes.BonusStam * 2;

			if ( item.Attributes.BonusStr > 0 )
				value += item.Attributes.BonusStr * 4;

			if ( item.Attributes.CastRecovery > 0 )
				value += item.Attributes.CastRecovery * 10;

			if ( item.Attributes.CastSpeed > 0 )
				value += item.Attributes.CastSpeed * 10;

			if ( item.Attributes.DefendChance > 0 )
				value += item.Attributes.DefendChance * 2;

			if ( item.Attributes.EnhancePotions > 0 )
				value += item.Attributes.EnhancePotions;

			if ( item.Attributes.LowerManaCost > 0 )
				value += item.Attributes.LowerManaCost * 2;

			if ( item.Attributes.LowerRegCost > 0 )
				value += item.Attributes.LowerRegCost;

			if ( item.Attributes.Luck > 0 )
				value += item.Attributes.Luck / 2;

			if ( item.Attributes.NightSight > 0 )
				value += 10;

			if ( item.Attributes.ReflectPhysical > 0 )
				value += item.Attributes.ReflectPhysical * 2;

			if ( item.Attributes.RegenHits > 0 )
				value += item.Attributes.RegenHits * 10;

			if ( item.Attributes.RegenMana > 0 )
				value += item.Attributes.RegenMana * 10;

			if ( item.Attributes.RegenStam > 0 )
				value += item.Attributes.RegenStam * 10;

			if ( item.Attributes.SpellChanneling > 0 )
				value += 10;

			if ( item.Attributes.SpellDamage > 0 )
				value += item.Attributes.SpellDamage * 2;

			if ( item.Attributes.WeaponDamage > 0 )
				value += item.Attributes.WeaponDamage * 2;

			if ( item.Attributes.WeaponSpeed > 0 )
				value += item.Attributes.WeaponSpeed * 2;

			//Start Absorption Attributes

			if ( item.AbsorptionAttributes.CastingFocus > 0 )
				value += item.AbsorptionAttributes.CastingFocus;

			if ( item.AbsorptionAttributes.EaterCold > 0 )
				value += item.AbsorptionAttributes.EaterCold;

			if ( item.AbsorptionAttributes.EaterDamage > 0 )
				value += item.AbsorptionAttributes.EaterDamage;

			if ( item.AbsorptionAttributes.EaterEnergy > 0 )
				value += item.AbsorptionAttributes.EaterEnergy;

			if ( item.AbsorptionAttributes.EaterFire > 0 )
				value += item.AbsorptionAttributes.EaterFire;

			if ( item.AbsorptionAttributes.EaterKinetic > 0 )
				value += item.AbsorptionAttributes.EaterKinetic;

			if ( item.AbsorptionAttributes.EaterPoison > 0 )
				value += item.AbsorptionAttributes.EaterPoison;

			if ( item.AbsorptionAttributes.ResonanceCold > 0 )
				value += item.AbsorptionAttributes.ResonanceCold;

			if ( item.AbsorptionAttributes.ResonanceEnergy > 0 )
				value += item.AbsorptionAttributes.ResonanceEnergy;

			if ( item.AbsorptionAttributes.ResonanceFire > 0 )
				value += item.AbsorptionAttributes.ResonanceFire;

			if ( item.AbsorptionAttributes.ResonanceKinetic > 0 )
				value += item.AbsorptionAttributes.ResonanceKinetic;

			if ( item.AbsorptionAttributes.ResonancePoison > 0 )
				value += item.AbsorptionAttributes.ResonancePoison;

			if ( item.AbsorptionAttributes.SoulChargeCold > 0 )
				value += item.AbsorptionAttributes.SoulChargeCold;

			if ( item.AbsorptionAttributes.SoulChargeEnergy > 0 )
				value += item.AbsorptionAttributes.SoulChargeEnergy;

			if ( item.AbsorptionAttributes.SoulChargeFire > 0 )
				value += item.AbsorptionAttributes.SoulChargeFire;

			if ( item.AbsorptionAttributes.SoulChargeKinetic > 0 )
				value += item.AbsorptionAttributes.SoulChargeKinetic;

			if ( item.AbsorptionAttributes.SoulChargePoison > 0 )
				value += item.AbsorptionAttributes.SoulChargePoison;

			//Start Resist Bonus

			if ( item.ColdBonus > 0 )
			{
				value += item.ColdBonus * 2;
				value += 2;
			}

			if ( item.EnergyBonus > 0 )
			{
				value += item.EnergyBonus * 2;
				value += 2;
			}

			if ( item.FireBonus > 0 )
			{
				value += item.FireBonus * 2;
				value += 2;
			}

			if ( item.PhysicalBonus > 0 )
			{
				value += item.PhysicalBonus * 2;
				value += 2;
			}

			if ( item.PoisonBonus > 0 )
			{
				value += item.PoisonBonus * 2;
				value += 2;
			}

			if ( value == 0 )
				return ItemValue.Trash;

			if ( value <= 50 )
				return ItemValue.Common;

			if ( value <= 100 )
				return ItemValue.Uncommon;

			if ( value <= 200 )
				return ItemValue.Rare;

			if ( value <= 300 )
				return ItemValue.Epic;

			return ItemValue.Legendary;
		}

		public static ItemValue CheckWeapon( BaseWeapon item )
		{
			int value = 0;

			foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
			{
				if ( item != null && item.Attributes[ (AosAttribute)i ] > 0 )
					value += 2;
			}

			foreach( int i in Enum.GetValues(typeof( AosWeaponAttribute ) ) )
			{
				if ( item.WeaponAttributes[ (AosWeaponAttribute)i ] > 0 )
					value += 2;
			}

			//Start skill bonus

			if ( item.SkillBonuses.Skill_1_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_1_Value * 4;
				value += 2;
			}

			if ( item.SkillBonuses.Skill_2_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_2_Value * 4;
				value += 2;
			}

			if ( item.SkillBonuses.Skill_3_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_3_Value * 4;
				value += 2;
			}

			if ( item.SkillBonuses.Skill_4_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_4_Value * 4;
				value += 2;
			}

			if ( item.SkillBonuses.Skill_5_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_5_Value * 4;
				value += 2;
			}

			//Start Slayers

			if ( item.Slayer != SlayerName.None )
				value += 20;

			if ( item.Slayer2 != SlayerName.None )
				value += 20;

			if ( item.Slayer3 != TalismanSlayerName.None )
				value += 20;

			//Start weapon attributes

			if ( item.WeaponAttributes.BattleLust > 0 )
				value += 20;

			if ( item.WeaponAttributes.BloodDrinker > 0 )
				value += 20;

			if ( item.WeaponAttributes.DurabilityBonus > 0 )
				value += item.WeaponAttributes.DurabilityBonus / 4;

			if ( item.WeaponAttributes.HitColdArea > 0 )
				value += item.WeaponAttributes.HitColdArea / 2;

			if ( item.WeaponAttributes.HitCurse > 0 )
				value += item.WeaponAttributes.HitCurse / 2;

			if ( item.WeaponAttributes.HitDispel > 0 )
				value += item.WeaponAttributes.HitDispel / 2;

			if ( item.WeaponAttributes.HitEnergyArea > 0 )
				value += item.WeaponAttributes.HitEnergyArea / 2;

			if ( item.WeaponAttributes.HitFatigue > 0 )
				value += item.WeaponAttributes.HitFatigue / 2;

			if ( item.WeaponAttributes.HitFireArea> 0 )
				value += item.WeaponAttributes.HitFireArea / 2;

			if ( item.WeaponAttributes.HitFireball > 0 )
				value += item.WeaponAttributes.HitFireball / 2;

			if ( item.WeaponAttributes.HitHarm > 0 )
				value += item.WeaponAttributes.HitHarm / 2;

			if ( item.WeaponAttributes.HitLeechHits > 0 )
				value += item.WeaponAttributes.HitLeechHits / 2;

			if ( item.WeaponAttributes.HitLeechMana > 0 )
				value += item.WeaponAttributes.HitLeechMana / 2;

			if ( item.WeaponAttributes.HitLeechStam > 0 )
				value += item.WeaponAttributes.HitLeechStam / 2;

			if ( item.WeaponAttributes.HitLightning > 0 )
				value += item.WeaponAttributes.HitLightning / 2;

			if ( item.WeaponAttributes.HitLowerAttack > 0 )
				value += item.WeaponAttributes.HitLowerAttack / 2;

			if ( item.WeaponAttributes.HitLowerDefend > 0 )
				value += item.WeaponAttributes.HitLowerDefend / 2;

			if ( item.WeaponAttributes.HitMagicArrow > 0 )
				value += item.WeaponAttributes.HitMagicArrow / 2;

			if ( item.WeaponAttributes.HitManaDrain > 0 )
				value += item.WeaponAttributes.HitManaDrain / 2;

			if ( item.WeaponAttributes.HitPhysicalArea > 0 )
				value += item.WeaponAttributes.HitPhysicalArea / 2;

			if ( item.WeaponAttributes.HitPoisonArea > 0 )
				value += item.WeaponAttributes.HitPoisonArea / 2;

			if ( item.WeaponAttributes.LowerStatReq > 0 )
				value += item.WeaponAttributes.LowerStatReq / 2;

			if ( item.WeaponAttributes.MageWeapon > 0 )
				value += item.WeaponAttributes.MageWeapon;

			if ( item.WeaponAttributes.ResistColdBonus > 0 )
				value += item.WeaponAttributes.ResistColdBonus / 2;

			if ( item.WeaponAttributes.ResistEnergyBonus > 0 )
				value += item.WeaponAttributes.ResistEnergyBonus / 2;

			if ( item.WeaponAttributes.ResistFireBonus > 0 )
				value += item.WeaponAttributes.ResistFireBonus / 2;

			if ( item.WeaponAttributes.ResistPhysicalBonus > 0 )
				value += item.WeaponAttributes.ResistPhysicalBonus / 2;

			if ( item.WeaponAttributes.ResistPoisonBonus > 0 )
				value += item.WeaponAttributes.ResistPoisonBonus / 2;

			if ( item.WeaponAttributes.SelfRepair > 0 )
				value += item.WeaponAttributes.SelfRepair * 2;

			if ( item.WeaponAttributes.UseBestSkill > 0 )
				value += 10;

			//Start standard attributes

			if ( item.Attributes.AttackChance > 0 )
				value += item.Attributes.AttackChance * 2;

			if ( item.Attributes.BonusDex > 0 )
				value += item.Attributes.BonusDex * 4;

			if ( item.Attributes.BonusHits > 0 )
				value += item.Attributes.BonusHits * 2;

			if ( item.Attributes.BonusInt > 0 )
				value += item.Attributes.BonusInt * 4;

			if ( item.Attributes.BonusMana > 0 )
				value += item.Attributes.BonusMana * 2;

			if ( item.Attributes.BonusStam > 0 )
				value += item.Attributes.BonusStam * 2;

			if ( item.Attributes.BonusStr > 0 )
				value += item.Attributes.BonusStr * 4;

			if ( item.Attributes.CastRecovery > 0 )
				value += item.Attributes.CastRecovery * 10;

			if ( item.Attributes.CastSpeed > 0 )
				value += item.Attributes.CastSpeed * 10;

			if ( item.Attributes.DefendChance > 0 )
				value += item.Attributes.DefendChance * 2;

			if ( item.Attributes.EnhancePotions > 0 )
				value += item.Attributes.EnhancePotions;

			if ( item.Attributes.LowerManaCost > 0 )
				value += item.Attributes.LowerManaCost * 2;

			if ( item.Attributes.LowerRegCost > 0 )
				value += item.Attributes.LowerRegCost;

			if ( item.Attributes.Luck > 0 )
				value += item.Attributes.Luck / 2;

			if ( item.Attributes.NightSight > 0 )
				value += 10;

			if ( item.Attributes.ReflectPhysical > 0 )
				value += item.Attributes.ReflectPhysical * 2;

			if ( item.Attributes.RegenHits > 0 )
				value += item.Attributes.RegenHits * 10;

			if ( item.Attributes.RegenMana > 0 )
				value += item.Attributes.RegenMana * 10;

			if ( item.Attributes.RegenStam > 0 )
				value += item.Attributes.RegenStam * 10;

			if ( item.Attributes.SpellChanneling > 0 )
				value += 10;

			if ( item.Attributes.SpellDamage > 0 )
				value += item.Attributes.SpellDamage * 2;

			if ( item.Attributes.WeaponDamage > 0 )
				value += item.Attributes.WeaponDamage * 2;

			if ( item.Attributes.WeaponSpeed > 0 )
				value += item.Attributes.WeaponSpeed * 2;

			//Start Absorption Attributes

			if ( item.AbsorptionAttributes.CastingFocus > 0 )
				value += item.AbsorptionAttributes.CastingFocus;

			if ( item.AbsorptionAttributes.EaterCold > 0 )
				value += item.AbsorptionAttributes.EaterCold;

			if ( item.AbsorptionAttributes.EaterDamage > 0 )
				value += item.AbsorptionAttributes.EaterDamage;

			if ( item.AbsorptionAttributes.EaterEnergy > 0 )
				value += item.AbsorptionAttributes.EaterEnergy;

			if ( item.AbsorptionAttributes.EaterFire > 0 )
				value += item.AbsorptionAttributes.EaterFire;

			if ( item.AbsorptionAttributes.EaterKinetic > 0 )
				value += item.AbsorptionAttributes.EaterKinetic;

			if ( item.AbsorptionAttributes.EaterPoison > 0 )
				value += item.AbsorptionAttributes.EaterPoison;

			if ( item.AbsorptionAttributes.ResonanceCold > 0 )
				value += item.AbsorptionAttributes.ResonanceCold;

			if ( item.AbsorptionAttributes.ResonanceEnergy > 0 )
				value += item.AbsorptionAttributes.ResonanceEnergy;

			if ( item.AbsorptionAttributes.ResonanceFire > 0 )
				value += item.AbsorptionAttributes.ResonanceFire;

			if ( item.AbsorptionAttributes.ResonanceKinetic > 0 )
				value += item.AbsorptionAttributes.ResonanceKinetic;

			if ( item.AbsorptionAttributes.ResonancePoison > 0 )
				value += item.AbsorptionAttributes.ResonancePoison;

			if ( item.AbsorptionAttributes.SoulChargeCold > 0 )
				value += item.AbsorptionAttributes.SoulChargeCold;

			if ( item.AbsorptionAttributes.SoulChargeEnergy > 0 )
				value += item.AbsorptionAttributes.SoulChargeEnergy;

			if ( item.AbsorptionAttributes.SoulChargeFire > 0 )
				value += item.AbsorptionAttributes.SoulChargeFire;

			if ( item.AbsorptionAttributes.SoulChargeKinetic > 0 )
				value += item.AbsorptionAttributes.SoulChargeKinetic;

			if ( item.AbsorptionAttributes.SoulChargePoison > 0 )
				value += item.AbsorptionAttributes.SoulChargePoison;

			//Start Element Damage

			if ( item.AosElementDamages.Chaos > 0 )
				value += item.AosElementDamages.Chaos;

			if ( item.AosElementDamages.Cold > 0 )
				value += item.AosElementDamages.Cold;

			if ( item.AosElementDamages.Direct > 0 )
				value += item.AosElementDamages.Direct;

			if ( item.AosElementDamages.Energy > 0 )
				value += item.AosElementDamages.Energy;

			if ( item.AosElementDamages.Fire > 0 )
				value += item.AosElementDamages.Fire;

			if ( item.AosElementDamages.Physical > 0 )
				value += item.AosElementDamages.Physical;

			if ( item.AosElementDamages.Poison > 0 )
				value += item.AosElementDamages.Poison;

			//Start Calculate

			if ( value == 0 )
				return ItemValue.Trash;

			if ( value <= 50 )
				return ItemValue.Common;

			if ( value <= 100 )
				return ItemValue.Uncommon;

			if ( value <= 200 )
				return ItemValue.Rare;

			if ( value <= 300 )
				return ItemValue.Epic;

			return ItemValue.Legendary;
		}

		public static ItemValue CheckClothing( BaseClothing item )
		{
			int value = 0;

			foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
			{
				if ( item != null && item.Attributes[ (AosAttribute)i ] > 0 )
					value += 2;
			}

			//Start skill bonus

			if ( item.SkillBonuses.Skill_1_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_1_Value * 4;
				value += 2;
			}

			if ( item.SkillBonuses.Skill_2_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_2_Value * 4;
				value += 2;
			}

			if ( item.SkillBonuses.Skill_3_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_3_Value * 4;
				value += 2;
			}

			if ( item.SkillBonuses.Skill_4_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_4_Value * 4;
				value += 2;
			}

			if ( item.SkillBonuses.Skill_5_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_5_Value * 4;
				value += 2;
			}

			//Start standard attributes

			if ( item.Attributes.AttackChance > 0 )
				value += item.Attributes.AttackChance * 2;

			if ( item.Attributes.BonusDex > 0 )
				value += item.Attributes.BonusDex * 4;

			if ( item.Attributes.BonusHits > 0 )
				value += item.Attributes.BonusHits * 2;

			if ( item.Attributes.BonusInt > 0 )
				value += item.Attributes.BonusInt * 4;

			if ( item.Attributes.BonusMana > 0 )
				value += item.Attributes.BonusMana * 2;

			if ( item.Attributes.BonusStam > 0 )
				value += item.Attributes.BonusStam * 2;

			if ( item.Attributes.BonusStr > 0 )
				value += item.Attributes.BonusStr * 4;

			if ( item.Attributes.CastRecovery > 0 )
				value += item.Attributes.CastRecovery * 10;

			if ( item.Attributes.CastSpeed > 0 )
				value += item.Attributes.CastSpeed * 10;

			if ( item.Attributes.DefendChance > 0 )
				value += item.Attributes.DefendChance * 2;

			if ( item.Attributes.EnhancePotions > 0 )
				value += item.Attributes.EnhancePotions;

			if ( item.Attributes.LowerManaCost > 0 )
				value += item.Attributes.LowerManaCost * 2;

			if ( item.Attributes.LowerRegCost > 0 )
				value += item.Attributes.LowerRegCost;

			if ( item.Attributes.Luck > 0 )
				value += item.Attributes.Luck / 2;

			if ( item.Attributes.NightSight > 0 )
				value += 10;

			if ( item.Attributes.ReflectPhysical > 0 )
				value += item.Attributes.ReflectPhysical * 2;

			if ( item.Attributes.RegenHits > 0 )
				value += item.Attributes.RegenHits * 10;

			if ( item.Attributes.RegenMana > 0 )
				value += item.Attributes.RegenMana * 10;

			if ( item.Attributes.RegenStam > 0 )
				value += item.Attributes.RegenStam * 10;

			if ( item.Attributes.SpellChanneling > 0 )
				value += 10;

			if ( item.Attributes.SpellDamage > 0 )
				value += item.Attributes.SpellDamage * 2;

			if ( item.Attributes.WeaponDamage > 0 )
				value += item.Attributes.WeaponDamage * 2;

			if ( item.Attributes.WeaponSpeed > 0 )
				value += item.Attributes.WeaponSpeed * 2;

			//Start Element Resist

			if ( item.Resistances.Chaos > 0 )
				value += item.Resistances.Chaos;

			if ( item.Resistances.Cold > 0 )
				value += item.Resistances.Cold;

			if ( item.Resistances.Direct > 0 )
				value += item.Resistances.Direct;

			if ( item.Resistances.Energy > 0 )
				value += item.Resistances.Energy;

			if ( item.Resistances.Fire > 0 )
				value += item.Resistances.Fire;

			if ( item.Resistances.Physical > 0 )
				value += item.Resistances.Physical;

			if ( item.Resistances.Poison > 0 )
				value += item.Resistances.Poison;

			//Start Calculate

			if ( value == 0 )
				return ItemValue.Trash;

			if ( value <= 50 )
				return ItemValue.Common;

			if ( value <= 100 )
				return ItemValue.Uncommon;

			if ( value <= 200 )
				return ItemValue.Rare;

			if ( value <= 300 )
				return ItemValue.Epic;

			return ItemValue.Legendary;
		}

		public static ItemValue CheckJewel( BaseJewel item )
		{
			int value = 0;

			foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
			{
				if ( item != null && item.Attributes[ (AosAttribute)i ] > 0 )
					value += 2;
			}

			//Start skill bonus

			if ( item.SkillBonuses.Skill_1_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_1_Value * 4;
				value += 2;
			}

			if ( item.SkillBonuses.Skill_2_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_2_Value * 4;
				value += 2;
			}

			if ( item.SkillBonuses.Skill_3_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_3_Value * 4;
				value += 2;
			}

			if ( item.SkillBonuses.Skill_4_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_4_Value * 4;
				value += 2;
			}

			if ( item.SkillBonuses.Skill_5_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_5_Value * 4;
				value += 2;
			}

			//Start standard attributes

			if ( item.Attributes.AttackChance > 0 )
				value += item.Attributes.AttackChance * 2;

			if ( item.Attributes.BonusDex > 0 )
				value += item.Attributes.BonusDex * 4;

			if ( item.Attributes.BonusHits > 0 )
				value += item.Attributes.BonusHits * 2;

			if ( item.Attributes.BonusInt > 0 )
				value += item.Attributes.BonusInt * 4;

			if ( item.Attributes.BonusMana > 0 )
				value += item.Attributes.BonusMana * 2;

			if ( item.Attributes.BonusStam > 0 )
				value += item.Attributes.BonusStam * 2;

			if ( item.Attributes.BonusStr > 0 )
				value += item.Attributes.BonusStr * 4;

			if ( item.Attributes.CastRecovery > 0 )
				value += item.Attributes.CastRecovery * 10;

			if ( item.Attributes.CastSpeed > 0 )
				value += item.Attributes.CastSpeed * 10;

			if ( item.Attributes.DefendChance > 0 )
				value += item.Attributes.DefendChance * 2;

			if ( item.Attributes.EnhancePotions > 0 )
				value += item.Attributes.EnhancePotions;

			if ( item.Attributes.LowerManaCost > 0 )
				value += item.Attributes.LowerManaCost * 2;

			if ( item.Attributes.LowerRegCost > 0 )
				value += item.Attributes.LowerRegCost;

			if ( item.Attributes.Luck > 0 )
				value += item.Attributes.Luck / 2;

			if ( item.Attributes.NightSight > 0 )
				value += 10;

			if ( item.Attributes.ReflectPhysical > 0 )
				value += item.Attributes.ReflectPhysical * 2;

			if ( item.Attributes.RegenHits > 0 )
				value += item.Attributes.RegenHits * 10;

			if ( item.Attributes.RegenMana > 0 )
				value += item.Attributes.RegenMana * 10;

			if ( item.Attributes.RegenStam > 0 )
				value += item.Attributes.RegenStam * 10;

			if ( item.Attributes.SpellChanneling > 0 )
				value += 10;

			if ( item.Attributes.SpellDamage > 0 )
				value += item.Attributes.SpellDamage * 2;

			if ( item.Attributes.WeaponDamage > 0 )
				value += item.Attributes.WeaponDamage * 2;

			if ( item.Attributes.WeaponSpeed > 0 )
				value += item.Attributes.WeaponSpeed * 2;

			//Start Absorption Attributes

			if ( item.AbsorptionAttributes.CastingFocus > 0 )
				value += item.AbsorptionAttributes.CastingFocus;

			if ( item.AbsorptionAttributes.EaterCold > 0 )
				value += item.AbsorptionAttributes.EaterCold;

			if ( item.AbsorptionAttributes.EaterDamage > 0 )
				value += item.AbsorptionAttributes.EaterDamage;

			if ( item.AbsorptionAttributes.EaterEnergy > 0 )
				value += item.AbsorptionAttributes.EaterEnergy;

			if ( item.AbsorptionAttributes.EaterFire > 0 )
				value += item.AbsorptionAttributes.EaterFire;

			if ( item.AbsorptionAttributes.EaterKinetic > 0 )
				value += item.AbsorptionAttributes.EaterKinetic;

			if ( item.AbsorptionAttributes.EaterPoison > 0 )
				value += item.AbsorptionAttributes.EaterPoison;

			if ( item.AbsorptionAttributes.ResonanceCold > 0 )
				value += item.AbsorptionAttributes.ResonanceCold;

			if ( item.AbsorptionAttributes.ResonanceEnergy > 0 )
				value += item.AbsorptionAttributes.ResonanceEnergy;

			if ( item.AbsorptionAttributes.ResonanceFire > 0 )
				value += item.AbsorptionAttributes.ResonanceFire;

			if ( item.AbsorptionAttributes.ResonanceKinetic > 0 )
				value += item.AbsorptionAttributes.ResonanceKinetic;

			if ( item.AbsorptionAttributes.ResonancePoison > 0 )
				value += item.AbsorptionAttributes.ResonancePoison;

			if ( item.AbsorptionAttributes.SoulChargeCold > 0 )
				value += item.AbsorptionAttributes.SoulChargeCold;

			if ( item.AbsorptionAttributes.SoulChargeEnergy > 0 )
				value += item.AbsorptionAttributes.SoulChargeEnergy;

			if ( item.AbsorptionAttributes.SoulChargeFire > 0 )
				value += item.AbsorptionAttributes.SoulChargeFire;

			if ( item.AbsorptionAttributes.SoulChargeKinetic > 0 )
				value += item.AbsorptionAttributes.SoulChargeKinetic;

			if ( item.AbsorptionAttributes.SoulChargePoison > 0 )
				value += item.AbsorptionAttributes.SoulChargePoison;

			//Start Element Resist

			if ( item.Resistances.Chaos > 0 )
				value += item.Resistances.Chaos;

			if ( item.Resistances.Cold > 0 )
				value += item.Resistances.Cold;

			if ( item.Resistances.Direct > 0 )
				value += item.Resistances.Direct;

			if ( item.Resistances.Energy > 0 )
				value += item.Resistances.Energy;

			if ( item.Resistances.Fire > 0 )
				value += item.Resistances.Fire;

			if ( item.Resistances.Physical > 0 )
				value += item.Resistances.Physical;

			if ( item.Resistances.Poison > 0 )
				value += item.Resistances.Poison;

			//Start Calculate

			if ( value == 0 )
				return ItemValue.Trash;

			if ( value <= 50 )
				return ItemValue.Common;

			if ( value <= 100 )
				return ItemValue.Uncommon;

			if ( value <= 200 )
				return ItemValue.Rare;

			if ( value <= 300 )
				return ItemValue.Epic;

			return ItemValue.Legendary;
		}

		public static ItemValue CheckQuiver( BaseQuiver item )
		{
			int value = 0;

			foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
			{
				if ( item != null && item.Attributes[ (AosAttribute)i ] > 0 )
					value += 2;
			}

			//Start standard attributes

			if ( item.Attributes.AttackChance > 0 )
				value += item.Attributes.AttackChance * 2;

			if ( item.Attributes.BonusDex > 0 )
				value += item.Attributes.BonusDex * 4;

			if ( item.Attributes.BonusHits > 0 )
				value += item.Attributes.BonusHits * 2;

			if ( item.Attributes.BonusInt > 0 )
				value += item.Attributes.BonusInt * 4;

			if ( item.Attributes.BonusMana > 0 )
				value += item.Attributes.BonusMana * 2;

			if ( item.Attributes.BonusStam > 0 )
				value += item.Attributes.BonusStam * 2;

			if ( item.Attributes.BonusStr > 0 )
				value += item.Attributes.BonusStr * 4;

			if ( item.Attributes.CastRecovery > 0 )
				value += item.Attributes.CastRecovery * 10;

			if ( item.Attributes.CastSpeed > 0 )
				value += item.Attributes.CastSpeed * 10;

			if ( item.Attributes.DefendChance > 0 )
				value += item.Attributes.DefendChance * 2;

			if ( item.Attributes.EnhancePotions > 0 )
				value += item.Attributes.EnhancePotions;

			if ( item.Attributes.LowerManaCost > 0 )
				value += item.Attributes.LowerManaCost * 2;

			if ( item.Attributes.LowerRegCost > 0 )
				value += item.Attributes.LowerRegCost;

			if ( item.Attributes.Luck > 0 )
				value += item.Attributes.Luck / 2;

			if ( item.Attributes.NightSight > 0 )
				value += 10;

			if ( item.Attributes.ReflectPhysical > 0 )
				value += item.Attributes.ReflectPhysical * 2;

			if ( item.Attributes.RegenHits > 0 )
				value += item.Attributes.RegenHits * 10;

			if ( item.Attributes.RegenMana > 0 )
				value += item.Attributes.RegenMana * 10;

			if ( item.Attributes.RegenStam > 0 )
				value += item.Attributes.RegenStam * 10;

			if ( item.Attributes.SpellChanneling > 0 )
				value += 10;

			if ( item.Attributes.SpellDamage > 0 )
				value += item.Attributes.SpellDamage * 2;

			if ( item.Attributes.WeaponDamage > 0 )
				value += item.Attributes.WeaponDamage * 2;

			if ( item.Attributes.WeaponSpeed > 0 )
				value += item.Attributes.WeaponSpeed * 2;

			// Start Quiver Bonuses

			if ( item.DamageIncrease > 0 )
				value += item.DamageIncrease;

			if ( item.LowerAmmoCost > 0 )
				value += item.LowerAmmoCost;

			//Start Calculate

			if ( value == 0 )
				return ItemValue.Trash;

			if ( value <= 50 )
				return ItemValue.Common;

			if ( value <= 100 )
				return ItemValue.Uncommon;

			if ( value <= 200 )
				return ItemValue.Rare;

			if ( value <= 300 )
				return ItemValue.Epic;

			return ItemValue.Legendary;
		}

		public static ItemValue CheckTalisman( BaseTalisman item )
		{
			int value = 0;

			foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
			{
				if ( item != null && item.Attributes[ (AosAttribute)i ] > 0 )
					value += 2;
			}

			//Start skill bonus

			if ( item.SkillBonuses.Skill_1_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_1_Value * 4;
				value += 2;
			}

			if ( item.SkillBonuses.Skill_2_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_2_Value * 4;
				value += 2;
			}

			if ( item.SkillBonuses.Skill_3_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_3_Value * 4;
				value += 2;
			}

			if ( item.SkillBonuses.Skill_4_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_4_Value * 4;
				value += 2;
			}

			if ( item.SkillBonuses.Skill_5_Value > 0 )
			{
				value += (int)item.SkillBonuses.Skill_5_Value * 4;
				value += 2;
			}

			//Start standard attributes

			if ( item.Attributes.AttackChance > 0 )
				value += item.Attributes.AttackChance * 2;

			if ( item.Attributes.BonusDex > 0 )
				value += item.Attributes.BonusDex * 4;

			if ( item.Attributes.BonusHits > 0 )
				value += item.Attributes.BonusHits * 2;

			if ( item.Attributes.BonusInt > 0 )
				value += item.Attributes.BonusInt * 4;

			if ( item.Attributes.BonusMana > 0 )
				value += item.Attributes.BonusMana * 2;

			if ( item.Attributes.BonusStam > 0 )
				value += item.Attributes.BonusStam * 2;

			if ( item.Attributes.BonusStr > 0 )
				value += item.Attributes.BonusStr * 4;

			if ( item.Attributes.CastRecovery > 0 )
				value += item.Attributes.CastRecovery * 10;

			if ( item.Attributes.CastSpeed > 0 )
				value += item.Attributes.CastSpeed * 10;

			if ( item.Attributes.DefendChance > 0 )
				value += item.Attributes.DefendChance * 2;

			if ( item.Attributes.EnhancePotions > 0 )
				value += item.Attributes.EnhancePotions;

			if ( item.Attributes.LowerManaCost > 0 )
				value += item.Attributes.LowerManaCost * 2;

			if ( item.Attributes.LowerRegCost > 0 )
				value += item.Attributes.LowerRegCost;

			if ( item.Attributes.Luck > 0 )
				value += item.Attributes.Luck / 2;

			if ( item.Attributes.NightSight > 0 )
				value += 10;

			if ( item.Attributes.ReflectPhysical > 0 )
				value += item.Attributes.ReflectPhysical * 2;

			if ( item.Attributes.RegenHits > 0 )
				value += item.Attributes.RegenHits * 10;

			if ( item.Attributes.RegenMana > 0 )
				value += item.Attributes.RegenMana * 10;

			if ( item.Attributes.RegenStam > 0 )
				value += item.Attributes.RegenStam * 10;

			if ( item.Attributes.SpellChanneling > 0 )
				value += 10;

			if ( item.Attributes.SpellDamage > 0 )
				value += item.Attributes.SpellDamage * 2;

			if ( item.Attributes.WeaponDamage > 0 )
				value += item.Attributes.WeaponDamage * 2;

			if ( item.Attributes.WeaponSpeed > 0 )
				value += item.Attributes.WeaponSpeed * 2;

			// Start Talisman Bonuses

			if ( item.ExceptionalBonus > 0 )
				value += item.ExceptionalBonus;

			if ( item.KarmaLoss > 0 )
				value += item.KarmaLoss;

			// Add Killer, Protection, Removal, and Summon to list of check

			//Start Calculate

			if ( value == 0 )
				return ItemValue.Trash;

			if ( value <= 50 )
				return ItemValue.Common;

			if ( value <= 100 )
				return ItemValue.Uncommon;

			if ( value <= 200 )
				return ItemValue.Rare;

			if ( value <= 300 )
				return ItemValue.Epic;

			return ItemValue.Legendary;
		}
	}
}