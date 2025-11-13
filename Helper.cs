using System;
public static class Helper
{
  public static void WriteArray<T>(ICollection<T> c)
  {
    foreach (T i in c) { Console.WriteLine(i); }
  }
  public static void readNLine(string path, int n)
  {
    using (StreamReader s = new StreamReader(path))
    {
      int count = 0;
      string? line;
      while ((line = s.ReadLine()) != null && count < n)
      {
        Console.WriteLine(line);
        count++;
      }
    }
  }
  public static void printDictionary<K, V>(Dictionary<K, V> d) where K : notnull
  {
    foreach (var i in d)
    {
      Console.WriteLine($"{i.Key}: {i.Value}");
    }
  }
}