using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Management.Exceptions.GroupExceptions
{
	public class StudentAlreadyInGroupException : Exception
	{
		private static readonly string defaultMessage = "The student is already in the group";
		public StudentAlreadyInGroupException() : base(defaultMessage) { }
		public StudentAlreadyInGroupException(string message) : base(message) { }
	}
}
