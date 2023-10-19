using System.Security.Cryptography;

namespace GuessNumber.Entity
{
    public class GameSession
    {
        public int[] SecretNumber { get; }
        public int Attempts { get; set; }

        public List<string> Description { get; set; }

        public GameSession()
        {
            SecretNumber = GenerateSecretNumber();
            Attempts = 8;
            Description = new List<string>();
        }

        private int[] GenerateSecretNumber()
        {
            Random random = new Random();
            int[] digits = new int[4];

            for (int i = 0; i < 4; i++)
            {
                while (true)
                {
                    int digit = random.Next(0, 10);

                    if (!digits.Contains(digit))
                    {
                        digits[i] = digit;
                        break;
                    }
                }
            }

            return digits;
        }
    }
}
