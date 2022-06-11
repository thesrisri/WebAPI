using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Models;
using WebAPI.constants;
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateUserController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public ValidateUserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult getValid(user u)
        {

            int sendNumber = 0;
            string sqlDataSource = _configuration.GetConnectionString("UserAppCon");
            user UserDetails = new user();
            SqlDataReader myReader;
            Hashing obj_aes = new Hashing();
            string decrypted_text_from_from = obj_aes.MD5Hash(u.encryptedPassword.ToString());
            
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                using (SqlCommand myCommand = new SqlCommand(Query.selectUserCmd, myCon))
                {
                    myCon.Open();
                    myCommand.Parameters.AddWithValue("@email", u.email);
                    myReader = myCommand.ExecuteReader();
                    
                    if (myReader.HasRows)
                    {
                        myReader.Read();
                        if (decrypted_text_from_from == myReader["encryptedPassword"].ToString()) {
                            UserDetails.email = myReader["email"].ToString();
                            UserDetails.encryptedPassword = myReader["encryptedPassword"].ToString();
                            sendNumber = 1;
                        }

                    }

                    myReader.Close();
                    myCon.Close();
                }
            }
            if (sendNumber == 1)
            {
                return Ok(1);
            }
            else {
                return Ok(0);
            }
        }
    }
}
