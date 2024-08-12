using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;

public class Crypto
{
    private static TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
    public static MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

    public static Byte[] MD5Hash(String value)
    {
        return md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(value));
    }

    public static string Encrypt(string stringToEncrypt, string key)
    {
        des.Key = Crypto.MD5Hash(key);
        des.Mode = CipherMode.ECB;
        Byte[] buffer = ASCIIEncoding.ASCII.GetBytes(stringToEncrypt);
        return Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
    }

    public static string Decrypt(string encryptedString, string key)
    {
        des.Key = Crypto.MD5Hash(key);
        des.Mode = CipherMode.ECB;
        Byte[] buffer = Convert.FromBase64String(encryptedString);
        return ASCIIEncoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
    }

    public static string EncryptHash(string StringToEncrypt)
    {
        try
        {
            MD5 md5Hash = MD5.Create();
            return GetMd5Hash(md5Hash, StringToEncrypt);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static string GetMd5Hash(MD5 md5Hash, string input)
    {
        try
        {
            Byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data and format each one as a hexadecimal string.             
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}

public class FileReadWrite
{

    public static string GetFileContents(string FullPath, string ErrInfo = "")
    {
        string strContents = "";
        StreamReader objReader;
        try
        {
            objReader = new StreamReader(FullPath);
            strContents = objReader.ReadToEnd();
            objReader.Close();
        }
        catch (Exception ex)
        {
            ErrInfo = ex.Message;
        }
        return strContents;
    }

    public static Boolean SaveTextToFile(string strData, string FullPath, string ErrInfo = "")
    {
        bool bAns = false;
        StreamWriter objReader;
        try
        {
            objReader = new StreamWriter(FullPath);
            objReader.Write(strData);
            objReader.Close();
            bAns = true;
        }
        catch (Exception ex)
        {
            ErrInfo = ex.Message;
        }
        return bAns;
    }

}