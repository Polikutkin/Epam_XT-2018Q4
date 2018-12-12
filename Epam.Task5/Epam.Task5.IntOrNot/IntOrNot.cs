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
            if (str == string.Empty || !char.IsNumber(str.First()) || !char.IsNumber(str.Last()))
            {
                return false;
            }

            if (str.All(char.IsNumber))
            {
                return true;
            }

            const char DotChar = '.';
            const char EChar = 'E';
            const char EsmallChar = 'e';
            const char PlusChar = '+';

            bool dot = false;
            bool e = false;
            bool plus = false;

            try
            {
                for (int i = 1; i < str.Length; i++)
                {
                    if (!char.IsNumber(str[i]))
                    {
                        if (str[i] == DotChar)
                        {
                            if (!char.IsNumber(str[i - 1]) || !char.IsNumber(str[i + 1]) || dot || e || plus)
                            {
                                return false;
                            }

                            dot = true;
                            continue;
                        }

                        if (str[i] == EsmallChar || str[i] == EChar)
                        {
                            if (!char.IsNumber(str[i - 1]) || (str[i + 1] != PlusChar && !char.IsNumber(str[i + 1])) || !dot || e || plus)
                            {
                                return false;
                            }

                            e = true;
                            continue;
                        }

                        if (str[i] == PlusChar)
                        {
                            if (!char.IsNumber(str[i + 1]) || !dot || !e || plus)
                            {
                                return false;
                            }

                            plus = true;
                            continue;
                        }

                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
