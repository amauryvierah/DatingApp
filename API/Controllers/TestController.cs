using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    //Controller derives from ControllerBase and should be the same to use Controller here
    //https://stackoverflow.com/questions/55239380/why-derive-from-controllerbase-vs-controller-for-asp-net-core-web-api
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet, Route("GetMilesRate")]
        public decimal GetMilesRate()
        {
            return 11.2m;
        }

        [HttpGet, Route("GetMinimumQuote")]
        public decimal GetMinimumQuote()
        {
            return 12.2m;
        }

        [HttpGet, Route("GetMinimumQuoteByID")]
        public decimal GetMinimumQuote(int id)
        {
            return 13.2m;
        }

        [HttpGet, Route("GetWestCoastStates")]
        public IEnumerable<string> GetWestCoastStates()
        {
            return new List<string>() { "LA", "FL" };
        }
    }
}
