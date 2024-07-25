using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumberParser;

namespace NumberParserTests
{
    // Reference: http://sierra.nmsu.edu/morandi/coursematerials/RomanNumerals.html
    // I	1
    // V	5
    // X	10
    // L	50
    // C	100
    // D	500
    // M	1000
    // There are a few rules for writing numbers with Roman numerals.
    // - Repeating a numeral up to three times represents addition of the number.For example, III represents 1 + 1 + 1 = 3. Only I, X, C, and M can be repeated; V, L, and D cannot be, and there is no need to do so.
    // - Writing numerals that decrease from left to right represents addition of the numbers.For example, LX represents 50 + 10 = 60 and XVI represents 10 + 5 + 1 = 16.
    // - To write a number that otherwise would take repeating of a numeral four or more times, there is a subtraction rule.Writing a smaller numeral to the left of a larger numeral represents subtraction.For example, IV represents 5 - 1 = 4 and IX represents 10 - 1 = 9. To avoid ambiguity, the only pairs of numerals that use this subtraction rule are
    //      IV	4 = 5 - 1
    //      IX	9 = 10 - 1
    //      XL	40 = 50 - 10
    //      XC	90 = 100 - 10
    //      CD	400 = 500 - 100
    //      CM	900 = 1000 - 100
    //
    // Max value you can specify without "bars" is 3,999 MMMCMXCIX


    [TestClass]
    public class RomanNumeralParserTests
    {
        [TestMethod]
        public void Parse_InvalidCharacters_ThrowsInvalidRomanNumeralFormatException()
        {
            // Arrange
            var invalidCharacters = "ABCWYZ";
            var parser = new RomanNumeralParser();

            // Act/Assert
            Assert.ThrowsException<InvalidRomanNumeralFormatException>(() => parser.Parse(invalidCharacters));
        }

        [TestMethod]
        [DataRow("IIII")]
        [DataRow("VVVV")]
        [DataRow("VVVVVVVV")]
        [DataRow("XXXX")]
        [DataRow("LLLL")]
        [DataRow("CCCC")]
        [DataRow("CCCCC")]
        [DataRow("DDDD")]
        [DataRow("MMMM")]
        [DataRow("VV")]
        [DataRow("LL")]
        [DataRow("DD")]
        public void Parse_InvalidCharacterRepeats_ThrowsInvalidRomanNumeralFormatException(string invalidRomanNumeral)
        {
            // Arrange
            var parser = new RomanNumeralParser();

            // Act/Assert
            Assert.ThrowsException<InvalidRomanNumeralFormatException>(() => parser.Parse(invalidRomanNumeral));
        }

        [TestMethod]
        [DataRow("I", 1)]
        [DataRow("III", 3)]
        [DataRow("IV", 4)]
        [DataRow("V", 5)]
        [DataRow("X", 10)]
        [DataRow("L", 50)]
        [DataRow("C", 100)]
        [DataRow("D", 500)]
        [DataRow("M", 1000)]
        [DataRow("CMXC", 990)]
        [DataRow("MMMCDXCIV", 3494)]
        [DataRow("MMMDXC", 3590)]
        [DataRow("MMMDCXLI", 3641)]
        [DataRow("MMMDCLXXXIX", 3689)]
        [DataRow("MMMDCCLXXXIV", 3784)]
        [DataRow("MMMDCCCLXXXVIII", 3888)]
        [DataRow("MMMCMLXX", 3970)]
        [DataRow("MMMCMXCIX", 3999)]
        public void Parse_GivenRomanNumeral_ParsesCorrectly(string romanNumeral, int expectedValue)
        {
            // Arrange
            var parser = new RomanNumeralParser();

            // Act
            var result = parser.Parse(romanNumeral);

            // Assert
            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("   ")]
        public void Parse_RomanNumeralIsNullOrWhiteSpace_ReturnsZero(string nullOrWhiteSpaceRomanNumeral)
        {
            // Arrange
            var parser = new RomanNumeralParser();

            // Act
            var result = parser.Parse(nullOrWhiteSpaceRomanNumeral);

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}
