using Course_Management.Entities;
using Course_Management.Exceptions.StudentExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Course_Management.Services
{
	public class StudentService:IService<Student>
	{
		private readonly List<Student> _students = [];
		public void Create(Student student) => GetAll().Add(student);
		public void Update(int id, Student updatedStudent)
		{
			Student student = FindById(id);
			int index = GetAll().IndexOf(student);
			GetAll()[index] = updatedStudent;
		}
		public void Delete(int id)
		{
			Student student = FindById(id);
			GetAll().Remove(student);
		}
		public List<Student> GetAll() => _students;
		public Student FindById(int id) => GetAll().Find(student => student.Id == id) ?? throw new StudentNotFoundException("telebe tapilmadi");
		public int Count { get { return GetAll().Count; } }
		public void AddToGroup(int studentId, int groupId)
		{
			Student student = FindById(studentId);
			student.GroupId = groupId;
		}
		public int GetGroup(int id)
		{
			Student student = FindById(id);
			return student.GroupId;
		}
		public void RemoveGroup(int id)
		{
			Student student = FindById(id);
			student.GroupId = 0;
		}
	}
}
