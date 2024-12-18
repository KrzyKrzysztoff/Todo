namespace ToDo.API.DTOs
{
    public class TodoDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public decimal PercentComplete { get; set; }
        public bool IsDone { get; set; }
    }
}
