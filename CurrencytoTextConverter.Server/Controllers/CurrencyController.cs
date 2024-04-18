using CurrencytoTextConverter.Server.Helper;
using CurrencytoTextConverter.Server.Model;
using Microsoft.AspNetCore.Mvc;

namespace CurrencytoTextConverter.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyHelper _helper;

        public CurrencyController(CurrencyHelper helper)
        {
            _helper = helper;
        }

        [HttpPost("ConvertToText")]
        public async Task<IActionResult> ConvertToText([FromBody] Currency currency)
        {
            if (currency == null)
            {
                return BadRequest("Amount cannot be empty.");
            }

            var textResult = await _helper.SliceAmount(currency);

            return Ok(new { result = textResult });
        }
    }
}