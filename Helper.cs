using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
public static class Helper
{
  public static void WriteArray<T>(ICollection<T> c)
  {
    foreach (T i in c) { Console.WriteLine(i); }
  }
  public static void ReadNLine(string path, int n)
  {
    using StreamReader s = new(path);
    int count = 0;
    string? line;
    while ((line = s.ReadLine()) != null && count < n)
    {
      Console.WriteLine(line);
      count++;
    }
  }
  public static void PrintDictionary<K, V>(Dictionary<K, V> d) where K : notnull
  {
    foreach (var i in d)
    {
      Console.WriteLine($"{i.Key}: {i.Value}");
    }
  }
  public static void PrintTopBy<T, TKey>(this IEnumerable<T> collection, Func<T, TKey> keySelector, int top = 10)
  {
    {
      ArgumentNullException.ThrowIfNull(collection);
      ArgumentNullException.ThrowIfNull(keySelector);
      ArgumentOutOfRangeException.ThrowIfNegativeOrZero(top);
      var result = collection.OrderByDescending(keySelector).Take(top);
      Console.WriteLine($"===Top {top} items ===");

      int index = 1;
      foreach (T item in result)
      {
        Console.WriteLine($"{index++,2}: {item})");
      }
    }
  }
  public static void GenerateLogFile(string path, long lines)
  {
    string[] levels = ["INFO", "WARN", "ERROR"];
    string[] errorTypes = ["Database", "IO", "Auth", "Network", "UI", "BusinessRule", "Timeout"];
    var rand = new Random(1234);

    // Use buffered FileStream + StreamWriter for performance; set a large buffer
    using var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 1 << 20, FileOptions.SequentialScan);
    using var sw = new StreamWriter(fs);

    for (long i = 0; i < lines; i++)
    {
      var level = levels[rand.Next(levels.Length)];
      var ts = DateTime.UtcNow.AddSeconds(-rand.Next(0, 60 * 60 * 24)).ToString("o");
      if (level == "ERROR")
      {
        var t = errorTypes[rand.Next(errorTypes.Length)];
        sw.WriteLine($"{ts} [{level}] Type={t}; Id={rand.Next(1, 100000)}; Message=Something bad happened #{i}");
      }
      else if (level == "WARN")
      {
        var t = errorTypes[rand.Next(errorTypes.Length)];
        sw.WriteLine($"{ts} [{level}] Type={t}; Message=Be careful #{i}");
      }
      else
      {
        sw.WriteLine($"{ts} [{level}] Message=Regular event #{i}");
      }

      // occasional flush to keep memory usage controllable
      if (i % 100_000 == 0) sw.Flush();
    }
  }
}