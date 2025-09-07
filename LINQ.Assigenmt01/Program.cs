using Day_01_G03;
namespace LINQ.Assigenmt01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Restriction Operators
            Console.WriteLine("=== Restriction Operators ===");

     
            var products = ListGenerator.GetProducts();


            var outOfStock = products.Where(p => p.UnitsInStock == 0);
            Console.WriteLine("\n1. Out of Stock Products:");
            foreach (var p in outOfStock)
                Console.WriteLine(p.ProductName);

 
            var expensiveInStock = products.Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3.00M);
            Console.WriteLine("\n2. In stock and cost > 3:");
            foreach (var p in expensiveInStock)
                Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");

           
            string[] ArrRestriction = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var shortNames = ArrRestriction
                .Select((name, value) => new { name, value })
                .Where(x => x.name.Length < x.value);
            Console.WriteLine("\n3. Digits name shorter than value:");
            foreach (var item in shortNames)
                Console.WriteLine($"{item.name} - {item.value}");
            #endregion

            #region Element Operators
            Console.WriteLine("\n=== Element Operators ===");
            int[] ArrElement = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

          
            var firstOutOfStock = products.FirstOrDefault(p => p.UnitsInStock == 0);
            Console.WriteLine("\n1. First product out of stock: " + firstOutOfStock?.ProductName);


            var priceGreater1000 = products.FirstOrDefault(p => p.UnitPrice > 1000);
            Console.WriteLine("\n2. First product > 1000: " + (priceGreater1000?.ProductName ?? "null"));

          
            var secondGreaterThan5 = ArrElement.Where(n => n > 5).Skip(1).FirstOrDefault();
            Console.WriteLine("\n3. Second number greater than 5: " + secondGreaterThan5);
            #endregion

            #region Aggregate Operators
            Console.WriteLine("\n=== Aggregate Operators ===");
            int[] ArrAggregate = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var oddCount = ArrAggregate.Count(n => n % 2 == 1);
            Console.WriteLine("\n1. Count of odd numbers: " + oddCount);

         
            var customers = ListGenerator.GetCustomers();
            var custOrders = customers.Select(c => new { c.CustomerID, Orders = c.Orders.Count() });
            Console.WriteLine("\n2. Customers and order count:");
            foreach (var c in custOrders)
                Console.WriteLine($"{c.CustomerID} - {c.Orders}");

          
            var catProducts = products.GroupBy(p => p.Category)
                                      .Select(g => new { Category = g.Key, Count = g.Count() });
            Console.WriteLine("\n3. Categories and product count:");
            foreach (var cp in catProducts)
                Console.WriteLine($"{cp.Category}: {cp.Count}");

  
            var total = ArrAggregate.Sum();
            Console.WriteLine("\n4. Total numbers: " + total);


            string[] dictWords = File.ReadAllLines("dictionary_english.txt");

            Console.WriteLine("\n5. Total chars: " + dictWords.Sum(w => w.Length));
            Console.WriteLine("6. Shortest word length: " + dictWords.Min(w => w.Length));
            Console.WriteLine("7. Longest word length: " + dictWords.Max(w => w.Length));
            Console.WriteLine("8. Average word length: " + dictWords.Average(w => w.Length));
            #endregion

            #region Ordering Operators
            Console.WriteLine("\n=== Ordering Operators ===");


            var sortedByName = products.OrderBy(p => p.ProductName);
            Console.WriteLine("\n1. Products sorted by name:");
            foreach (var p in sortedByName)
                Console.WriteLine(p.ProductName);

   
            string[] ArrOrder1 = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var caseInsensitive = ArrOrder1.OrderBy(w => w, StringComparer.OrdinalIgnoreCase);
            Console.WriteLine("\n2. Case insensitive sort:");
            foreach (var w in caseInsensitive)
                Console.WriteLine(w);

            var stockDesc = products.OrderByDescending(p => p.UnitsInStock);
            Console.WriteLine("\n3. Products by stock descending:");
            foreach (var p in stockDesc)
                Console.WriteLine($"{p.ProductName} - {p.UnitsInStock}");


            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var digitSort = digits.OrderBy(d => d.Length).ThenBy(d => d);
            Console.WriteLine("\n4. Digits sorted:");
            foreach (var d in digitSort)
                Console.WriteLine(d);


            var sort5 = ArrOrder1.OrderBy(w => w.Length).ThenBy(w => w, StringComparer.OrdinalIgnoreCase);
            Console.WriteLine("\n5. Words sorted:");
            foreach (var w in sort5)
                Console.WriteLine(w);


            var sortCatPrice = products.OrderBy(p => p.Category).ThenByDescending(p => p.UnitPrice);
            Console.WriteLine("\n6. Products sorted by category then price:");
            foreach (var p in sortCatPrice)
                Console.WriteLine($"{p.Category} - {p.ProductName} - {p.UnitPrice}");

     
            var sort7 = ArrOrder1.OrderBy(w => w.Length).ThenByDescending(w => w, StringComparer.OrdinalIgnoreCase);
            Console.WriteLine("\n7. Words sorted:");
            foreach (var w in sort7)
                Console.WriteLine(w);


            var reverseDigits = digits.Where(d => d.Length > 1 && d[1] == 'i').Reverse();
            Console.WriteLine("\n8. Digits with 'i' as second letter reversed:");
            foreach (var d in reverseDigits)
                Console.WriteLine(d);
            #endregion

            #region Transformation Operators
            Console.WriteLine("\n=== Transformation Operators ===");


            var productNames = products.Select(p => p.ProductName);
            Console.WriteLine("\n1. Product Names:");
            foreach (var n in productNames)
                Console.WriteLine(n);


            string[] words = { "aPPLE", "BlUeBeRrY", "cHeRry" };
            var wordTransform = words.Select(w => new { Upper = w.ToUpper(), Lower = w.ToLower() });
            Console.WriteLine("\n2. Words upper & lower:");
            foreach (var w in wordTransform)
                Console.WriteLine($"{w.Upper} - {w.Lower}");


            var productProps = products.Select(p => new { p.ProductName, Price = p.UnitPrice });
            Console.WriteLine("\n3. Products with Price:");
            foreach (var p in productProps)
                Console.WriteLine($"{p.ProductName} - {p.Price}");


            int[] ArrTransform = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var matchIndex = ArrTransform.Select((num, index) => new { num, index, match = (num == index) });
            Console.WriteLine("\n4. Number matches position:");
            foreach (var m in matchIndex)
                Console.WriteLine($"{m.num}: {m.match}");


            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            var pairs = from a in numbersA
                        from b in numbersB
                        where a < b
                        select new { a, b };
            Console.WriteLine("\n5. Pairs a < b:");
            foreach (var p in pairs)
                Console.WriteLine($"{p.a} is less than {p.b}");

            
            var ordersLess500 = customers.SelectMany(c => c.Orders)
                                         .Where(o => o.Total < 500);
            Console.WriteLine("\n6. Orders < 500:");
            foreach (var o in ordersLess500)
                Console.WriteLine($"Order {o.OrderID} - {o.Total}");

          
            var ordersAfter98 = customers.SelectMany(c => c.Orders)
                                         .Where(o => o.OrderDate.Year >= 1998);
            Console.WriteLine("\n7. Orders after 1998:");
            foreach (var o in ordersAfter98)
                Console.WriteLine($"Order {o.OrderID} - {o.OrderDate}");
            #endregion
        }
    }
}
