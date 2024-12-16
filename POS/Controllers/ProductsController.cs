using FireSharp.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Data;
using POS.Models;
using POS.Dto;
using Dapper;
using System.Data;

namespace POS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IDataContextDapper _dapper;

        public ProductsController(IDataContextDapper IDapper)
        {
            _dapper = IDapper;
        }

        [HttpGet("GetProducts")]
        public IEnumerable<Products> GetProducts()
        {
            string sql = "SELECT * FROM Products WHERE isDeleted <> 1";

            var prods = _dapper.LoadData<Products>(sql);

            return prods;
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct(ProductDto prodDto)
        {
            string res = "";

            string sql = @" EXEC sp_Product_Upsert 
            @ItemName = @ItemNameParam, 
            @Category = @CategoryParam, 
            @Price = @PriceParam";


            DynamicParameters sqlParams = new DynamicParameters();

            sqlParams.Add("@ItemNameParam", prodDto.ItemName, DbType.String);
            sqlParams.Add("@CategoryParam", prodDto.Category, DbType.String);
            sqlParams.Add("@PriceParam", prodDto.Price, DbType.Decimal);

            if(_dapper.ExecuteSqlWithParameters(sql, sqlParams)) 
            {
                res = "Products Successfully Populated!";
            }

            else 
            {
                 res = "Failed to Populate Products";
            }
            return Ok(res);
        }

        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct(ProductDto prodDto)
        {
            string res = "";

            string sql = @" EXEC sp_Product_Upsert 
            @ItemId = @ItemIdParam, 
            @ItemName = @ItemNameParam, 
            @Category = @CategoryParam, 
            @Price = @PriceParam";

            DynamicParameters sqlParams = new DynamicParameters();

            sqlParams.Add("@ItemNameParam", prodDto.ItemName, DbType.String);
            sqlParams.Add("@CategoryParam", prodDto.Category, DbType.String);
            sqlParams.Add("@PriceParam", prodDto.Price, DbType.Decimal);
            sqlParams.Add("@ItemIdParam", prodDto.ItemId, DbType.Int32);

            if(_dapper.ExecuteSqlWithParameters(sql, sqlParams)) 
            {
                res = "Products Updated Successfully!";
            }

            else 
            {
                 res = "Failed to Update Product";
            }
            return Ok(res);
        }
        
        [HttpDelete("DeleteProduct/{itemId}")]
        public IActionResult DeleteProduct(int itemId)
        {
            string res = "";


            string sql = "EXEC sp_Product_Delete @ItemId = @ItemIdParam";

            DynamicParameters sqlParams = new DynamicParameters();

            sqlParams.Add("@ItemIdParam", itemId, DbType.String);

            if(_dapper.ExecuteSqlWithParameters(sql, sqlParams)) 
            {
                res = $"Product [{itemId}] Successfully Deleted!";
            }

            else 
            {
                 res = $"Failed to Delete Product [{itemId}]";
            }
            
            return Ok(res);

        }
    }
}
