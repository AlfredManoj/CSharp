using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AdvancedApp
{
    delegate int SquareDelegate(int x);

    delegate void PrintSquareEventHandlerDelegate(int x);

    class Program
    {
        static int Square(int num) => num * num;

        static void Main(string[] args)
        {
            Console.Write("Number to square: ");
            int.TryParse(Console.ReadLine(), out int value);
            SquareDelegate square = Square;
            Console.WriteLine($"Square is: {square.Invoke(value)}");
            var collectionCheck = new CollectionCheck();
            collectionCheck.AddItem("A");
            collectionCheck.AddItem("B");
            collectionCheck.AddItem("C");
            collectionCheck.AddItem("D");
            collectionCheck.AddItem("E");
            var items = collectionCheck.Items;
            items.ToList();
            Console.WriteLine($"Const are static: {CollectionCheck.CONST_ARE_STATIC}");
            Console.ReadLine();
        }
    }

    class DelegateEventsClass
    {
        public event PrintSquareEventHandlerDelegate PrintSquareEventNotifier;
    }

    class CollectionCheck
    {
        private readonly IList<string> m_items = new List<string>();

        public const int CONST_ARE_STATIC = 20;

        public IReadOnlyCollection<string> Items => new ReadOnlyCollection<string>(m_items);

        public void AddItem(string item)
        {
            m_items.Add(item);
        }
    }

    public static class Extension
    {
        public static TransactionScope CreateLockTransaction()
        {
            var options = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };
            return new TransactionScope(TransactionScopeOption.Required, options);
        }

        /// <summary>
        /// This is just another version of the ToList method that you have
        /// with an IEnumberable set but with a transaction Scope.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this IEnumerable<T> query)
        {
            using (TransactionScope ts = CreateLockTransaction())
            {
                return System.Linq.Enumerable.ToList(query);
            }
        }
    }
}
