using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a fat bloated corpse" )]
	public class Grobbubatus : BaseCreature
	{
		public override bool IgnoreYoungProtection { get { return Core.ML; } }

		[Constructable]
		public Grobbubatus() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Grobbubatus the Damned";
			Body = 0x100;
			BaseSoundID = 0xE0;

			SetStr( 201, 230 );
			SetDex( 101, 110 );
			SetInt( 201, 230 );

			SetHits( 1800 );

			SetDamage( 22, 26 );

			SetDamageType( ResistanceType.Physical, 85 );
			SetDamageType( ResistanceType.Poison, 15 );

			SetResistance( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Fire, 60 );
			SetResistance( ResistanceType.Cold, 100 );
			SetResistance( ResistanceType.Poison, 90 );
			SetResistance( ResistanceType.Energy, 75 );

			SetSkill( SkillName.DetectHidden, 80.0 );
			SetSkill( SkillName.EvalInt, 118.1, 120.0 );
			SetSkill( SkillName.Magery, 112.6, 120.0 );
			SetSkill( SkillName.Meditation, 150.0 );
			SetSkill( SkillName.Poisoning, 120.0 );
			SetSkill( SkillName.MagicResist, 90.1, 90.9 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 90.9 );

			Fame = 22000;
			Karma = -22000;

			VirtualArmor = 34;
		}

		public override void OnDamagedBySpell( Mobile attacker )
		{
			base.OnDamagedBySpell( attacker );

			DoCounter( attacker );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			DoCounter( attacker );
		}

		private void DoCounter( Mobile attacker )
		{
			if ( 0.2 > Utility.RandomDouble() )
			{
				Mobile target = null;

				if ( attacker is BaseCreature )
				{
					Mobile m = ((BaseCreature)attacker).GetMaster();

					if ( m != null )
						target = m;
				}

				if ( target == null || !target.InRange( this, 25 ) )
					target = attacker;

				this.Location = target.Location;
				this.Map = target.Map;

				this.FixedParticles( 0x376A, 9, 32, 0x13AF, EffectLayer.Waist );
				this.PlaySound( 0x1FE );

				DoHarmful( target );
				AOS.Damage( target, this, Utility.RandomMinMax( 20, 25 ), true, 20, 20, 20, 20, 20 );

				if ( target is PlayerMobile)
					target.SendMessage( "Grobubatus has charged you!" );
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 4 );
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Gems, 6 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( !Summoned && !NoKillAwards && DemonKnight.CheckArtifactChance( this ) )
				DemonKnight.DistributeArtifact( this );
		}

		public override bool BardImmune{ get{ return !Core.SE; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool AreaPeaceImmune { get { return Core.SE; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }

		public override int TreasureMapLevel{ get{ return 1; } }

		public Grobbubatus( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 471 )
				BaseSoundID = 0xE0;
		}

		public override bool OnBeforeDeath()
		{
			if ( !NoKillAwards )
			{
				Map map = this.Map;

				if ( map != null )
				{
					for ( int x = -12; x <= 12; ++x )
					{
						for ( int y = -12; y <= 12; ++y )
						{
							double dist = Math.Sqrt(x*x+y*y);

							if ( dist <= 12 )
								new GoodiesTimer( map, X + x, Y + y ).Start();
						}
					}
				}
			}

			return base.OnBeforeDeath();
		}

		private class GoodiesTimer : Timer
		{
			private Map m_Map;
			private int m_X, m_Y;

			public GoodiesTimer( Map map, int x, int y ) : base( TimeSpan.FromSeconds( Utility.RandomDouble() * 10.0 ) )
			{
				m_Map = map;
				m_X = x;
				m_Y = y;
			}

			protected override void OnTick()
			{
				int z = m_Map.GetAverageZ( m_X, m_Y );
				bool canFit = m_Map.CanFit( m_X, m_Y, z, 6, false, false );

				for ( int i = -3; !canFit && i <= 3; ++i )
				{
					canFit = m_Map.CanFit( m_X, m_Y, z + i, 6, false, false );

					if ( canFit )
						z += i;
				}

				if ( !canFit )
					return;

				Gold g = new Gold( 100, 300 );
				
				g.MoveToWorld( new Point3D( m_X, m_Y, z ), m_Map );

				if ( 0.5 >= Utility.RandomDouble() )
				{
					switch ( Utility.Random( 3 ) )
					{
						case 0: // Fire column
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 5052 );
							Effects.PlaySound( g, g.Map, 0x208 );

							break;
						}
						case 1: // Explosion
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x36BD, 20, 10, 5044 );
							Effects.PlaySound( g, g.Map, 0x307 );

							break;
						}
						case 2: // Ball of fire
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x36FE, 10, 10, 5052 );

							break;
						}
					}
				}
			}
		}
	}
}