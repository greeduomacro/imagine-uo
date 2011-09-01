using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2B74, 0x316B )]
	public class GraveDiggerRing : GoldRing
	{
		public override SetItem SetID{ get{ return SetItem.Grave; } }
		public override int Pieces{ get{ return 3; } }

		[Constructable]
		public GraveDiggerRing() : base()
		{
			Name = "Bone Keepers Ring";
			SetHue = 1175;
			
			Attributes.BonusMana = 5;
			Attributes.BonusStam = 5;
			Attributes.BonusHits = 5;
			
			SetSkillBonuses.SetValues( 0, SkillName.Forensics, 20.0 );
			SetSkillBonuses.SetValues( 1, SkillName.Mining, 20.0 );

			SetAttributes.Luck = 200;

			SetAttributes.RegenMana = 2;
			SetAttributes.RegenHits = 2;

			ItemValue = ItemValue.Epic;
		}

		public GraveDiggerRing( Serial serial ) : base( serial )
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
