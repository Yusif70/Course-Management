using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Management.Exceptions.GroupExceptions
{
	public class TeacherAlreadyInGroupException : Exception
	{
		private static readonly string defaultMessage = "The teacher is already in group";
		public TeacherAlreadyInGroupException() : base(defaultMessage) { }
		public TeacherAlreadyInGroupException(string message) : base(message) { }
	}
}
