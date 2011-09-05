using System;
using Server.Items;

namespace Server.Items
{
	public class PhoenixCasterScepter : Scepter
	{
		public override SetItem SetID{ get{ return SetItem.PhoenixCaster; } }
		public override int Pieces{ get{ return 14; } }

		[Constructable]
		public PhoenixCasterScepter() : base()
		{
			Name = "Match Stick";

			SetHue = 43;
			
			Slayer = SlayerName.Repond;
			Slayer2 = SlayerName.Exorcism;

			Attributes.SpellChanneling = 1;

			WeaponAttributes.MageWeapon = 30;
			WeaponAttributes.BattleLust = 1;
			WeaponAttributes.HitFireArea = 75;
			WeaponAttributes.HitLeechMana = 50;
			
			SetAttributes.LowerRegCost = 100;
			SetAttributes.LowerManaCost = 50;
			SetAttributes.Luck = 2500;
			SetAttributes.BonusInt = 10;
			SetSkillBonuses.SetValues( 0, SkillName.Meditation, 120 );
		}

		public PhoenixCasterScepter( Serial serial ) : base( serial )
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