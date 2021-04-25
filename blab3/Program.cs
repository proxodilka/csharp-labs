using System;

namespace blab3
{
  class BaseClass
  {
    public virtual void Method()
    {
      Console.WriteLine("Hello from Base class");
    }
  }

  class DerivedClass : BaseClass
  {
    public override void Method()
    {
      Console.WriteLine("Hello from Derived class");
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      DerivedClass dInstance = new DerivedClass();
      dInstance.Method(); // Hello from Derived class

      // Down cast
      BaseClass bInstance = dInstance;
      bInstance.Method(); // Hello from Derived class

      // Up cast
      DerivedClass uInstance = (bInstance as DerivedClass);
      uInstance.Method(); // Hello from Derived class
    }
  }
}
