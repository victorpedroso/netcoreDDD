using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace TechSolutions.Application.Helper;

public class CryptHelper(IConfiguration config)
{
    public string Encrypt(string plainText)
    {
        string key = config["Cryptography:Key"];

        using var aes = Aes.Create();
        aes.Key = GenerateKey(key);
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream();
        ms.Write(aes.IV, 0, aes.IV.Length);
        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        using (var sw = new StreamWriter(cs))
        {
            sw.Write(plainText);
        }

        return Convert.ToBase64String(ms.ToArray());
    }

    public string Decrypt(string encrypted)
    {
        string key = config["Cryptography:Key"];

        var cipherData = Convert.FromBase64String(encrypted);

        using var aes = Aes.Create();
        aes.Key = GenerateKey(key);

        var iv = new byte[aes.BlockSize / 8];
        Array.Copy(cipherData, 0, iv, 0, iv.Length);
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream(cipherData, iv.Length, cipherData.Length - iv.Length);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        {
            return sr.ReadToEnd();
        }
    }

    private static byte[] GenerateKey(string key)
    {
        using var sha256 = SHA256.Create();
        return sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
    }
}
