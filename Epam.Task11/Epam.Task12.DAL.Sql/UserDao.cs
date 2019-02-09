using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Epam.Task7.DAL.Contracts;
using Epam.Task7.Entities;

namespace Epam.Task12.DAL.Sql
{
    public class UserDao : IUserDao
    {
        public static readonly string SqlErrorMessage = "Database has duplicated ID values." +
            "Check Database fields you want to be unique to identity.";

        private readonly string conStr;

        public UserDao(string connectionString)
        {
            this.conStr = connectionString;
        }

        private UserDao()
        {
        }

        public void Add(User user)
        {
            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "AddUser";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@BirthDate", user.BirthDate);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AddImage(int id, byte[] image)
        {
            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "AddUserImage";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Image", image);

                connection.Open();

                if (cmd.ExecuteNonQuery() > 1)
                {
                    throw new InvalidOperationException(SqlErrorMessage);
                }
            }
        }

        public IEnumerable<User> GetAll()
        {
            var users = new List<User>();

            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "GetAllUsers";
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(InitilizeNewUser(reader));
                }
            }

            var userAwards = this.GetAllUserAwards();
            var userAwardIdList = this.GetUserAwardIdList();

            foreach (var id in userAwardIdList)
            {
                users.Find(u => u.Id == id[0]).Awards.Add(userAwards.Find(a => a.Id == id[1]));
            }

            return users;
        }

        public User GetById(int id)
        {
            User user = default(User);

            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "GetUserById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    user = InitilizeNewUser(reader);
                }
            }

            if (user != default(User))
            {
                this.GetUserAward(user);
            }

            return user;
        }

        public bool Remove(int id)
        {
            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "RemoveUser";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

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
                    throw new InvalidOperationException(SqlErrorMessage);
                }
            }
        }

        public bool RemoveImage(int id)
        {
            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "RemoveUserImage";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

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
                    throw new InvalidOperationException(SqlErrorMessage);
                }
            }
        }

        public void Update(int id, User user)
        {
            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "UpdateUser";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@BirthDate", user.BirthDate);

                connection.Open();

                if (cmd.ExecuteNonQuery() > 1)
                {
                    throw new InvalidOperationException(SqlErrorMessage);
                }
            }
        }

        private static User InitilizeNewUser(SqlDataReader reader)
        {
            return new User
            {
                Id = (int)reader["Id"],
                FirstName = (string)reader["FirstName"],
                LastName = (string)reader["LastName"],
                BirthDate = (DateTime)reader["BirthDate"],
                Image = reader["Image"] as byte[]
            };
        }

        private void GetUserAward(User user)
        {
            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "GetUserAward";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", user.Id);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user.Awards.Add(new Award
                    {
                        Id = (int)reader["Id"],
                        Title = (string)reader["Title"],
                        Image = reader["Image"] as byte[]
                    });
                }
            }
        }

        private List<Award> GetAllUserAwards()
        {
            var awards = new List<Award>();

            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "GetAllUserAwards";
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    awards.Add(new Award
                    {
                        Id = (int)reader["Id"],
                        Title = (string)reader["Title"],
                        Image = reader["Image"] as byte[]
                    });
                }
            }

            return awards;
        }

        private List<int[]> GetUserAwardIdList()
        {
            var ua = new List<int[]>();

            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "GetUserAwardIdList";
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var a = new[] { (int)reader["UserId"], (int)reader["AwardId"] };

                    ua.Add(a);
                }
            }

            return ua;
        }
    }
}
