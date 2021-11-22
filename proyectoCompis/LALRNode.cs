using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoCompis
{
    public class LALRNode
    {
		public string[] Tokens { get; set; }
		public PrecedenceGroup[] PrecedenceGroups { get; set; }

		public enum Derivation
		{
			None,
			LeftMost,
			RightMost
		};

		public class Production
		{
			public int Left { get; set; }
			public int[] Right { get; set; }
		};

		public class PrecedenceGroup
		{
			public Derivation Derivation { get; set; }
			public Production[] Productions { get; set; }
		};
	}
}
