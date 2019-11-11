using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Web.Helpers
{
	public static class PasswordHelper
	{
		/// <summary>
		/// Generate a 128 bit salt secure using Pseudorandom number generator
		/// </summary>
		/// <returns>The 128 bit random salt</returns>
		public static byte[] GenerateSalt()
		{
			byte[] salt = new byte[128 / 8];

			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}

			return salt;
		}

		public static string Hash(string password, byte[] salt)
		{
			return Convert.ToBase64String(
				KeyDerivation.Pbkdf2(
					password,
					salt,
					KeyDerivationPrf.HMACSHA512,
					10000,
					512 / 8));
		}
	}
}
