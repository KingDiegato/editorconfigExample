using editorconfigExample.DTO;

namespace editorconfigExample;

public class Example
{
  public static void Main()
  {
    Method();
  }

  public static void Method()
  {
    var instance = GetInstanceDto();
    Console.WriteLine("This one intended to be well formed.");
    Console.WriteLine($"Name: {instance.Name}");
    Console.WriteLine($"Description: {instance.Description}");
    Console.WriteLine($"IsEverythingOk?: {instance.MustBeLowerInStart}");
  }

  private static WellNamedDto GetInstanceDto()
  {
    return new WellNamedDto
    {
      Id = 1,
      Name = "Example",
      Description = "This is an example DTO.",
      IsEverythingOk = true,
      MustBeLowerInStart = "Everything is ok!"
    };
  }
}
