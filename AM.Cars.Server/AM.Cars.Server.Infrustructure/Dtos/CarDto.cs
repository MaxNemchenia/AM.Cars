using System.Text.Json.Serialization;

namespace AM.Cars.Server.Infrustructure.Dtos;

public class CarDto
{
    public long Id { get; set; }

    public string Name { get; set; }

    public byte[] Image { get; set; }

    [JsonIgnore]
    public string ImagePath { get; set; }
}
