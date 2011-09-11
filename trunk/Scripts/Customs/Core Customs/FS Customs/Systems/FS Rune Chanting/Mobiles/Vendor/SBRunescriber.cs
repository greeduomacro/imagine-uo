using System; 
using System.Collections.Generic; 
using Server.Items; 

namespace Server.Mobiles 
{ 
	public class SBRunescriber : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBRunescriber() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				Add( new GenericBuyInfo( typeof( RuneChisel ), 10000, 20, 0x1026, 0 ) );
				Add( new GenericBuyInfo( typeof( RefinedStoneBrick ), 1000, 20, 7139, 0 ) );
				Add( new GenericBuyInfo( typeof( BlankScribingRune ), 500, 20, 0x1F17, 743 ) );
				Add( new GenericBuyInfo( typeof( MagicalDust ), 500, 20, 0x2DB5, 0 ) );
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				Add( typeof( RuneChisel ), 3000 );
				Add( typeof( RefinedStoneBrick ), 300 );
				Add( typeof( BlankScribingRune ), 100 );
				Add( typeof( MagicalDust ), 100 );
			} 
		} 
	} 
}