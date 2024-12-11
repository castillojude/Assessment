using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        
        [HttpGet("GetSum/{a}/{b}")]
        public IActionResult GetSum(float a, float b)
        {
            try
            {
                return Ok(a + b);
            }
            
            catch(Exception ex){

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetDifference/{a}/{b}")]
        public IActionResult GetDifference(float a, float b)
        {
            try
            {
                return Ok(a - b);
            }
            
            catch(Exception ex){

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetProduct/{a}/{b}")]
        public IActionResult GetProduct(float a, float b)
        {
            try
            {
                return Ok(a * b);
            }
            
            catch(Exception ex){

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetQuotient/{a}/{b}")]
        public IActionResult GetQuotient(float a, float b)
        {
           try
            {
                return Ok(a / b);
            }
            
            catch(Exception ex){

                return StatusCode(500, ex.Message);
            }
        }
    }
}