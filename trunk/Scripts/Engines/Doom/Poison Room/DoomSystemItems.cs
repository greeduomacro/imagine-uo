using Server.Items;
using Server.Mobiles;
using Server.Engines.PartySystem;

namespace Server.Events.DoomSystem
{
	public class Penta : Item
	{
		public Penta() : base(0x0FEA)
		{
			Movable = false;
		}

		public override bool HandlesOnMovement { get { return true; } }

		public override void OnMovement(Mobile m, Point3D oldLocation)
		{
			if (!DoomSystem.PoisonRoom.Active && DoomSystem.CanActivate(m) && Utility.InRange(Location, m.Location, 3))
			{
				DoomSystem.PoisonRoom.Activate();
			}
		}

		public Penta(Serial serial) : base(serial) { }
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); }
	}
}