using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task3.MyString
{
    public class MyString
    {
        public MyString()
        {
        }

        public MyString(string str)
        {
            this.CharList = str.ToCharArray();
        }

        public MyString(char[] charArray)
        {
            this.CharList = charArray;
        }

        public int Length => this.CharList.Length;

        private char[] CharList { get; set; }

        public char this[int i]
        {
            get
            {
                return this.CharList[i];
            }

            set
            {
                this.CharList[i] = value;
            }
        }

        public static MyString operator +(MyString s1, MyString s2)
        {
            return new MyString((string)s1 + (string)s2);
        }

        public static explicit operator MyString(string s)
        {
            return new MyString(s);
        }

        public static explicit operator MyString(char[] charArray)
        {
            return new MyString(charArray);
        }

        public static explicit operator string(MyString myString)
        {
            return new string(myString.CharList);
        }

        public static implicit operator char[](MyString myString)
        {
            return myString.CharList;
        }

        public static implicit operator StringBuilder(MyString myString)
        {
            return new StringBuilder(myString.ToString());
        }

        public int Compare(string s)
        {
            if (this.CharList.Length > s.Length)
            {
                return 1;
            }
            else if (this.CharList.Length < s.Length)
            {
                return -1;
            }
            else
            {
                for (int i = 0; i < this.CharList.Length; i++)
                {
                    if (this.CharList[i] > s[i])
                    {
                        return 1;
                    }
                    else if (this.CharList[i] < s[i])
                    {
                        return -1;
                    }
                }

                return 0;
            }
        }

        public int Compare(MyString s)
        {
            if (this.CharList.Length > s.Length)
            {
                return 1;
            }
            else if (this.CharList.Length < s.Length)
            {
                return -1;
            }
            else
            {
                for (int i = 0; i < this.CharList.Length; i++)
                {
                    if (this.CharList[i] > s[i])
                    {
                        return 1;
                    }
                    else if (this.CharList[i] < s[i])
                    {
                        return -1;
                    }
                }

                return 0;
            }
        }

        public MyString Concat(char c)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.CharList);
            sb.Append(c);

            return (MyString)sb.ToString();
        }

        public MyString Concat(string s)
        {
            return (MyString)(new string(this.CharList) + s);
        }

        public MyString Concat(MyString s)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.CharList);
            sb.Append((char[])s);

            return (MyString)sb.ToString();
        }

        public bool Contains(char c)
        {
            for (int i = 0; i < this.CharList.Length; i++)
            {
                if (this.CharList[i] == c)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Contains(string s)
        {
            if (this.CharList.Length < s.Length)
            {
                return false;
            }

            for (int i = 0; i < this.CharList.Length; i++)
            {
                if (this.CharList[i] == s[0])
                {
                    for (int j = 0; j < s.Length; j++)
                    {
                        if (i + j == this.CharList.Length)
                        {
                            return false;
                        }

                        if (s[j] != this.CharList[i + j])
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        public bool Contains(MyString s)
        {
            if (this.CharList.Length < s.Length)
            {
                return false;
            }

            for (int i = 0; i < this.CharList.Length; i++)
            {
                if (this.CharList[i] == s[0])
                {
                    for (int j = 0; j < s.Length; j++)
                    {
                        if (i + j == this.CharList.Length)
                        {
                            return false;
                        }

                        if (s[j] != this.CharList[i + j])
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            return new string(this.CharList);
        }
    }
}
