using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.constants;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace WebAPI.Controllers
{
    [Route("api/getPhotoList")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PhotoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult getImageList()
        {


            string sqlDataSource = _configuration.GetConnectionString("UserAppCon");

            SqlDataReader myReader;
            List<string> imageList = new List<string>();

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                using (SqlCommand myCommand = new SqlCommand(Query.getAllPhotos, myCon))
                {
                    myCon.Open();

                    myReader = myCommand.ExecuteReader();

                    if (myReader.HasRows)
                    {
                        //myReader.Read();

                       
                        while (myReader.Read()) {
                            imageList.Add(myReader["photoFileName"].ToString());
                        }
                        
                    }

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(imageList);
        }
    }
}
