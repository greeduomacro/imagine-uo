using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Server;
using Server.Commands.Generic;
using Server.Commands;
using Server.Targeting;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Gumps
{
    public class VendorSearchGump : Gump
    {
        private string m_SearchString;
        private Type[] m_SearchResults;
        private int m_Page;
        private ArrayList m_alVendors = new ArrayList();
        
        public static void Initialize()
        {
            CommandSystem.Register("VSM", AccessLevel.GameMaster, new CommandEventHandler(VendorSearchMenu_OnCommand));
        }
        [Usage("VSM")]
        [Description("Searches vendors for items")]

        private static void VendorSearchMenu_OnCommand(CommandEventArgs e)
        {
            Type[] types = Type.EmptyTypes;
            e.Mobile.SendGump(new VendorSearchGump(e.Mobile, "", 0, types, false));
        }

        public VendorSearchGump(Mobile from, string searchString, int page, Type[] searchResults, bool explicitSearch): base(50, 50)
        {
            m_SearchString = searchString;
            m_SearchResults = searchResults;
            m_Page = page;

            from.CloseGump(typeof(VendorSearchGump));
            AddPage(0);
            AddBackground(0, 0, 420, 300, 5054); //main box
            AddLabel(10, 5 , 0x480, "Enter the item you are looking for below.");//label at top
            AddButton(190, 25, 4011, 4013, 1, GumpButtonType.Reply, 0); //search button
            AddImageTiled(10, 25, 180, 20, 2624); //Background for Search textbox
            AddTextEntry(10, 25, 180, 20, 0x480, 0, searchString); //search textbox
            //AddHtmlLocalized(230, 50, 100, 20, 3010005, 0x7FFF, false, false); //word search
            AddImageTiled(10, 50, 400, 200, 2624); //background results
            //AddAlphaRegion(10, 40, 400, 200);

            if (searchResults.Length > 0)
            {
                for (int i = (page * 10); i < ((page + 1) * 10) && i < searchResults.Length; ++i)
                {
                    int index = i % 10;
                    AddLabel(44, 50 + (index * 20), 0x480, searchResults[i].Name);
                    AddButton(10, 50 + (index * 20), 4023, 4025, 4 + i, GumpButtonType.Reply, 0);
                }
            }
            else
            {
                AddLabel(15, 55, 0x480, explicitSearch ? "Nothing matched your search terms." : "No results to display.");
            }
            AddImageTiled(10, 250, 400, 20, 2624);
            //AddAlphaRegion(10, 250, 400, 20);

            if (m_Page > 0)
            {
                AddButton(10, 270, 4014, 4016, 2, GumpButtonType.Reply, 0);
            }
            else
            {
                AddImage(10, 270, 4014);
            }

            AddHtmlLocalized(44, 270, 170, 20, 1061028, m_Page > 0 ? 0x7FFF : 0x5EF7, false, false); // Previous page

            if (((m_Page + 1) * 10) < searchResults.Length)
            {
                AddButton(210, 270, 4005, 4007, 3, GumpButtonType.Reply, 0);
            }
            else
            {
                AddImage(210, 270, 4005);
            }

            AddHtmlLocalized(244, 270, 170, 20, 1061027, ((m_Page + 1) * 10) < searchResults.Length ? 0x7FFF : 0x5EF7, false, false); // Next page
        }

        private static Type typeofItem = typeof(Item), typeofMobile = typeof(Mobile);

        private static void Match(string match, Type[] types, ArrayList results)
        {
            if (match.Length == 0)
            {
                return;
            }

            match = match.ToLower();

            for (int i = 0; i < types.Length; ++i)
            {
                Type t = types[i];
                if (typeofItem.IsAssignableFrom(t) && t.Name.ToLower().IndexOf(match) >= 0 && !results.Contains(t))
                {
                    ConstructorInfo[] ctors = t.GetConstructors();

                    for (int j = 0; j < ctors.Length; ++j)
                    {
                        if (ctors[j].GetParameters().Length == 0 && ctors[j].IsDefined(typeof(ConstructableAttribute), false))
                        {
                            results.Add(t);
                            break;
                        }
                    }
                }
            }
        }

        public static ArrayList Match(string match)
        {
            ArrayList results = new ArrayList();
            Type[] types;

            Assembly[] asms = ScriptCompiler.Assemblies;

            for (int i = 0; i < asms.Length; ++i)
            {
                types = ScriptCompiler.GetTypeCache(asms[i]).Types;
                Match(match, types, results);
            }

            types = ScriptCompiler.GetTypeCache(Core.Assembly).Types;
            Match(match, types, results);

            results.Sort(new TypeNameComparer());

            return results;
        }

        private class TypeNameComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                Type a = x as Type;
                Type b = y as Type;

                return a.Name.CompareTo(b.Name);
            }
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch (info.ButtonID)
            {
                case 1: // Search
                    {
                        TextRelay te = info.GetTextEntry(0);
                        string match = (te == null ? "" : te.Text.Replace(" ",""));

                        if (match.Length < 3)
                        {
                            from.SendMessage("Invalid search string.");
                            from.SendGump(new VendorSearchGump(from, match, m_Page, m_SearchResults, false));
                        }
                        else
                        {
                            from.SendGump(new VendorSearchGump(from, match, 0, (Type[])Match(match).ToArray(typeof(Type)), false));
                        }

                        break;
                    }
                case 2: // Previous page
                    {
                        if (m_Page > 0)
                            from.SendGump(new VendorSearchGump(from, m_SearchString, m_Page - 1, m_SearchResults, true));

                        break;
                    }
                case 3: // Next page
                    {
                        if ((m_Page + 1) * 10 < m_SearchResults.Length)
                            from.SendGump(new VendorSearchGump(from, m_SearchString, m_Page + 1, m_SearchResults, true));

                        break;
                    }
                default:
                    {
                        int index = info.ButtonID - 4;

                        if (index >= 0 && index < m_SearchResults.Length)
                        {
                            List<VendorItem> alVendorResultsVi = new List<VendorItem>();
                            ArrayList alVendors = new ArrayList();
                            List<PlayerVendor> alVendorResultsPv = new List<PlayerVendor>();
                            

                            //list all the vendors
                            foreach (Mobile mb in World.Mobiles.Values)
                            {
                                if (mb is PlayerVendor)
                                {
                                    PlayerVendor pv = mb as PlayerVendor;
                                    alVendors.Add(pv);
                                }
                            }
                            //list all the vendors that have the item type in it
                            int count = 0;
                            foreach (PlayerVendor pv in alVendors)
                            {
                                
                                if (pv.Backpack.FindItemsByType((Type)m_SearchResults[index]).Length > 0)
                                {
                                    count++;
                                    if (count == 100)
                                    {
                                        break;
                                    }
                                    alVendorResultsVi.Add((VendorItem)pv.GetVendorItem((Item)pv.Backpack.FindItemByType((Type)m_SearchResults[index])));
                                    alVendorResultsPv.Add(pv);

                                }
                            }
                            //display results
                            if (alVendorResultsPv.Count > 0)
                            {
                                from.SendGump(new PlayersVendorsGump(from, alVendorResultsPv, 1, alVendorResultsVi));

                            }
                            else
                            {
                                from.SendMessage("Cannot find any vendors with that item for sale");
                            }
                        }

                        break;
                    }
            }
        }
    }
}