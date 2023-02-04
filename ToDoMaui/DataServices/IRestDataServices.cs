using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoMaui.Models;

namespace ToDoMaui.DataServices
{
		public interface IRestDataServices
		{
				Task<List<ToDo>> GetAllToDos();
				Task AddToDoAsync(ToDo toDo);
				Task UpdateToDoAsync(ToDo toDo);
				Task DeleteToDoAsync(int toDo);
		}
}
