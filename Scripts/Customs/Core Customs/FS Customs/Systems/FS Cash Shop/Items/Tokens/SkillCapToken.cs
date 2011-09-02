using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class SkillCapToken : Item
	{
		public static int MaxSkillCap = 15000;

		[Constructable]
		public SkillCapToken() : base( 0x2AAA )
		{
			Name = "a reward token";
			LootType = LootType.Blessed;
			Light = LightType.Circle300;
			Weight = 5.0;
			ItemValue = ItemValue.Legendary;
		}

		public SkillCapToken( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1070998, "skill cap increase: 100.0" ); // Use this to redeem<br>your ~1_PROMO~
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1062334 ); // This item must be in your backpack to be used.
			}
			else
			{
				if ( from.SkillsCap < MaxSkillCap )
				{
					from.SkillsCap += 1000;
					from.SendMessage( "Your skill cap has increased by 100.0." );
					this.Delete();
				}
				else
				{
					from.SendMessage( "You cannot increase your skill cap any further." );
				}
			}
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