using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Microsoft.Win32.SafeHandles;

public class Program
{
  static async Task Main(string[] args)
  {
    // // readNLine(path, 100);
    // Student[] students = new[]
    // {
    //   new Student(1, "John", 73, "NYC"),
    //   new Student(2, "Modal", 4, "HN"),
    //   new Student(3, "Chair", 7, "HN"),
    //   new Student(4, "Chat", 3, "HN"),
    //   new Student(5, "Fan", 733, "NYC"),
    //   new Student(6, "Nak", 713, "NYC"),
    // };
    // var res = from s in students orderby s.Mark ascending where s.StudentName.Contains("C") select s;
    string path = @"C:\Users\quan\Downloads\HDFS_v3_TraceBench\preprocessed\normal_trace.csv";
    FreqSubstring f = new FreqSubstring() { FilePath = path };
    long fileSize = new System.IO.FileInfo(path).Length;
    Console.WriteLine("file: " + f.FilePath);
    Console.WriteLine("file size: " + fileSize / 1024d / 1024d);
    var watch = System.Diagnostics.Stopwatch.StartNew();
    f.doTask();
    // f.doTaskParallel();
    // f.doTaskChunk();
    // TaskAsyncIO.testNonAsync();
    // await TaskAsyncIO.testAsync();
    // await TaskAsyncIO.testAsync2();
    watch.Stop();

    var elapsedMs = watch.ElapsedMilliseconds;
    Console.WriteLine($"done - Execution time : {elapsedMs}ms");
    Console.WriteLine($"record num: {f.freq.Count}");
    System.Threading.Thread.Sleep(2000);
    // printDictionary<String, int>(freq);
    // int[] arr = { 1, 2, 3, 4, 5 };/
    // Task<int> sumUp = Task.Run(() =>
    // {
    //   int sum = 0;
    //   foreach (int i in arr)
    //   {
    //     sum += i;
    //   }
    //   return sum;
    // });
    // await sumUp.ContinueWith((res) => Console.WriteLine(res.Result));
    
  }
}