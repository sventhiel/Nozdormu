﻿using LiteDB;
using Nozdormu.Server.Entities;
using Nozdormu.Server.Utilities;
using NuGet.Common;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;

namespace Nozdormu.Server.Services
{
    public class UserService
    {
        private readonly ConnectionString _connectionString;

        public UserService(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public long Create(string username, string password, string pattern, long accountId)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var users = db.GetCollection<User>("users");
                var accounts = db.GetCollection<Account>("accounts");

                // salt
                var salt = CryptographyUtils.GetRandomBase64String(16);

                var user = new User()
                {
                    Username = username,
                    Salt = salt,
                    Password = CryptographyUtils.GetSHA512HashAsBase64(salt, password),
                    Pattern = pattern,
                    Token = CryptographyUtils.GetRandomHexadecimalString(32),
                    //Account = accounts.FindById(accountId)
                };

                return users.Insert(user);
            }
        }

        public bool Verify(string username, string password)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var users = db.GetCollection<User>("users");

                var user = users.FindOne(u => u.Username == username);

                if (user == null)
                    return false;

                return (user.Password == CryptographyUtils.GetSHA512HashAsBase64(user.Salt, password));
            }
        }

        public bool Delete(long id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<User>("users");

                return col.Delete(id);
            }
        }

        public User FindById(long id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<User>("users");

                return col.FindById(id);
            }
        }

        public User FindByToken(string token)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<User>("users");

                var users = col.Find(u => u.Token.Equals(token));

                if (users.Count() != 1)
                    return null;

                return users.First();
            }
        }

        public User FindByName(string username)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<User>("users");

                var users = col.Find(u => u.Username.Equals(username));

                if (users.Count() != 1)
                    return null;

                return users.First();
            }
        }

        public List<User> Find()
        {
            List<User> users = null;

            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<User>("users");

                users = col.Query().ToList();
            }

            return users;
        }

        public bool Update(User user)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var col = db.GetCollection<User>("users");

                return col.Update(user);
            }
        }
    }
}