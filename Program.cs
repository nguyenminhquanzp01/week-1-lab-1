public class Program
{
  static async Task Main(string[] args)
  {
    string path = @"example.log";
    long targetLines = 8_000_000;
    bool generate = false;

    if (generate || !File.Exists(path))
    {
      Console.WriteLine($"Generating log file '{path}' with {targetLines:N0} lines...");
      Helper.GenerateLogFile(path, targetLines);
      Console.WriteLine("Generate finished.");
    }
    FreqSubstring f = new() { FilePath = path };
    long fileSize = new System.IO.FileInfo(path).Length;
    Console.WriteLine("file path: " + f.FilePath);
    Console.WriteLine($"file size: {fileSize / 1024d / 1024d} MB");
    var watch = System.Diagnostics.Stopwatch.StartNew();
    // f.doTask();
    f.doTaskParallel();
    // f.doTaskChunk();
    // TaskAsyncIO.testNonAsync();
    // await TaskAsyncIO.testAsync();
    // await TaskAsyncIO.testAsync2();
    watch.Stop();

    var elapsedMs = watch.ElapsedMilliseconds;
    Console.WriteLine($"done - Execution time : {elapsedMs}ms");
    Console.WriteLine($"record num: {f.freq.Count}");
    Thread.Sleep(2000);
    var result = f.freq.ToDictionary();


    result.PrintTopBy(i => i.Value, 10);
  }
}