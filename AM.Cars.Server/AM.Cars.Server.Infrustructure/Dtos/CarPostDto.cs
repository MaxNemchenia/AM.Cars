using System.Text.Json.Serialization;

namespace AM.Cars.Server.Infrustructure.Dtos;

public class CarPostDto
{
    /// <summary>
    /// Gets or sets the name of the car.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the base64 encoded image.
    /// </summary>
    public string Image { get; set; }

    /// <summary>
    /// Gets or sets the image path (not serialized).
    /// </summary>
    [JsonIgnore]
    public string? ImagePath { get; set; }
}
