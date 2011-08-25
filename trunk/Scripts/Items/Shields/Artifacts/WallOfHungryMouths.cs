using System;
using Server;

namespace Server.Items
{
	public class WallOfHungryMouths : HeaterShield
	{
		public override int BasePhysicalResistance{ get{ return 5; } }
		public override int BaseFireResistance{ get{ return 1; } }
		public override int BaseColdResistance{ get{ return 0; } }
		public override int BasePoisonResistance{ get{ return 0; } }
		public override int BaseEnergyResistance{ get{ return 0; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public WallOfHungryMouths() 
		{
		Name = ("Wall Of Hungry Mouths");
		
		Hue = 1866;	
		
		AbsorptionAttributes.EaterEnergy = 10;
		AbsorptionAttributes.EaterPoison = 10;
		AbsorptionAttributes.EaterCold = 10;
		AbsorptionAttributes.EaterFire = 10;
		}

		public WallOfHungryMouths( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
		}
	}
}
