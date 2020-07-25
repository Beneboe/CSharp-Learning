using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericInheritance
{
    class Base {}
    class Child1 : Base {}
    class Child2 : Base {}
    class Child3 : Base {}


    // This class needs to exist.
    class Box {}
    class Box<T> : Box where T : Base {}

    class Program
    {
        static void Main(string[] args)
        {
            // I want to store the Child-classes in a single collection. My first idea is having a List of Box<Base> ...
            List<Box<Base>> genericBoxes = new List<Box<Base>>();

            // ... and then add the child classes. But this will not work because Box<Child1> does not inherit from
            // Box<Base>.
            // genericBoxes.Add(new Box<Child1>());

            // But because Box<T> inherits from Box and therefore Box<Child1> also inherits from Box...
            List<Box> baseBoxes = new List<Box>();

            // ... I can add now the child classes.
            baseBoxes.Add(new Box<Child1>());
            baseBoxes.Add(new Box<Child1>());
            baseBoxes.Add(new Box<Child2>());
            baseBoxes.Add(new Box<Child3>());

            // I can now also count the occurences.
            Console.WriteLine(CountMessage<Box, Box<Child1>>(baseBoxes));
            Console.WriteLine(CountMessage<Box, Box<Child2>>(baseBoxes));
            Console.WriteLine(CountMessage<Box, Box<Child3>>(baseBoxes));
        }

        static string CountMessage<TBase, TChild>(IEnumerable<TBase> collection)
            where TChild : TBase
        {
            var count = collection.OfType<TChild>().Count();
            var name = typeof(TChild).GetGenericArguments().First().Name;
            return $"The collections contains {count} boxes containing a {name}.";
        }
    }
}
