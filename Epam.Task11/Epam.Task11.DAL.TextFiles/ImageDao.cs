using System.IO;
using System.Linq;

namespace Epam.Task7.DAL.TextFiles
{
    public static class ImageDao
    {
        internal static readonly char DirSeparator = Path.DirectorySeparatorChar;

        public static void AddAwardImage(int id, byte[] image, string folder)
        {
            var awards = UserAwardDao.GetAwards().ToList();
            int awardIndex = awards.IndexOf(awards.FirstOrDefault(a => a.Id == id));

            if (awardIndex > -1)
            {
                AddImage(id, image, folder);

                awards[awardIndex].Image = image;
                var awardsAsTxt = awards.Select(AwardDao.AwardAsTxt);

                File.WriteAllLines(AwardDao.AwardsFilePath, awardsAsTxt);
            }
        }

        public static void AddUserImage(int id, byte[] image, string folder)
        {
            var users = UserAwardDao.GetUsers().ToList();
            int userIndex = users.IndexOf(users.FirstOrDefault(a => a.Id == id));

            if (userIndex > -1)
            {
                AddImage(id, image, folder);

                users[userIndex].Image = image;
                var usersAsTxt = users.Select(UserDao.UserAsTxt);

                File.WriteAllLines(UserDao.UsersFilePath, usersAsTxt);
            }
        }

        public static bool RemoveAwardImage(int id, string folder)
        {
            if (RemoveImage(id, folder))
            {
                var awards = UserAwardDao.GetAwards().ToList();
                var awardsAsTxt = awards.Select(AwardDao.AwardAsTxt);
                File.WriteAllLines(AwardDao.AwardsFilePath, awardsAsTxt);

                return true;
            }

            return false;
        }

        public static bool RemoveUserImage(int id, string folder)
        {
            if (RemoveImage(id, folder))
            {
                var users = UserAwardDao.GetUsers().ToList();
                var usersAsTxt = users.Select(UserDao.UserAsTxt);
                File.WriteAllLines(UserDao.UsersFilePath, usersAsTxt);

                return true;
            }

            return false;
        }

        private static void AddImage(int id, byte[] image, string folder)
        {
            string filePath = $"{UserAwardDao.Folder}{folder}{DirSeparator}{id}";
            string file = filePath.Take(filePath.Length - 1).CharCollectionAsString();

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (var writer = new BinaryWriter(new FileStream(filePath, FileMode.Create, FileAccess.Write)))
            {
                writer.Write(image, 0, image.Length);
            }
        }

        private static bool RemoveImage(int id, string folder)
        {
            string filePath = $"{UserAwardDao.Folder}{folder}{DirSeparator}{id}";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);

                return true;
            }

            return false;
        }
    }
}
