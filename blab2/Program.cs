using System;

namespace blab1
{
  class BaseClass
  {
    public void Method()
    {
      Console.WriteLine("Hello from Base class");
    }
  }

  class DerivedClass : BaseClass
  {
    public void Method()
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
        bInstance.Method(); // Hello from Base class

        // Up cast
        DerivedClass uInstance = (bInstance as DerivedClass);
        uInstance.Method(); // Hello from Derived class
    }
  }
}
