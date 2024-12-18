namespace ToDo.API.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ExpireDate { get; set; }
        public decimal CompletePercent { get; set; }
    }
}
