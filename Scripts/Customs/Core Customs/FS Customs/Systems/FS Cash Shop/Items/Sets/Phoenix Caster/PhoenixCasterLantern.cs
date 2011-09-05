using System;
using Server.Items;

namespace Server.Items
{
	public class PhoenixCasterLantern : MetalKiteShield
	{
		public override SetItem SetID{ get{ return SetItem.PhoenixCaster; } }
		public override int Pieces{ get{ return 14; } }

		[Constructable]
		public PhoenixCasterLantern() : base()
		{
			ItemID = 2597;
			Name = "Blinding Light";

			SetHue = 43;
			
			Attributes.NightSight = 1;
			Attributes.DefendChance = 25;
			Attributes.SpellChanneling = 1;
			Attributes.BonusHits = 15;

			PhysicalBonus = 15;
			EnergyBonus = 9;
			
			SetAttributes.LowerRegCost = 100;
			SetAttributes.LowerManaCost = 50;
			SetAttributes.Luck = 2500;
			SetAttributes.BonusInt = 10;
			SetSkillBonuses.SetValues( 0, SkillName.Meditation, 120 );

			SetSelfRepair = 5;
			SetPhysicalBonus = 10;
			SetFireBonus = 10;
			SetColdBonus = 10;
			SetPoisonBonus = 10;
			SetEnergyBonus = 10;
		}

		public PhoenixCasterLantern( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
}