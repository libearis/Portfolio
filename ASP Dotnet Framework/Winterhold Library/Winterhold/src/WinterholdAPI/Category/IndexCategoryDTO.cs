namespace WinterholdAPI.Category;

public class IndexCategoryDTO
{
    public List<CategoryDTO> Categories { get; set; }
    public PaginationVM Pagination { get; set; }
    public string Name { get; set; }
}
