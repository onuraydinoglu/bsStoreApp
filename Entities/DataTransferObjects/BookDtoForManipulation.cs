using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects;

public abstract record BookDtoForManipulation
{
    [Required(ErrorMessage = "Title is a required field.")]
    [MinLength(2, ErrorMessage = "Title must consist of at leasd 2 characters")]
    [MaxLength(50, ErrorMessage = "Title must consist of at maximumm 50 characters")]
    public String Title { get; init; }


    [Required(ErrorMessage = "Title is a required field.")]
    [Range(10,1000)]
    public decimal Price { get; init; }
}
