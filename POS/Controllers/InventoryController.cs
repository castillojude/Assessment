using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Data;
using POS.Models;

namespace POS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly DataContextDapper _dapper;

        public InventoryController(IConfiguration cfg)
        {
            _dapper = new DataContextDapper(cfg);
        }

        [HttpGet("GetInventory")]
        public IEnumerable<Stocks> GetProducts()
        {
            string sql = "SELECT * FROM vw_Stocks";

            return _dapper.LoadData<Stocks>(sql);
        }
    }
}
