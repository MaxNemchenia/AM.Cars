using System.ComponentModel.DataAnnotations;

namespace AM.Cars.Server.Domain.Entities;

public class Car
{
    public long Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    public string ImagePath { get; set; }
}
