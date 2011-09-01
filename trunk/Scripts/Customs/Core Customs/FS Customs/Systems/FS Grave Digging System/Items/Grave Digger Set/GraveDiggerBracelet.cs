using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2B74, 0x316B )]
	public class GraveDiggerBracelet : GoldBracelet
	{
		public override SetItem SetID{ get{ return SetItem.Grave; } }
		public override int Pieces{ get{ return 3; } }

		[Constructable]
		public GraveDiggerBracelet() : base()
		{
			Name = "Bone Keepers Bracelet";
			SetHue = 1175;
			
			Attributes.BonusInt = 2;
			Attributes.BonusDex = 2;
			Attributes.BonusStr = 2;
			
			SetSkillBonuses.SetValues( 0, SkillName.Forensics, 20.0 );
			SetSkillBonuses.SetValues( 1, SkillName.Mining, 20.0 );

			SetAttributes.Luck = 200;

			SetAttributes.RegenMana = 2;
			SetAttributes.RegenHits = 2;

			ItemValue = ItemValue.Epic;
		}

		public GraveDiggerBracelet( Serial serial ) : base( serial )
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
