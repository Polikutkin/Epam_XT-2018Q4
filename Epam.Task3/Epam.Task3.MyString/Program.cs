using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.MyString
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Some work with class \"MyString\"");
            Console.WriteLine(Environment.NewLine);

            string s1 = "First string";
            string s2 = "Second string";
            string s3 = "First string";
            char[] charArray = s3.ToCharArray();

            MyString myStr1 = new MyString(s1);
            MyString myStr2 = new MyString(s2);
            MyString myStr3 = new MyString(charArray);

            int comp1 = myStr1.Compare(myStr2);
            int comp2  = myStr1.Compare(s3);

            char l = 'l';
            string s = "rst stri";
            bool b1 = myStr1.Contains(l);
            bool b2 = myStr1.Contains(s);
            bool b3 = myStr1.Contains(myStr3);

            char dot = '.';
            string arrow = " => ";
            MyString myStr4 = myStr1.Concat('.');
            MyString myStr5 = myStr1.Concat(" => ");
            MyString myStr6 = myStr1.Concat(myStr2);

            MyString myStr7 = myStr1 + myStr3;

            Console.WriteLine($"{nameof(myStr1)} = {myStr1}");
            Console.WriteLine($"{nameof(myStr2)} = {myStr2}");
            Console.WriteLine($"{nameof(myStr3)} = {myStr3}");

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"{nameof(myStr1)} is bigger (1), smaller (2) or equal (0) to {nameof(myStr2)}: {comp1}");
            Console.WriteLine($"{nameof(myStr1)} is bigger (1), smaller (2) or equal (0) to {nameof(s3)}: {comp2}");

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"{nameof(myStr1)} contains '{l}': {b1}");
            Console.WriteLine($"{nameof(myStr1)} contains \"{s}\": {b2}");
            Console.WriteLine($"{nameof(myStr1)} contains {nameof(myStr3)}: {b3}");

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Sum of MyStrings with Concat():");
            Console.WriteLine($"{nameof(myStr1)} and '{dot}': {myStr4}");
            Console.WriteLine($"{nameof(myStr1)} and \"{arrow}\": {myStr5}");
            Console.WriteLine($"{nameof(myStr1)} and {nameof(myStr2)}: {myStr6}");

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Sum of MyStrings with overloaded operator '+':");
            Console.WriteLine($"{nameof(myStr1)} + {nameof(myStr3)}: {myStr7}");

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"{nameof(myStr1)}[6] = {myStr1[6]}");
        }
    }
}
