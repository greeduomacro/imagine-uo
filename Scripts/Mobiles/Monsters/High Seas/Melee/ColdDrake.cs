using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a cold ColdDrake corpse" )]
	public class ColdDrake : BaseCreature
	{
		[Constructable]
		public ColdDrake () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a cold ColdDrake";
			Body = Utility.RandomList( 60, 61 );
			BaseSoundID = 362;
			Hue = 951;

			SetStr( 617, 669 );
			SetDex( 134, 152 );
			SetInt( 152, 189 );

			SetHits( 461, 495 );
			SetMana( 309, 340 );

			SetDamage( 17, 20 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Cold, 50 );

			SetResistance( ResistanceType.Physical, 57, 65 );
			SetResistance( ResistanceType.Fire, 31, 39 );
			SetResistance( ResistanceType.Cold, 75, 89 );
			SetResistance( ResistanceType.Poison, 45, 59 );
			SetResistance( ResistanceType.Energy, 41, 50 );

			SetSkill( SkillName.MagicResist, 96.7, 109.8 );
			SetSkill( SkillName.Tactics, 116.2, 139.5 );
			SetSkill( SkillName.Wrestling, 115.4, 125.4 );

			Fame = 8500;
			Karma = -8500;

			VirtualArmor = 46;

			Tamable = false;

			PackReg( 3 );
		}

		//TODO: Area Cold Attack

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool ReacquireOnMovement{ get{ return true; } }
		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override int TreasureMapLevel{ get{ return 2; } }
		public override int Meat{ get{ return 10; } }
		public override int Hides{ get{ return 22; } }
		public override HideType HideType{ get{ return HideType.Horned; } }
		public override int Scales{ get{ return 8; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Yellow ); } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish; } }

		public ColdDrake( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}