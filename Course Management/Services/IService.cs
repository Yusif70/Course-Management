using Course_Management.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Management.Services
{
	public interface IService<T> where T : Entity
	{
		void Create(T entity);
		void Update(int id, T entity);
		void Delete(int id);
		List<T> GetAll();
		T FindById(int id);
	}
}
