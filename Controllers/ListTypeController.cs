using Microsoft.AspNetCore.Mvc;
using RedisExhange.Web.Api.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedisExhange.Web.Api.Controllers
{
    public class ListTypeController : Controller
    {
        private readonly IRedisService _redisService;
        private string KEY = "names";
        public ListTypeController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        public IActionResult Index()
        {
            var db = _redisService.GetDatabase(1);
            var names = new List<string>();
            if (db.KeyExists(KEY)){
              
                db.ListRange(KEY).ToList().ForEach(i=>
                {
                    names.Add(i.ToString());
                });
            }
            return View(names);
        }


        [HttpPost]
        public IActionResult Add(string name)
        {
            var db = _redisService.GetDatabase(1);
            db.ListRightPushAsync(KEY, name).Wait();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(string name)
        {
            var db = _redisService.GetDatabase(1);
            db.ListRemoveAsync(KEY, name).Wait();
            return RedirectToAction("Index");

        }
        public IActionResult DeleteFirstItem()
        {
            var db = _redisService.GetDatabase(1);
            db.ListLeftPopAsync(KEY).Wait();
            return RedirectToAction("Index");
        }
    }
}

