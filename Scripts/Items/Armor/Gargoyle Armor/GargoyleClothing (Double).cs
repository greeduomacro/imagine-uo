using Server;
using Server.Items;

namespace Server.Items
{
	public class FemaleGargishClothChest : BaseClothing
	{
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		public override bool CanBeWornByGargoyles{ get{ return true; } }

		[Constructable]
		public FemaleGargishClothChest() : this( 0 )
		{
		}

		[Constructable]
		public FemaleGargishClothChest( int hue ) : base( 0x0405, Layer.InnerTorso, hue )
		{
			Weight = 2.0;
		}

		public FemaleGargishClothChest( Serial serial ) : base( serial )
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

	public class FemaleGargishClothArms : BaseClothing
	{
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		public override bool CanBeWornByGargoyles{ get{ return true; } }

		[Constructable]
		public FemaleGargishClothArms() : this( 0 )
		{
		}

		[Constructable]
		public FemaleGargishClothArms( int hue ) : base( 0x0403, Layer.Arms, hue )
		{
			Weight = 2.0;
		}

		public FemaleGargishClothArms( Serial serial ) : base( serial )
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

	public class FemaleGargishClothKilt : BaseClothing
	{
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		public override bool CanBeWornByGargoyles{ get{ return true; } }

		[Constructable]
		public FemaleGargishClothKilt() : this( 0 )
		{
		}

		[Constructable]
		public FemaleGargishClothKilt( int hue ) : base( 0x0407, Layer.OuterLegs, hue )
		{
			Weight = 2.0;
		}

		public FemaleGargishClothKilt( Serial serial ) : base( serial )
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

	public class FemaleGargishClothLegs : BaseClothing
	{
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		public override bool CanBeWornByGargoyles{ get{ return true; } }

		[Constructable]
		public FemaleGargishClothLegs() : this( 0 )
		{
		}

		[Constructable]
		public FemaleGargishClothLegs( int hue ) : base( 0x0409, Layer.Pants, hue )
		{
			Weight = 2.0;
		}

		public FemaleGargishClothLegs( Serial serial ) : base( serial )
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

	public class MaleGargishClothChest : BaseClothing
	{
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		public override bool CanBeWornByGargoyles{ get{ return true; } }

		[Constructable]
		public MaleGargishClothChest() : this( 0 )
		{
		}

		[Constructable]
		public MaleGargishClothChest( int hue ) : base( 0x0406, Layer.InnerTorso, hue )
		{
			Weight = 2.0;
		}

		public MaleGargishClothChest( Serial serial ) : base( serial )
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

	public class MaleGargishClothArms : BaseClothing
	{
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		public override bool CanBeWornByGargoyles{ get{ return true; } }

		[Constructable]
		public MaleGargishClothArms() : this( 0 )
		{
		}

		[Constructable]
		public MaleGargishClothArms( int hue ) : base( 0x0404, Layer.Arms, hue )
		{
			Weight = 2.0;
		}

		public MaleGargishClothArms( Serial serial ) : base( serial )
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

	public class MaleGargishClothKilt : BaseClothing
	{
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		public override bool CanBeWornByGargoyles{ get{ return true; } }

		[Constructable]
		public MaleGargishClothKilt() : this( 0 )
		{
		}

		[Constructable]
		public MaleGargishClothKilt( int hue ) : base( 0x0408, Layer.OuterLegs, hue )
		{
			Weight = 2.0;
		}

		public MaleGargishClothKilt( Serial serial ) : base( serial )
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

	public class MaleGargishClothLegs : BaseClothing
	{
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		public override bool CanBeWornByGargoyles{ get{ return true; } }

		[Constructable]
		public MaleGargishClothLegs() : this( 0 )
		{
		}

		[Constructable]
		public MaleGargishClothLegs( int hue ) : base( 0x040A, Layer.Pants, hue )
		{
			Weight = 2.0;
		}

		public MaleGargishClothLegs( Serial serial ) : base( serial )
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

