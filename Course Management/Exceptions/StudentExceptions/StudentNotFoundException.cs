using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Management.Exceptions.StudentExceptions
{
	public class StudentNotFoundException : Exception
	{
		private static readonly string defaultMessage = "Student not found";
		public StudentNotFoundException() : base(defaultMessage) { }
		public StudentNotFoundException(string message) : base(message) { }
	}
}
