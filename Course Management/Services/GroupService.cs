using Course_Management.Entities;
using Course_Management.Exceptions.GroupExceptions;

namespace Course_Management.Services
{
	public class GroupService:IService<Group>
	{
		private readonly List<Group> _groups = [];
		public void Create(Group group) => GetAll().Add(group);
		public void Update(int id, Group updatedGroup)
		{
			Group group = FindById(id);
			int index = GetAll().IndexOf(group);
			GetAll()[index] = updatedGroup;
		}
		public void Delete(int id)
		{
			Group group = FindById(id);
			GetAll().Remove(group);
        }
		public List<Group> GetAll() => _groups;
		public Group FindById(int id) => GetAll().Find(group => group.Id == id) ?? throw new GroupNotFoundException("qrup tapilmadi");
		public void AddTeacher(int id, int teacherId)
		{
			if (GetTeacher(id) == 0)
			{
				Group group = FindById(id);
				group.TeacherId = teacherId;
			}
			else
			{
				bool isSameTeacher = GetTeacher(id) == teacherId;
				if (isSameTeacher)
				{
					throw new TeacherAlreadyInGroupException("muellim artiq qrupdadir");
				}
				else
				{
					throw new TeacherAlreadyAssignedException("bu qrupa artiq muellim teyin edilib");
				}
			}
		}
		public int GetTeacher(int id) => FindById(id).TeacherId;
		public void RemoveTeacher(int teacherId)
		{
			List<Group> groups = GetAll().FindAll(group=>group.TeacherId == teacherId);
			foreach (Group group in groups)
			{
				group.TeacherId = 0;
			}
		}
		public void AddStudent(int id, int studentId)
		{
			bool isSameStudent = GetStudents(id).Exists(existingStudentId=>existingStudentId==studentId);
			if(!isSameStudent)
			{
				GetStudents(id).Add(studentId);
			}
			else
			{
				throw new StudentAlreadyInGroupException("telebe artiq qrupdadir");
            }
		}
		public List<int> GetStudents(int id)
		{
			Group group = FindById(id);
			return group.StudentsId;
		}
		public void RemoveStudent(int id, int studentId) => GetStudents(id).Remove(studentId);
		public int Count { get { return GetAll().Count; } }
	}
}
