using System.Text.Json.Serialization;

namespace editorconfigExample.DTO;

public class WellNamedDto
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string? Description { get; set; }
  public bool IsEverythingOk { get; set; } = true;
  [JsonPropertyName("mustBeLowerInStart")]
  public string? MustBeLowerInStart { get; set; }
}
