using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Management.Exceptions.TeacherExceptions
{
	public class TeacherNotFoundException : Exception
	{
		private static readonly string defaultMessage = "Teacher not found";
		public TeacherNotFoundException() : base(defaultMessage) { }
		public TeacherNotFoundException(string message) : base(message) { }
	}
}
