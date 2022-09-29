using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RStore.Api.Dto.Author;

public class AuthorCreateDto
{
    [Required]
    [StringLength(50)]
    public string Firstname { get; set; }
    
    [Required]
    [StringLength(50)] public string Lastname { get; set; }

    [StringLength(250)] 
    public string Bio { get; set; }
}
