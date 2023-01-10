using System.ComponentModel.DataAnnotations;

namespace Library.Models.ProductDtos;

public class ProductCategoryForUpdateDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
}
