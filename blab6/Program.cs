using System;
using System.Collections;

namespace blab6
{
  public class Element
  {
    public Element(string name, int field1, int field2)
    {
      Name = name;
      Field1 = field1;
      Field2 = field2;
    }

    public string ToString()
    {
      return $"Name: {Name}, Field1: {Field1}, Field2: {Field2}";
    }
    public int Field1 { get; set; }
    public int Field2 { get; set; }
    public string Name { get; set; }
  }

  public partial class UserCollection : IEnumerable
  {
    Element[] elementsArray;

    public UserCollection(int size=4)
    {
      elementsArray = new Element[size];
      for (int i = 0; i < size; i++)
      {
        elementsArray[i] = new Element($"Element {i}", i, -i);
      }
    }
    public IEnumerator GetEnumerator()
    {
      return new Enumerator(this);
    }
  }

  public partial class UserCollection : IEnumerable
  {
    private class Enumerator : IEnumerator
    {
      UserCollection parrent;
      int position = -1;

      public Enumerator(UserCollection parrent)
      {
        this.parrent = parrent;
      }

      public object Current { get { return parrent.elementsArray[position]; } }

      public bool MoveNext()
      {
        return ++position < parrent.elementsArray.Length;
      }

      public void Reset()
      {
        position = -1;
      }
      public void Dispose() { }

    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      UserCollection ls = new UserCollection();
      
      foreach (Element element in ls)
      {
        Console.WriteLine(element.ToString());
      }

      foreach (Element element in ls)
      {
        Console.WriteLine(element.ToString());
      }

      IEnumerable enumerable = new UserCollection();
      IEnumerator enumerator = enumerable.GetEnumerator();

      while (enumerator.MoveNext())
      {
        Element element = enumerator.Current as Element;
        Console.WriteLine(element.ToString());
      }
      enumerator.Reset();
    }
  }
}
