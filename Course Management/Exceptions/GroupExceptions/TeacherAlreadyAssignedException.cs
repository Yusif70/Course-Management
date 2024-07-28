using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Management.Exceptions.GroupExceptions
{
	public class TeacherAlreadyAssignedException : Exception
	{
		private static readonly string defaultMessage = "A teacher is already assigned in this group";
		public TeacherAlreadyAssignedException() : base(defaultMessage) { }
		public TeacherAlreadyAssignedException(string message) : base(message) { }
	}
}
