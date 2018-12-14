using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task5.IntOrNot
{
    public static class IntOrNot
    {
        public static bool IsEvenNumber(this string str)
        {
            if (str == string.Empty || !IsArabicDigit(str.Last()))
            {
                return false;
            }

            if (str.All(IsArabicDigit))
            {
                return true;
            }

            const char CommaChar = ',';
            const char EBigChar = 'E';
            const char EsmallChar = 'e';
            const char PlusChar = '+';
            const char MinusChar = '-';
            const char Zero = '0';

            bool comma = false;
            bool exponent = false;
            bool plus = false;
            bool minus = false;

            char e = default(char);
            int commaIndex = -1;

            if (str[0] == PlusChar)
            {
                str = str.Skip(1).CharCollectionToString();
            }

            if (!IsArabicDigit(str[0]))
            {
                return false;
            }

            try
            {
                for (int i = 1; i < str.Length; i++)
                {
                    if (!IsArabicDigit(str[i]))
                    {
                        if (str[i] != CommaChar && str[i] != EBigChar && str[i] != EsmallChar && str[i] != PlusChar && str[i] != MinusChar)
                        {
                            return false;
                        }

                        if (str[i] == CommaChar)
                        {
                            if (!IsArabicDigit(str[i - 1]) || !IsArabicDigit(str[i + 1]) || comma || exponent || plus || minus)
                            {
                                return false;
                            }

                            comma = true;
                            commaIndex = i;
                            continue;
                        }

                        if (str[i] == EsmallChar || str[i] == EBigChar)
                        {
                            if (!IsArabicDigit(str[i - 1]) || exponent || plus || minus)
                            {
                                return false;
                            }

                            exponent = true;
                            e = str[i];
                            continue;
                        }

                        if (str[i] == PlusChar)
                        {
                            if (!IsArabicDigit(str[i + 1]) || !exponent || plus || minus)
                            {
                                return false;
                            }

                            plus = true;
                            continue;
                        }

                        if (str[i] == MinusChar)
                        {
                            if (!IsArabicDigit(str[i + 1]) || !exponent || plus || minus)
                            {
                                return false;
                            }

                            minus = true;
                            continue;
                        }
                    }
                }

                if (!minus)
                {
                    plus = true;
                }

                if (!exponent)
                {
                    return false;
                }

                IEnumerable<char> integer = str.TakeWhile(c => IsArabicDigit(c));
                IEnumerable<char> fraction = null;
                IEnumerable<char> order = str.Reverse().TakeWhile(c => IsArabicDigit(c)).Reverse();

                int integerAsNumber = CharCollectionAsInt(integer);
                int orderAsNumber = CharCollectionAsInt(order);
                int fractionAsReversedNumber = 0;

                if (comma)
                {
                    fraction = str.Skip(commaIndex + 1).TakeWhile(c => IsArabicDigit(c));
                    fractionAsReversedNumber = CharCollectionAsInt(fraction.Reverse());

                    if (integerAsNumber == 0)
                    {
                        if (fractionAsReversedNumber == 0)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (integerAsNumber == 0)
                    {
                        return false;
                    }
                }

                if (comma && plus)
                {
                    int fractionCount = fraction.Reverse().SkipWhile(c => c == Zero).Count();

                    if (fractionCount > orderAsNumber)
                    {
                        return false;
                    }
                }
                else if (!comma && minus)
                {
                    int integerZeroCount = integer.Reverse().TakeWhile(c => c == Zero).Count();

                    if (integerZeroCount < orderAsNumber)
                    {
                        return false;
                    }
                }
                else if (comma && minus)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            
            return true;
        }

        public static bool IsArabicDigit(char c)
        {
            return c >= '0' && c <= '9'; 
        }

        private static int CharCollectionAsInt(IEnumerable<char> collection)
        {
            int number = 0;
            int rate = collection.Count() - 1;

            foreach (var c in collection)
            {
                number += (int)((c - '0') * Math.Pow(10, rate));
                rate--;
            }

            return number;
        }

        private static string CharCollectionToString(this IEnumerable<char> collection)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var c in collection)
            {
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
