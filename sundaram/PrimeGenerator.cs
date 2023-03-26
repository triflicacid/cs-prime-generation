using System;
using System.Collections;
using System.Collections.Generic;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine("Welcome to Prime Number Generator! To start, enter the upper bound of the algorithm below: ");

			// Get limit
			Console.Write("Limit (0 < n < 9,223,372,036,854,775,807): ");

			try
			{
                #region GeneratePrime
                int limit = Convert.ToInt32(Console.ReadLine());
				if (limit < 1)
				{
					Console.WriteLine("Error: Number out of range");
				}
				else
				{
					Console.Write("Generating primes n <= " + limit + ". Please wait... ");
					try
					{
						long time_start = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();
						BitArray primeMarkers = Generate(limit);
						long time_end = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();

						Console.Write(" DONE [in " + (time_end - time_start) + " ms]");
						Console.WriteLine("\nFound " + CountPrimes(primeMarkers) + " primes <= " + limit + "\n");
						
						// Option to view primes
						Console.Write("View Primes [Y/N]: ");
						string choice = Console.ReadLine();
						if (choice == "Y" || choice == "y")
						{
							Console.Write("Primes: ");
							PrintPrimes(limit, primeMarkers);
							Console.Write("\n");
						}
					}
					catch (Exception e)
					{
						Console.Write(" [ TERMINATED ] ");
						Console.WriteLine("\nAn error occured: ");
						Console.WriteLine(e);
					}
				}
                #endregion
            }
            catch (FormatException)
			{
				Console.WriteLine("Error: Number required");
			}
			catch (OverflowException err)
			{
				Console.WriteLine("Error: Number out of range (Overflow)");
				Console.WriteLine("\n\n" + err);
			}

			Console.Write("\nPress any key to quit...");
			Console.ReadKey();

		}

		public static void PrintPrimes(int limit, ICollection primes)
		{
			if (limit > 2)
				Console.Write("2 ");

			long i = 1;
			foreach (object bit in primes)
			{
				if (Convert.ToBoolean(bit))
				{
					long p = 2 * i + 1;
					Console.Write(p + " ");
				}
				i++;
			}
		}
		
		public static long CountPrimes(ICollection primeMarkers)
		{
			long count = 0;
			foreach (object bit in primeMarkers)
			{
				if (Convert.ToBoolean(bit))
					count++;
			}
			return count;
		}
		
		public static BitArray Generate(int limit)
		{
			int k = (limit - 2) / 2;

			// Create marks array
			BitArray marks = new BitArray(k + 1);
			marks.SetAll(true);
			marks.Set(0, false);

			for (int i = 1; i < k + 1; i++) {
				for (int j = i; i + j + 2 * i * j <= k; j++) {
					marks.Set(i + j + 2 * i * j, false);
				}
			}

			return marks;
		}
	}
}