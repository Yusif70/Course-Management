using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Management.Entities
{
	public class Teacher:Entity
	{
		public string Surname { get; set; }
		public List<int> GroupsId { get; set; }
		public Teacher(int id, string name, string surname, List<int> groupsId):base(id, name)
		{
			Id = id;
			Name = name;
			Surname = surname;
			GroupsId = groupsId;
		}
	}
}
