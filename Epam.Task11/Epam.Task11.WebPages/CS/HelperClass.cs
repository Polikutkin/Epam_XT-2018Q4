using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Helpers;
using Epam.Task7.Entities;

namespace Epam.Task11.WebPages.CS
{
    public static class HelperClass
    {
        private const string DefaultUserImageFilePath = "/Content/images/DefaultUserIcon.png";
        private const string DefaultAwardImageFilePath = "/Content/images/DefaultAwardIcon.png";

        public static string GetUserImagePath(User user)
        {
            if (user.Image != null)
            {
                WebImage img = new WebImage(user.Image);

                return $"data:image/{img.ImageFormat};base64,{Convert.ToBase64String(user.Image)}";
            }

            return DefaultUserImageFilePath;
        }

        public static string GetAwardImagePath(Award award)
        {
            if (award.Image != null)
            {
                WebImage img = new WebImage(award.Image);

                return $"data:image/{img.ImageFormat};base64,{Convert.ToBase64String(award.Image)}";
            }

            return DefaultAwardImageFilePath;
        }

        public static void WorkWithImage(WebImage img, int x, int y)
        {
            int width = img.Width;
            int height = img.Height;
            int sizeDiff = width - height;

            if (sizeDiff > 0)
            {
                img.Crop(0, sizeDiff / 2, 0, sizeDiff / 2);
            }
            else if (sizeDiff < 0)
            {
                sizeDiff = ~sizeDiff + 1;

                img.Crop(sizeDiff / 2, 0, sizeDiff / 2, 0);
            }

            img.Resize(x, y);
        }

        public static string HashStringWithSha512(string inputString)
        {
            var crypt = new SHA512Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(inputString));

            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("X2"));
            }

            return hash.ToString();
        }

        public static bool CheckName(this string name)
        {
            if (name.Length < 1
                || name.Length > 30
                || !char.IsLetter(name.First())
                || !char.IsLetter(name.Last()))
            {
                return false;
            }

            var allowedSeparatorSymbols = new char[] { '-', '\'', ' ' };

            for (int i = 1; i < name.Length - 1; i++)
            {
                if (!char.IsLetter(name[i]) && !allowedSeparatorSymbols.Contains(name[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CheckDate(this DateTime date)
        {
            DateTime now = DateTime.Now;

            if (now.Year - date.Year > 150
                || now.Year - date.Year < 5
                || date > now)
            {
                return false;
            }

            return true;
        }

        public static bool CheckAwardTitle(this string title)
        {
            return title != null
                && title.Length < 50
                && !title.Contains('|');
        }

        public static bool IsValidPassword(string password, string repeatPassword)
        {
            string passwordTemplate = "^[a-zA-Z0-9]{6,20}$";

            return Regex.IsMatch(password, passwordTemplate, RegexOptions.IgnoreCase)
                && password == repeatPassword;
        }
    }
}