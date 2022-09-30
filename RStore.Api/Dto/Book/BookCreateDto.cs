using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RStore.Api.Dto.Book;

public class BookCreateDto
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
