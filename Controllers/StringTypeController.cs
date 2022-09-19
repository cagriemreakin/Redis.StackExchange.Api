using Microsoft.AspNetCore.Mvc;
using RedisExhange.Web.Api.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedisExhange.Web.Api.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly IRedisService _redisService;
        public StringTypeController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        public IActionResult Index()
        {
            var db = _redisService.GetDatabase(0);
            db.StringSet("name", "Redis Poc");
            db.StringSet("visitor", 1);
            return View();
        }

        public IActionResult Show()
        {
            var db = _redisService.GetDatabase(0);

            ViewBag.value = db.StringGet("name").ToString() ?? string.Empty;
            ViewBag.visitor = db.StringIncrement("visitor");
            return View();
        }
    }
}

