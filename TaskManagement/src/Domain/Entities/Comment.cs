namespace Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int TaskId { get; set; }
    }
}
