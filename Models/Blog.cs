namespace MVCusingEFCoreDBFirst.Models
{
    public class Blog
    {
        public string BlogId { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Picture { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
