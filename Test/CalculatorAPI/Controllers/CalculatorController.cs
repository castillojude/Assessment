using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        
        [HttpGet("GetSum/{x}/{y}")]
        public IActionResult GetSum(float x, float y)
        {
            try
            {
                return Ok(x + y);
            }
            
            catch(Exception ex){

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetDifference/{x}/{y}")]
        public IActionResult GetDifference(float x, float y)
        {
            try
            {
                return Ok(x - y);
            }
            
            catch(Exception ex){

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetProduct/{x}/{y}")]
        public IActionResult GetProduct(float x, float y)
        {
            try
            {
                return Ok(MathF.Round(x * y, 4));
            }
            
            catch(Exception ex){

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetQuotient/{x}/{y}")]
        public IActionResult GetQuotient(float x, float y)
        {
           try
            {
                return Ok(MathF.Round(x / y, 4));
            }
            
            catch(Exception ex){

                return StatusCode(500, ex.Message);
            }
        }
    }
}