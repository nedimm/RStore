using System.ComponentModel.DataAnnotations;

namespace RStore.Api.Dto.Book;

public class BookUpdateDto : BaseDto
{
    [Required]
    [StringLength(50)]
    public string Title { get; set; }
    [Required]
    [Range(1800, int.MaxValue)]
    public int Year { get; set; }
    [Required]
    public string Isbn { get; set; }
    [Required]
    public string Summary { get; set; }
    public string Image { get; set; }
    [Required]
    [Range(0, int.MaxValue)]
    public double Price { get; set; }
}
