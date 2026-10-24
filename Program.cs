using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace G_NET_65_LINQ01
{
    class Order
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
    }
    class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int UnitsInStock { get; private set; }

        public Product(int productId, string productName, string category, decimal price, int unitsInStock)
        {
            ProductID = productId;
            ProductName = productName;
            Category = category;
            Price = price;
            UnitsInStock = unitsInStock;
        }
    }

    public static class listExtension
    {
        public static List<int> WHERE(this IEnumerable<int> List, Predicate<int> op) { 
            var result = new List<int>();
            foreach (var x in List) {
                if (op(x))
                    result.Add(x);
            }
            return result;
        } 
    }

    internal class Program
    {

        static void print<T>(IEnumerable<T> list, Func<T, string> selector, string title = "")
        {
            Console.WriteLine($"{title}[{string.Join(", ", list.Select(x => selector(x)?.ToString()))}]");
        }
        static void Calc<T>(T a, T b, Action<T,T> op) where T : INumber<T>
        {
            op(a, b);
        }

        static List<Product> ProductList = new List<Product>
        {
            new Product(1, "Chai", "Beverages", 18m, 39),
            new Product(2, "Chang", "Beverages", 19m, 17),
            new Product(3, "Coffee", "Beverages", 30m, 25),
            new Product(4, "Green Tea", "Beverages", 12m, 0),
            new Product(5, "Salmon", "Seafood", 250m, 25),
            new Product(6, "Shrimp", "Seafood", 320m, 18),
            new Product(7, "Tuna", "Seafood", 180m, 30),
            new Product(8, "Sardines", "Seafood", 90m, 50),
            new Product(9, "Squid", "Seafood", 210m, 0),
            new Product(10, "Crab", "Seafood", 28m, 12),
            new Product(11, "Chicken Breast", "Meat", 160m, 40),
            new Product(12, "Beef", "Meat", 350m, 15),
            new Product(13, "Cheddar Cheese", "Dairy", 120m, 22),
            new Product(14, "Milk", "Dairy", 45m, 60),
            new Product(15, "Yogurt", "Dairy", 15m, 0),
            new Product(16, "Mustard", "Condiments", 12m, 20),
            new Product(17, "Hot Sauce", "Condiments", 8m, 0),
            new Product(18, "Ketchup", "Condiments", 25m, 15),
            new Product(19, "Mayonnaise", "Condiments", 32m, 7),
            new Product(20, "Rice", "Grains", 70m, 100),
            new Product(21, "Wheat Flour", "Grains", 22m, 45),
            new Product(22, "Corn", "Grains", 9m, 0)
        };

        static List<Order> OrderList = new List<Order>
        {
            new Order { OrderID = 1, CustomerID = "ALFKI", OrderDate = new DateTime(1996, 5, 10) },
            new Order { OrderID = 2, CustomerID = "ANATR", OrderDate = new DateTime(1996, 11, 20) },
            new Order { OrderID = 3, CustomerID = "ANTON", OrderDate = new DateTime(1997, 1, 15) },
            new Order { OrderID = 4, CustomerID = "AROUT", OrderDate = new DateTime(1997, 3, 22) },
            new Order { OrderID = 5, CustomerID = "BERGS", OrderDate = new DateTime(1997, 7, 8) },
            new Order { OrderID = 6, CustomerID = "BLAUS", OrderDate = new DateTime(1997, 12, 30) },
            new Order { OrderID = 7, CustomerID = "BONAP", OrderDate = new DateTime(1998, 2, 14) },
            new Order { OrderID = 8, CustomerID = "BOTTM", OrderDate = new DateTime(1998, 6, 5) },
            new Order { OrderID = 9, CustomerID = "BSBEV", OrderDate = new DateTime(1998, 9, 18) },
            new Order { OrderID = 10, CustomerID = "CACTU", OrderDate = new DateTime(1999, 4, 25) },
            new Order { OrderID = 11, CustomerID = "CENTC", OrderDate = new DateTime(1996, 8, 12) },
            new Order { OrderID = 12, CustomerID = "CHOPS", OrderDate = new DateTime(2000, 1, 3) }
        };
        static async Task Main(string[] args)
        {
            #region Practice LINQ and ADV
            // await AsyncDemo();
            /*
            var v = 10;
            object o = 10;
            dynamic d = 10;

            Console.WriteLine($"v: {v.GetType()}");
            Console.WriteLine($"o: {((int)o).GetType()}"); // normal
            Console.WriteLine($"o: {o.ToString().GetType()}");
            Console.WriteLine($"o: {d.GetType()}");
            */
            /*
            int a = 10;
            int b = 20;

            Calc(a, b, (a, b) => Console.WriteLine(a + b));   */

            /*int[] numbers = { 2, 7, 4, 9, 1, 6 };

            var result = numbers.Where(x => x > 5);

            foreach (var x in result) { 
                Console.WriteLine(x);
            }*/
            /*
            int[] numbersExt = { 2, 7, 4, 9, 1, 6 };

            var resultExt = numbersExt.WHERE((x => x > 5));

            for (int i = 0; i < numbersExt.Length; i++)
            {
                Console.WriteLine(resultExt[i]);
            }
            */
            /* object [] numbers = { 2, 'A', "Men3m", 9, 2.55, "Ali" };

             var result = numbers.OfType<string>();

             foreach (var x in result) { 
                 Console.WriteLine(x);
             }
            */
            /*
            int[] numbers = { 2, 7, 4, 9, 1, 6 };

            var result = numbers.OrderBy(n => n);

            print(result);
            */
            #endregion



            #region Q1
            var result1 = ProductList.Where(p => p.Category == "Seafood");

            print(result1, p => $"{p.ProductName} | {p.Price}", "Seafood products: ");
            #endregion

            #region Q2
            var result2 = ProductList.Select(p => p.ProductName);

            print(result2, name => name, "Product names: ");
            #endregion

            #region Q3
            var result3 = ProductList.OrderBy(p => p.Price);

            print(result3, p => $"{p.ProductName} | {p.Price}", "Products by price: ");
            #endregion

            #region Q4
            var result4 = ProductList.Where(p => p.Price >= 10 && p.Price <= 30);

            print(result4, p => $"{p.ProductName} | {p.Price}", "Products priced between 10 and 30: ");
            #endregion

            #region Q5
            var result5 = ProductList.Where(p => p.UnitsInStock > 0 && p.Category == "Condiments");

            print(result5, p => $"{p.ProductName} | {p.Price}", "Available condiments: ");
            #endregion

            #region Q6
            var result6 = ProductList.Select(p => new
            {
                Name = p.ProductName,
                Price = p.Price,
                StockStatus = p.UnitsInStock > 0 ? "Available" : "Out of Stock"
            });

            print(result6, p => $"{p.Name} | {p.Price} | {p.StockStatus}");
            #endregion

            #region Q7
            var result7 = ProductList.Select((p, index) => new
            {
                Position = index + 1,
                Name = p.ProductName
            });

            print(result7, p => $"{p.Position}. {p.Name}");
            #endregion

            #region Q8
            var result8 = ProductList.OrderBy(p => p.Category).ThenByDescending(p => p.Price);

            print(result8, p => $"{p.Category} | {p.ProductName} | {p.Price}");
            #endregion

            #region Q9
            var result9 = ProductList.Where(p => p.Category == "Beverages").OrderByDescending(p => p.UnitsInStock);

            print(result9, p => $"{p.ProductName} | Stock: {p.UnitsInStock}");
            #endregion

            #region Q10
            var result10 =
                from order in OrderList
                from date in new[] { order.OrderDate }
                where date.Year >= 1997
                select new
                {
                    order.CustomerID,
                    OrderDate = date
                };

            foreach (var order in result10)
                Console.WriteLine($"{order.CustomerID} | {order.OrderDate:d}");
            #endregion

            #region Q11
            var result11 = ProductList.Select((p, index) => new
            {
                Position = index + 1,
                ProductName = p.ProductName
            });

            print(result11, p => $"{p.Position}. {p.ProductName}");
            #endregion

            #region Q12
            string[] Arr = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            var result12 = Arr.OrderBy(word => word.Length).ThenBy(word => word, StringComparer.OrdinalIgnoreCase);

            print(result12, word => word);
            #endregion

            #region Q13
            string[] digitWords = {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var result13 = digitWords.Where(word => word[1] == 'i').Reverse().ToList();

            print(result13, word => word);
            #endregion
        }
        static async Task AsyncDemo()
        {
            #region Async Methods
            async Task<string> GetAnyThingAsync1()
            {
                await Task.Delay(2000);
                return "GetAnyThingAsync1() >> Success";
            }

            async Task<string> GetAnyThingAsync2()
            {
                await Task.Delay(2000);
                return "GetAnyThingAsync2() >> Success";
            }

            #endregion

            #region Sync Methods
            string GetAnyThing1()
            {
                Thread.Sleep(2000);
                return "GetAnyThing1() >> Success";
            }

            string GetAnyThing2()
            {
                Thread.Sleep(2000);
                return "GetAnyThing2() >> Success";
            }

            #endregion

            #region Async Execution
            Stopwatch asyncStopwatch = Stopwatch.StartNew();

            Task<string> task1 = GetAnyThingAsync1();
            Task<string> task2 = GetAnyThingAsync2();

            string resultAsync1 = await task1;
            string resultAsync2 = await task2;

            asyncStopwatch.Stop();

            #endregion

            #region Sync Execution

            Stopwatch syncStopwatch = Stopwatch.StartNew();

            string resultSync1 = GetAnyThing1();
            string resultSync2 = GetAnyThing2();

            syncStopwatch.Stop();

            #endregion

            #region Output

            Console.WriteLine("========== Async Execution ==========");
            Console.WriteLine(resultAsync1);
            Console.WriteLine(resultAsync2);
            Console.WriteLine($"Async Execution Time: {asyncStopwatch.Elapsed.TotalMilliseconds} ms");

            Console.WriteLine("\n========== Sync Execution ==========");
            Console.WriteLine(resultSync1);
            Console.WriteLine(resultSync2);
            Console.WriteLine($"Sync Execution Time: {syncStopwatch.Elapsed.TotalMilliseconds} ms");

            #endregion
        }
    }
}