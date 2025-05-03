using System;
using System.Data.SQLite;
using System.IO;
using WindowsApp.Models;
using WindowsApp.Utilities;

namespace WindowsApp.Services
{
    public static class DatabaseService
    {
        private static readonly string DatabasePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "WindowsApp",
            "database.db"
        );

        private static string ConnectionString => $"Data Source={DatabasePath};Version=3;";

        public static void InitializeDatabase()
        {
            try
            {
                string directory = Path.GetDirectoryName(DatabasePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                if (!File.Exists(DatabasePath))
                {
                    SQLiteConnection.CreateFile(DatabasePath);
                }

                using var connection = new SQLiteConnection(ConnectionString);
                connection.Open();

                string createTableSql = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Username TEXT NOT NULL UNIQUE,
                        PasswordHash TEXT NOT NULL,
                        FullName TEXT,
                        Email TEXT,
                        IsActive INTEGER DEFAULT 1,
                        CreatedAt TEXT DEFAULT CURRENT_TIMESTAMP
                    );";

                using var command = new SQLiteCommand(createTableSql, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("فشل في تهيئة قاعدة البيانات", ex);
            }
        }

        public static bool AuthenticateUser(string username, string password)
        {
            try
            {
                using var connection = new SQLiteConnection(ConnectionString);
                connection.Open();

                string sql = "SELECT PasswordHash FROM Users WHERE Username = @username AND IsActive = 1";
                using var command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@username", username);

                var result = command.ExecuteScalar();
                if (result != null)
                {
                    string storedHash = result.ToString();
                    // في الإصدار النهائي، يجب استخدام دالة تشفير آمنة للتحقق من كلمة المرور
                    return storedHash == SecurityHelper.HashPassword(password);
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("فشل في عملية المصادقة", ex);
            }
        }

        public static bool CreateUser(User user, string password)
        {
            try
            {
                using var connection = new SQLiteConnection(ConnectionString);
                connection.Open();

                string sql = @"
                    INSERT INTO Users (Username, PasswordHash, FullName, Email, IsActive)
                    VALUES (@username, @passwordHash, @fullName, @email, @isActive)";

                using var command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@passwordHash", SecurityHelper.HashPassword(password));
                command.Parameters.AddWithValue("@fullName", user.FullName ?? "");
                command.Parameters.AddWithValue("@email", user.Email ?? "");
                command.Parameters.AddWithValue("@isActive", user.IsActive ? 1 : 0);

                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("فشل في إنشاء المستخدم", ex);
            }
        }
    }
}
