using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Management.Entities
{
	public class Student:Entity
	{
		public string Surname { get; set; }
		public int GroupId { get; set; }
		public Student(int id, string name, string surname, int groupId) : base(id, name)
		{
			Id = id;
			Name = name;
			Surname = surname;
			GroupId = groupId;
		}
	}
}
