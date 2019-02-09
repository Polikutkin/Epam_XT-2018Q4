using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Epam.Task7.DAL.Contracts;
using Epam.Task7.Entities;

namespace Epam.Task7.DAL.TextFiles
{
    public class AccountDao : IAccountDao
    {
        internal static readonly string AccountsFilePath;
        internal static readonly string CurrentIdFilePath;

        private const string CurrentId = "accountId.txt";
        private const string AccountsFile = "accounts.txt";
        private const string AdminRole = "admin";
        private const string UserRole = "user";
        private const char Separator = '|';

        private static int maxId = 0;
        private static object locker = new object();

        static AccountDao()
        {
            AccountsFilePath = Path.Combine(UserAwardDao.Folder, AccountsFile);
            CurrentIdFilePath = Path.Combine(UserAwardDao.Folder, CurrentId);

            if (!File.Exists(CurrentIdFilePath))
            {
                File.WriteAllText(CurrentIdFilePath, maxId.ToString());
            }

            if (!File.Exists(AccountsFilePath))
            {
                File.WriteAllText(AccountsFilePath, $"{maxId}{Separator}admin{Separator}{HashStringWithSha512("admin")}{Separator}admin@email.com{Separator}{AdminRole}{Environment.NewLine}");
            }
        }

        public AccountDao()
        {
            if (File.Exists(CurrentIdFilePath))
            {
                lock (locker)
                {
                    bool idParse = int.TryParse(File.ReadAllText(CurrentIdFilePath), out var id);

                    maxId = id; 
                }
            }
        }

        public Account GetAccount(string login)
        {
            var accounts = this.GetAll().ToList();

            return accounts.FirstOrDefault(u => u.Login == login);
        }

        public IEnumerable<Account> GetAll()
        {
            if (File.Exists(AccountsFilePath))
            {
                lock (locker)
                {
                    var accounts = File.ReadAllLines(AccountsFilePath)
                                .Select(account =>
                                {
                                    var accountData = account.Split(new[] { Separator }, 5);

                                    return new Account(int.Parse(accountData[0]), accountData[1], accountData[2], accountData[3], accountData[4]);
                                });

                    return accounts.Skip(1);
                }
            }
            else
            {
                return Enumerable.Empty<Account>();
            }
        }

        public bool Login(string login, string password)
        {
            if (File.Exists(AccountsFilePath))
            {
                lock (locker)
                {
                    using (var reader = new StreamReader(AccountsFilePath))
                    {
                        string line = string.Empty;

                        while (!reader.EndOfStream)
                        {
                            line = reader.ReadLine();

                            var accountData = line.Split(new[] { Separator }, 5);

                            if (accountData[1] == login)
                            {
                                if (accountData[2] == password)
                                {
                                    return true;
                                }

                                break;
                            }
                        }
                    } 
                }
            }

            return false;
        }

        public bool Register(string email, string login, string password)
        {
            if (!this.CheckUserIdentity(email, login))
            {
                return false;
            }

            maxId++;

            lock (locker)
            {
                File.WriteAllText(CurrentIdFilePath, maxId.ToString());
                File.AppendAllLines(AccountsFilePath, new[] { $"{maxId}{Separator}{login}{Separator}{password}{Separator}{email}{Separator}{UserRole}" }); 
            }

            return true;
        }

        public bool GiveAdminRights(string login)
        {
            return this.ChangeRole(login, AdminRole);
        }

        public bool TakeAdminRights(string login)
        {
            return this.ChangeRole(login, UserRole);
        }

        public string[] GetRoles(string login)
        {
            var account = this.GetAll().FirstOrDefault(acc => acc.Login == login);

            if (account != null)
            {
                return new[] { account.Role };
            }

            return Enumerable.Empty<string>().ToArray();
        }

        private static string HashStringWithSha512(string inputString)
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

        private bool CheckUserIdentity(string email, string login)
        {
            if (File.Exists(AccountsFilePath))
            {
                lock (locker)
                {
                    using (var reader = new StreamReader(AccountsFilePath))
                    {
                        string line = string.Empty;

                        while (!reader.EndOfStream)
                        {
                            line = reader.ReadLine();

                            var accountData = line.Split(new[] { Separator }, 5);

                            if (accountData[1] == login || accountData[3] == email)
                            {
                                return false;
                            }
                        }
                    } 
                }
            }

            return true;
        }
        
        private bool ChangeRole(string login, string role)
        {
            var accounts = this.GetAll().ToList();
            Account accountToUpdate = accounts.FirstOrDefault(u => u.Login == login);

            if (accountToUpdate != null)
            {
                if (accountToUpdate.Role == role)
                {
                    return true;
                }

                int index = accounts.IndexOf(accountToUpdate);

                accounts[index] = new Account(accountToUpdate.Id, accountToUpdate.Login, accountToUpdate.Password, accountToUpdate.Email, role);

                var accountsAsTxt = accounts.Select(this.AccountAsTxt);

                lock (locker)
                {
                    File.WriteAllLines(AccountsFilePath, accountsAsTxt);
                }

                return true;
            }

            return false;
        }

        private string AccountAsTxt(Account acc)
        {
            return $"{acc.Id}{Separator}{acc.Login}{Separator}{acc.Password}{Separator}{acc.Email}{Separator}{acc.Role}";
        }
    }
}
