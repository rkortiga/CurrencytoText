namespace CurrencytoTextConverter.Server.Utility
{
    // This utility class contains the logic to convert a currency amount to text.
    // It contains helper methods such as DecimalConverter, OnesConverter, TensConverter.
    // The MainConverter method is the main method that converts the currency amount to text.

    public class Utility
    {
        //These dictionaries are used to map numbers to their word representations for numbers in ones and tens places.
        //This allows for easy lookup of the word representation of numbers.

        private static readonly Dictionary<int, string> OnesDictionary = new Dictionary<int, string>
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
        };

        private static readonly Dictionary<int, string> TensDictionary = new Dictionary<int, string>
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
        };

        // This method is used to convert the decimal part of the currency amount to text.
        public static string DecimalConverter(decimal amount)
        {
            string wholeNumber = amount.ToString().Split('.')[0];
            string decimalNumber = amount.ToString().Split('.')[1];
            string result;

            if (decimalNumber != "0")
            {
                result = $"{MainConverter(Convert.ToDecimal(wholeNumber))} and {MainConverter(Convert.ToDecimal(decimalNumber))} cents";
            }
            else
            {
                result = $"{MainConverter(Convert.ToDecimal(wholeNumber))} dollars";
            }
            return result;
        }

        // These methods are used to convert numbers in the ones and tens places to their word representations.
        public static string OnesConverter(decimal amount)
        {
            int _amount = Convert.ToInt32(amount);
            string result = "";

            if (OnesDictionary.ContainsKey(_amount))
            {
                result = OnesDictionary[_amount];
            }
            return result;
        }

        public static string TensConverter(decimal amount)
        {
            int _amount = Convert.ToInt32(amount);
            string result;

            if (TensDictionary.ContainsKey(_amount))
            {
                result = TensDictionary[_amount];
            }
            else
            {
                result = TensConverter(_amount - _amount % 10) + "-" + OnesConverter(_amount % 10);
            }
            return result;
        }

        //This is the main method used to convert the currency amount to text.

        public static string MainConverter(decimal amount)
        {
            double _amount = Convert.ToDouble(amount);
            string result = "";
            bool isFinished = false;
            if (_amount > 0)
            {
                int numLength = _amount.ToString().Length;
                string placeValue = "";
                int position = 0;

                switch (numLength)
                {
                    case 1:
                        result = OnesConverter((decimal)_amount);
                        isFinished = true;
                        break;
                    case 2:
                        result = TensConverter((decimal)_amount);
                        isFinished = true;
                        break;
                    case 3:
                        position = 1;
                        placeValue = "Hundred";
                        break;
                    case 4:
                    case 5:
                    case 6:
                        position = (numLength % 4) + 1;
                        placeValue = "Thousand";
                        break;
                    case 7:
                    case 8:
                    case 9:
                        position = (numLength % 7) + 1;
                        placeValue = "Million";
                        break;
                    case 10:
                    case 11:
                    case 12:
                        position = (numLength % 10) + 1;
                        placeValue = "Billion";
                        break;
                    case 13:
                    case 14:
                    case 15:
                        position = (numLength % 13) + 1;
                        placeValue = "Trillion";
                        break;
                    default:
                        return "";
                }

                if (!isFinished)
                {
                    if (_amount.ToString()[..position] != "0" && _amount.ToString()[position..] != "0")
                    {
                        result = MainConverter(Convert.ToDecimal(_amount.ToString()[..position])) + " " + placeValue + " " + MainConverter(Convert.ToDecimal(_amount.ToString()[position..]));
                    }
                    else
                    {
                        result = MainConverter(Convert.ToDecimal(_amount.ToString()[..position])) + " " + MainConverter(Convert.ToDecimal(_amount.ToString()[position..]));
                    }
                }

            }
            return result;
        }
    }
}
