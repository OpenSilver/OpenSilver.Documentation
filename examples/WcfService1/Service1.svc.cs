using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace WcfService1
{
    public class Service1 : IService1
    {
        private static Dictionary<Guid, ToDoItem> _todos = new Dictionary<Guid, ToDoItem>();

        public List<ToDoItem> GetToDos()
        {
            return _todos.Values.ToList();
        }

        public void AddOrUpdateToDo(ToDoItem toDoItem)
        {
            _todos[toDoItem.Id] = toDoItem;
        }

        public void DeleteToDo(ToDoItem toDoItem)
        {
            if (_todos.ContainsKey(toDoItem.Id))
                _todos.Remove(toDoItem.Id);
            else
                throw new FaultException("ID not found: " + toDoItem.Id);
        }
    }
}