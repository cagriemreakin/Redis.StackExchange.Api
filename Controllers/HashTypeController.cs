using Microsoft.AspNetCore.Mvc;
using RedisExhange.Web.Api.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedisExhange.Web.Api.Controllers
{
    public class HashTypeController : Controller
    {
        private readonly IRedisService _redisService;
        private string HASH_KEY = "HashTypeNames";

        public HashTypeController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        public IActionResult Index()
        {
            var db = _redisService.GetDatabase(4);
            var names = new Dictionary<string,string>();
            if (db.KeyExists(HASH_KEY))
            {

                db.HashGetAll(HASH_KEY).ToList().ForEach(i =>
                {
                    names.Add(i.Name,i.Value);
                });
            }
            return View(names);
        }


        [HttpPost]
        public IActionResult Add(string name, string val)
        {
            var db = _redisService.GetDatabase(4);
            db.HashSet(HASH_KEY, name, val);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(string name)
        {
            var db = _redisService.GetDatabase(4);
            db.HashDelete(HASH_KEY, name);
            return RedirectToAction("Index");

        }
    }
}

