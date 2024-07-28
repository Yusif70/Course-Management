using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Management.Entities
{
	public class Group: Entity
	{
		public int TeacherId { get; set; }
		public List<int> StudentsId { get; set; }
		public Group(int id, string name, int teacherId, List<int> studentsId):base(id, name)
		{
			Id = id;
			Name = name;
			TeacherId = teacherId;
			StudentsId = studentsId;
		}
	}
}
