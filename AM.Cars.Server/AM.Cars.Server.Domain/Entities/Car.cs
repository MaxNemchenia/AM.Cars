using System.ComponentModel.DataAnnotations;

namespace AM.Cars.Server.Domain.Entities;

public class Car
{
    public long Id { get; set; }

    [Required]
    public string FileName { get; set; }

    [Required]
    public string ImagePath { get; set; }
}
