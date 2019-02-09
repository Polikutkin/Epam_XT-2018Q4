using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Epam.Task7.DAL.Contracts;
using Epam.Task7.Entities;

namespace Epam.Task12.DAL.Sql
{
    public class AccountDao : IAccountDao
    {
        private const string AdminRoleName = "admin";
        private const string UserRoleName = "user";

        private readonly string conStr;

        public AccountDao(string connectionString)
        {
            this.conStr = connectionString;
        }

        private AccountDao()
        {
        }

        public Account GetAccount(string login)
        {
            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "GetAccount";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Login", login);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                return new Account((int)reader["Id"], (string)reader["Login"], (string)reader["Password"], (string)reader["Email"], (string)reader["Role"]);
            }
        }

        public IEnumerable<Account> GetAll()
        {
            var accounts = new List<Account>();

            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "GetAllAccounts";
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    accounts.Add(new Account((int)reader["Id"], (string)reader["Login"], (string)reader["Password"], (string)reader["Email"], (string)reader["Role"]));
                }
            }

            return accounts;
        }

        public string[] GetRoles(string login)
        {
            var roles = new List<string>();

            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "GetAccountRoles";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Login", login);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    roles.Add((string)reader["Role"]);
                }
            }

            return roles.ToArray();
        }

        public bool GiveAdminRights(string login)
        {
            return this.ChangeAccountRole(login, AdminRoleName);
        }

        public bool Login(string login, string password)
        {
            var roles = new List<string>();

            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Login";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Login", login);
                cmd.Parameters.AddWithValue("@Password", password);

                connection.Open();

                var queryResult = (int)cmd.ExecuteScalar();

                if (queryResult == 0)
                {
                    return false;
                }
                else if (queryResult == 1)
                {
                    return true;
                }
                else
                {
                    throw new InvalidOperationException(UserDao.SqlErrorMessage);
                }
            }
        }

        public bool Register(string email, string login, string password)
        {
            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Register";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Login", login);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Role", UserRoleName);

                connection.Open();
                var queryResult = cmd.ExecuteNonQuery();

                if (queryResult == 0)
                {
                    return false;
                }
                else if (queryResult == 1)
                {
                    return true;
                }
                else
                {
                    throw new InvalidOperationException(UserDao.SqlErrorMessage);
                }
            }
        }

        public bool TakeAdminRights(string login)
        {
            return this.ChangeAccountRole(login, UserRoleName);
        }

        private bool ChangeAccountRole(string login, string roleName)
        {
            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "ChangeAccountRole";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Login", login);
                cmd.Parameters.AddWithValue("@Role", roleName);

                connection.Open();
                var queryResult = cmd.ExecuteNonQuery();

                if (queryResult == 0)
                {
                    return false;
                }
                else if (queryResult == 1)
                {
                    return true;
                }
                else
                {
                    throw new InvalidOperationException(UserDao.SqlErrorMessage);
                }
            }
        }
    }
}
