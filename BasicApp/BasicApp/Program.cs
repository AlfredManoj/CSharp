using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = InstanceRestrict.Create();
            var simpleSingleTon = SimpleSingleTon.Create();
            var simpleSingleTon1 = SimpleSingleTon.Instance;
            if (simpleSingleTon.Equals(simpleSingleTon1))
            {
                Console.WriteLine("SimpleSingleTon Valid");
            }
            else
            {
                Console.WriteLine("SimpleSingleTon Invalid");
            }

            var single = SingleTonThreadSafeWithoutLock.Instance;
            var single1 = SingleTonThreadSafeWithoutLock.SingleTon;
            if (single.Equals(single1))
            {
                Console.WriteLine("SimpleSingleTon Valid");
            }
            else
            {
                Console.WriteLine("SimpleSingleTon Invalid");
            }

            if (args.Length == 0)
            {
                Console.WriteLine("No args");
            }
            else
            {
                Eager.StaticMethod();
            }

            Console.WriteLine("Before static method");
            Lazy.StaticMethod();
            Console.WriteLine("Before construction");
            Lazy lazy = new Lazy();
            Console.WriteLine("Before instance method");
            lazy.InstanceMethod();
            Console.WriteLine("Before static method using field");
            Lazy.StaticMethodUsingField();
            Console.WriteLine("End");

            var constDest = new ConstDecons();
            Console.WriteLine(constDest.IsSquare() ? "It is square" : "It is not square");
            (int len, int bre) = constDest;
            Console.WriteLine($"Len: {len}");
            Console.WriteLine($"Bre: {bre}");
            string test = "hi";
            Console.WriteLine(test?[0]);
            Console.WriteLine(SuperLazySingleTon.ToString());
            Console.WriteLine("Boxing and Unboxing");
            int a = 1;
            object b = a; // boxing a value type to ref type
            int c = (int)b; // unboxing ref type to value type
            Console.Write("Generic");
            Inherited1 in1 = new Inherited1() { C = 1, D = 2 };
            Inherited2 in2 = new Inherited2() { C = 3, D = 4 };
            BaseClass baseClass = AddTwo<BaseClass>(in1, in2);
            Console.WriteLine($"Base Class = C:{baseClass.C} D:{baseClass.D}");
            var resultCompare = Max<BaseClass>(in1, in2);
            Console.WriteLine($"Comparison : {resultCompare.D}");
            Console.ReadLine();
        }

        public static T AddTwo<T>(T obj1, T obj2) where T : BaseClass
        {
            var baseclass = new BaseClass();
            baseclass.C = obj1.C + obj2.C;
            baseclass.D = obj1.D + obj2.D;
            return (T)baseclass;
        }

        public static T Max<T>(T param1, T param2) where T : IComparable<T>
        {
            return param1.CompareTo(param2) > 0 ? param1 : param2;
        }
    }

    public class Stack<T>
    {
        int position;
        T[] data = new T[100];

        public void Push(T obj) => data[position++] = obj;

        public T Pop() => data[--position];
    }

    public class BaseClass : IComparable<BaseClass>
    {
        private int A { get; set; }
        protected int B { get; set; }
        internal virtual int C { get; set; } = 0;
        public int D { get; set; }

        public int CompareTo(BaseClass other)
        {
            return this.D > other.D ? 1 : 0;
        }
    }

    public class Inherited1 : BaseClass
    {
        internal override int C { get => base.C; set => base.C = (value + 1); }

        public Inherited1()
        {

        }
    }

    public class Inherited2 : BaseClass
    {
        public Inherited2()
        {

        }
    }

    public struct StructExample
    {
        int x, y;
        /// <summary>
        /// parameterized constructor only possible
        /// parameter less constructor not possible. 
        /// But we can invoke by simple StructExample()
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public StructExample(int a,int b)
        {
            x = a;
            y = b;
        }

        //public StructExample()
        //{
        //    x = 1;
        //    y = 3;
        //} 
    }

    public static class Extensions
    {
        public static bool IsSquare(this ConstDecons constDecons)
        {
            return constDecons.length == constDecons.breadth;
        }
    }

    public class InstanceRestrict
    {
        private InstanceRestrict()
        {

        }

        public static InstanceRestrict Create()
        {
            var obj = new InstanceRestrict();
            return obj;
        }
    }

    public sealed class SimpleSingleTon
    {
        private static SimpleSingleTon singleTon = null;

        private SimpleSingleTon()
        {

        }

        public static SimpleSingleTon Create()
        {
            if (singleTon == null)
            {
                singleTon = new SimpleSingleTon();
            }
            return singleTon;
        }

        public static SimpleSingleTon Instance
        {
            get
            {
                if (singleTon == null)
                {
                    singleTon = new SimpleSingleTon();
                }
                return singleTon;
            }
        }
    }

    public sealed class SimpleSingleTonThreadSafe
    {
        private static SimpleSingleTonThreadSafe singleTon = null;
        private static readonly object lockObject = new object();

        private SimpleSingleTonThreadSafe()
        {

        }

        public static SimpleSingleTonThreadSafe Instance
        {
            get
            {
                lock(lockObject)
                {
                    if (singleTon == null)
                    {
                        singleTon = new SimpleSingleTonThreadSafe();
                    }
                    return singleTon;
                }
            }
        }
    }

    public sealed class SingleTonThreadSafeWithoutLock
    {
        private static readonly SingleTonThreadSafeWithoutLock singleTon = new SingleTonThreadSafeWithoutLock();

        private SingleTonThreadSafeWithoutLock()
        {

        }

        static SingleTonThreadSafeWithoutLock()
        {

        }

        public static SingleTonThreadSafeWithoutLock Instance
        {
            get
            {
                return singleTon;
            }
        }

        public static SingleTonThreadSafeWithoutLock SingleTon => singleTon;
    }

    public sealed class BestLazySingleTn
    {
        private BestLazySingleTn()
        {

        }

        /// <summary>
        /// Only nested reference is here, so np
        /// </summary>
        public static BestLazySingleTn Instance => NestedItem.Instance;

        private class NestedItem
        {
            static NestedItem()
            {

            }

            internal static readonly BestLazySingleTn Instance = new BestLazySingleTn();
        }
    }

    /// <summary>
    /// Best way to create a singeton which is threadsafe
    /// </summary>
    public sealed class SuperLazySingleTon
    {
        private SuperLazySingleTon()
        {

        }

        private static readonly Lazy<SuperLazySingleTon> superLazySingleTon = new Lazy<SuperLazySingleTon>(() => new SuperLazySingleTon());
        
        /// <summary>
        /// the best way to create
        /// </summary>
        public static SuperLazySingleTon Instance => superLazySingleTon.Value;

        public static new string ToString()
        {
            return nameof(SuperLazySingleTon);
        }
    }

    /// <summary>
    /// in new C# static constructor is not necessary (previously because of the beforeinit issue)
    /// Now this is being taken care from 4.0 f/w onwards.
    /// </summary>
    class Eager
    {
        private static int x = Log();

        private static int Log()
        {
            Console.WriteLine("Type Initialized");
            return 0;
        }

        public static void StaticMethod() { }
    }

    /// <summary>
    /// very good example to the initialization pattern change in 4.6
    /// in previous versions upto 4, the static fields will only be
    /// initialized before calling some static methods which involve
    /// static fields i.e in previous .net f/w versions "Type initialized"
    /// will be printed before calling StaticMethodUsingField and now it is
    /// printed before the first static method call
    /// </summary>
    class Lazy
    {
        private static int x = Log();
        private static int y = 0;

        private static int Log()
        {
            Console.WriteLine("Type Initialized");
            return 0;
        }

        public static void StaticMethod()
        {
            Console.WriteLine("In static method");
        }

        public static void StaticMethodUsingField()
        {
            Console.WriteLine("In static method using filed");
            Console.WriteLine("y = {0}", y);
        }

        public void InstanceMethod()
        {
            Console.WriteLine("In Instance Method");
        }
    }

    public class ConstDecons
    {
        public readonly int length, breadth;
        public ConstDecons()
        {
            length = 10;
            breadth = 10;
        }

        public void Deconstruct(out int len,out int bre)
        {
            len = length;
            bre = breadth;
        }
    }
}
