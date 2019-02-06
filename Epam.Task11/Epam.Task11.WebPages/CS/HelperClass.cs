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
        public static string GetUserImagePath(User user)
        {
            if (user.Image != null)
            {
                WebImage img = new WebImage(user.Image);
                DeleteImage(user.Id, BLLProvider.UserImageFIlePath);

                string fullPath = $"{GetFullFilePath(BLLProvider.UserImageFIlePath)}{user.Id}.{img.ImageFormat}";
                img.Save(fullPath);

                return $"/Content/images/userIcons/{user.Id}.{img.ImageFormat}";
            }

            return "/Content/images/DefaultUserIcon.png";
        }

        public static string GetAwardImagePath(Award award)
        {
            if (award.Image != null)
            {
                WebImage img = new WebImage(award.Image);
                DeleteImage(award.Id, BLLProvider.AwardImageFIlePath);

                string fullPath = $"{GetFullFilePath(BLLProvider.AwardImageFIlePath)}{award.Id}.{img.ImageFormat}";
                img.Save(fullPath);

                return $"/Content/images/awardIcons/{award.Id}.{img.ImageFormat}";
            }

            return "/Content/images/DefaultAwardIcon.png";
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

        public static void DeleteImage(int id, string imagePath)
        {
            var images = Directory.GetFiles(GetFullFilePath(imagePath), id + ".*");

            if (images.Length > 0)
            {
                foreach (var img in images)
                {
                    File.Delete(img);
                }
            }
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
            if (name.Length < 1 || name.Length > 30)
            {
                return false;
            }

            var allowedSeparatorSymbols = new char[] { '-', '\'', ' ' };

            if (!char.IsLetter(name.First())
                || !char.IsLetter(name.Last()))
            {
                return false;
            }

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
            return title != null && title.Length < 50 && !title.Contains('|');
        }

        public static bool IsValidRegistrationInfo(string email, string login, string password, string repeatPassword)
        {
            return CheckEmailFormat(email) && CheckLoginFormat(login) && CheckPasswordFormat(password) && password == repeatPassword;
        }

        private static bool CheckEmailFormat(this string email)
        {
            string emailTemplate = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

            return Regex.IsMatch(email, emailTemplate, RegexOptions.IgnoreCase);
        }

        private static bool CheckLoginFormat(string login)
        {
            string loginTemplate = "^[a-zA-Z]{3,20}$";

            if (BLLProvider.AccountLogic.GetAll().All(acc => acc.Login != login))
            {
                return Regex.IsMatch(login, loginTemplate, RegexOptions.IgnoreCase);
            }

            return false;
        }

        private static bool CheckPasswordFormat(string password)
        {
            string passwordTemplate = "^[a-zA-Z0-9]{6,20}$";

            return Regex.IsMatch(password, passwordTemplate, RegexOptions.IgnoreCase);
        }

        private static string GetFullFilePath(string path)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            path = new string(path.SkipWhile(c => c != Path.DirectorySeparatorChar).Skip(1).ToArray());

            return baseDir + path;
        }
    }
}