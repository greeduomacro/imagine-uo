using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class ChargerOfTheFallenToken : PromotionalToken
	{

		public override Item CreateItemFor( Mobile from )
		{
			return new ChargerOfTheFallen();
		}

		public override TextDefinition ItemGumpName{ get{ return 1079726; } } // <center>Charger of the Fallen</center>
		public override TextDefinition ItemName { get { return 1114845; } } //soulstone fragment
		public override TextDefinition ItemReceiveMessage{ get{ return 1062307; } } // Your gift has been created and placed in your bank box.

		[Constructable]
		public ChargerOfTheFallenToken() : base()
		{
		}

		public ChargerOfTheFallenToken( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
