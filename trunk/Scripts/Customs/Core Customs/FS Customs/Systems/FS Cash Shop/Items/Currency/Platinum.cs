using System;
using System.Globalization;

namespace Server.Items
{
	public class Platinum : Item
	{
		public override double DefaultWeight
		{
			get { return ( 0.02 ); }
		}

		[Constructable]
		public Platinum() : this( 1 )
		{
		}

		[Constructable]
		public Platinum( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		[Constructable]
		public Platinum( int amount ) : base( 0xEED )
		{
			Name = "platinum";
			Hue = 2101;
			Stackable = true;
			Amount = amount;
			LootType = LootType.Blessed;

			ItemValue = ItemValue.Legendary;
		}

		public Platinum( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			decimal amount = this.Amount * 0.10m;

			string newAmount = amount.ToString( "C", CultureInfo.GetCultureInfo( "en-US" ) );

			list.Add( 1053099, "{0}\t{1}", newAmount, "USD" );
		}

		public override int GetDropSound()
		{
			if ( Amount <= 1 )
				return 0x2E4;
			else if ( Amount <= 5 )
				return 0x2E5;
			else
				return 0x2E6;
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