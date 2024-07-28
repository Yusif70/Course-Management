using Course_Management.Entities;
using Course_Management.Exceptions.GroupExceptions;
using Course_Management.Exceptions.StudentExceptions;
using Course_Management.Exceptions.TeacherExceptions;
using Course_Management.Services;

Console.WriteLine("Kurs idare etme sistemine xos gelmisiniz");
GroupService groupService = new();
TeacherService teacherService = new();
StudentService studentService = new();

bool loop = true;
while (loop)
{
	Console.WriteLine("Esas menu");
	Console.WriteLine("    1. yeni qrup elave etme");
	Console.WriteLine("    2. movcud qruplara baxis");
	Console.WriteLine("    3. yeni muellim elave etme");
	Console.WriteLine("    4. muellimlerin siyahisi");
	Console.WriteLine("    5. yeni telebe elave etme");
	Console.WriteLine("    6. telebelerin siyahisi");

	int.TryParse(Console.ReadLine(), out int action);
	bool invalid = true;
	switch (action)
	{
		case 1:
			int newGroupId = groupService.Count == 0 ? 1 : groupService.GetAll().Last().Id + 1;
			Console.Write("qrup adini daxil edin: ");
			while (invalid)
			{
				string? groupName = Console.ReadLine();
				if (string.IsNullOrEmpty(groupName))
				{
					Console.WriteLine("melumatlari duzgun daxil edin");
				}
				else
				{
					invalid = false;
					Group newGroup = new(newGroupId, groupName, 0, []);
					groupService.Create(newGroup);
				}
			}
			break;
		case 2:
			if (groupService.Count != 0)
			{
				foreach (Group group in groupService.GetAll())
				{
					Console.WriteLine($"{group.Id}-{group.Name}");
					if (group.TeacherId == 0)
					{
						Console.WriteLine("qrupa muellim teyin edilmeyib");
					}
					else
					{
						Teacher teacher = teacherService.FindById(group.TeacherId);
						Console.WriteLine($"Muellim: {teacher.Id} {teacher.Name} {teacher.Surname}");
					}
					if (groupService.GetStudents(group.Id).Count != 0)
					{
						Console.WriteLine("telebeler:");
						foreach (int studentId in groupService.GetStudents(group.Id))
						{
							Student student = studentService.FindById(studentId);
							Console.WriteLine($"    {student.Id}-{student.Name} {student.Surname}");
						}
					}
				}
				Console.WriteLine("    1. qrupda deyisiklik");
				Console.WriteLine("    2. qrupu silmek");
				int.TryParse(Console.ReadLine(), out int groupAction);
				switch (groupAction)
				{
					case 1:
						invalid = true;
						Console.Write("qrup idsini daxil edin: ");
						while (invalid)
						{
							try
							{
								int.TryParse(Console.ReadLine(), out int groupId);
								groupService.FindById(groupId);
								Console.WriteLine("imtina etmek ucun bos enter sixin");
								Group group = groupService.FindById(groupId);
								Console.WriteLine($"kohne ad: {group.Name}");
								Console.Write("qrupun yeni adini daxil edin: ");
								string? newName = Console.ReadLine().Trim();

								if (!string.IsNullOrEmpty(newName))
								{
									int teacherId = groupService.GetTeacher(groupId);
									List<int> studentsId = groupService.GetStudents(groupId);
									Group updatedGroup = new(groupId, newName, teacherId, studentsId);
									groupService.Update(groupId, updatedGroup);
								}
								break;
							}
							catch (GroupNotFoundException ex)
							{
								Console.WriteLine(ex.Message);
							}
						}
						break;
					case 2:
						Console.Write("qrup idsi: ");
						invalid = true;
						while (invalid)
						{
							try
							{
								int.TryParse(Console.ReadLine(), out int groupId);
								groupService.FindById(groupId);
								int teacherId = groupService.GetTeacher(groupId);
								List<int> studentsId = groupService.GetStudents(groupId);
								if (teacherId != 0)
								{
									teacherService.RemoveFromGroup(teacherId, groupId);
								}
								if (studentsId.Count != 0)
								{
									groupService.GetStudents(groupId).Clear();
									studentService.RemoveGroup(groupId);
								}
								groupService.Delete(groupId);
								invalid = false;
							}
							catch (GroupNotFoundException ex)
							{
								Console.WriteLine(ex.Message);
							}
						}
						break;
					default:
						break;
				}
			}
			else
			{
				Console.WriteLine("sistemde hec bir qrup movcud deyil");
			}
			break;
		case 3:
			int id = teacherService.Count == 0 ? 1 : teacherService.GetAll().Last().Id + 1;
			Console.WriteLine("esas menuya qayitmaq ucun bos enter sixin");
			Console.Write("muellim adi: ");
			string teacherName = Console.ReadLine();
			Console.Write("muellim soyadi: ");
			string teacherSurname = Console.ReadLine();

			if (!string.IsNullOrEmpty(teacherName.Trim()) && !string.IsNullOrEmpty(teacherSurname.Trim()))
			{
				Teacher teacher = new(id, teacherName, teacherSurname, []);
				teacherService.Create(teacher);
			}
			break;
		case 4:
			if (teacherService.Count != 0)
			{
				foreach (Teacher teacher in teacherService.GetAll().ToList())
				{
					Console.WriteLine($"{teacher.Id}-{teacher.Name} {teacher.Surname}");
					if (teacherService.GetGroups(teacher.Id).Count != 0)
					{
						Console.WriteLine("oldugu qruplar: ");
						foreach (int groupId in teacherService.GetGroups(teacher.Id))
						{
							Group group = groupService.FindById(groupId);
							Console.WriteLine($"    {group.Id}-{group.Name}");
						}
					}

				}
				Console.WriteLine("    1. muellim melumatlarinda deyisiklik");
				Console.WriteLine("    2. muellimi silmek");
				Console.WriteLine("    3. yeni qrupa teyin etmek");
				int.TryParse(Console.ReadLine(), out int teacherAction);
				switch (teacherAction)
				{
					case 1:
						invalid = true;
						Console.Write("muellimin idsi: ");
						while (invalid)
						{
							try
							{
								int.TryParse(Console.ReadLine(), out int teacherId);
								teacherService.FindById(teacherId);
								Console.WriteLine("imtina etmek ucun bos enter sixin");
								Teacher teacher = teacherService.FindById(teacherId);
								Console.WriteLine($"kohne ad: {teacher.Name}, kohne soyad: {teacher.Surname}");
								Console.Write("muellimin yeni adini daxil edin: ");
								string? newName = Console.ReadLine().Trim();
								Console.Write("muellimin yeni soyadini daxil edin: ");
								string? newSurname = Console.ReadLine().Trim();
								if (!string.IsNullOrEmpty(newName) && !string.IsNullOrEmpty(newSurname))
								{
									List<int> groupsId = teacherService.GetGroups(teacherId);
									Teacher updatedTeacher = new(teacherId, newName, newSurname, groupsId);
									teacherService.Update(teacherId, updatedTeacher);
								}
								invalid = false;
							}
							catch (TeacherNotFoundException ex)
							{
								Console.WriteLine(ex.Message);
							}
						}
						break;
					case 2:
						Console.Write("muellim idsi: ");
						bool invalidId = true;
						while (invalidId)
						{
							try
							{
								int.TryParse(Console.ReadLine(), out int teacherId);
								teacherService.FindById(teacherId);
								invalidId = false;
								teacherService.Delete(teacherId);
								groupService.RemoveTeacher(teacherId);
							}
							catch (TeacherNotFoundException ex)
							{
								Console.WriteLine(ex.Message);
							}
						}
						break;
					case 3:
						if (groupService.Count != 0)
						{
							Console.Write("muellimin idsi: ");
							invalid = true;
							while (invalid)
							{
								try
								{
									int.TryParse(Console.ReadLine(), out int teacherId);
									teacherService.FindById(teacherId);
									foreach (Group group in groupService.GetAll())
									{
										Console.WriteLine($"{group.Id}-{group.Name}");
									}
									Console.Write("qrupun idsi: ");
									while (invalid)
									{
										try
										{
											int.TryParse(Console.ReadLine(), out int groupId);
											groupService.FindById(groupId);
											try
											{
												groupService.AddTeacher(groupId, teacherId);
												teacherService.AddToGroup(teacherId, groupId);
											}
											catch (TeacherAlreadyInGroupException ex)
											{
												Console.WriteLine(ex.Message);
											}
											catch (TeacherAlreadyAssignedException ex)
											{
												Console.WriteLine(ex.Message);
											}
											invalid = false;
										}
										catch (GroupNotFoundException ex)
										{
											Console.WriteLine(ex.Message);
										}
									}
								}
								catch (TeacherNotFoundException ex)
								{
									Console.WriteLine(ex.Message);
								}
							}
						}
						else
						{
							Console.WriteLine("sistemde hec bir qrup movcud deyil");
							break;
						}
						break;
					default:
						break;
				}
			}
			else
			{
				Console.WriteLine("sistemde hec bir muellim movcud deyil");
			}
			break;
		case 5:
			id = studentService.Count == 0 ? 1 : studentService.GetAll().Last().Id + 1;
			invalid = true;
			while (invalid)
			{
				Console.Write("telebe adi: ");
				string studentName = Console.ReadLine();
				Console.Write("telebe soyadi: ");
				string studentSurname = Console.ReadLine();
				if (!string.IsNullOrEmpty(studentName) && !string.IsNullOrEmpty(studentSurname))
				{
					invalid = false;
					Student student = new(id, studentName, studentSurname, 0);
					studentService.Create(student);
				}
				else
				{
					Console.WriteLine("melumatlari duzgun daxil edin");
				}
			}
			break;
		case 6:
			if (studentService.Count != 0)
			{
				foreach (Student student in studentService.GetAll().ToList())
				{
					Console.WriteLine($"{student.Id}-{student.Name} {student.Surname}");
					if (student.GroupId != 0)
					{
						int groupId = student.GroupId;
						Group group = groupService.FindById(groupId);
						if (group != null)
						{
							Console.Write("oldugu qrup: ");
							Console.WriteLine($"{group.Id} {group.Name}");
						}
					}
				}
				Console.WriteLine("    1. telebe melumatlarinda deyisiklik");
				Console.WriteLine("    2. telebeni silmek");
				Console.WriteLine("    3. telebeni qrupa elave etmek");
				int.TryParse(Console.ReadLine(), out int studentAcion);
				switch (studentAcion)
				{
					case 1:
						invalid = true;
						Console.Write("telebe idsini daxil edin: ");
						while (invalid)
						{
							try
							{
								int.TryParse(Console.ReadLine(), out int studentId);
								studentService.FindById(studentId);
								Console.WriteLine("imtina etmek ucun bos enter sixin");
								Student student = studentService.FindById(studentId);
								Console.WriteLine($"kohne ad: {student.Name}, kohne soyad: {student.Surname}");
								Console.Write("telebenin yeni adini daxil edin: ");
								string? newName = Console.ReadLine().Trim();
								Console.Write("telebenin yeni soyadini daxil edin: ");
								string? newSurname = Console.ReadLine().Trim();

								if (!string.IsNullOrEmpty(newName) && !string.IsNullOrEmpty(newSurname))
								{
									int groupId = studentService.GetGroup(studentId);
									Student updatedStudent = new(studentId, newName, newSurname, groupId);
									studentService.Update(studentId, updatedStudent);
								}
								invalid = false;
							}
							catch (StudentNotFoundException ex)
							{
								Console.WriteLine(ex.Message);
							}
						}
						break;
					case 2:
						Console.Write("telebe idsi: ");
						invalid = true;
						while (invalid)
						{
							try
							{
								int.TryParse(Console.ReadLine(), out int studentId);
								studentService.FindById(studentId);
								invalid = false;
								int groupId = studentService.GetGroup(studentId);
								if(groupId != 0)
								{
									groupService.RemoveStudent(groupId, studentId);
								}
								studentService.Delete(studentId);
							}
							catch (StudentNotFoundException ex)
							{
								Console.WriteLine(ex.Message);
							}
						}
						break;
					case 3:
						if (groupService.Count != 0)
						{
							Console.Write("telebenin idsi: ");
							invalid = true;
							while (invalid)
							{
								try
								{
									int.TryParse(Console.ReadLine(), out int studentId);
									studentService.FindById(studentId);
									if (studentService.GetGroup(studentId) == 0)
									{
										foreach (Group group in groupService.GetAll())
										{
											Console.WriteLine($"{group.Id}-{group.Name}");
										}
										Console.Write("qrupun idsi: ");
										try
										{
											int.TryParse(Console.ReadLine(), out int groupId);
											groupService.FindById(groupId);
											try
											{
												groupService.AddStudent(groupId, studentId);
												studentService.AddToGroup(studentId, groupId);
												invalid = false;
											}
											catch (StudentAlreadyInGroupException ex)
											{
												Console.WriteLine(ex.Message);
												break;
											}
										}
										catch (GroupNotFoundException ex)
										{
											Console.WriteLine(ex.Message);
										}
									}
									else
									{
										Console.WriteLine("telebe basqa qrupdadir");
										break;
									}
								}
								catch (StudentNotFoundException ex)
								{
									Console.WriteLine(ex.Message);
								}
							}
						}
						else
						{
							Console.WriteLine("sistemde hec bir qrup movcud deyil");
						}
						break;
					default:
						break;
				}
			}
			else
			{
				Console.WriteLine("sistemde hec bir telebe movcud deyil");
			}
			break;
		default:
			Console.WriteLine("zehmet olmasa menudan bir reqem daxil edin");
			break;
	}
}