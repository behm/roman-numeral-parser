using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberParser
{
    public class RomanNumeralParser
    {
        public const string ValidCharacters = "IVXLCDM";

        private readonly Dictionary<string, int> DigitValues;

        public RomanNumeralParser()
        {
            DigitValues = new Dictionary<string, int>
            {
                {"I", 1},
                {"V", 5},
                {"X", 10},
                {"L", 50},
                {"C", 100},
                {"D", 500},
                {"M", 1000},
                // subtraction combinations
                {"IV", 4},
                {"IX", 9},
                {"XL", 40},
                {"XC", 90},
                {"CD", 400},
                {"CM", 900},
            };
        }

        public int Parse(string romanNumeral)
        {
            if (string.IsNullOrWhiteSpace(romanNumeral))
            {
                return 0;
            }

            if (!romanNumeral.All(c => ValidCharacters.Contains(c)))
            {
                throw new InvalidRomanNumeralFormatException("Contains invalid characters");
            }

            if (romanNumeral.Contains("IIII")
                || romanNumeral.Contains("VV")
                || romanNumeral.Contains("LL")
                || romanNumeral.Contains("DD")
                || romanNumeral.Contains("XXXX")
                || romanNumeral.Contains("LLLL")
                || romanNumeral.Contains("CCCC")
                || romanNumeral.Contains("DDDD")
                || romanNumeral.Contains("MMMM"))
            {
                throw new InvalidRomanNumeralFormatException("Character repeats too many times");
            }

            var i = 0;
            var total = 0;
            while (i < romanNumeral.Length)
            {
                // Check for subtraction combo
                if ((i + 1) < romanNumeral.Length)
                {
                    var combo = romanNumeral.Substring(i, 2);
                    if (DigitValues.TryGetValue(combo, out var comboValue))
                    {
                        total += comboValue;
                        i += combo.Length;
                        continue;
                    }
                }

                var digit = romanNumeral[i].ToString();

                if (!DigitValues.TryGetValue(digit, out var value))
                {
                    // todo: invalid character
                }

                total += value;

                i++;
            }

            return total;
        }
    }
}
