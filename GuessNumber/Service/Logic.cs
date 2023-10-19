namespace GuessNumber.Service
{
    public static class Logic
    {
        public static bool TryParseInput(string input, out int[] digits)
        {
            if (input.Length == 4 && input.All(char.IsDigit))
            {
                digits = input.Select(ch => ch - '0').ToArray();
                return true;
            }
            else
            {
                digits = null;
                return false;
            }
        }

        public static int CalculateM(int[] secretNumber, int[] guess)
        {
            int count = 0;
            foreach (int digit in secretNumber)
            {
                foreach (int userDigit in guess)
                {
                    if (digit == userDigit)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public static int CalculateP(int[] secretNumber, int[] guess)
        {
            int count = 0;

            for (int i = 0; i < secretNumber.Length; i++)
            {
                if (secretNumber[i] == guess[i])
                {
                    count++;
                }
            }

            return count;
        }
    }
}
