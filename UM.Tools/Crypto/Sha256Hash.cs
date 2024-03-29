﻿using System.Security.Cryptography;
using System.Text;

namespace UM.Tools.Crypto;

public class Sha256Hash
{
	public static Byte[] Hash(string text)
	{
		using var sha256 = SHA256.Create();
		var bytes = Encoding.UTF8.GetBytes(text);

		return sha256.ComputeHash(bytes);
	}
}