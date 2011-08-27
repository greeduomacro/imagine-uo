using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;
using Server.Prompts;
using Server.Multis;
using Server.Targeting;
using System.Net;
using Server.Accounting;
using Server.Mobiles;

namespace Server.Gumps
{
    public class PlayersVendorsGump : Gump
    {
        private const int GreenHue = 0x40;
        private const int RedHue = 0x20;
        private List<PlayerVendor> m_alPv;
        private int m_DefaultIndex;
        private int m_Page;
        private Mobile m_From;
        private List<VendorItem> m_alVi;
        private string m_strBeneficiary;

        public void AddBlackAlpha(int x, int y, int width, int height)
        {
            AddImageTiled(x, y, width, height, 2624);
            AddAlphaRegion(x, y, width, height);
        }
        public PlayersVendorsGump(Mobile from, List<PlayerVendor> r_alPv, int page, List<VendorItem> r_alVi)
            : base(50, 40)
        {
            //from.CloseGump(typeof(PlayersVendorsGump));
            int pvs = 0;
            m_Page = page;
            m_From = from;
            int pageCount = 0;
            m_alPv = r_alPv;
            m_alVi = r_alVi;


            AddPage(0);
            AddBackground(0, 0, 720, 325, 3500);
            AddBlackAlpha(20, 20, 681, 277);

            if (m_alPv == null)
            {
                return;
            }
            else
            {
                pvs = r_alPv.Count;
                if (r_alPv.Count % 12 == 0)
                {
                    pageCount = (r_alPv.Count / 12);
                }
                else
                {
                    pageCount = (r_alPv.Count / 12) + 1;
                }
            }

            AddLabelCropped(32, 20, 100, 20, 1152, "Shop Name");
            AddLabelCropped(265, 20, 120, 20, 1152, "Owner");
            AddLabelCropped(350, 20, 120, 20, 1152, "Location");
            AddLabelCropped(430, 20, 120, 20, 1152, "Price");
            AddLabelCropped(470, 20, 120, 20, 1152, "Quantity");
            AddLabelCropped(550, 20, 120, 20, 1152, "Description");
            AddLabel(27, 298, 32, String.Format("{0} vendors found", pvs));

            if (page > 1)
                AddButton(653, 22, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 0);
            else
                AddImage(653, 22, 0x25EA);

            if (pageCount > page)
                AddButton(670, 22, 0x15E1, 0x15E5, 2, GumpButtonType.Reply, 0);
            else
                AddImage(670, 22, 0x25E6);

            if (m_alPv.Count == 0)
                AddLabel(180, 115, 1152, "No Items found");

            if (page == pageCount)
            {
                for (int i = (page * 12) - 12; i < pvs; ++i)
                    AddDetails(i);
            }
            else
            {
                for (int i = (page * 12) - 12; i < page * 12; ++i)
                    AddDetails(i);
            }
        }

        private void AddDetails(int index)
        {
            try
            {
                if (index < m_alPv.Count)
                {
                    int btnTeleport;
                    int row;
                    btnTeleport = (index) + 101;
                    row = index % 12;
                    PlayerVendor pv = m_alPv[index] as PlayerVendor;
                    Account a = pv.Owner.Account as Account;
                    VendorItem vi = m_alVi[index] as VendorItem;
                    AddLabel(32, 46 + (row * 20), 1152, String.Format("{0}", pv.ShopName));
                    AddLabel(265, 46 + (row * 20), 1152, String.Format("{0}", pv.Owner.Name));
                    AddLabel(350, 46 + (row * 20), 1152, String.Format("{0}", pv.Map));
                    AddLabel(430, 46 + (row * 20), 1152, String.Format("{0}", vi.Price));
                    AddLabel(490, 46 + (row * 20), 1152, String.Format("{0}", vi.Item.Amount));
                    AddLabel(550, 46 + (row * 20), 1152, String.Format("{0}", vi.Description));
                    AddButton(667, 51 + (row * 20), 2437, 2438, btnTeleport, GumpButtonType.Reply,0);
                    if (pv == null)
                    {
                        Console.WriteLine("No Items found.");
                        return;
                    }
                }
            }
            catch { }
        }
        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            int buttonID = info.ButtonID;
            
            
            if (buttonID == 2)
            {
                m_Page++;
                from.CloseGump(typeof(PlayersVendorsGump));
                from.SendGump(new PlayersVendorsGump(from, m_alPv, m_Page, m_alVi));
            }
            if (buttonID == 1)
            {
                m_Page--;
                from.CloseGump(typeof(PlayersVendorsGump));
                from.SendGump(new PlayersVendorsGump(from, m_alPv, m_Page, m_alVi));
            }
            if (buttonID > 100 && buttonID < 200)
            {
                int index = buttonID - 101;
                PlayerVendor pv = m_alPv[index] as PlayerVendor;
                Point3D xyz = pv.Location;
                int x = xyz.X;
                int y = xyz.Y;
                int z = xyz.Z;
                Point3D dest = new Point3D(x, y, z);

                Item[] Gold = from.Backpack.FindItemsByType(typeof(Gold));

                int flags = (int) (from.NetState == null ? 0 : from.NetState.Flags);

                if (Factions.Sigil.ExistsOn(from))
                {
                    from.SendLocalizedMessage(1061632); // You can't do that while carrying the sigil.
                }
                
                else if (from.Spell != null)
                {
                    from.SendLocalizedMessage(1049616); // You are too busy to do that at the moment.
                }
                //from.Backpack.ConsumeTotal(typeof(Gold), 500) ||
                else if (from.Backpack.ConsumeTotal(typeof(Gold), 500) || from.BankBox.ConsumeTotal(typeof(Gold), 500))
                {
                    if (pv.House != null)
                    {
                        from.MoveToWorld(pv.House.Sign.Location, pv.Map);
                    }
                    else
                    {
                        from.MoveToWorld(dest, pv.Map);
                    }

                    {
                        from.SendMessage("As a payment 500gp has been taken from your bag or bank.");
                    }
                }
                else
                {
                    from.SendMessage("You dont have enough gold to travel that vendor.");
                }               
            }
        }
    }
}