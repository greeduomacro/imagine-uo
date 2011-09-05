using System;
using Server.Items;

namespace Server.Items
{
	public class PhoenixCasterRing : GoldRing
	{
		public override SetItem SetID{ get{ return SetItem.PhoenixCaster; } }
		public override int Pieces{ get{ return 14; } }

		[Constructable]
		public PhoenixCasterRing() : base()
		{
			Name = "Blazing Phoenix Ring";

			SetHue = 43;

			Attributes.CastRecovery = 3;
			Attributes.CastSpeed = 1;
			Attributes.BonusMana = 5;
			Attributes.RegenMana = 1;
			
			SetAttributes.LowerRegCost = 100;
			SetAttributes.LowerManaCost = 50;
			SetAttributes.Luck = 2500;
			SetAttributes.BonusInt = 10;
			SetSkillBonuses.SetValues( 0, SkillName.Meditation, 120 );
		}

		public PhoenixCasterRing( Serial serial ) : base( serial )
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