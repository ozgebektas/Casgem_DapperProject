using Casgem_DapperProject.DAL.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Casgem_DapperProject.Controllers
{
    public class HeadingController : Controller
    {
        private readonly string _connectionString = "Server=LAPTOP-8JIDE4EC\\SQLEXPRESS;initial Catalog=CasgemDbDapper;integrated security=true";
        public async Task<IActionResult> Index()
        {
            await using var connection = new SqlConnection(_connectionString);
            var values = await connection.QueryAsync<Headings>("select *From TblHeading");
            return View(values);
        }

        [HttpGet]
        public IActionResult AddHeading()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddHeading(Headings headings)
        {
            await using var connection = new SqlConnection(_connectionString);
            var query = $"Insert into TblHeading (HeadinngName,HeadingStatus) values('{headings.HeadinngName}','True')";
            await connection.QueryAsync(query);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteHeading(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync($"delete from TblHeading where HeadID = '{id}'");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateHeading(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            var values = await connection.QueryFirstAsync<Headings>($"Select *from TblHeading where HeadID='{id}'");
            return View(values);
         
        }
        [HttpPost]
        public async Task<IActionResult> UpdateHeading(Headings headings)
        {
            await using var connection = new SqlConnection(_connectionString);
           await connection.ExecuteAsync( $"update TblHeading set HeadinngName='{headings.HeadinngName}',HeadingStatus='{headings.HeadingStatus}' where HeadID='{headings.HeadID}'");
           
            return RedirectToAction("Index");

        }


    }
}

