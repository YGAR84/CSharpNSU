using System;

namespace Task1sharp
{
	class Program
	{
		private const int MaxNumOfTries = 7;
		private const int MinBound = 0;
		private const int MaxBound = 100;

		static void Main(string[] args)
		{
			var rng = new Random();
			
			Console.WriteLine(
				$"Start playing. Guess number between {MinBound} and {MaxBound} less than in {MaxNumOfTries} tries");
			while (true)
			{
				var currentNumOfTries = 0;
				var currentNum = rng.Next(MinBound, MaxBound);

				Console.WriteLine("\nWe have number for you. Try to guess it. Good luck!");
				bool solved = false;
				while (currentNumOfTries < MaxNumOfTries)
				{
					string input = Console.ReadLine();

					if (input == "q")
					{
						return;
					}

					if (!int.TryParse(input, out var number))
					{
						Console.WriteLine("Try again, numbers, you know?");
						continue;
					}

					if (number < MinBound || number > MaxBound)
					{
						Console.WriteLine($"Hello, stupid, between {MinBound} and {MaxBound}, numbers, you know?");
						continue;
					}

					currentNumOfTries++;

					if (number == currentNum)
					{
						string winResult = $"You got this number {currentNum} in {currentNumOfTries}, Thank you for playing";
						Console.WriteLine(winResult);
						solved = true;
						break;
					}

					if (number > currentNum)
					{
						Console.WriteLine("Less");
					}

					if (number < currentNum)
					{
						Console.WriteLine("More");
					}


				}

				if (!solved)
				{
					string winResult = $"You dumb! Number was {currentNum}. Thank you for playing";
					Console.WriteLine(winResult);
				}

				Console.WriteLine("Try again?(y\\n)");
				switch (Console.ReadLine())
				{
					case "y":
						break;
					default:
						return;
				}
			}
			
		}
	}
}
