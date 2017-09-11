
using System;
using System.Configuration;

namespace DH.Curbside.Core.Enterprise
{
    /// <summary>
    /// Contains logic to generate unique code
    /// </summary>
    public class RandomCodeGenerator : SingletonBase<RandomCodeGenerator>
    {
        private RandomCodeGenerator() { }
        string chars = "0123456789";

        /// <summary>
        /// Generates unique code
        /// </summary>
        /// <returns>Unique code</returns>
        public string Generate()
        {
            string length = ConfigurationManager.AppSettings["VerificationCode.Length"];
            var stringChars = new char[int.Parse(length)];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }


    }
}
