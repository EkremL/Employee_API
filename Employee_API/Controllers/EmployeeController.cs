using Employee_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Data;

namespace Employee_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        [HttpGet]
        public IActionResult Get()
        {
            string query = @"
            
            select EmployeeId as ""EmployeeId"",
                    EmployeeName as ""EmployeeName"",
                    EmployeeSurname as ""EmployeeSurname"",
                    Department as ""Department"",
                    to_char(DateOfJoining,'YYYY-MM-DD') as ""DateOfJoining""
            from Employee
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection my_Con = new NpgsqlConnection(sqlDataSource))
            {

                my_Con.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, my_Con))
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
        public JsonResult Post(Employee emp)
        {
            string query = @"
            
                insert into Employee(EmployeeName,EmployeeSurname,Department,DateOfJoining)
                values              (@EmployeeName,@EmployeeSurname,@Department,@DateOfJoining)
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection my_Con = new NpgsqlConnection(sqlDataSource))
            {

                my_Con.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, my_Con))
                {
                    myCommand.Parameters.AddWithValue("@EmployeetId", emp.EmployeeId);
                    myCommand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    myCommand.Parameters.AddWithValue("@EmployeeSurname", emp.EmployeeSurname);
                    myCommand.Parameters.AddWithValue("@Department", emp.Department);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", Convert.ToDateTime(emp.DateOfJoining));
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    my_Con.Close();
                }
            }

            return new JsonResult("Added Succesfully!");
        }

        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            string query = @"
            
                update Employee
                set EmployeeName = @EmployeeName,
                EmployeeSurname = @EmployeeSurname,
                Department = @Department,
                DateOfJoining = @DateOfJoining
                where EmployeeId = @EmployeeId
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection my_Con = new NpgsqlConnection(sqlDataSource))
            {

                my_Con.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, my_Con))
                {
                    myCommand.Parameters.AddWithValue("@EmployeetId", emp.EmployeeId);
                    myCommand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    myCommand.Parameters.AddWithValue("@EmployeeSurname", emp.EmployeeSurname);
                    myCommand.Parameters.AddWithValue("@Department", emp.Department);
                    myCommand.Parameters.AddWithValue("@DateOfJoining",Convert.ToDateTime(emp.DateOfJoining));
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
            
                delete from Employee
                where EmployeeId = @EmployeeId
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection my_Con = new NpgsqlConnection(sqlDataSource))
            {

                my_Con.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, my_Con))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeId", id);
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
