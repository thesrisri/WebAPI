using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using WebAPI.constants;
namespace WebAPI.Controllers
{
    [Route("api/SaveFile")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public FileController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                user UserDetails = new user();
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                Console.WriteLine(_env.ContentRootPath);
                var physicalPath = "S:\\Study\\angular\\project\\pinterest_clone\\src\\assets" + "/images/" + filename;
                //var physicalPath = _env.ContentRootPath + "/Photos/" + filename;
                var physicalPathwofileName = physicalPath; 
                    //_env.ContentRootPath + "/Photos/";
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    UserDetails.photoFileName = filename;
                    UserDetails.photoFilePath = physicalPathwofileName;
                }

                string sqlDataSource = _configuration.GetConnectionString("UserAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    
                    using (SqlCommand myCommand = new SqlCommand(Query.insertImageCmd, myCon))
                    {

                        myCommand.Parameters.AddWithValue("@photoFileName", filename);
                        myCommand.Parameters.AddWithValue("@photoPath", physicalPathwofileName);
                        //myCommand.Parameters.AddWithValue("@username", UserName);
                        myCon.Open();
                        myReader = myCommand.ExecuteReader();
                       
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return new JsonResult(UserDetails);
            }

            catch (Exception) {
                return new JsonResult("anonymous.png");
            }
        }

    }
}
