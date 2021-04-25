using System;

namespace blab4
{
  public abstract class AbstractClass
  {
    public AbstractClass()
    {
      Console.WriteLine("1");
      this.Method();
      Console.WriteLine("2");
    }

    public abstract void Method();
  }

  public class ConcreteClass : AbstractClass
  {
    public ConcreteClass()
    {
      Console.WriteLine("3");
    }

    public override void Method()
    {
      Console.WriteLine("Hello from overridden method!");
    }
  }
  class Program
  {
    static void Main(string[] args)
    {
      AbstractClass instance = new ConcreteClass();
      instance.Method();
    }
  }
}
