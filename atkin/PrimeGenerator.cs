using System;
using System.Collections.Generic;

namespace SieveOfAtkinNs
{
	class Atkin
	{
		public static List<ulong> Primes;
		public static ulong Limit;
		
		private static void FindPrimes()
		{
			Primes = new List<ulong>();
			
			bool[] isPrime = new bool[Limit + 1];
			var sqrt = Math.Sqrt(Limit);
			
			for (ulong x = 1; x <= sqrt; x += 1)
			{
				//Console.WriteLine(x + ":\n");
				for (ulong y = 1; y <= sqrt; y += 1)
				{
					//Console.Write(y + ", ");
					var n = 4*x*x + y*y;
					if (n <= Limit && (n % 12 == 1 || n % 12 == 5))
						isPrime[n] ^= true;

					n = 3*x*x + y*y;
					if (n <= Limit && n % 12 == 7)
						isPrime[n] ^= true;

					n = 3*x*x - y*y;
					if (x > y && n <= Limit && n % 12 == 11)
						isPrime[n] ^= true;
				}
				//Console.Write("\n");
			}
			
			for (ulong n = 5; n <= sqrt; n += 1)
			{
				if (isPrime[n])
				{
					ulong nSqrd = n*n;
					for (ulong k = nSqrd; k <= Limit; k += nSqrd)
						isPrime[k] = false;
				}
			}
			
			Primes.Add(2);
			Primes.Add(3);
			for (ulong n = 5; n <= Limit; n+= 1)
				if (isPrime[n])
					Primes.Add(n);
		}
		
		public static void Main(string[] args)
		{
			Console.WriteLine("Prime Number Limit: ");
			Limit = Convert.ToUInt64(Console.ReadLine());
			
			Console.WriteLine("Generating... ");
			long start = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();
			FindPrimes();
			long end = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();
			
			Console.WriteLine("\nTime Elapsed: " + (end - start) + " ms");
			Console.WriteLine("Found " + Primes.Count + " primes");
			
			Console.WriteLine("\nView Primes [Y/N]: ");
			string choice = Console.ReadLine();
			
			if (choice == "y" || choice == "Y")
			{
				foreach (ulong prime in Primes)
					Console.Write(prime + " ");
				Console.ReadKey();
			}
		}
	}
}