using WinterholdAPI.Validations;

namespace WinterholdAPI.Category;

public class CategoryDTO
{
    [UniqueCategoryName]
    public string Name { get; set; } = null!;
    public int Floor { get; set; }
    public string Isle { get; set; } = null!;
    public string Bay { get; set; } = null!;
    public int TotalBook { get; set; }
}
