namespace Fuvar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fuvarok = Fuvar.LoadFromCsv("../../../fuvar.csv");

            // Feladat: 3
            Console.WriteLine("3. feladat: " + fuvarok.Count + " fuvar");

            // Feladat: 4
            var taxi6185Rides = fuvarok.Where(f => f.TaxiId == 6185);
            double totalRevenue = taxi6185Rides.Sum(f => f.Viteldij + f.Borravalo);
            Console.WriteLine($"4. feladat: {taxi6185Rides.Count()} fuvar alatt: {totalRevenue}$");
            // Feladat: 5
            Console.WriteLine("5. feladat:");
            var paymentMethods = fuvarok
                .GroupBy(f => f.FizetesModja)
                .ToDictionary(g => g.Key, g => g.Count());
            foreach (var method in paymentMethods)
            {
                Console.WriteLine($"{method.Key}: {method.Value} fuvar");
            }

            // Feladat: 6
            double totalDistanceMiles = fuvarok.Sum(f => f.Tavolsag);
            double totalDistanceKm = totalDistanceMiles * 1.6;
            Console.WriteLine($"6. feladat: {totalDistanceKm:F2} km");

            // Feladat: 7
            var longestRide = fuvarok.OrderByDescending(f => f.Ido).First();
            Console.WriteLine("7. feladat: Leghosszabb fuvar:");
            Console.WriteLine($"Fuvar hossza: {longestRide.Ido} másodperc");
            Console.WriteLine($"Taxi azonosító: {longestRide.TaxiId}");
            Console.WriteLine($"Megtett távolság: {longestRide.Tavolsag * 1.6:F1} km");
            Console.WriteLine($"Viteldíj: {longestRide.Viteldij}$");

            // Feladat: 8
            var faultyRides = fuvarok
                .Where(f => f.Ido > 0 && f.Viteldij > 0 && f.Tavolsag == 0)
                .OrderBy(f => f.Indulas)
                .Select(f => $"{f.TaxiId};{f.Indulas:yyyy-MM-dd HH:mm:ss};{f.Ido};{f.Tavolsag};{f.Viteldij};{f.Borravalo};{f.FizetesModja}")
                .ToList();
            File.WriteAllLines("hibak.txt", new[] { "taxi_id;indulas_idopontja;idotartam;tavolsag;viteldij;borravalo;fizetes_modja" }.Concat(faultyRides));
            Console.WriteLine("8. feladat: hibak.txt");

        }
    }
}
