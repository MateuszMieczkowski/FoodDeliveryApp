using Library.DataPersistence;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("/api/Restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public RestaurantController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
       public IActionResult Get()
        {
            return Ok("OKKKK");
        }
    }
}
