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
					Console.WriteLine("Error: Number out of range (if)");
				}
				else
				{
					Console.Write("Generating primes n <= " + limit + ". Please wait... ");
					try
					{
						long time_start = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();
						List<BitArray> primeMarkers = Generate(limit);
						long time_end = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();

						Console.Write(" DONE [in " + (time_end - time_start) + " ms]");
						Console.WriteLine("\nFound " + CountPrimes(primeMarkers) + " primes <= " + limit + "\n");
						
						// Option to view primes
						Console.Write("View Primes [Y/N]: ");
						string choice = Console.ReadLine();
						if (choice == "Y" || choice == "y")
						{
							Console.Write("Primes: ");
							PrintPrimes(primeMarkers);
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

		public static void PrintPrimes(IEnumerable primes)
		{
			long i = 0;
			foreach (object bit in primes)
			{
				if (Convert.ToBoolean(bit))
				{
					Console.Write(i + " ");
				}
				i += 1;
			}
		}
		
		public static long CountPrimes(IEnumerable primeMarkers)
		{
			long count = 0;
			foreach (object bit in primeMarkers)
			{
				if (Convert.ToBoolean(bit))
					count += 1;
			}
			return count;
		}
		
		public static BitArray Generate(int limit)
		{
			// Create marks array
			// True -> prime
			// False -> not prime
			BitArray marks = new BitArray(limit + 1);
			marks.SetAll(true);

			// Loop through each value
			for (int i = 2; i * i <= limit; i += 1)
			{
				//Console.WriteLine("Pointer @ " + i + ":\n");
				// If is prime...
				if (marks.Get(i))
				{
					//Console.Write(">\t");
					// Mark off all multiples as non-prime
					for (int j = i * i; j <= limit; j += i)
					{
						//marks[j] = true;
						marks.Set(j, false);
						//Console.Write(j + " ");
					}
					//Console.Write("\n");
				}
			}

			// Set (0) and (1) as non-prime
			marks.Set(0, false);
			marks.Set(1, false);
			return marks;
		}
	}
}