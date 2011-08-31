using System;
using System.Collections;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Events.DoomSystem
{
	public static class DoomSystem
	{
		public static void Initialize()
		{
			PoisonRoom.Deactivate();
			CommandSystem.Register("GenDoomSystem", AccessLevel.Administrator, new CommandEventHandler(GenDoomSystem_OnCommand));
		}

		public static PoisonRoomRegion PoisonRoom = new PoisonRoomRegion();

		[Usage("GenDoomSystem")]
		[Description("creates all items needed for the doomsystem.")]
		private static void GenDoomSystem_OnCommand(CommandEventArgs e)
		{
			#region PoisonRoom
			CreateDoor(334, 14, -1);
			CreateDoor(344, 14, -1);
			CreateDoor(355, 14, -1);
			CreateType(365, 15, -1, typeof(Penta));
			#endregion
		}

		private static void CreateDoor(int x, int y, int z)
		{
			ArrayList removelist = new ArrayList();

			for (int i = 0; i < 2; i++)
			{
				IPooledEnumerable ipe = Map.Malas.GetItemsInRange(new Point3D(x, y + i, z), 0);

				foreach (Item item in ipe)
					if (item is DarkWoodDoor)
						removelist.Add(item);                
			}

			foreach (Item item in removelist)
				item.Delete();

			DarkWoodDoor door1 = new DarkWoodDoor(DoorFacing.NorthCCW);
			DarkWoodDoor door2 = new DarkWoodDoor(DoorFacing.SouthCW);

			door1.Link = door2;
			door2.Link = door1;

			door1.MoveToWorld(new Point3D(x, y, z), Map.Malas);
			door2.MoveToWorld(new Point3D(x, y + 1, z), Map.Malas);
		}

		private static void CreateType(int x, int y, int z, Type type)
		{
			Point3D point = new Point3D(x, y, z);

			if ( !Exists( point, type ) )
			{
				( (Item)Activator.CreateInstance(type) ).MoveToWorld(point, Map.Malas );
			}
		}

		private static bool Exists(Point3D point, Type type)
		{
			IPooledEnumerable ipe = Map.Malas.GetItemsInRange(point, 0);

			foreach (Item item in ipe)
			{
				if (item.GetType() == type)
				{
					return true;
				}
			}

			return false;
		}

		public static bool CanActivate(Mobile m)
		{
			if (m.AccessLevel == AccessLevel.Player && (m.Player || ((m is BaseCreature && ((BaseCreature)m).Controlled)) && !((BaseCreature)m).IsDeadPet) && m.Alive)
				return true;

			return false;
		}
	}
}