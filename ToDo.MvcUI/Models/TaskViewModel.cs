namespace ToDo.MvcUI.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int State { get; set; } 
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int UserId { get; set; }
    }
}
