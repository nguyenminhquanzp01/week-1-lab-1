using System.Data.Common;
using System.Data.SqlTypes;
using System.Security.Cryptography.X509Certificates;

class Student
{
  public int StudentId
  {
    get;
    set;
  }
  public string? StudentName { get; set; }
  public int Mark { get; set; }
  public string? City { get; set; }
  public Student(int id, string name, int mark, string city)
  {
    StudentId = id;
    StudentName = name;
    Mark = mark;
    City = city;
  }
  public override string ToString()
  {
    return $"id = {StudentId}, name = {StudentName}, mark = {Mark}, city = {City}";
  }
  public Student() {} 
}