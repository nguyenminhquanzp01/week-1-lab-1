using System;
public class Indexer
{
  public int id { get; set; }
  public string name { get; set; }
  public int[] LoveNumber = new int[100];
  public int this[int index]
  {
    get => LoveNumber[index];
    set => LoveNumber[index] = value;
  }
}