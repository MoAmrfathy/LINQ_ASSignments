using static Day_01_G03.ListGenerator;

namespace LINQ.Assignment02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Aggregate Operators
            Console.WriteLine("=== Aggregate Operators ===");

            var totalUnitsByCategory = ProductsList
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, TotalUnits = g.Sum(p => p.UnitsInStock) });

            Console.WriteLine("\n1. Total Units in Stock per Category:");
            foreach (var item in totalUnitsByCategory)
                Console.WriteLine($"{item.Category}: {item.TotalUnits}");

            var cheapestPrice = ProductsList
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, MinPrice = g.Min(p => p.UnitPrice) });

            Console.WriteLine("\n2. Cheapest Price per Category:");
            foreach (var item in cheapestPrice)
                Console.WriteLine($"{item.Category}: {item.MinPrice}");

            var cheapestProducts = from p in ProductsList
                                   group p by p.Category into g
                                   let minPrice = g.Min(p => p.UnitPrice)
                                   from p2 in g
                                   where p2.UnitPrice == minPrice
                                   select new { g.Key, p2.ProductName, p2.UnitPrice };

            Console.WriteLine("\n3. Cheapest Products per Category:");
            foreach (var item in cheapestProducts)
                Console.WriteLine($"{item.Key}: {item.ProductName} - {item.UnitPrice}");

            var maxPrice = ProductsList
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, MaxPrice = g.Max(p => p.UnitPrice) });

            Console.WriteLine("\n4. Most Expensive Price per Category:");
            foreach (var item in maxPrice)
                Console.WriteLine($"{item.Category}: {item.MaxPrice}");

            var expensiveProducts = from p in ProductsList
                                    group p by p.Category into g
                                    let maxPriceVal = g.Max(p => p.UnitPrice)
                                    from p2 in g
                                    where p2.UnitPrice == maxPriceVal
                                    select new { g.Key, p2.ProductName, p2.UnitPrice };

            Console.WriteLine("\n5. Most Expensive Products per Category:");
            foreach (var item in expensiveProducts)
                Console.WriteLine($"{item.Key}: {item.ProductName} - {item.UnitPrice}");

            var avgPrice = ProductsList
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, AvgPrice = g.Average(p => p.UnitPrice) });

            Console.WriteLine("\n6. Average Price per Category:");
            foreach (var item in avgPrice)
                Console.WriteLine($"{item.Category}: {item.AvgPrice}");
            #endregion

            #region Set Operators
            Console.WriteLine("\n=== Set Operators ===");
            var uniqueCategories = ProductsList.Select(p => p.Category).Distinct();
            Console.WriteLine("\n1. Unique Categories:");
            foreach (var c in uniqueCategories)
                Console.WriteLine(c);

            var uniqueFirstLetters = ProductsList.Select(p => p.ProductName[0])
                .Union(CustomersList.Select(c => c.CustomerID[0]));
            Console.WriteLine("\n2. Unique First Letters:");
            foreach (var l in uniqueFirstLetters)
                Console.WriteLine(l);

            var commonFirstLetters = ProductsList.Select(p => p.ProductName[0])
                .Intersect(CustomersList.Select(c => c.CustomerID[0]));
            Console.WriteLine("\n3. Common First Letters:");
            foreach (var l in commonFirstLetters)
                Console.WriteLine(l);

            var exceptLetters = ProductsList.Select(p => p.ProductName[0])
                .Except(CustomersList.Select(c => c.CustomerID[0]));
            Console.WriteLine("\n4. Product First Letters NOT in Customers:");
            foreach (var l in exceptLetters)
                Console.WriteLine(l);

            var lastThreeChars = ProductsList
                .Select(p => p.ProductName.Substring(Math.Max(0, p.ProductName.Length - 3)))
                .Concat(CustomersList.Select(c => c.CustomerID.Substring(Math.Max(0, c.CustomerID.Length - 3))));
            Console.WriteLine("\n5. Last 3 Characters:");
            foreach (var s in lastThreeChars)
                Console.WriteLine(s);
            #endregion

            #region Partitioning Operators
            Console.WriteLine("\n=== Partitioning Operators ===");

            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var first3Washington = CustomersList.Where(c => c.City == "Washington")
                                                .SelectMany(c => c.Orders)
                                                .Take(3);
            Console.WriteLine("\n1. First 3 Orders from Washington:");
            foreach (var o in first3Washington)
                Console.WriteLine($"Order {o.OrderID}");

            var skip2Washington = CustomersList.Where(c => c.City == "Washington")
                                               .SelectMany(c => c.Orders)
                                               .Skip(2);
            Console.WriteLine("\n2. Skip First 2 Orders from Washington:");
            foreach (var o in skip2Washington)
                Console.WriteLine($"Order {o.OrderID}");

            var untilLess = numbers.TakeWhile((n, idx) => n >= idx);
            Console.WriteLine("\n3. Take While n >= index:");
            foreach (var n in untilLess)
                Console.WriteLine(n);

            var fromDiv3 = numbers.SkipWhile(n => n % 3 != 0);
            Console.WriteLine("\n4. From First Divisible by 3:");
            foreach (var n in fromDiv3)
                Console.WriteLine(n);

            var fromLessPos = numbers.SkipWhile((n, idx) => n >= idx);
            Console.WriteLine("\n5. From First n < index:");
            foreach (var n in fromLessPos)
                Console.WriteLine(n);
            #endregion

            #region Quantifiers
            Console.WriteLine("\n=== Quantifiers ===");

            string[] dictWords = File.ReadAllLines("dictionary_english.txt");

            bool hasEi = dictWords.Any(w => w.Contains("ei"));
            Console.WriteLine($"\n1. Any word contains 'ei'? {hasEi}");

            var someOutOfStock = ProductsList.GroupBy(p => p.Category)
                .Where(g => g.Any(p => p.UnitsInStock == 0));
            Console.WriteLine("\n2. Categories with Some Out of Stock:");
            foreach (var g in someOutOfStock)
                Console.WriteLine($"{g.Key}");

            var allInStock = ProductsList.GroupBy(p => p.Category)
                .Where(g => g.All(p => p.UnitsInStock > 0));
            Console.WriteLine("\n3. Categories All In Stock:");
            foreach (var g in allInStock)
                Console.WriteLine($"{g.Key}");
            #endregion

            #region Grouping Operators
            Console.WriteLine("\n=== Grouping Operators ===");

            List<int> nums = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            var groupByRemainder = nums.GroupBy(n => n % 5);
            Console.WriteLine("\n1. Numbers grouped by remainder %5:");
            foreach (var g in groupByRemainder)
            {
                Console.WriteLine($"Remainder {g.Key}:");
                foreach (var n in g)
                    Console.WriteLine(n);
            }

            var groupByFirst = dictWords.GroupBy(w => w[0]);
            Console.WriteLine("\n2. Words grouped by first letter:");
            foreach (var g in groupByFirst)
            {
                Console.WriteLine($"Letter {g.Key}:");
                foreach (var w in g.Take(5))
                    Console.WriteLine(w);
            }

            string[] Arr = { "from", "salt", "earn", "last", "near", "form" };
            var groupByAnagram = Arr.GroupBy(w => String.Concat(w.OrderBy(c => c)));
            Console.WriteLine("\n3. Words grouped by same characters:");
            foreach (var g in groupByAnagram)
            {
                Console.WriteLine("Group:");
                foreach (var w in g)
                    Console.WriteLine(w);
            }
            #endregion
        }
    }
}
