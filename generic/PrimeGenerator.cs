using System;
using System.IO;
using System.Text;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine("Welcome to Prime Number Generator! To start, enter the upper bound of the algorithm below: ");

			try
			{
                #region GeneratePrime
				Console.Write("Start Point (0 < n < 2,000,000,000): ");
                long start = Convert.ToInt64(Console.ReadLine());
				
				Console.Write("Limit (0 < n < 2,000,000,000): ");
                long limit = Convert.ToInt64(Console.ReadLine());
				
				
				if (limit < 1 || start < 1 || start >= limit)
				{
					Console.WriteLine("Error: Please make sure numbers are inside correct ranges and start is not larger than or equal the limit");
				}
				else
				{
					Console.Write("Generating primes " + start + " <= n <= " + limit + ". Please wait... ");
					try
					{
						long time_start = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();
						ulong primes = Generate(start, limit);
						long time_end = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();

						Console.WriteLine("\n\n == [ DONE ] ===\n  Elapsed: " + (time_end - time_start) + " ms");
						Console.WriteLine("\nFound " + primes + " primes " + start + " <= n <= " + limit + "\n");
						
						Console.ReadKey();
					}
					catch (Exception e)
					{
						Console.Write(" [ TERMINATED ] ");
						Console.WriteLine("An error occured: ");
						Console.WriteLine(e);
					}
				}
                #endregion
            }
            catch (FormatException)
			{
				Console.WriteLine("Error: Number required");
			}
			catch (OverflowException)
			{
				Console.WriteLine("Error: Number out of range (Overflow)");
			}

			Console.Write("\nPress any key to quit...");
			Console.ReadKey();

		}

		public static bool IsPrime(long n)
		{
			if (n <= 1)
				return false;
			if (n <= 3)
				return true;
			if (n % 2 == 0 || n % 3 == 0)
				return false;
			
			for (int i = 5; i * i <= n; i += 6)
			{
				if (n % i == 0 || n % (i + 2) == 0)
					return false;
			}
			return true;
		}
		
		public static ulong Generate(long start, long limit)
		{
			ulong counter = 0;
			FileStream stream = File.Open("primes.txt", FileMode.Append, FileAccess.Write);
			StreamWriter writer = new StreamWriter(stream);
			
			// If "2" in range, add to list. NB: "L" converts to long
			if (2 >= start && 2 <= limit)
			{
				Console.Write("2  ");
				counter += 1;
			}
			
			// Miss out all even numbers. If even, add 1 to start
			if (start % 2 == 0)
				start += 1;
			
			for (long n = start; n <= limit; n += 2)
			{
				if (IsPrime(n))
				{
					//Console.Write(n + "  ");
					writer.Write(n + ", ");
					counter += 1;
				}
			}
			writer.Flush();
			writer.Close();
			return counter;
		}
	}
}