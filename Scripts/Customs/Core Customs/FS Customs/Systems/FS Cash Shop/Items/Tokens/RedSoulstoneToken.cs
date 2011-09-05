using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public class RedSoulstoneToken : PromotionalToken
	{
		public override Item CreateItemFor( Mobile from )
		{
			if( from != null && from.Account != null )
				return new RedSoulstone( from.Account.ToString() );
			else
				return null;
		}

		public override TextDefinition ItemGumpName{ get{ return 1030903; } } // <center>Soulstone</center>
		public override TextDefinition ItemName { get { return 1076155; } } // Red Soulstone
		public override TextDefinition ItemReceiveMessage{ get{ return 1062307; } } // Your gift has been created and placed in your bank box.

		[Constructable]
		public RedSoulstoneToken() : base()
		{
		}

		public RedSoulstoneToken( Serial serial ) : base( serial )
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
