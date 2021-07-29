namespace NumericPalindromes
{
    static class PalindromeUtility
    {
        /// <summary>Check whether an input string is a palindrome.</summary>
        /// <param name="str">The string to be checked.</param>
        /// <returns>A boolean of whether it is a palindrome.</returns>
        public static bool IsPalindrome(string str)
        {
            int check_length = str.Length / 2;
            int last_index = str.Length - 1;
            for (int i = 0; i < check_length; i++)
            {
                if (str[i] != str[last_index - i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>Check whether an input number is a palindrome or has a palindrome descendant.
        /// This is a wrapper function to perform the initial number conversion to string before the inner function
        /// takes over.</summary>
        /// <param name="number">The number to be checked.</param>
        /// <returns>A boolean of whether it is a palindrome.</returns>
        public static bool PalindromeDescendant(int number)
        {
            return PalindromeDescendant(number.ToString());
        }

        // <summary>Check whether an input number is a palindrome or has a palindrome descendant recursively.</summary>
        /// <param name="numberString">The number string to be checked.</param>
        /// <returns>A boolean of whether it is a palindrome.</returns>
        private static bool PalindromeDescendant(string numberString)
        {
            if (numberString.Length < 2)
            {
                return false;
            }

            if (IsPalindrome(numberString))
            {
                return true;
            }
            else
            {
                // Add each adjacent number to form a palindrome descendant.
                // Note: instructions say that the numbers will always have an even number of digits, but if that is not
                // not true then the last digit will be truncated.
                string descendant = "";
                for (int i = 1; i < numberString.Length; i += 2)
                {
                    int sum = int.Parse(numberString[i - 1].ToString()) + int.Parse(numberString[i].ToString());
                    descendant += sum.ToString();
                }
                return PalindromeDescendant(descendant);
            }
        }
    }
}
