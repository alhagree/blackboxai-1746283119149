using System;
using System.Security.Cryptography;
using System.Text;

namespace WindowsApp.Utilities
{
    public static class SecurityHelper
    {
        private static readonly string Salt = "WindowsApp2023"; // في التطبيق الحقيقي، يجب تخزين هذا في مكان آمن

        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            // إضافة Salt إلى كلمة المرور
            string saltedPassword = string.Concat(password, Salt);
            
            using (var sha256 = SHA256.Create())
            {
                // تحويل النص إلى مصفوفة بايت
                byte[] bytes = Encoding.UTF8.GetBytes(saltedPassword);
                
                // حساب الهاش
                byte[] hash = sha256.ComputeHash(bytes);
                
                // تحويل الهاش إلى نص
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }
                
                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string password, string hash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash))
                return false;

            string passwordHash = HashPassword(password);
            return passwordHash.Equals(hash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
