using System;
using System.Collections.Generic;

namespace PokemonCareTaker
{
	class Program
	{
		static void Main()
		{
			// Create a list of available pokemons to choose in the beggining of the game
			List<Pokemon> pAvailablePokemons = new List<Pokemon>();

			// Add to the list pokemon through constructor
			pAvailablePokemons.Add(new Pokemon(
				iNumber: 1,
				sName: "Bulbasaur",
				sType: "Leaf"
			));

			// Add to the list pokemon through constructor
			pAvailablePokemons.Add(new Pokemon(
				iNumber: 2,
				sName: "Squirtle",
				sType: "Water"
			));

			// Add to the list pokemon through constructor
			pAvailablePokemons.Add(new Pokemon(
				iNumber: 3,
				sName: "Charmander",
				sType: "Fire"
			));

			// UI to get the Pokemon
			Pokemon pChosenPokemon = ChoosingPokemonToTakeCare(pAvailablePokemons);

			// GameLoop
			GameLoop(pChosenPokemon);

		}

		private static void GameLoop(Pokemon pChosenPokemon)
		{
			// Set initial control variables to the game.
			int iDaysCounter = 1;
			int iActionsCounter = 0;
			int iMaxActionsPerDay = 2;
			bool bStillPlaying = true;

			// The major game loop and interaction to user is provided to this for. It checks if user is still playing and keep showing the stats.
			for (int i = 0; bStillPlaying; i++)
			{
				// Control variables inside every iteraction.
				string sTimeOfTheDay = iActionsCounter == 0 ? "Morning" : "Night";
				List<string> sActions = new List<string>() { "Put to Sleep", "Feed", "Train", "Pet", "Put inside the Pokeball" };

				// UI to show Pokemon Stats
				Console.Clear();
				Console.WriteLine("=====================");
				Console.WriteLine($"Day: { iDaysCounter } - { sTimeOfTheDay }");
				Console.WriteLine("=====================");
				Console.WriteLine($"{ pChosenPokemon.SName } - Lvl. { pChosenPokemon.ILevel }");
				Console.WriteLine("=====================");
				Console.WriteLine($"HP: { pChosenPokemon.IHP } / { pChosenPokemon.IMaxAtributeValue }");
				Console.WriteLine($"Hunger: { pChosenPokemon.IHunger } / { pChosenPokemon.IMaxAtributeValue }");
				Console.WriteLine($"Sleep: { pChosenPokemon.ISleep } / { pChosenPokemon.IMaxAtributeValue }");
				Console.WriteLine($"Loyalty: { pChosenPokemon.ILoyalty } / { pChosenPokemon.IMaxAtributeValue }");
				Console.WriteLine($"Experience Points: { pChosenPokemon.IExperiencePoints }");
				Console.WriteLine("=====================\n");
				Console.WriteLine($"What would you like to do with { pChosenPokemon.SName }? [Pick by the number]");

				// Show available actions in kind of a menu
				for (int j = 0; j < sActions.Count; j++)
				{
					Console.WriteLine($"#{ j + 1 } { sActions[j] }");
				}
				Console.WriteLine("=====================");

				// Get proper user input to execute actions
				while (true)
				{
					// Getting input and parse to int
					string sUserInput = Console.ReadLine();
					bool bIsActionValid = int.TryParse(sUserInput, out int iInputInteger);

					// If is a valid action...
					if (bIsActionValid && iInputInteger >= 1 && iInputInteger <= sActions.Count)
					{
						// the execute it
						ExecutePokemonAction(iInputInteger, pChosenPokemon);

						// and increase the actions counter as well
						iActionsCounter++;

						// and also if it reached the maximum available, then...
						if (iActionsCounter == iMaxActionsPerDay)
						{
							// Set actions counter to zero and start a new day.
							iActionsCounter = 0;
							iDaysCounter++;

							// and get feedback of that day.
							pChosenPokemon.Feedback();
						}
						break;
					}
					else
					{
						// UI to call user to repeat input
						PrintErrorMessage($"{ pChosenPokemon.SName } don't understand your command. Try again.");
					}
				}

				// Check if the Pokemon is in conditions to keep playing
				if (!pChosenPokemon.IsWell())
				{
					// If not, then game over... UI for Game Over
					Console.WriteLine("Prof. Oak: Ok, my assistant. Accidents happen!");
					Console.WriteLine("Prof. Oak: Fortunately we have Nurse Joy to help us.");

					while (true)
					{
						// Option to keep playing and user input
						Console.WriteLine($"Prof. Oak: Would you like to keep taking care of { pChosenPokemon.SName }? [Y,n]");
						string sUserInputKeepPlaying = Console.ReadLine();

						// If keep playing...
						if (sUserInputKeepPlaying.Equals("Y", StringComparison.OrdinalIgnoreCase))
						{
							// Back to first day, set actions to zero and revive Pokemon.
							iActionsCounter = 0;
							iDaysCounter = 1;
							pChosenPokemon.RevivePokemon();
							break;
						}
						else if (sUserInputKeepPlaying.Equals("N", StringComparison.OrdinalIgnoreCase))
						{
							// Break the loop and finish the game
							Console.WriteLine("Prof. Oak: Hmm... Guess I was wrong about you, but it's okay! Have a nice day!");
							bStillPlaying = false;
							break;
						}
					}
				}
			}
		}

		private static void ExecutePokemonAction(int iInputInteger, Pokemon pChosenPokemon)
		{
			// Call the Pokemon's class method by number in actions menu
			switch (iInputInteger)
			{
				case 1:
					pChosenPokemon.Sleep();
					break;
				case 2:
					pChosenPokemon.Feed();
					break;
				case 3:
					pChosenPokemon.Practice();
					break;
				case 4:
					pChosenPokemon.Pet();
					break;
				case 5:
					pChosenPokemon.PutInsideThePokeball();
					break;
			}
		}

		private static Pokemon ChoosingPokemonToTakeCare(List<Pokemon> pAvailablePokemons)
		{
			// Context of the game.
			Console.WriteLine("Pokemon Caretaker 101");
			Console.WriteLine("=====================");

			Console.WriteLine("\nHello! My name's Prof. Oak!");
			Console.WriteLine($"I heard that you'd like to become my assistant...");
			Console.WriteLine("The job isn't easy, but I think you fit the role.");

			Console.WriteLine($"\nAs long as you are new here I will give you only one Pokemon to take.");
			Console.ReadKey();

			Console.Clear();
			Console.WriteLine("Ok! No more small talk! We have a lot of things to do!");
			Console.WriteLine("\nYou can choose to take care between...\n");
			// End of context

			// Show available options for the pokemons
			foreach (var pPokemon in pAvailablePokemons)
			{
				Console.WriteLine($"#{ pPokemon.INumber } { pPokemon.SName } - Type: { pPokemon.SType }");
			}

			Pokemon pChosenPokemon;
			while (true)
			{
				// Get the pokemon by name or number
				Console.WriteLine("\nWhich one will be under your responsability? [Choose by number or name]");
				string sUserInput = Console.ReadLine();
				bool bIsThePokemonNumber = int.TryParse(sUserInput, out int iInputInteger);

				// try to get the pokemon in the pokemon's list... if success, then break this loop.
				try
				{
					pChosenPokemon = bIsThePokemonNumber ? new Pokemon(pAvailablePokemons[iInputInteger - 1]) : new Pokemon(pAvailablePokemons[pAvailablePokemons.FindIndex(p => p.SName.ToUpper() == sUserInput.ToUpper())]);
					break;
				}
				catch (Exception)
				{
					// If not, then call user to try again
					PrintErrorMessage("You know, I'm starting to become deaf! Could you try again?");
				}
			}
			return pChosenPokemon;
		}

		// Auxiliar function to print friendly error messages
		private static void PrintErrorMessage(string sMessage)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(sMessage);
			Console.ResetColor();
		}
	}
}
