using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.constants
{
    public static class Query
    {
        public static string insertUserCmd = "insert into dbo.users(UserName,age,email,encryptedPassword,photoFileName) " +
            "values (@UserName, @age, @email, @encryptedPassword, @photoFileName)";


        public static string selectUserCmd= "select * from dbo.users where email = @email";
        public static string findUserNameCmd = "select * from dbo.users where UserName = @username";
        public static string insertImageCmd = "insert into dbo.photos(photoFileName,photoPath) " +
            "values (@photoFileName, @photoPath)";
        public static string getAllPhotos = "select * from dbo.photos";

    }
}
