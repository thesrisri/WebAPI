using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Models;
using WebAPI.constants;
using System.Web ;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace WebAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class userController : ControllerBase
    {

        private readonly IConfiguration _configuration;


        public userController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

       
        [HttpPost]
        public JsonResult Post(user u)
        {
            
            DataTable table = new DataTable();
            user UserDetails = new user();
            Hashing obj_aes = new Hashing();
            string encrypted_text = obj_aes.MD5Hash(u.encryptedPassword);
            string sqlDataSource = _configuration.GetConnectionString("UserAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
               
                using (SqlCommand myCommand = new SqlCommand(Query.selectUserCmd, myCon))
                {

                  
                   
                    myCommand.Parameters.AddWithValue("@email", u.email);

                    myReader = myCommand.ExecuteReader();
                    if (myReader.HasRows)
                    {
                        myReader.Read();
                        return new JsonResult("Email Already Exists Try Logging in!");
                    }

                    myReader.Close();
                    myCon.Close();
                }
                myCon.Open();

                using (SqlCommand myCommand = new SqlCommand(Query.findUserNameCmd, myCon))
                {

                    myCommand.Parameters.AddWithValue("@UserName", u.UserName);


                    myReader = myCommand.ExecuteReader();
                    if (myReader.HasRows)
                    {
                        myReader.Read();
                        return new JsonResult("Username taken,  try something new !");
                    }

                    myReader.Close();
                    myCon.Close();
                }
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(Query.insertUserCmd, myCon))
                {
                    
                    myCommand.Parameters.AddWithValue("@UserName", u.UserName);
                    myCommand.Parameters.AddWithValue("@age", u.age);
                    myCommand.Parameters.AddWithValue("@email", u.email);
                    myCommand.Parameters.AddWithValue("@encryptedPassword", encrypted_text);
                    
                    myCommand.Parameters.AddWithValue("@photoFileName", u.photoFileName);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Ok");
        } 
    }
}
