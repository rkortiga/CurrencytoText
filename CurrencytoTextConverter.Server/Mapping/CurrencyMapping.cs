using System.Collections.ObjectModel;

namespace CurrencytoTextConverter.Server.Mapping
{
	//This class contains dictionaries that are used to map numbers to their word representations for numbers in ones and tens places.
	//This allows for easy lookup of the word representation of numbers.
	public class CurrencyMapping
	{
		public ReadOnlyDictionary<int, string> OnesDictionary { get; } = new(new Dictionary<int, string>
		{
			{ 1, "One" },
			{ 2, "Two" },
			{ 3, "Three" },
			{ 4, "Four" },
			{ 5, "Five" },
			{ 6, "Six" },
			{ 7, "Seven" },
			{ 8, "Eight" },
			{ 9, "Nine" }
		});

		public ReadOnlyDictionary<int, string> TensDictionary { get; } = new(new Dictionary<int, string>
		{
			{ 10, "Ten" },
			{ 11, "Eleven" },
			{ 12, "Twelve" },
			{ 13, "Thirteen" },
			{ 14, "Fourteen" },
			{ 15, "Fifteen" },
			{ 16, "Sixteen" },
			{ 17, "Seventeen" },
			{ 18, "Eighteen" },
			{ 19, "Nineteen" },
			{ 20, "Twenty" },
			{ 30, "Thirty" },
			{ 40, "Forty" },
			{ 50, "Fifty" },
			{ 60, "Sixty" },
			{ 70, "Seventy" },
			{ 80, "Eighty" },
			{ 90, "Ninety" }
		});
	}
}