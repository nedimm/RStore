using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RStore.Api.Dto.Author;

public class AuthorUpdateDto : BaseDto
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)] public string LastName { get; set; }

    [StringLength(250)]
    public string Bio { get; set; }
}
