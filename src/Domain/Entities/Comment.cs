using Domain.Common;

namespace Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string? Text { get; set; }
        public int TaskId { get; set; }
    }
}
