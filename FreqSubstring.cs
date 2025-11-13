using System.Collections.Concurrent;
using System.Dynamic;

public class FreqSubstring
{
  public ConcurrentDictionary<String, int> freq { get; } = new ConcurrentDictionary<string, int>();
  public char[] delimiter = { ' ', ',', ':' };
  public string FilePath { get; set; } = "example.log";
  public void doTask()
  {
    Console.WriteLine("linear");
    try
    {
      using (StreamReader sr = new StreamReader(FilePath))
      {
        string? line;
        while ((line = sr.ReadLine()) != null)
        {
          var split = line.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
          foreach (string i in split)
          {
            freq[i] = freq.GetValueOrDefault(i, 0) + 1;
          }
        }
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }
  public void doTaskParallel()
  {
    Console.WriteLine("parallel");
    try
    {
      var lines = File.ReadLines(FilePath);
      Parallel.ForEach(lines, line =>
      {
        var split = line.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
        foreach (string word in split)
        {
          freq.AddOrUpdate(word, 1, (_, old) => old + 1);
        }
      });
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }
  public void doTaskChunk()
  {
    Console.WriteLine("chunk");
    Parallel.ForEach(File.ReadLines(FilePath),
    new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
    () => new Dictionary<string, int>(), // dictionary cho mỗi thread
    (line, _, localDict) =>
    {
      foreach (var word in line.Split(delimiter, StringSplitOptions.RemoveEmptyEntries))
      {
        if (localDict.TryGetValue(word, out int count))
          localDict[word] = count + 1;
        else
          localDict[word] = 1;
      }
      return localDict;
    },
    localDict =>
    {
      foreach (var kvp in localDict)
      {
        freq.AddOrUpdate(kvp.Key, kvp.Value, (_, old) => old + kvp.Value);
      }
    });
  }
}