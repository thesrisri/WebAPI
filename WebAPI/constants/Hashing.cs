﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace WebAPI.constants
{
    public class Hashing
    {
        public string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

       
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
       
    }

    
}
