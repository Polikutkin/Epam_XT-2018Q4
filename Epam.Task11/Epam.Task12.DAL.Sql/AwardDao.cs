using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Epam.Task7.DAL.Contracts;
using Epam.Task7.Entities;

namespace Epam.Task12.DAL.Sql
{
    public class AwardDao : IAwardDao
    {
        private readonly string conStr;

        public AwardDao(string connectionString)
        {
            this.conStr = connectionString;
        }

        private AwardDao()
        {
        }

        public void Add(Award award)
        {
            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "AddAward";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Title", award.Title);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AddImage(int id, byte[] image)
        {
            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "AddAwardImage";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Image", image);

                connection.Open();

                if (cmd.ExecuteNonQuery() > 1)
                {
                    throw new InvalidOperationException(UserDao.SqlErrorMessage);
                }
            }
        }

        public IEnumerable<Award> GetAll()
        {
            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "GetAllAwards";
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    yield return new Award
                    {
                        Id = (int)reader["Id"],
                        Title = (string)reader["Title"],
                        Image = reader["Image"] as byte[]
                    };
                }
            }
        }

        public bool GiveAward(int userId, int awardId)
        {
            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "GiveAward";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@AwardId", awardId);

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

        public bool Remove(int id)
        {
            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "RemoveAward";
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
                    throw new InvalidOperationException(UserDao.SqlErrorMessage);
                }
            }
        }

        public bool RemoveImage(int id)
        {
            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "RemoveAwardImage";
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
                    throw new InvalidOperationException(UserDao.SqlErrorMessage);
                }
            }
        }

        public bool TakeAward(int userId, int awardId)
        {
            using (var connection = new SqlConnection(this.conStr))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "TakeAward";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@AwardId", awardId);

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
