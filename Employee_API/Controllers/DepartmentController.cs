using Employee_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Employee_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        

        [HttpGet]
        public IActionResult Get()
        {
            string query = @"
            
            select DepartmentId as ""DepartmentId"",
                    DepartmentName as ""DepartmentName""
            from Department
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection my_Con = new NpgsqlConnection(sqlDataSource))
            {

                my_Con.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query,my_Con))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    my_Con.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Department dep)
        {
            string query = @"
            
                insert into Department(DepartmentName)
                values (@DepartmentName)
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection my_Con = new NpgsqlConnection(sqlDataSource))
            {

                my_Con.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, my_Con))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentName",dep.DepartmentName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    my_Con.Close();
                }
            }

            return new JsonResult("Added Succesfully!");
        }

        [HttpPut]
        public JsonResult Put(Department dep)
        {
            string query = @"
            
                update Department
                set DepartmentName = @DepartmentName
                where DepartmentId = @DepartmentId
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection my_Con = new NpgsqlConnection(sqlDataSource))
            {

                my_Con.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, my_Con))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", dep.DepartmentId);
                    myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    my_Con.Close();
                }
            }

            return new JsonResult("Updated Succesfuly!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
            
                delete from Department
                where DepartmentId = @DepartmentId
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection my_Con = new NpgsqlConnection(sqlDataSource))
            {

                my_Con.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, my_Con))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", id);    
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    my_Con.Close();
                }
            }

            return new JsonResult("Deleted Succesfuly!");
        }

    }  
}
