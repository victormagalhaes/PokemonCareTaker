using System;

namespace PokemonCareTaker
{
	class Pokemon
	{
		// List of attributes
		private int m_iNumber;
		private string m_sName;
		private string m_sType;
		private int m_iLevel;
		private int m_iHP;
		private int m_iHunger;
		private int m_iSleep;
		private int m_iMaxAtributeValue;
		private int m_iLoyalty;
		private int m_iExperiencePoints;

		// Getters and Setters
		public int INumber { get => m_iNumber; set => m_iNumber = value; }
		public string SName { get => m_sName; set => m_sName = value; }
		public string SType { get => m_sType; set => m_sType = value; }
		public int ILevel { get => m_iLevel; set => m_iLevel = value; }
		public int IHP { get => m_iHP; set => m_iHP = value; }
		public int IHunger { get => m_iHunger; set => m_iHunger = value; }
		public int ISleep { get => m_iSleep; set => m_iSleep = value; }
		public int IMaxAtributeValue { get => m_iMaxAtributeValue; set => m_iMaxAtributeValue = value; }
		public int ILoyalty { get => m_iLoyalty; set => m_iLoyalty = value; }
		public int IExperiencePoints { get => m_iExperiencePoints; set => m_iExperiencePoints = value; }

		// Constructor from other Pokemon
		public Pokemon(Pokemon pPokemonToCopy)
		{
			this.INumber = pPokemonToCopy.INumber;
			this.SName = pPokemonToCopy.SName;
			this.SType = pPokemonToCopy.SType;
			this.ILevel = 5;
			this.IHP = 15;
			this.IHunger = 15;
			this.ISleep = 15;
			this.ILoyalty = 15;
			this.IMaxAtributeValue = 15;
			this.IExperiencePoints = 0;
		}

		// Constructor by attributes
		public Pokemon(int iNumber, string sName, string sType)
		{
			this.INumber = iNumber;
			this.SName = sName;
			this.SType = sType;
			this.ILevel = 5;
			this.IHP = 15;
			this.IHunger = 15;
			this.ISleep = 15;
			this.ILoyalty = 15;
			this.IMaxAtributeValue = 15;
			this.IExperiencePoints = 0;
		}

		// Method to return if Pokemon is ok to continue game or not
		public bool IsWell()
		{
			return this.IHP > 0 && this.ISleep > 0 && this.IHunger > 0 && this.ILoyalty > 0;
		}

		// Method for Leveling Up: It will increase the level, some attributes and set experience to zero again
		public void LevelUp()
		{
			this.ILevel++;
			this.IMaxAtributeValue += this.ILevel;
			this.ILoyalty += this.ILevel;
			this.IHunger += this.ILevel;
			this.ISleep += this.ILevel;
			this.IExperiencePoints = 0;
		}

		// Method to Feed Pokemon: It increase hunger and HP, but decrease sleep
		public void Feed()
		{
			this.IHunger = SumAndCheckAttributes(this.IHunger, 2);
			this.IHP = SumAndCheckAttributes(this.IHP, 1);
			this.ISleep -= 1;

			this.Answer($"You fed { this.SName }. It loved your food!");
		}

		// Method to put pokemon to sleep: It restores the Pokemon's HP and sleepness, but make it hunger and less loyalty
		public void Sleep()
		{
			this.ISleep = SumAndCheckAttributes(this.ISleep, 2);
			this.IHP = SumAndCheckAttributes(this.IHP, 1);
			this.IHunger -= 2;
			this.ILoyalty -= 1;

			this.Answer($"You put { this.SName } to sleep. It's now resting!");
		}

		// Method to put pokemon to practice: It increases the experience in battle and also the loyalty, but take some HP. In this method we also check to level up
		public void Practice()
		{
			this.IExperiencePoints += this.ILevel;
			this.ILoyalty = SumAndCheckAttributes(this.ILoyalty, 1);
			this.IHP -= 2;

			if (this.IExperiencePoints % (this.ILevel * 10) == 0)
			{
				this.LevelUp();
				this.Answer($"You trained { this.SName }. So much training make it stronger! It raised one level!");
			}
			else
			{
				this.Answer($"You trained { this.SName }. Keep training and it could level up!");
			}
		}

		// Get feedback by stats
		public void Feedback()
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			// Check for HP
			if (this.IHP <= 5)
			{
				Console.WriteLine($"{ this.SName } is dying do something!");
			}
			else if (this.IHP > 5 && this.IHP < 10)
			{
				Console.WriteLine($"Be caureful with { this.SName }'s health.");
			}
			else
			{
				Console.WriteLine($"{ this.SName } is full of life.");
			}

			// Check for Sleep
			if (this.ISleep <= 5)
			{
				Console.WriteLine($"{ this.SName } is dying because it's so tired!");
			}
			else if (this.ISleep > 5 && this.ISleep < 10)
			{
				Console.WriteLine($"{ this.SName } need to rest.");
			}
			else
			{
				Console.WriteLine($"{ this.SName } is full of energy.");
			}

			// Check for Hunger
			if (this.IHunger <= 5)
			{
				Console.WriteLine($"{ this.SName } is too hungry! Do something!");
			}
			else if (this.IHunger > 5 && this.IHunger < 10)
			{
				Console.WriteLine($"{ this.SName } need to eat.");
			}
			else
			{
				Console.WriteLine($"{ this.SName } is full of food, almost rolling.");
			}

			// Check for Loyalty
			if (this.ILoyalty <= 5)
			{
				Console.WriteLine($"{ this.SName } is one step to not obey you anymore!");
			}
			else if (this.ILoyalty > 5 && this.ILoyalty < 10)
			{
				Console.WriteLine($"{ this.SName } is becoming a rebel. Be caureful.");
			}
			else
			{
				Console.WriteLine($"{ this.SName } love to do everything you ask.");
			}

			Console.ResetColor();
			Console.ReadKey();
		}

		// Method to pet pokemon: Creates a boundary with the pokemon. Increases loyalty, but make it more sleepy and hunger.
		public void Pet()
		{
			this.ILoyalty = SumAndCheckAttributes(this.ILoyalty, 2);
			this.ISleep -= 2;
			this.IHunger -= 1;

			this.Answer($"You pet { this.SName }. Creating affection is very good for both of you!");
		}

		// Method to put pokemon inside the pokeball: It restores the Pokemon's HP, but reduces loyalty and make it sleepy.
		public void PutInsideThePokeball()
		{
			this.IHP = SumAndCheckAttributes(this.IHP, 2);
			this.ILoyalty -= 2;
			this.ISleep -= 1;

			this.Answer($"You put { this.SName } inside the pokeball. It is a good place to rest.");
		}

		// Auxiliar method to control the max attribute value. That is, any attribute can only reach the max value.
		private int SumAndCheckAttributes(int iAttribute, int iToAdd)
		{
			iAttribute += iToAdd;
			if (iAttribute > this.IMaxAtributeValue)
			{
				return this.IMaxAtributeValue;
			}

			return iAttribute;
		}

		// Restore the Pokemon's stats to begginig.
		public void RevivePokemon()
		{
			this.ILevel = 5;
			this.IHP = 15;
			this.IHunger = 15;
			this.ISleep = 15;
			this.ILoyalty = 15;
			this.IMaxAtributeValue = 15;
			this.IExperiencePoints = 0;
		}

		// Friendly way to show pokemon's answer to user
		private void Answer(string message)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(message);
			Console.ResetColor();
			Console.ReadKey();
		}
	}
}
