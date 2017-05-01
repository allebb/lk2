using System;
using System.Collections.Generic;
using System.Linq;

namespace LK2.Services
{
    /// <summary>
    /// A Base62 Encoding (and decoding) service.
    /// </summary>
    public static class Base62
    {

        /// <summary>
        /// The character list that will be used to generate Base62 strings from.
        /// </summary>
        private const string CharList = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Encode the given number into a Base62 string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String Encode(int input)
        {

            char[] clistarr = CharList.ToCharArray();
            var result = new Stack<char>();
            while (input != 0)
            {
                result.Push(clistarr[input % 62]);
                input /= 62;
            }
            return new string(result.ToArray());
        }

        /// <summary>
        /// Decode the Base62 encoded string into a number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Int64 Decode(string input)
        {
            var reversed = input.ToLower().Reverse();
            long result = 0;
            int pos = 0;
            foreach (char c in reversed)
            {
                result += CharList.IndexOf(c) * (long)Math.Pow(62, pos);
                pos++;
            }
            return result;
        }
    }
}
