using GuessNumber.Service;

namespace Test
{
    public class ControllerTest
    {
        [Fact]
        public void TryParseInput_ValidInput_ReturnsTrueAndParsesDigits()
        {
            // Arrange
            string input = "1234";

            // Act
            bool result = Logic.TryParseInput(input, out int[] digits);

            // Assert
            Assert.True(result);
            Assert.Equal(new int[] { 1, 2, 3, 4 }, digits);
        }

        [Fact]
        public void TryParseInput_InvalidInput_ReturnsFalse()
        {
            // Arrange
            string input = "12AB"; // Contains non-digit characters

            // Act
            bool result = Logic.TryParseInput(input, out int[] digits);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CalculateM_ReturnsCorrectCountOfMatchingDigits()
        {
            // Arrange

            int[] secretNumber = { 1, 2, 3, 4 };
            int[] userGuess = { 3, 2, 5, 6 };

            // Act
            int m = Logic.CalculateM(secretNumber, userGuess);

            // Assert
            Assert.Equal(2, m); // 2 digits match (2 and 3)
        }

        [Fact]
        public void CalculateP_ReturnsCorrectCountOfMatchingPositions()
        {
            // Arrange
            int[] secretNumber = { 1, 2, 3, 4 };
            int[] userGuess = { 3, 2, 5, 6 };

            // Act
            int p = Logic.CalculateP(secretNumber, userGuess);

            // Assert
            Assert.Equal(1, p); // 2 digits are in the correct positions (3 and 2)
        }
    }
}