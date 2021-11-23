using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoCompis
{
	public class LR0Item
	{
		public int Production { get; set; }
		public int Position { get; set; }

		public bool Equals(LR0Item item)
		{
			return (Production == item.Production) && (Position == item.Position);
		}
	};

	public class LR1Item
	{
		public int LR0ItemID { get; set; }
		public int LookAhead { get; set; }

		public bool Equals(LR1Item item)
		{
			return (LR0ItemID == item.LR0ItemID) && (LookAhead == item.LookAhead);
		}
	};
}
