using System;

namespace blab5
{
  interface Interface1
  {
    void Method1();
  }

  interface Interface2
  {
    void Method2();
  }

  class Base
  {
    public void Method()
    {
      Console.WriteLine("Base.Method");
    }
  }

  class Derived : Base, Interface1, Interface2
  {
    public void Method1()
    {
      Console.WriteLine("Derived.Interface1.Method1");
    }

    public void Method2()
    {
      Console.WriteLine("Derived.Interface2.Method2");
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      Derived dInstance = new Derived();
      dInstance.Method();
      dInstance.Method1();
      dInstance.Method2();

      Base instance0 = dInstance;
      instance0.Method();

      Interface1 instance1 = dInstance;
      instance1.Method1();

      Interface2 instance2 = dInstance;
      instance2.Method2();
    }
  }
}
