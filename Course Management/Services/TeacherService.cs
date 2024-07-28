using Course_Management.Entities;
using Course_Management.Exceptions.TeacherExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Management.Services
{
	public class TeacherService:IService<Teacher>
	{
		private readonly List<Teacher> _teachers = [];
		public void Create(Teacher teacher) => GetAll().Add(teacher);
		public void Update(int id, Teacher updatedTeacher)
		{
			Teacher teacher = FindById(id);
			int index = GetAll().IndexOf(teacher);
			GetAll()[index] = updatedTeacher;
		}
		public void Delete(int id)
		{
			Teacher teacher = FindById(id);
			GetAll().Remove(teacher);
		}
		public List<Teacher> GetAll() => _teachers;
		public Teacher FindById(int id) => GetAll().Find(teacher => teacher.Id == id) ?? throw new TeacherNotFoundException("muellim tapilmadi");
		public int Count { get { return GetAll().Count; } }
		public void AddToGroup(int teacherId, int groupId)
		{
			if (GetGroups(teacherId) == null)
			{
				GetGroups(teacherId).Add(groupId);
			}
			else
			{
				bool inGroup = GetGroups(teacherId).Exists(existingGroupId => existingGroupId == groupId);
				if (!inGroup)
				{
					GetGroups(teacherId).Add(groupId);
				}
			}
		}
		public List<int> GetGroups(int id)
		{
			Teacher teacher = FindById(id);
			return teacher.GroupsId;
		}
		public void RemoveFromGroup(int teacherId, int groupId)
		{
			GetGroups(teacherId).Remove(groupId);
		}
	}
}