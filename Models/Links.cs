namespace MVCusingEFCoreDBFirst.Models
{
    public class Links
    {
        public int LinkID { get; set; }
        public string? LinkName { get; set; }
        public string? LinkURL { get; set; }
        public string? LinkDescription { get; set; }

        public CategoryModel? Category { get; set; }
    }
    public class CategoryModel
    {
        public string? CategoryName { get; set; }
    }
}
