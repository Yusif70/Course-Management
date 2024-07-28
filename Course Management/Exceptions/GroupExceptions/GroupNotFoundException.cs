using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Management.Exceptions.GroupExceptions
{
	public class GroupNotFoundException : Exception
	{
		private static readonly string defaultMessage = "Group not found";
		public GroupNotFoundException() : base(defaultMessage) { }
		public GroupNotFoundException(string message) : base(message) { }
	}
}
