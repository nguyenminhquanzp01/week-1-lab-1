using System.Threading.Tasks;

public class TaskAsyncIO
{
  public static string path { get; set; } = "app.log";
  public static async Task testAsync() //20s chậm hơn nhiều 
  {
    Console.WriteLine("read file async");
    async Task readFile()
    {
      var text = await File.ReadAllTextAsync(path);
      Console.WriteLine(text.Length);
    }
    List<Task> tasks = new();
    for (int i = 0; i < 10; i++)
    {
      tasks.Add(readFile());
    }
    await Task.WhenAll(tasks);
  }
  public static void testNonAsync() // 10s
  {
    Console.WriteLine("read file non async");
    for (int i = 0; i < 10; i++)
    {
      var text = File.ReadAllText(path);
      Console.WriteLine(text.Length);
    }
  }
  public static async Task testAsync2()
  {
    Console.WriteLine("read file async2");
    var tasks = Enumerable.Range(0, 10)
    .Select(async _ =>
    {
      var text = await File.ReadAllTextAsync(path);
      Console.WriteLine(text.Length);
    });
    await Task.WhenAll(tasks);
  }
}