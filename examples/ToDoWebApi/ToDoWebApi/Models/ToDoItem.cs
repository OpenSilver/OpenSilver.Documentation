namespace ToDoWebApi.Models
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string? Description { get; set; }
    }
}
