using System;

namespace NumberParser
{
    public class InvalidRomanNumeralFormatException : Exception
    {
        public InvalidRomanNumeralFormatException()
        {
        }

        public InvalidRomanNumeralFormatException(string message)
            : base(message)
        {
        }
    }
}