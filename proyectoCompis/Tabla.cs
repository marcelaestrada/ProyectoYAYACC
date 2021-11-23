using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoCompis
{
	public enum ActionType
	{
		Reduce,
		Shift,
		Error
	};

	public class Action
	{
		public ActionType ActionType { get; set; }
		public int ActionParameter { get; set; }

		public bool Equals(Action action)
		{
			return (ActionType == action.ActionType) && (ActionParameter == action.ActionParameter);
		}
	};

	class ParseTable
	{
		public Action[,] Actions { get; set; }
	}
}
