using System.Text.Json;

namespace AM.Cars.Client.Application.Extensions;

public static class StreamX
{
    public static async Task<T> DeserializeJsonAsync<T>(this Stream stream)
    {
        if (stream == default || !stream.CanRead)
        {
            return default;
        }

        var json = await stream.ToStringAsync();

        if (string.IsNullOrEmpty(json))
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public static async Task<string> ToStringAsync(this Stream stream)
    {
        if (stream == default)
        {
            return default;
        }

        using var streamReader = new StreamReader(stream);

        return await streamReader.ReadToEndAsync();
    }
}
