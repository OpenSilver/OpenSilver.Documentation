using Microsoft.AspNetCore.Mvc;
using ToDoWebApi.Models;

namespace ToDoWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private static readonly Dictionary<Guid, ToDoItem> ToDos = new();

        // GET api/ToDo
        [HttpGet]
        public IEnumerable<ToDoItem> GetToDos()
        {
            return ToDos.Values.ToList();
        }

        // GET api/ToDo/5
        [HttpGet("{id}")]
        public ActionResult GetToDo(Guid id)
        {
            if (ToDos.ContainsKey(id))
            {
                return Ok(ToDos[id]);
            }

            return NotFound("ID not found: " + id);
        }

        // PUT api/Todo/5
        [HttpPut("{id}")]
        public ActionResult PutToDoItem(Guid id, ToDoItem toDoItem)
        {
            if (id != toDoItem.Id)
            {
                return BadRequest("The ID must be the same as the ID of the todo");
            }

            ToDos[toDoItem.Id] = toDoItem;
            return Ok();
        }

        // POST api/Todo
        [HttpPost]
        public ActionResult PostToDoItem(ToDoItem toDoItem)
        {
            ToDos[toDoItem.Id] = toDoItem;

            return Ok();
        }

        // DELETE api/Todo/5
        [HttpDelete("{id}")]
        public ActionResult DeleteToDoItem(Guid id)
        {
            if (!ToDos.ContainsKey(id))
            {
                return NotFound("ID not found: " + id);
            }

            ToDos.Remove(id);
            return Ok();
        }
    }
}
