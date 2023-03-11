namespace Tello.Api.Test.DTOs.Comment
{
    public class CommentGetDto
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string AppUserId { get; set; }
        public string AppName { get; set; }
        public List<string> Variations { get; set; }

    }
}
